import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core'
import { BehaviorSubject, map } from 'rxjs'
import { User } from '../_models/user'
import { environment } from 'src/environments/environment'

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  // baseUrl = 'https://localhost:7777/api/'
  baseUrl = environment.apiUrl

  constructor(private http: HttpClient) { }

  private currentUserSource = new BehaviorSubject<User | null>(null)
  currentUser$ = this.currentUserSource.asObservable()

  login(model: any) {
    return this.http.post<User>(`${this.baseUrl}account/login`, model).pipe(
      map((user: User) => {
        if (user) {
          // localStorage.setItem('user', JSON.stringify(user))
          // this.currentUserSource.next(user)
          this.setCurrentUser(user)
        }
      })
    )
  }

  logout() {
    localStorage.removeItem('user')
    this.currentUserSource.next(null)
  }

  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user))
    this.currentUserSource.next(user)
  }

  register(model: any) {
    return this.http.post<User>(`${this.baseUrl}account/register`, model).pipe(
      map(user => {
        if (user) {
          // localStorage.setItem('user', JSON.stringify(user))
          // this.currentUserSource.next(user)
          this.setCurrentUser(user)
        }
      })
    )
  }

}