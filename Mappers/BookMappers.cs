using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Book;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using api.Models;

namespace api.Mappers
{
  public static class BookMappers
  {
    public static BookDto ToBookDto(this Book bookModel)
    {
        return new BookDto
        {
            Name = bookModel.Name,
            Genre = bookModel.Genre,
            Year = bookModel.Year,
            AuthorId = bookModel.AuthorId
        };
    }

    public static Book ToBookFromCreateDTO(this CreateBookRequestDto BookDto)
    {
      return new Book
      {
        Name = BookDto.Name,
        Genre = BookDto.Genre,
        Year = BookDto.Year,
        AuthorId = BookDto.AuthorId
      };
    }
  }
}
