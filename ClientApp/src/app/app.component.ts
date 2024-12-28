import { Component, inject } from '@angular/core';
import { LayoutComponent } from "./layout/layout.component";
import { AuthService } from '@auth0/auth0-angular';
import { AsyncPipe } from '@angular/common';
import { LoginPageComponent } from "./login-page/login-page.component";
import { ToastrComponent } from './shared/toastr/toastr.component';
import { ModalComponent } from './shared/modal/modal.component';

@Component({
  selector: 'app-root',
  imports: [LayoutComponent, AsyncPipe, LoginPageComponent, ToastrComponent, ModalComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'ClientApp';
  auth = inject(AuthService)


}
