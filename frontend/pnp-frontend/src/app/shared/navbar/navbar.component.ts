import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  constructor(private router: Router) {}

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
    this.router.navigate(['']);
  }
}
