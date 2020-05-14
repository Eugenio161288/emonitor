import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError } from 'rxjs/operators';

import { BaseService } from '../../../shared/base.service';
import { ConfigService } from '../../../shared/config.service';

@Injectable({
  providedIn: 'root'
})
export class BooksService extends BaseService {
  private currentBook = {};

  constructor(private http: HttpClient, private configService: ConfigService) {
    super();
  }

  setCurrentBook(book: any): void {
    this.currentBook = book;
  }

  getCurrentBook(): any {
    return this.currentBook;
  }

  fetchBooksData(token: string) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: token
      })
    };

    return this.http.get(this.configService.resourceApiURI + '/books', httpOptions).pipe(catchError(this.handleError));
  }

  fetchMyBooksData(token: string) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: token
      })
    };

    return this.http.get(this.configService.resourceApiURI + '/subscription/UserBooks', httpOptions).pipe(catchError(this.handleError));
  }

  subscribeBook(token: string, isbn: string) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: token
      })
    };

    const body = {
      isbn,
    };

    return this.http.post(
      this.configService.resourceApiURI + '/subscription/addbook',
      body,
      httpOptions,
    ).pipe(catchError(this.handleError));
  }

  deleteSubscription(token: string, isbn: string) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: token
      }),
      body: {
        isbn,
      },
    };

    return this.http.delete(
      this.configService.resourceApiURI + '/subscription/deletebook ',
      httpOptions,
    ).pipe(catchError(this.handleError));
  }
}
