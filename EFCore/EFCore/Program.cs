using BookLibrary;
using Microsoft.EntityFrameworkCore;
using System.Linq;

static IEnumerable<Author> CreateFakeData()
{
    var authors = new List<Author>
            {
                new Author
                {
                    Name = "Jane Austen", Books = new List<Book>
                    {
                        new Book {Title = "Emma", YearOfPublication = 1815},
                        new Book {Title = "Persuasion", YearOfPublication = 1818},
                        new Book {Title = "Mansfield Park", YearOfPublication = 1814}
                    }
                },
                new Author
                {
                    Name = "Ian Fleming", Books = new List<Book>
                    {
                        new Book {Title = "Dr No", YearOfPublication = 1958},
                        new Book {Title = "Goldfinger", YearOfPublication = 1959},
                        new Book {Title = "From Russia with Love", YearOfPublication = 1957}
                    }
                }
            };

    return authors;
}

var options = new DbContextOptionsBuilder<BooksContext>()
    .UseSqlite("Filename=../../../MyLocalLibrary.db")
    .Options;

using var db = new BooksContext(options);
db.Database.EnsureCreated();

//var authors = CreateFakeData();

//db.Authors.AddRange(authors);
//db.SaveChanges();

var recentBooks =  from b in db.Books
                   where b.YearOfPublication > 1900
                   select b;
foreach (var book in recentBooks.Include(a => a.Author))
{
    Console.WriteLine($"{book} is by {book.Author}");
    
}