import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  regisMode = false;

  regisToggle() {
    this.regisMode = !this.regisMode;
  }

  users: any;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    // this.getUsers();
  }

  // getUsers() {
  //   this.http.get('https://localhost:7777/api/users').subscribe({
  //     next: (response) => (this.users = response),
  //     error: (err) => console.log(err),
  //     complete: () => console.log('request completed'),
  //   });
  // }

  cancelRegister(event: boolean) {
    this.regisMode = !event;
  }
}
