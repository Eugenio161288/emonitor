import { Component } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { finalize } from 'rxjs/operators';

import { AuthService } from '../../core/authentication/auth.service';
import { UserRegistration } from '../../shared/models/user.registration';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

  public success: boolean;
  public error: string;
  public userRegistration: UserRegistration = {name: '', email: '', password: ''};
  public submitted = false;

  constructor(private authService: AuthService, private spinner: NgxSpinnerService) {}

  public onSubmit() {
    this.spinner.show();

    this.authService.register(this.userRegistration)
      .pipe(finalize(() => {
        this.spinner.hide();
      }))
      .subscribe(
        result => {
          if (result) {
            this.success = true;
          }
        },
        error => {
          this.error = error;
        });
  }
}
