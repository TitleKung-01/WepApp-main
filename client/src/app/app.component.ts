import { HttpClient } from '@angular/common/http'
import { Component, OnInit } from '@angular/core'
import { faBell } from '@fortawesome/free-solid-svg-icons'
import { AccountService } from './_services/account.service'
import { User } from './_models/user'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  users: any
  faBell = faBell;

  constructor(private http: HttpClient, private accountService: AccountService) { }

  ngOnInit(): void {
    // this.getUsers()
    this.setCurrentUser()
  }

  // private getUsers() {
  //   this.http.get('https://localhost:7777/api/users').subscribe({
  //     next: (response) => this.users = response,
  //     error: (err) => console.log(err),
  //     complete: () => console.log('request completed')
  //   })
  // }

  setCurrentUser() {
    const userString = localStorage.getItem('user')
    if (!userString) return
    const user: User = JSON.parse(userString)
    this.accountService.setCurrentUser(user)
  }

}
