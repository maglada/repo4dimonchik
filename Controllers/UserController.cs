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
    public async Task<IActionResult> BorrowBook(int userId, int bookId)
    {
        var user = await _context.User
            .Include(u => u.BorrowedBooks)
            .FirstOrDefaultAsync(u => u.UserId == userId);
    
        if (user == null)
            return NotFound("User not found");

        var book = await _context.Book.FindAsync(bookId);
        if (book == null)
            return NotFound("Book not found");

        var isBookBorrowed = await _context.User
            .AnyAsync(u => u.BorrowedBooks.Any(b => b.BookId == bookId));

        if (isBookBorrowed)
            return BadRequest("Book is already borrowed");

        if (!user.BorrowedBooks.Any(b => b.BookId == bookId))
        {
            user.BorrowedBooks.Add(book);
            await _context.SaveChangesAsync();
        }

        return Ok(user.ToUserDto());
    }

    [HttpPost("{userId}/return/{bookId}")]
    public async Task<IActionResult> ReturnBook(int userId, int bookId)
    {
        var user = await _context.User
            .Include(u => u.BorrowedBooks)
            .FirstOrDefaultAsync(u => u.UserId == userId);
    
        if (user == null)
            return NotFound("User not found");

        var book = user.BorrowedBooks.FirstOrDefault(b => b.BookId == bookId);
        if (book == null)
            return NotFound("Book not found in user's borrowed books");

        user.BorrowedBooks.Remove(book);
        await _context.SaveChangesAsync();

        return Ok(user.ToUserDto());
    }

  }
}