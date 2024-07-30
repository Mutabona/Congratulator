import { Component } from '@angular/core';
import { PersonService } from '../../shared/person.service';
import { FormsModule, NgForm } from '@angular/forms';
import { Guid } from 'guid-typescript';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { eventNames } from 'process';

@Component({
  selector: 'app-person-form',
  standalone: true,
  imports: [FormsModule, RouterLink, RouterLinkActive],
  templateUrl: './person-form.component.html',
  styles: ``
})
export class PersonFormComponent {
  imageUrl : string = "";
  fileToUpload : File|null = null;

  constructor(public service : PersonService) {

  }

  onSubmit(form:NgForm) {
    this.service.formSubmited = true
    if(form.valid) {
      if(this.service.formData.id.toString() == Guid.EMPTY) {
        this.insertRecord(form)
      }
      else {
        this.updateRecord(form)
      }
    }
    //window.location.href = '/congratulator'
  }

  insertRecord(form:NgForm) {
    this.service.postPerson(this.fileToUpload!)
    .subscribe({
      next:res => {
        this.service.refreshNearest()
        this.service.resetForm(form);
      },
      error:err => {console.log(err)}
    })
  }

  updateRecord(form:NgForm) {
    this.service.putPerson(this.fileToUpload!)
    .subscribe({
      next:res => {
        this.service.refreshNearest()
        this.service.resetForm(form);
      },
      error:err => {console.log(err)}
    })
  }

  handleFileInput(event : any) {
    var reader = new FileReader();
    this.fileToUpload = <File>event.target.files[0];
    console.log(event);

    reader.onload = (event:any) => {
      this.imageUrl = event.target.result;
    }
    reader.readAsDataURL(this.fileToUpload!);
  }
}
