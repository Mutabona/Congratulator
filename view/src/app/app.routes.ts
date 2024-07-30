import { Routes } from '@angular/router';
import { CongratulatorComponent } from './congratulator/congratulator.component';
import { PersonFormComponent } from './congratulator/person-form/person-form.component';

export const routes: Routes = [
    {path: 'congratulator', component: CongratulatorComponent},
    {path: 'congratulator/person-form', component: PersonFormComponent},
    {path: '', redirectTo: 'congratulator', pathMatch: 'full'}
];
