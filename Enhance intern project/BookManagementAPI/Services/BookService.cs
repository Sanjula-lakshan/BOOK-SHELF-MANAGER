using BookManagementAPI.Models;

namespace BookManagementAPI.Services
{
    public class BookService
    {
        private static List<Book> _books = new List<Book>();
        private static int _nextId = 1;

        public BookService()
        {
            // Add some sample data if empty
            if (_books.Count == 0)
            {
                _books.Add(new Book
                {
                    Id = _nextId++,
                    Title = "Clean Code",
                    Author = "Robert C. Martin",
                    ISBN = "978-0132350884",
                    PublicationDate = new DateTime(2008, 8, 1)
                });

                _books.Add(new Book
                {
                    Id = _nextId++,
                    Title = "Design Patterns",
                    Author = "Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides",
                    ISBN = "978-0201633610",
                    PublicationDate = new DateTime(1994, 10, 31)
                });
            }
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _books;
        }

        public Book? GetBookById(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        public Book AddBook(Book book)
        {
            book.Id = _nextId++;
            _books.Add(book);
            return book;
        }

        public bool UpdateBook(Book updatedBook)
        {
            var existingBook = GetBookById(updatedBook.Id);
            if (existingBook == null)
                return false;

            existingBook.Title = updatedBook.Title;
            existingBook.Author = updatedBook.Author;
            existingBook.ISBN = updatedBook.ISBN;
            existingBook.PublicationDate = updatedBook.PublicationDate;

            return true;
        }

        public bool DeleteBook(int id)
        {
            var book = GetBookById(id);
            if (book == null)
                return false;

            _books.Remove(book);
            return true;
        }

    }
}