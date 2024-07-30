import { Component, OnInit } from '@angular/core';
import { PersonFormComponent } from "./person-form/person-form.component";
import { PersonService } from '../shared/person.service';
import { NgFor } from '@angular/common';
import { Person } from '../shared/person.model';
import { Guid } from 'guid-typescript';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-congratulator',
  standalone: true,
  imports: [PersonFormComponent, NgFor, RouterLink, RouterLinkActive],
  templateUrl: './congratulator.component.html',
  styles: ``
})
export class CongratulatorComponent implements OnInit {
  showingAll: boolean = false;
  showButtonText: string = "";
  label = "";

  constructor(public service : PersonService) {}
  ngOnInit(): void {
    this.showNearest();
  }

  populateForm(selectedRecord:Person) {
    this.service.formData = Object.assign({}, selectedRecord) 
  }

  changeShowingForm() {
    if (this.showingAll) {
      this.showNearest();
    }
    else {
      this.showAll();
    }
  }

  showAll() {
    this.showingAll = true;;
    this.label = "Все дни рождения";
    this.service.refreshAll();   
    this.showButtonText = "Показать ближайшие";
  }

  showNearest() {
    this.showingAll = false;
    this.label = "Дни рождения в ближайшие 7 дней";
    this.service.refreshNearest();
    this.showButtonText = "Показать все";
  }

  onDelete(id:Guid) {
    if (confirm('Вы уверены?')) {
      this.service.deletePerson(id)
      .subscribe({
        next: res => { this.showNearest() },
        error: err => { console.log(err) }
      })
    }
  }
}
