import { bootstrapApplication } from '@angular/platform-browser';
import { provideHttpClient } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { AppComponent } from './app/app.component';

const routes = [
  { path: '', loadComponent: () => import('./app/components/book-list/book-list.component').then(m => m.BookListComponent) },
  { path: 'add', loadComponent: () => import('./app/components/book-form/book-form.component').then(m => m.BookFormComponent) },
  { path: 'edit/:id', loadComponent: () => import('./app/components/book-form/book-form.component').then(m => m.BookFormComponent) },
  { path: '**', redirectTo: '' }
];

bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient(),
    provideRouter(routes)
  ]
}).catch(err => console.error(err));