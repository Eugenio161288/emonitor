import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../core/authentication/auth.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-auth-callback',
  templateUrl: './auth-callback.component.html',
  styleUrls: ['./auth-callback.component.scss']
})
export class AuthCallbackComponent implements OnInit {
  public isError: boolean;

  constructor(private authService: AuthService, private router: Router, private route: ActivatedRoute) {}

  public async ngOnInit() {
    // check for error
    if (this.route.snapshot.fragment.indexOf('error') >= 0) {
      this.isError = true;
      return;
    }

    await this.authService.completeAuthentication();
    this.router.navigate(['/home']);
  }
}
