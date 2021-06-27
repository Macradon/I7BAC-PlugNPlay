import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ValidationErrors,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  public registerForm: FormGroup;
  public usernameTaken = false;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private authService: AuthService
  ) {
    this.registerForm = this.formBuilder.group(
      {
        username: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        password: [
          '',
          [
            Validators.required,
            Validators.pattern(
              '^(?=.*d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z])(?=.*[@$!%*?&])$'
            ),
          ],
        ],
        confirmPassword: ['', Validators.required],
      },
      { validators: this.passwordMatchValidator }
    );
  }

  public register() {
    console.log(' This', this.registerForm.value);
    this.authService
      .register({
        Username: this.registerForm.get('username').value,
        Email: this.registerForm.get('email').value,
        Password: this.registerForm.get('password').value,
      })
      .subscribe(
        (res) => {
          this.redirectLogin();
        },
        (err: HttpErrorResponse) => {
          if (err.status === 200) {
            this.redirectLogin();
          } else if (err.status === 409) {
            this.usernameTaken = true;
          }
        }
      );
  }

  public redirectLogin() {
    this.router.navigate(['auth/login']);
  }

  public passwordMatchValidator(
    control: AbstractControl
  ): ValidationErrors | null {
    const password = control.get('password');
    const confirmPassword = control.get('confirmPassword');

    return password &&
      confirmPassword &&
      password.value !== confirmPassword.value
      ? { passwordMismatch: true }
      : null;
  }

  public getEmailErrorMessage() {
    if (this.registerForm.controls.email.hasError('required')) {
      return 'You must enter a value';
    }

    return this.registerForm.controls.email.hasError('email')
      ? 'Not a valid email'
      : '';
  }

  public getPasswordErrorMessage() {
    if (this.registerForm.controls.password.hasError('required')) {
      return 'You must enter a value';
    }

    return this.registerForm.controls.password.hasError('pattern')
      ? 'Password must be a minimum of 8 characters. It must also contain at least 1 lower and 1 upper case letter, a number and a special character: $ ! % * ? &'
      : 'Humbuhg';
  }

  public getConfirmPasswordErrorMessage() {
    if (this.registerForm.controls.password.hasError('required')) {
      return 'You must enter a value';
    }

    return this.registerForm.hasError('passwordMismatch')
      ? 'Passwords must be identical'
      : 'humbug';
  }
}
