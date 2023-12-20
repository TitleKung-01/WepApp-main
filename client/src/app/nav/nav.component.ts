import { Component, OnInit } from '@angular/core'
import { AccountService } from '../_services/account.service'
import { Observable, of } from 'rxjs'
import { User } from '../_models/user'
import { Router, Routes } from '@angular/router'
import { ToastrService } from 'ngx-toastr'

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: { username: string | undefined, password: string | undefined } = {
    username: undefined,
    password: undefined
  }

  currentUser$: Observable<User | null> = of(null)

  constructor(private accountService: AccountService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$
  }

  getCurrentUser() {
    this.accountService.currentUser$.subscribe({
      // next: user => this.isLogin = !!user, // user?true:false
      error: err => console.log(err)
    })
  }

  login(): void {
    this.accountService.login(this.model).subscribe({ //Observable
      next: response => {
        this.router.navigateByUrl('/members')
      },
      // error: err => this.toastr.error(err.error)
    })
  }

  logout() {
    this.accountService.logout()
    this.router.navigateByUrl("/")
  }
}
