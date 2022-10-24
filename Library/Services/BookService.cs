using Library.Contracts;
using Library.Data;
using Library.Data.Entities;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _dbContext;

        public BookService(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddBookAsync(AddBookViewModel model)
        {
            var entity = new Book()
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,
                CategoryId = model.CategoryId,
            };

            await _dbContext.Books.AddAsync(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task AddBookToCollectionAsync(int bookId, string userId)
        {
            var user = await _dbContext.Users
                                       .Where(x => x.Id == userId)
                                       .Include(x => x.ApplicationUsersBooks)
                                       .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == bookId);

            if (book == null)
            {
                throw new ArgumentException("Invalid book ID");
            }

            if (!user.ApplicationUsersBooks.Any(x => x.BookId == bookId))
            {
                user.ApplicationUsersBooks.Add(new ApplicationUserBook()
                {
                    ApplicationUserId = userId,
                    ApplicationUser = user,
                    BookId = bookId,
                    Book = book,
                });

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BookViewModel>> GetAllBooksAsync()
        {
            var books = await _dbContext.Books
                        .Include(x => x.Category)
                        .ToListAsync();

            return books.Select(b => new BookViewModel()
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                ImageUrl = b.ImageUrl,
                Rating = b.Rating,
                CategoryId = b.CategoryId,
                Category = b.Category.Name,
            });
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<IEnumerable<BookViewModel>> GetMyBooksAsync(string userId)
        {
            var user = await _dbContext.Users
                            .Where(u => u.Id == userId)
                            .Include(u => u.ApplicationUsersBooks)
                            .ThenInclude(x => x.Book)
                            .ThenInclude(c => c.Category)
                            .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            return user.ApplicationUsersBooks
                       .Select(x => new BookViewModel()
                       {
                           Id = x.BookId,
                           Title = x.Book.Title,
                           Author = x.Book.Author,
                           Description = x.Book.Description,
                           ImageUrl = x.Book.ImageUrl,
                           Rating = x.Book.Rating,
                           Category = x.Book.Category.Name,
                       });
        }

        public async Task RemoveBookFromCollectionAsync(int bookId, string userId)
        {
            var user = await _dbContext.Users
                                       .Where(x => x.Id == userId)
                                       .Include(x => x.ApplicationUsersBooks)
                                       .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var book = user.ApplicationUsersBooks.FirstOrDefault(b => b.BookId == bookId);

            if (book != null)
            {
                user.ApplicationUsersBooks.Remove(book);

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
