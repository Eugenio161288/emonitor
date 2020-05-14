import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import { AuthService } from '../../core/authentication/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit, OnDestroy {

  public name: string;
  public isAuthenticated: boolean;
  private subscription: Subscription;

  constructor(private authService: AuthService) {}

  public ngOnInit(): void {
    this.subscription = this.authService.authNavStatus$.subscribe(status => this.isAuthenticated = status);
    this.name = this.authService.name;
  }

  public async signout() {
    await this.authService.signout();
  }

  public ngOnDestroy(): void {
    // prevent memory leak when component is destroyed
    this.subscription.unsubscribe();
  }
}
