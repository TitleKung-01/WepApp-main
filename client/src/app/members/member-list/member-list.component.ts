import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs';
import { UserParams } from 'src/app/_models/_userParams';
import { Member } from 'src/app/_models/member';
import { Pagination } from 'src/app/_models/pagination';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css'],
})
export class MemberListComponent implements OnInit {
  user: User | undefined;
  members: Member[] = [];
  pagination: Pagination | undefined;
  userParams: UserParams | undefined;

  genderList = [
    { value: 'male', display: 'Male' },
    { value: 'female', display: 'Female' },
    { value: 'non-binary', display: 'Non-binary' },
  ];

  constructor(
    private accountService: AccountService,
    private memberService: MembersService
  ) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: (user) => {
        if (user) this.user = user;
      },
    });
  }

  ngOnInit(): void {
    this.resetFilters();
    if (this.user) {
      const paramsString = localStorage.getItem('userParams');
      if (paramsString) {
        const localParams = JSON.parse(paramsString);
        if (localParams.username === this.user.username)
          this.userParams = localParams.params;
      }
    }
    this.loadMember();
  }

  resetFilters() {
    if (this.user) this.userParams = new UserParams(this.user); // TODO: Self Implemented
  }

  loadMember() {
    if (this.userParams) {
      this._saveParams();
      this.memberService.getMembers(this.userParams).subscribe({
        next: (response) => {
          if (response.result && response.pagination) {
            this.members = response.result;
            this.pagination = response.pagination;
          }
        },
      });
    }
  }

  pageChanged(event: any) {
    if (!this.userParams) return;
    if (this.userParams.pageNumber === event.page) return;
    this.userParams.pageNumber = event.page;
    this.loadMember();
  }

  private _saveParams() {
    if (this.user)
      localStorage.setItem(
        'userParams',
        JSON.stringify({
          username: this.user.username,
          params: this.userParams,
        })
      );
  }
}
