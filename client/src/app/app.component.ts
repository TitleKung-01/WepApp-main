import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { faBell } from '@fortawesome/free-regular-svg-icons';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'Hello World!!!';
  faBell = faBell;
  users: any;

  constructor(private accountService: AccountService) {}

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user: User = JSON.parse(userString);
    this.accountService.setCurrentUser(user);
  }

  ngOnInit(): void {
    this.setCurrentUser();
  }
}
