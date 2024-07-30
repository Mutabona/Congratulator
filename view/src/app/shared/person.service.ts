import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { environment } from '../../environments/environment';
import { Person } from './person.model';
import { NgForm } from '@angular/forms';
import { Guid } from 'guid-typescript';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

  url:string = environment.apiBaseUrl + '/Person'
  list:Person[] = [];
  photos = new Map<Guid, string>()
  formSubmited:boolean = false;
  formData : Person = new Person()

  constructor(private http: HttpClient) { }

  refreshNearest() {
    this.http.get(this.url + '/nearest')
    .subscribe({
      next: res=>{
        this.list = res as Person[]
        for (var person of this.list) {
          this.getPersonPhoto(person.id)
        }
      },
      error: err => {
        console.log(err)
      }
    })
  }

  refreshAll() {
    this.http.get(this.url + '/all')
    .subscribe({
      next: res=>{
        this.list = res as Person[]
        for (var person of this.list) {
          this.getPersonPhoto(person.id)
        }
      },
      error: err => {
        console.log(err)
      }
    })
  }

  postPerson(file:File) {
    this.formData.photo = file;
    var form = new FormData();
    form.append('FirstName', this.formData.firstName)
    form.append('LastName', this.formData.lastName)
    form.append('MiddleName', this.formData.middleName)
    form.append('Birthday', this.formData.birthday)
    form.append('Photo', this.formData.photo)
    return this.http.post(this.url + '/add', form)
  }

  putPerson(file:File) {
    this.formData.photo = file;
    var form = new FormData();
    form.append('Id', this.formData.id.toString())
    form.append('FirstName', this.formData.firstName)
    form.append('LastName', this.formData.lastName)
    form.append('MiddleName', this.formData.middleName)
    form.append('Birthday', this.formData.birthday)
    form.append('Photo', this.formData.photo)
    return this.http.put(this.url + '/' + this.formData.id, form)
  }

  deletePerson(id:Guid) {
    return this.http.delete(this.url + '/' + id)
  }

  getPersonPhoto(id:Guid) {
    this.http.get(this.url + '/' + id + '/photo', {responseType: 'arraybuffer'})
    .subscribe({
      next: res=> {
        let blob = new Blob([res], {type : "File"})
        console.log(blob.type)
        let reader = new FileReader();
        reader.onload = (event:any) => {
          this.photos.set(id, event.target.result) //= event.target.result;
        };
        reader.readAsDataURL(blob);
        return res
      },
      error: err => {
        console.log(err)
      }
    })
  }

  resetForm(form:NgForm) {
    form.form.reset()
    this.formData = new Person();
    this.formSubmited = false;
  }
}
