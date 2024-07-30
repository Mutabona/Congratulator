import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CongratulatorComponent } from "./congratulator/congratulator.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CongratulatorComponent, FormsModule, RouterLink, RouterLinkActive],
  templateUrl: './app.component.html',
  styles: [],
})
export class AppComponent {
  title = 'Congratulator';
}
