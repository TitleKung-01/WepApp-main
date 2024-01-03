import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent {
  constructor(
    private toastr: ToastrService,
    private router: Router,
    public accountService: AccountService
  ) {}

  model: { username: string | undefined; password: string | undefined } = {
    username: undefined,
    password: undefined,
  };

  getCurrentUser() {
    this.accountService.currentUser$.subscribe({
      next: (user) => console.log(user),
      error: (err) => console.log(err),
    });
  }

  login(): void {
    this.accountService.login(this.model).subscribe({
      next: (response) => this.router.navigateByUrl('/members'),
      // error: (err) => this.toastr.error(err.error),
    });
  }

  logout(): void {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
