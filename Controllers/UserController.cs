using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Mappers;

namespace api.Controllers
{
  [Route("api/user")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly ApplicationDBContext _context;
    public UserController(ApplicationDBContext context)
    {
       _context = context;   
    }

    [HttpGet]
    public IActionResult  GetAll()
    {
        var user = _context.User
            .Include(u => u.BorrowedBooks)
            .ToList()
            .Select(s => s.ToUserDto());

        return Ok(user);
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var user = _context.User.Find(id);

        if (user == null)
        {
            return NotFound();
        }
        return Ok(user.ToUserDto());
    }

    [HttpPost("{userId}/borrow/{bookId}")]
    public IActionResult BorrowBook(int userId, int bookId)
    {
        var user = _context.User
            .Include(u => u.BorrowedBooks)
            .FirstOrDefault(u => u.UserId == userId);
        
        if (user == null)
            return NotFound("User not found");

        var book = _context.Book.Find(bookId);
        if (book == null)
            return NotFound("Book not found");

        // Check if user already borrowed this book
        if (!user.BorrowedBooks.Any(b => b.BookId == bookId))
        {
            user.BorrowedBooks.Add(book);
            _context.SaveChanges();
        }

        return Ok(user.ToUserDto());
        }

    [HttpPost("{userId}/return/{bookId}")]
    public IActionResult ReturnBook(int userId, int bookId)
    {
        var user = _context.User
            .Include(u => u.BorrowedBooks)
            .FirstOrDefault(u => u.UserId == userId);
        
        if (user == null)
            return NotFound("User not found");

        var book = user.BorrowedBooks.FirstOrDefault(b => b.BookId == bookId);
        if (book == null)
            return NotFound("Book not found in user's borrowed books");

        user.BorrowedBooks.Remove(book);
        _context.SaveChanges();

        return Ok(user.ToUserDto());
    }

  }
}