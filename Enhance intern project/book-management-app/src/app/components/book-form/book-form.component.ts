import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { BookService } from '../../services/book.service';
import { Book } from '../../models/book.model';

@Component({
  selector: 'app-book-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './book-form.component.html',
  styleUrls: ['./book-form.component.css']
})
export class BookFormComponent implements OnInit {
  bookForm: FormGroup;
  isEditMode = false;
  bookId: number | null = null;
  formSubmitted = false;

  constructor(
    private fb: FormBuilder,
    private bookService: BookService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.bookForm = this.fb.group({
      title: ['', [Validators.required]],
      author: ['', [Validators.required]],
      isbn: ['', [Validators.required, Validators.pattern(/^[0-9-]+$/)]],
      publicationDate: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.isEditMode = true;
        this.bookId = +id;
        this.loadBookData(+id);
      }
    });
  }

  loadBookData(id: number): void {
    this.bookService.getBook(id).subscribe(book => {
      // Format date for the input type="date"
      const formattedDate = new Date(book.publicationDate).toISOString().split('T')[0];
      
      this.bookForm.patchValue({
        title: book.title,
        author: book.author,
        isbn: book.isbn,
        publicationDate: formattedDate
      });
    });
  }

  onSubmit(): void {
    this.formSubmitted = true;
    
    if (this.bookForm.invalid) {
      return;
    }

    const bookData: Book = {
      id: this.isEditMode && this.bookId ? this.bookId : 0,
      ...this.bookForm.value
    };

    if (this.isEditMode && this.bookId) {
      this.bookService.updateBook(bookData).subscribe(() => {
        this.router.navigate(['/']);
      });
    } else {
      this.bookService.addBook(bookData).subscribe(() => {
        this.router.navigate(['/']);
      });
    }
  }

  get title() { return this.bookForm.get('title'); }
  get author() { return this.bookForm.get('author'); }
  get isbn() { return this.bookForm.get('isbn'); }
  get publicationDate() { return this.bookForm.get('publicationDate'); }
}