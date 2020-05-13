import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { finalize } from 'rxjs/operators';

import { AuthService } from '../../core/authentication/auth.service';
import { BooksService } from '../books/services/books.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.scss']
})
export class BookComponent implements OnInit {
  busy = false;
  isSubscribed = false;
  currentBook: any = {};

  public constructor(
    private authService: AuthService,
    private booksService: BooksService,
    private route: ActivatedRoute,
    private spinner: NgxSpinnerService,
  ) {}

  public ngOnInit(): void {
    this.busy = true;
    this.spinner.show();
    // @ts-ignore
    const name = this.route.params.value.name;
    this.currentBook = this.booksService.getCurrentBook();

    this.booksService.fetchMyBooksData(this.authService.authorizationHeaderValue)
      .pipe(finalize(() => {
        this.spinner.hide();
        this.busy = false;
      })).subscribe(
      result => {
        const books = result;
        // @ts-ignore
        this.isSubscribed = !!books.find((book) => book.name === this.currentBook.name);
        console.log('this.isSubscribed', this.isSubscribed);
      });
  }

  public onSubscribeClick(name): void {
    this.busy = true;
    this.spinner.show();

    this.booksService.subscribeBook(this.authService.authorizationHeaderValue, name)
      .pipe(finalize(() => {
        this.spinner.hide();
        this.busy = false;
      })).subscribe(
      result => {
        console.log('success');
        this.isSubscribed = true;
      });
  }
}
