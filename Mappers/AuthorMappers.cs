using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Author;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using api.Models;
using api.Dtos.Book;

namespace api.Mappers
{
  public static class AuthorMappers
  {
    public static AuthorDto ToAuthorDto(this Author authorModel)
    {
        return new AuthorDto
        {
            Name = authorModel.Name,
            Biography = authorModel.Biography,
            BookIds = authorModel.Books?.Select(b => b.BookId).ToList()
        };
    }

    public static Author ToAuthorFromCreateDTO(this CreateAuthorRequestDto AuthorDto, Data.ApplicationDBContext _context)
    {
      var books = _context.Book
        .Where(b => AuthorDto.BookIds.Contains(b.BookId))
        .ToList();

      return new Author
      {
        Name = AuthorDto.Name,
        Biography = AuthorDto.Biography,
        Books = books
      };
    }


  }
}