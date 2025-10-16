import { Component } from '@angular/core';
import { RouterOutlet, RouterLink } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [RouterLink],
  templateUrl: './login.component.html',
  //styleUrl: './login.component.scss'
  styleUrl: './auth.css'
})
export class LoginComponent {
}
