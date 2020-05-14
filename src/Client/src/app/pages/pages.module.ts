import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthCallbackComponent } from './auth-callback/auth-callback.component';
import { BooksComponent } from './books/books.component';
import { BookComponent } from './book/book.component';

import { SharedModule } from '../shared/shared.module';
import { PagesRoutingModule } from './pages.routing-module';

import { AuthService } from '../core/authentication/auth.service';

@NgModule({
  declarations: [
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    AuthCallbackComponent,
    BooksComponent,
    BookComponent,
  ],
  providers: [AuthService],
  imports: [
    CommonModule,
    FormsModule,
    PagesRoutingModule,
    SharedModule,
    RouterModule,
  ]
})
export class PagesModule {
}
