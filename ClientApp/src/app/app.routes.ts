import { Routes } from '@angular/router';
import { HomePageComponent } from './home-page/home-page.component';
import { AuthGuard } from '@auth0/auth0-angular';
import { AboutUsPageComponent } from './about-us-page/about-us-page.component';
import { ProductsPageComponent } from './products-page/products-page.component';
import { ProductComponent } from './products-page/product/product.component';
import { MasterDataPageComponent } from './master-data-page/master-data-page.component';

export const routes: Routes = [
  {
    path: '',
    canActivate: [AuthGuard],
    children: [
      { path: 'home', component: HomePageComponent },
      {
        path: 'products', component: ProductsPageComponent
      },
      {
        path: 'masterdata', children: [
          { path: 'categories', component: MasterDataPageComponent },
          { path: 'manufacturers', component: MasterDataPageComponent }
        ]
      },
      { path: 'products/new', component: ProductComponent },
      { path: 'products/edit/:productId', component: ProductComponent },
      { path: 'about', component: AboutUsPageComponent },
      { path: '**', redirectTo: 'home', pathMatch: 'full' }]
  }];
