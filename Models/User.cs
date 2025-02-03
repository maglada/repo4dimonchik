using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
  public class User
  {
    public User()
    {
        BorrowedBooks = new List<Book>();
    }
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public ICollection<Book> BorrowedBooks { get; set; }
  }
}
