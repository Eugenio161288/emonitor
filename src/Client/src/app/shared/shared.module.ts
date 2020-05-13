import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { NgxSpinnerModule } from 'ngx-spinner';

import { AutofocusDirective } from './directives/auto-focus.directive';

import { HeaderComponent } from '../components/header/header.component';

@NgModule({
  imports: [CommonModule, NgxSpinnerModule, RouterModule],
  declarations: [AutofocusDirective, HeaderComponent],
  exports: [NgxSpinnerModule, AutofocusDirective, HeaderComponent],
  providers: []
})
export class SharedModule {
}
