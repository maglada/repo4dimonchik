using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Author;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

// example implementation of repo.  IT IS NOT USED IN CODE OF ACTUAL API BECAUSE I AM DUMB AF NGL
namespace api.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDBContext _context;
        public AuthorRepository(ApplicationDBContext context)
        {
            _context = context; 
        }

        public async Task<Author> CreateAsync(Author authorModel)
        {
            await _context.Author.AddAsync(authorModel);
            await _context.SaveChangesAsync();
            return authorModel;
        }

        public async Task<Author?> DeleteAsync(int id)
        {
            var authorModel = await _context.Author.FirstOrDefaultAsync(x => x.AuthorId == id);

            if(authorModel == null)
            {
                return null;
            }

            _context.Author.Remove(authorModel);
            await _context.SaveChangesAsync();
            return authorModel;
        }

        public List<Author> GetAll()
        {
            return _context.Author.ToList();   // usage eg _authorRepo.GetAll();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Author.FindAsync(id);
        }

        public async Task<Author?> UpdateAsync(int id, UpdatesAuthorRequestDto authorDto)
        {
            var existingAuthor = await _context.Author.FirstOrDefaultAsync(x => x.AuthorId == id);

            if(existingAuthor == null)
            {
                return null;
            }
            existingAuthor.Name = authorDto.Name;
            existingAuthor.Biography = authorDto.Biography;

            await _context.SaveChangesAsync();
            return existingAuthor;
        }
    }
}