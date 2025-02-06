using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
  public class Book
  {
    public int BookId { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
    public string Genre { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; }
  }
}
