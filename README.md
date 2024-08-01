docker run --name CongratulatorDb -p 5432:5432 -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=password -e POSTGRES_DB=postgres -d postgres \n  
  
*Накатить миграцию*  
  
*Запустить API*  (Возможно в appsettings придётся поменять Host на localhost)
  
.../view> ng serve -o  
