using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Author
{
  public class AuthorDto
  {
    public int AuthorId { get; set; }
    public string Name { get; set; }
    public string Biography { get; set; }
    public ICollection<int> BookIds { get; set; }
  }
}
