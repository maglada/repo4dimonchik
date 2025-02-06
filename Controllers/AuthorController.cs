using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Mappers;
using api.Dtos.Author;
using api.Interfaces;

namespace api.Controllers
{
  [Route("api/author")]
  [ApiController]
  public class AuthorController : ControllerBase
  {
    private readonly ApplicationDBContext _context;
    private readonly IAuthorRepository _authorRepo;
    public AuthorController(ApplicationDBContext context, IAuthorRepository authorRepo)
    {
       _authorRepo = authorRepo;
       _context = context;   
    }

    [HttpGet]
    public IActionResult  GetAll()
    {
        var authors = _context.Author
            .Include(a => a.Books)
            .ToList()
            .Select(s => s.ToAuthorDto());

        return Ok(authors);
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var authors = _context.Author
            .Include(a => a.Books)
            .FirstOrDefault(a => a.AuthorId == id);

        if (authors == null)
        {
            return NotFound();
        }
        return Ok(authors.ToAuthorDto());
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateAuthorRequestDto AuthorDto)
    {
      var authorModel = AuthorDto.ToAuthorFromCreateDTO(_context);
      _context.Author.Add(authorModel);
      _context.SaveChanges();
      return CreatedAtAction(nameof(GetById), new { id = authorModel.AuthorId }, authorModel.ToAuthorDto()); // Add () to call the method
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdatesAuthorRequestDto updateDto)
    {
      var authorModel = _context.Author.FirstOrDefault(x => x.AuthorId == id);

      if(authorModel == null)
      {
        return NotFound();
      }

      authorModel.Name = updateDto.Name;
      authorModel.Biography = updateDto.Biography;
      
      if (updateDto.BookIds != null)
      {
        var books = _context.Book
          .Where(b => updateDto.BookIds.Contains(b.BookId))
          .ToList();
    
        authorModel.Books = books;
      }

      _context.SaveChanges();

      return Ok(authorModel.ToAuthorDto());
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
      var authorModel = _context.Author.FirstOrDefault(x => x.AuthorId == id);

      if(authorModel == null)
      {
        return NotFound();
      }

      _context.Author.Remove(authorModel);
      _context.SaveChanges();

      return NoContent();
    } 

  }
}