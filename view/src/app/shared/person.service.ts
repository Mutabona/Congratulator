import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { environment } from '../../environments/environment';
import { Person } from './person.model';
import { NgForm } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

  url:string = environment.apiBaseUrl + '/Person'
  list:Person[] = [];
  formSubmited:boolean = false;
  formData : Person = new Person()

  constructor(private http: HttpClient) { }

  refreshList(){
    this.http.get(this.url + '/all')
    .subscribe({
      next: res=>{
        this.list = res as Person[]
      },
      error: err => {
        console.log(err)
      }
    })
  }

  postPerson() {
    return this.http.post(this.url + '/add', this.formData)
  }

  resetForm(form:NgForm) {
    form.form.reset()
    this.formData = new Person();
    this.formSubmited = false;
  }
}
