import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { Shell } from '../shell/shell.service';

import { AuthGuard } from '../core/authentication/auth.guard';

import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { TopSecretComponent } from './top-secret/top-secret.component';
import { BooksComponent } from './books/books.component';
import { BookComponent } from './book/book.component';

const routes: Routes = [
  Shell.childRoutes([
    { path: '', redirectTo: '/home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'topsecret', component: TopSecretComponent, canActivate: [AuthGuard] },
    { path: 'books', component: BooksComponent, canActivate: [AuthGuard] },
    { path: 'my-books', component: BooksComponent, canActivate: [AuthGuard] },
    { path: 'book/:name', component: BookComponent, canActivate: [AuthGuard] },
  ])
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class PagesRoutingModule {
}
