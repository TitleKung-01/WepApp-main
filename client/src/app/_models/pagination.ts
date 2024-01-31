export interface Pagination {
  currentPage: number;
  itemsPerpage: number;
  totalItems: number;
  totalPages: number;
}
export class PaginationResult<T> {
  result?: T;
  pagination?: Pagination;
}
