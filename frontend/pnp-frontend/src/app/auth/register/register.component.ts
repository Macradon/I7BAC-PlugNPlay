import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ValidationErrors,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  public registerForm: FormGroup;

  constructor(private router: Router, private formBuilder: FormBuilder) {
    this.registerForm = this.formBuilder.group(
      {
        username: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        password: [
          '',
          [
            Validators.required,
            Validators.pattern(
              '^(?=.*[a-z])(?=.*[A-Z])(?=.*d)(?=.*[@$!%*?&])[A-Za-zd@$!%*?&]{8,}$'
            ),
          ],
        ],
        confirmPassword: ['', Validators.required],
      },
      this.passwordMatchValidator
    );
  }

  ngOnInit(): void {}

  public redirectLogin() {
    this.router.navigate(['login']);
  }

  public passwordMatchValidator(
    control: AbstractControl
  ): ValidationErrors | null {
    const password: string = control.get('password').value;
    const confirmPassword: string = control.get('confirmPassword').value;
    console.log('pw: ', password, ' cpw: ', confirmPassword);

    return password === confirmPassword ? null : { passwordMismatch: true };
  }

  public register() {
    console.log(' This', this.registerForm.controls.password.errors);
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

    return this.registerForm.controls.email.hasError('pattern')
      ? 'Password must be a minimum of 8 characters. It must also contain at least 1 lower and 1 upper case letter, a number and a special character: $ ! % * ? &'
      : '';
  }

  public getConfirmPasswordErrorMessage() {
    if (this.registerForm.controls.password.hasError('required')) {
      return 'You must enter a value';
    }

    return this.registerForm.hasError('passwordMismatch')
      ? 'Must match password'
      : 'humbug';
  }
}
