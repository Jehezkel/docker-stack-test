import { Component, inject } from '@angular/core';
import { AuthService, User } from '@auth0/auth0-angular';
import { AsyncPipe, NgIf } from '@angular/common';
import { Observable, map, filter } from 'rxjs';
import { RouterLink } from '@angular/router';


@Component({
  selector: 'app-top-bar',
  imports: [AsyncPipe, RouterLink, NgIf],
  templateUrl: './top-bar.component.html',
  styleUrl: './top-bar.component.scss'
})
export class TopBarComponent {
  masterDataPaneOpen = false
  onLogout() {
    this.auth.logout()
  }
  auth = inject(AuthService)
  userName$: Observable<string>;
  constructor() {
    this.userName$ = this.auth.user$.pipe(filter(u => !!u), map(u => u.name!))
  }

}
