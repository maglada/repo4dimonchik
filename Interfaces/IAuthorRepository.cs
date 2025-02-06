using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Dtos.Author;
using api.Models;

// example for repository for author(can be used in diff proj)
namespace api.Interfaces
{
    public interface IAuthorRepository
    {
        List<Author> GetAll(); // for normal, not async next are for async
        Task<Author?> GetByIdAsync(int id); // ? can be null. gotta state whihch var type and name
        Task<Author> CreateAsync(Author authorModel); //for models inside controllers.
        Task<Author?> UpdateAsync(int id, UpdatesAuthorRequestDto authorDto); // can be null, needs to return smth so we add dto
        Task<Author?> DeleteAsync(int id); // delete author. can be null

    }
}