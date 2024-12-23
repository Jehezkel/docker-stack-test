import { Component, inject } from '@angular/core';
import { LayoutComponent } from "./layout/layout.component";
import { AuthService } from '@auth0/auth0-angular';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { LoginPageComponent } from "./login-page/login-page.component";

@Component({
  selector: 'app-root',
  imports: [LayoutComponent, AsyncPipe, LoginPageComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'ClientApp';
  auth = inject(AuthService)


}
