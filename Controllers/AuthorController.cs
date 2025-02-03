using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Mappers;
using api.Dtos.Author;

namespace api.Controllers
{
  [Route("api/author")]
  [ApiController]
  public class AuthorController : ControllerBase
  {
    private readonly ApplicationDBContext _context;
    public AuthorController(ApplicationDBContext context)
    {
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

  }
}