import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of, take } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserParams } from '../_models/_userParams';
import { Member } from '../_models/member';
import { User } from '../_models/user';
import { AccountService } from './account.service';
import { getPaginationHeaders, getPaginationResult } from './paginationHelper';

@Injectable({
  providedIn: 'root',
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members: Member[] = [];
  memberCache = new Map();
  user: User | undefined;

  constructor(
    private accountService: AccountService,
    private http: HttpClient
  ) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: (user) => {
        if (user) {
          this.user = user;
        }
      },
    });
  }

  private _key(userParams: UserParams) {
    return Object.values(userParams).join('_');
  }

  getMembers(userParams: UserParams) {
    const key = this._key(userParams);
    const response = this.memberCache.get(key);
    if (response) return of(response);
    let params = getPaginationHeaders(
      userParams.pageNumber,
      userParams.pageSize
    );
    params = params.append('minAge', userParams.minAge);
    params = params.append('maxAge', userParams.maxAge);
    params = params.append('gender', userParams.gender);
    params = params.append('orderBy', userParams.orderBy);
    const url = this.baseUrl + '/users';
    return getPaginationResult<Member[]>(url, params, this.http).pipe(
      map((response) => {
        this.memberCache.set(key, response);
        return response;
      })
    );
  }

  getMember(username: string) {
    const cache = [...this.memberCache.values()];
    const members = cache.reduce((arr, item) => arr.concat(item.result), []);
    const member = members.find(
      (member: Member) => member.userName === username
    );
    if (member) return of(member);

    return this.http.get<Member>(this.baseUrl + '/users/username/' + username);
  }

  updateProfile(member: Member) {
    return this.http.put(this.baseUrl + '/users', member).pipe(
      map((_) => {
        const index = this.members.indexOf(member);
        this.members[index] = { ...this.members[index], ...member };
      })
    );
  }

  setMainPhoto(photoId: number) {
    const endpoint = this.baseUrl + '/users/set-main-photo/' + photoId;
    return this.http.put(endpoint, {});
  }

  deletePhoto(photoId: number) {
    const endpoint = this.baseUrl + '/users/delete-photo/' + photoId;
    return this.http.delete(endpoint);
  }

  addLike(username: string) {
    return this.http.post(this.baseUrl + '/likes/' + username, {});
  }

  getLikes(predicate: string, pageNumber: number, pageSize: number) {
    let params = getPaginationHeaders(pageNumber, pageSize);
    params = params.append('predicate', predicate);
    const url = this.baseUrl + '/likes';
    return getPaginationResult<Member[]>(url, params, this.http);
  }
}
