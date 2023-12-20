import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  constructor(
    private toaster: ToastrService,
    public accountService: AccountService
  ) {}

  @Input() usersFromHomeCpmponent: any;

  model: any = {};

  register() {
    this.accountService.register(this.model).subscribe({
      next: (response) => this.cancel(),
      error: (err) => this.toaster.error(err.error),
    });
    this.cancel();
  }

  @Output() isCancel = new EventEmitter();
  cancel() {
    this.isCancel.emit(true);
  }
}
