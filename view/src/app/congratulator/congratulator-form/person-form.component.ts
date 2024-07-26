import { Component } from '@angular/core';
import { PersonService } from '../../shared/person.service';
import { FormsModule, NgForm } from '@angular/forms';

@Component({
  selector: 'app-person-form',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './person-form.component.html',
  styles: ``
})
export class PersonFormComponent {

  constructor(public service : PersonService) {

  }

  onSubmit(form:NgForm) {
    this.service.formSubmited = true
    if(form.valid) {
      this.service.postPerson()
      .subscribe({
        next:res => {
          this.service.refreshList()
          this.service.resetForm(form);
        },
        error:err => {console.log(err)}
      })
    }
  }
}
