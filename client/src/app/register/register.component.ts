import { Component, EventEmitter, Input, Output } from '@angular/core'
import { AccountService } from '../_services/account.service'
import { Router } from '@angular/router'
import { ToastrService } from 'ngx-toastr'

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  // @Input() usersFromHomeComponent: any
  @Output() isCancel = new EventEmitter()
  model: any = {}

  constructor(private accountService: AccountService, private router: Router, private toastr: ToastrService) { }

  register() {
    this.accountService.register(this.model).subscribe({
      error: err => this.toastr.error(err),
      next: () => { this.cancel(), this.router.navigateByUrl("/members") }
    })
  }

  cancel() {
    this.isCancel.emit(true)
  }

}
