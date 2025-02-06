using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.User;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using api.Models;

namespace api.Mappers
{
  public static class UserMappers
  {
    public static UserDto ToUserDto(this User userModel)
    {
        return new UserDto
        {
            UserId = userModel.UserId,
            Name = userModel.Name,
            Email = userModel.Email,
            BorrowedBooks = userModel.BorrowedBooks?.Select(b => b.BookId).ToList()
        };
    }
  }
}
