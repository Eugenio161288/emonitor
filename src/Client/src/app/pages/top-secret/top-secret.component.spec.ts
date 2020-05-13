import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NgxSpinnerModule } from 'ngx-spinner';
import { AuthService } from '../../core/authentication/auth.service';
import { MockAuthService } from '../../shared/mocks/mock-auth.service';
import { TopSecretService } from './services/top-secret.service';
import { MockTopSecretService } from '../../shared/mocks/mock-top-secret.service';

import { TopSecretComponent } from './top-secret.component';

describe('IndexComponent', () => {
  let component: TopSecretComponent;
  let fixture: ComponentFixture<TopSecretComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TopSecretComponent ],
      imports: [NgxSpinnerModule],
      providers: [
        {provide: AuthService, useClass: MockAuthService},
        {provide: TopSecretService, useClass: MockTopSecretService}
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TopSecretComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
