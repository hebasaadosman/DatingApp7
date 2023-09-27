import { AccountService } from './../_services/account.service';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent {
  model: any = {};

  constructor(public accountService: AccountService, private router: Router) {}

  login() {
    this.accountService.login(this.model).subscribe({
      next: (response) => {
        this.router.navigateByUrl('/members');
        this.model = {};
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
    this.model = {};
  }
}
