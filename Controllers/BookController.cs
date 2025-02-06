using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Mappers;
using api.Dtos.Book;

namespace api.Controllers
{
  [Route("api/book")]
  [ApiController]
  public class BookController : ControllerBase
  {
    private readonly ApplicationDBContext _context;
    public BookController(ApplicationDBContext context)
    {
       _context = context;   
    }

    [HttpGet]
    public async Task<IActionResult>  GetAll()
    {
        var books = await _context.Book
            .ToListAsync();

        var bookDto = books.Select(s => s.ToBookDto());

        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var book =  await _context.Book.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }
        return Ok(book.ToBookDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookRequestDto BookDto)
    {
      var bookModel = BookDto.ToBookFromCreateDTO();
      await _context.Book.AddAsync(bookModel);
      await _context.SaveChangesAsync();
      return CreatedAtAction(nameof(GetById), new { id = bookModel.BookId }, bookModel.ToBookDto()); // Add () to call the method
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatesBookRequestDto updateDto)
    {
      var bookModel = await _context.Book.FirstOrDefaultAsync(x => x.BookId == id);

      if(bookModel == null)
      {
        return NotFound();
      }

      bookModel.Name = updateDto.Name;
      bookModel.Year = updateDto.Year;
      bookModel.Genre = updateDto.Genre;
      bookModel.AuthorId = updateDto.AuthorId;

      await _context.SaveChangesAsync();

      return Ok(bookModel.ToBookDto());
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
      var bookModel = await _context.Book.FirstOrDefaultAsync(x => x.BookId == id);

      if(bookModel == null)
      {
        return NotFound();
      }

      _context.Book.Remove(bookModel);
      await _context.SaveChangesAsync();

      return NoContent();
    }    
  }
}