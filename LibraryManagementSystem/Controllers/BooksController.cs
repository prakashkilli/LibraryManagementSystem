using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Service;

namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public BooksController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _libraryService.GetAllBooksAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _libraryService.GetBookByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Book book) => Ok(await _libraryService.AddBookAsync(book));


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Book book)
        {
            if (id != book.Id) return BadRequest();
            var updatedBook = await _libraryService.UpdateBookAsync(book);
            if (updatedBook == null) return NotFound();
            return Ok(updatedBook);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _libraryService.DeleteBookAsync(id);
            if (!deleted) return NotFound();
            return Ok();
        }
    }
}
