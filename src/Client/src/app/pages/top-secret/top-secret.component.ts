import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { finalize } from 'rxjs/operators';

import { AuthService } from '../../core/authentication/auth.service';
import { TopSecretService } from './services/top-secret.service';

@Component({
  selector: 'app-top-secret',
  templateUrl: './top-secret.component.html',
  styleUrls: ['./top-secret.component.scss']
})
export class TopSecretComponent implements OnInit {
  claims = null;
  busy: boolean;

  constructor(private authService: AuthService, private topSecretService: TopSecretService, private spinner: NgxSpinnerService) {
  }

  ngOnInit() {
    this.busy = true;
    this.spinner.show();
    this.topSecretService.fetchTopSecretData(this.authService.authorizationHeaderValue)
      .pipe(finalize(() => {
        this.spinner.hide();
        this.busy = false;
      })).subscribe(
      result => {
        this.claims = result;
      });
  }
}
