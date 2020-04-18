using Infrastructure.MongoDB;
using Library.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests
{
    public class AuthorTests
    {
        private MongoDbSettings settings;
        private MongoDbContext context;
        private Repository<Author, string> authorRepo;
        private AuthorFullName authorName;
        private Author insertedAuthor;
        private Repository<Book, string> bookRepo;
        private ServiceProvider serviceProvider;

        [SetUp]
        public void Setup()
        {
            serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILogger<Repository<Author, string>>>();
            settings = new MongoDbSettings(MongoDefaultSettings.ConnectionString, "AuthorTests");
            context = new MongoDbContext(settings);
            authorRepo = new Repository<Author, string>(context, settings, logger);
            authorName = new AuthorFullName("Test", "Author");
            authorRepo.InsertNewAsync(Author.Create(authorName)).GetAwaiter().GetResult();


        }

        [Test]
        public async Task UpdateAuthorName()
        {
            insertedAuthor = context.GetCollection<Author>().AsQueryable().ToList().Find(a => a.authorFullName == authorName);

            var bookLogger = serviceProvider.GetService<ILogger<Repository<Book, string>>>();
            bookRepo = new Repository<Book, string>(context, settings, bookLogger);
            await bookRepo.InsertNewAsync(Book.Create("Test Book", "Test description", (AuthorId)insertedAuthor.Id, authorName));

            await Task.Delay(1000);

            insertedAuthor = context.GetCollection<Author>().AsQueryable().ToList().Find(a => a.authorFullName == authorName);

            Assert.IsTrue(insertedAuthor.Books.Any(a => a.Title == new BookTitle("Test Book")));

            var updatedName = new AuthorFullName("Test", "Author 2");
            await authorRepo.ModifyAsync
                (author => author.UpdateAuthorFullName(updatedName), insertedAuthor.Id);

            insertedAuthor = context.GetCollection<Author>().AsQueryable().ToList().Find(a => a.authorFullName == updatedName);
            Assert.IsTrue(insertedAuthor.Books.Any(a => a.Title == new BookTitle("Test Book")));

            var foundBook = context.GetCollection<Book>().AsQueryable().ToList().Find(a => a.Title == new BookTitle("Test Book"));
            Assert.AreEqual(foundBook.AuthorName, updatedName);
        }
    }
}
