import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  constructor(private router: Router, private auth: AuthService) {}

  ngOnInit(): void {}

  public redirectHome() {
    this.router.navigate(['']);
  }

  public redirectRegister() {
    this.router.navigate(['auth/register']);
  }

  public redirectLogin() {
    this.router.navigate(['auth/login']);
  }
  public logout() {
    this.auth.logout();
    this.router.navigate(['']);
  }
}
