import { Routes } from '@angular/router';
import { HomePageComponent } from './home-page/home-page.component';
import { AuthGuard } from '@auth0/auth0-angular';
import { AboutUsPageComponent } from './about-us-page/about-us-page.component';

export const routes: Routes = [
  {
    path: '',
    canActivate: [AuthGuard],
    children: [
      { path: 'home', component: HomePageComponent },
      { path: 'about', component: AboutUsPageComponent }
      , { path: '**', redirectTo: 'home', pathMatch: 'full' }]
  }];
