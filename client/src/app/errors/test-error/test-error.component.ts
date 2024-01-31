import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.css'],
})
export class TestErrorComponent {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  private _get(path: string) {
    this.http.get(this.baseUrl + path).subscribe({
      next: (resp) => console.log(resp),
      error: (err) => console.log(err),
    });
  }

  getNotFoundError() {
    this._get('error/not-found');
  }
  getBadRequest() {
    this._get('error/bad-request');
  }
  getAuthError() {
    this._get('error/auth');
  }
  getNullRefError() {
    this._get('error/server-error');
  }
  getValidationError() {
    this.http.post(this.baseUrl + '/account/register', {}).subscribe({
      next: (resp) => console.log(resp),
      error: (err) => console.log(err),
    });
  }
}
