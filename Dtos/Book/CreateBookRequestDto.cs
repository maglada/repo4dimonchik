using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Book
{
    public class CreateBookRequestDto
    {
    public string Name { get; set; }
    public int Year { get; set; }
    public string Genre { get; set; }
    public int AuthorId { get; set; }
    }
}