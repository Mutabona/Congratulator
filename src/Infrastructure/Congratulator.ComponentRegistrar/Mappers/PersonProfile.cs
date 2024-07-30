using AutoMapper;
using Congratulator.Contracts.Persons;
using Congratulator.Domain.Persons.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;


namespace Congratulator.ComponentRegistrar.Mappers
{
    /// <summary>
    /// Профиль для работы с людьми.
    /// </summary>
    public class PersonProfile : Profile
    {
        /// <summary>
        /// Создаёт экземпляр <see cref="PersonProfile"/>.
        /// </summary>
        public PersonProfile() 
        {
            CreateMap<AddPersonRequest, Person>()
                .ForMember(s => s.Id, map => map.MapFrom(s => Guid.NewGuid()))
                .ForMember(s => s.Photo, map => map.MapFrom(s => GetBytes(s.Photo)));

            CreateMap<PersonDto, Person>()
                .ForMember(s => s.Photo, map => map.MapFrom(s => GetBytes(s.Photo)));

            CreateMap<Person, PersonDto>()
                .ForMember(s => s.Photo, map => map.MapFrom(s => GetFormFile(s.Photo)));
        }

        /// <summary>
        /// Возвращает набор байт из файла.
        /// </summary>
        /// <param name="file">Файл <see cref="IFormFile"/></param>
        /// <returns>Набор байт.</returns>
        public byte[] GetBytes(IFormFile file)
        {
            var ms = new MemoryStream();
            file.CopyTo(ms);
            return ms.ToArray();
        }

        /// <summary>
        /// Преобразует набор байт в файл.
        /// </summary>
        /// <param name="bytes">Набор байт.</param>
        /// <returns>Файл <see cref="FormFile"/></returns>
        public static IFormFile GetFormFile(byte[] bytes) 
        {
            var stream = new MemoryStream(bytes);
            IFormFile file = new FormFile(stream, 0, bytes.Length, "Photo", "photo.jpg");
            return file;
        }
    }


    /// <summary>
    /// Всё, что находится дальше, должно было быть в пакете Microsoft.AspNetCore.Http.Internal,
    /// но с этим пакетом у всех, в том числе у меня, какие-то проблемы, он отказывается работать,
    /// так что я, воспользовавшись русской смекалочкой вставил нужный код сюда.
    /// </summary>

    public class FormFile : IFormFile
    {
        private const int DefaultBufferSize = 81920;

        private readonly Stream _baseStream;

        private readonly long _baseStreamOffset;

        //
        // Сводка:
        //     Gets the raw Content-Disposition header of the uploaded file.
        public string ContentDisposition
        {
            get
            {
                return Headers["Content-Disposition"];
            }
            set
            {
                Headers["Content-Disposition"] = value;
            }
        }

        //
        // Сводка:
        //     Gets the raw Content-Type header of the uploaded file.
        public string ContentType
        {
            get
            {
                return Headers["Content-Type"];
            }
            set
            {
                Headers["Content-Type"] = value;
            }
        }

        //
        // Сводка:
        //     Gets the header dictionary of the uploaded file.
        public IHeaderDictionary Headers { get; set; }

        //
        // Сводка:
        //     Gets the file length in bytes.
        public long Length { get; }

        //
        // Сводка:
        //     Gets the name from the Content-Disposition header.
        public string Name { get; }

        //
        // Сводка:
        //     Gets the file name from the Content-Disposition header.
        public string FileName { get; }

        public FormFile(Stream baseStream, long baseStreamOffset, long length, string name, string fileName)
        {
            _baseStream = baseStream;
            _baseStreamOffset = baseStreamOffset;
            Length = length;
            Name = name;
            FileName = fileName;
            Headers = new HeaderDictionary();
            Headers["Content-Type"] = "image/jpeg";
            Headers["Content-Disposition"] = "name=\"Photo\"; filename=\"photo.jpg\"";
        }

        //
        // Сводка:
        //     Opens the request stream for reading the uploaded file.
        public Stream OpenReadStream()
        {
            return new ReferenceReadStream(_baseStream, _baseStreamOffset, Length);
        }

        //
        // Сводка:
        //     Copies the contents of the uploaded file to the target stream.
        //
        // Параметры:
        //   target:
        //     The stream to copy the file contents to.
        public void CopyTo(Stream target)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            using Stream stream = OpenReadStream();
            stream.CopyTo(target, 81920);
        }

        //
        // Сводка:
        //     Asynchronously copies the contents of the uploaded file to the target stream.
        //
        //
        // Параметры:
        //   target:
        //     The stream to copy the file contents to.
        //
        //   cancellationToken:
        public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            using Stream readStream = OpenReadStream();
            await readStream.CopyToAsync(target, 81920, cancellationToken);
        }
    }

    internal class ReferenceReadStream : Stream
    {
        private readonly Stream _inner;

        private readonly long _innerOffset;

        private readonly long _length;

        private long _position;

        private bool _disposed;

        public override bool CanRead => true;

        public override bool CanSeek => _inner.CanSeek;

        public override bool CanWrite => false;

        public override long Length => _length;

        public override long Position
        {
            get
            {
                return _position;
            }
            set
            {
                ThrowIfDisposed();
                if (value < 0 || value > Length)
                {
                    throw new ArgumentOutOfRangeException("value", value, "The Position must be within the length of the Stream: " + Length);
                }

                VerifyPosition();
                _position = value;
                _inner.Position = _innerOffset + _position;
            }
        }

        public ReferenceReadStream(Stream inner, long offset, long length)
        {
            if (inner == null)
            {
                throw new ArgumentNullException("inner");
            }

            _inner = inner;
            _innerOffset = offset;
            _length = length;
            _inner.Position = offset;
        }

        private void VerifyPosition()
        {
            if (_inner.Position != _innerOffset + _position)
            {
                throw new InvalidOperationException("The inner stream position has changed unexpectedly.");
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    Position = offset;
                    break;
                case SeekOrigin.End:
                    Position = Length + offset;
                    break;
                default:
                    Position += offset;
                    break;
            }

            return Position;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            ThrowIfDisposed();
            VerifyPosition();
            long num = Math.Min(count, _length - _position);
            int num2 = _inner.Read(buffer, offset, (int)num);
            _position += num2;
            return num2;
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            ThrowIfDisposed();
            VerifyPosition();
            long num = Math.Min(count, _length - _position);
            int num2 = await _inner.ReadAsync(buffer, offset, (int)num, cancellationToken);
            _position += num2;
            return num2;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Flush()
        {
            throw new NotSupportedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _disposed = true;
            }
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("ReferenceReadStream");
            }
        }
    }
}
