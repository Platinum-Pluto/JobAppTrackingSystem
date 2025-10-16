import { Component } from '@angular/core';
import { RouterOutlet, RouterLink } from '@angular/router';

@Component({
  selector: 'app-homepage',
  imports: [RouterOutlet, RouterLink],
  templateUrl: './homepage.component.html',
  //styleUrl: './homepage.component.scss'

})
export class HomepageComponent {
  title = 'Platinum Pluto Job Tracking Website';
}
