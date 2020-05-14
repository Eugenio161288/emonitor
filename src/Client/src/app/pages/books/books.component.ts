import { Component, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute, Router } from '@angular/router';

import { AuthService } from '../../core/authentication/auth.service';
import { BooksService } from './services/books.service';

@Component({
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.scss']
})
export class BooksComponent implements OnInit {
  public claims = null;
  public busy: boolean;
  public pagetitle = '';
  public pageType = '';
  private fetchMethod = '';

  public constructor(
    private authService: AuthService,
    private booksService: BooksService,
    private spinner: NgxSpinnerService,
    private route: ActivatedRoute,
    private router: Router,
  ) {}

  private setParamByPath(path: string) {
    if (path === 'my-books') {
      this.pagetitle = 'My Books';
      this.pageType = 'my-books';
      this.fetchMethod = 'fetchMyBooksData';
    } else {
      this.pagetitle = 'Books';
      this.pageType = 'books';
      this.fetchMethod = 'fetchBooksData';
    }
  }

  public ngOnInit(): void {
    this.setParamByPath(this.route.routeConfig.path);

    this.busy = true;
    this.spinner.show();
    this.booksService[this.fetchMethod](this.authService.authorizationHeaderValue)
      .pipe(finalize(() => {
        this.spinner.hide();
        this.busy = false;
      })).subscribe(
      result => {
        this.claims = result;
      });
  }

  public onUnsubscribeClick(isbn: string): void {
    this.booksService.deleteSubscription(this.authService.authorizationHeaderValue, isbn)
      .pipe(finalize(() => {
        this.spinner.hide();
        this.busy = false;
      })).subscribe(
      result => {
        this.claims = this.claims.filter((book) => book.isbn !== isbn);
        console.log('success');
      });
  }

  public onBookClick(name: string): void {
    const currentBook = this.claims.find((book) => book.name === name);
    this.booksService.setCurrentBook(currentBook);
    this.router.navigate(['/book/' + name]);
  }
}
