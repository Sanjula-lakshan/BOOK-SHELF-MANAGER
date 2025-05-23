using Microsoft.AspNetCore.Mvc;
using BookManagementAPI.Models;
using BookManagementAPI.Services;

namespace BookManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/Books
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            // Log the request to the console for debugging
            Console.WriteLine("GetBooks method hit!");  // Logging to console

            // Retrieve all books using the BookService
            var books = _bookService.GetAllBooks();
            return Ok(books);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound(new { message = $"Book with ID : {id} not found", statusCode = 404 });
            }

            return Ok(book);
        }

        // POST: api/Books
        [HttpPost]
        public ActionResult<Book> PostBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid model state", statusCode = 400 });
            }

            var createdBook = _bookService.AddBook(book);
            if (createdBook == null)
            {
                return StatusCode(500, new { message = "Error creating book", statusCode = 500 });
            }

            return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, new
            {
                message = "Book successfully created",
                statusCode = 201,
                book = createdBook
            });
        }


        // PUT: api/Books/5
        [HttpPut]
        public IActionResult PutBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid model state", statusCode = 400 });
            }

            var existingBook = _bookService.GetBookById(book.Id);
            if (existingBook == null)
            {
                return NotFound(new { message = "Book not found", statusCode = 404 });
            }

            var updated = _bookService.UpdateBook(book);
            if (!updated)
            {
                return StatusCode(500, new { message = "Error updating book", statusCode = 500 });
            }

            return Ok(new { message = $"Successfully updated, ID: {book.Id}", statusCode = 200 });
        }



        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var deleted = _bookService.DeleteBook(id);
            if (!deleted)
            {
                return NotFound(new { message = "Book not found", statusCode = 404 });
            }

            return Ok(new { message = "Book successfully deleted", statusCode = 200 });
        }

    }
}
