using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Author
{
    public class CreateAuthorRequestDto
    {
    public string Name { get; set; }
    public string Biography { get; set; }
    public List<int> BookIds { get; set; }
    }
}