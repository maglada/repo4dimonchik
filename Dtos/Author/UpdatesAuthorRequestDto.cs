using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Author
{
    public class UpdatesAuthorRequestDto
    {
    public string Name { get; set; }
    public string Biography { get; set; }
    public ICollection<int> BookIds{ get; set; }
    }
}