import { HttpParams, HttpClient } from '@angular/common/http';
import { map } from 'rxjs';
import { PaginationResult } from '../_models/pagination';

export function getPaginationResult<T>(
  url: string,
  params: HttpParams,
  http: HttpClient
) {
  const paginationResult: PaginationResult<T> = new PaginationResult<T>();
  return http.get<T>(url, { observe: 'response', params }).pipe(
    map((response) => {
      if (response.body) paginationResult.result = response.body;

      const pagination = response.headers.get('Pagination');
      if (pagination) paginationResult.pagination = JSON.parse(pagination);

      return paginationResult;
    })
  );
}

export function getPaginationHeaders(pageNumber: number, pageSize: number) {
  let params = new HttpParams();
  params = params.append('pageNumber', pageNumber);
  params = params.append('pageSize', pageSize);
  return params;
}
