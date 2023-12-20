import { HttpClient, HttpHeaders } from '@angular/common/http'
import { environment } from 'src/environments/environment'
import { Member } from '../_modules/member'
import { Injectable } from '@angular/core'



@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl

  constructor(private http: HttpClient) { }

  // getHttpOptions() {
  //   const userString = localStorage.getItem('user')
  //   if (!userString) return
  //   const user: User = JSON.parse(userString)
  //   return {
  //     headers: new HttpHeaders({
  //       Authorization: 'Bearer ' + user.token
  //     })
  //   }
  // }

  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + 'users/')
  }

  getMember(username: string) {
    return this.http.get<Member>(this.baseUrl + 'users/username/' + username)
  }
}