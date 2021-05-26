import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  public loginForm: FormGroup;
  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService
  ) {
    this.loginForm = this.formBuilder.group({
      username: [''],
      password: [''],
    });
  }

  ngOnInit(): void {}

  login() {
    this.authService
      .login({
        Username: this.loginForm.get('username').value,
        Password: this.loginForm.get('password').value,
      })
      .subscribe((data) =>
        this.authService.loginSuccessful({
          username: this.loginForm.get('username').value,
        })
      );
  }
}
