import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/auth/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit, OnDestroy {
  private subscritions: Subscription[] = [];
  public userLoggedIn = false;
  constructor(private router: Router, private auth: AuthService) {}

  ngOnInit(): void {
    this.subscritions.push(
      this.auth.$userLoggedIn.subscribe((bool) => (this.userLoggedIn = bool))
    );
  }

  ngOnDestroy(): void {
    this.subscritions.forEach((sub) => sub.unsubscribe());
  }

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
