import {Component, OnInit} from '@angular/core';
import {AuthService} from './auth-service.component';
import {Router} from '@angular/router';


@Component({
  selector: 'app-signin-callback',
  template: `
    <div></div>`
})

export class SigninRedirectCallbackComponent implements OnInit {
  constructor(private _authService: AuthService,
              private _router: Router) {
  }

  ngOnInit() {
    this._authService.completeLogin().then(user => {
      window.history.replaceState({}, window.document.title, window.location.origin + window.location.pathname);
      window.location = user.state || '/';
    });
  }
}
