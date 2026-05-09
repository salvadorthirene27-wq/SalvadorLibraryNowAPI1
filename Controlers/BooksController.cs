using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalvadorLibraryNowAPI.Models;
using System.Net.NetworkInformation;

namespace SalvadorLibraryNowAPI.Controlers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private static List<Book> books = new List<Book>

        {
            new Book
            {
                Id = 1,
                Title = "As Old As Time: A Twisted Tale",
                Author = "Elizabeth J. Braswell",
                Genre = "Young Adult / Fantacy / Fairy Tale Retelling",
                Available = true,
                Publishedyear = 2016
            },
            new Book
            {
                Id = 2,
                Title = "What Once Was Mine: A Twisted Tale",
                Author = "Elizabeth J. Braswell",
                Genre = "Young Adult / Fantacy / Fairy Tale Retelling",
                Available = true,
                Publishedyear = 2021
            }
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new
            {
                status = "sucess",
                data = books,
                message = "Books retrived."
            });
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found."
                });
            return Ok(new
            {
                status = "success",
                data = book,
                message = "Book retrived."
            });
        }
        [HttpPost]
        public IActionResult Create([FromBody] Book newBook)
        {
            newBook.Id = books.Count + 1;
            books.Add(newBook);
            return CreatedAtAction(nameof(GetById),
                new { id = newBook.Id },
                new
                {
                    status = "success",
                    data = newBook,
                    message = "Book created."
                });
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Book updateBook)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found."
                });
            book.Title = updateBook.Title;
            book.Author = updateBook.Author;
            book.Genre = updateBook.Genre;
            book.Available = updateBook.Available;
            book.Publishedyear = updateBook.Publishedyear;

            return Ok(new
            {
                status = "success",
                data = book,
                message = "Book updated."
            });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found."
                });

            books.Remove(book);
            return Ok(new
            {
                status = "success",
                data = (object?)null,
                message = "Book deleted."
            });
        }
    }
}