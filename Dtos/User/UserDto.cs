using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.User
{
  public class UserDto
  {
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public ICollection<int> BorrowedBooks { get; set; }
  }
}
