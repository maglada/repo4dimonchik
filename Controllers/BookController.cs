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
    public IActionResult  GetAll()
    {
        var books = _context.Book
            .ToList()
            .Select(s => s.ToBookDto());

        return Ok(books);
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var book = _context.Book.Find(id);

        if (book == null)
        {
            return NotFound();
        }
        return Ok(book.ToBookDto());
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateBookRequestDto BookDto)
    {
      var bookModel = BookDto.ToBookFromCreateDTO();
      _context.Book.Add(bookModel);
      _context.SaveChanges();
      return CreatedAtAction(nameof(GetById), new { id = bookModel.BookId }, bookModel.ToBookDto()); // Add () to call the method
    }
  }
}