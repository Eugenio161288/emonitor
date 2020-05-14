import { Component } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthService } from '../../core/authentication/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  public title = 'Login';

  constructor(private authService: AuthService, private spinner: NgxSpinnerService) {}

  public login() {
    this.spinner.show();
    this.authService.login();
  }
}


