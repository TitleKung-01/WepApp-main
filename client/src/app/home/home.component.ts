import { HttpClient } from '@angular/common/http'
import { Component, OnInit } from '@angular/core'
import { faBell } from '@fortawesome/free-solid-svg-icons'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  users: any
  faBell = faBell;
  regisMode = false

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getUsers()
  }

  regisToggle() {
    this.regisMode = !this.regisMode
  }

  private getUsers() {
    this.http.get('https://localhost:7777/api/users').subscribe({
      next: (response) => this.users = response,
      error: (err) => console.log(err),
      complete: () => console.log('request completed')
    })
  }

  cancelRegister(event: boolean) {
    this.regisMode = !event
  }

}
