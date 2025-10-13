import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './dashboard.html'
})
export class AppComponent {
  title = 'Platinum Pluto Job Tracking Website';
}
