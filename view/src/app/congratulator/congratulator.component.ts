import { Component, OnInit } from '@angular/core';
import { PersonFormComponent } from "./congratulator-form/person-form.component";
import { PersonService } from '../shared/person.service';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-congratulator',
  standalone: true,
  imports: [PersonFormComponent, NgFor],
  templateUrl: './congratulator.component.html',
  styles: ``
})
export class CongratulatorComponent implements OnInit {

  constructor(public service : PersonService) {

  }
  ngOnInit(): void {
    this.service.refreshList();
  }
}
