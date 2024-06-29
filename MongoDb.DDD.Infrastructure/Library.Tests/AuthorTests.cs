using Infrastructure.Core;
using Infrastructure.MongoDB;
using Library.Domain;
using Library.Domain.DomainAggregates;
using Library.Domain.Id;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using NUnit.Framework;
using Polly;

namespace Library.Tests
{
    public class AuthorTests
    {
        private MongoDbSettings settings;
        private IMongoDbContext context;
        private IRepository<Author, string> authorRepo;
        private AuthorFullName authorName;
        private Author insertedAuthor;
        private IRepository<Book, string> bookRepo;
        private ServiceProvider serviceProvider;

        [SetUp]
        public void Setup()
        {
            settings = new MongoDbSettings(MongoDefaultSettings.ConnectionString, "AuthorTests");

            serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddMongoDbInfrastructure(settings)
                .BuildServiceProvider();
            authorRepo = serviceProvider.GetService<IRepository<Author, string>>();

            authorName = new AuthorFullName("Test", "Author");
            authorRepo.InsertNewAsync(Author.Create(authorName)).GetAwaiter().GetResult();

            context = serviceProvider.GetService<IMongoDbContext>();
        }

        [Test]
        public async Task UpdateAuthorName()
        {
            insertedAuthor = context.GetCollection<Author>().AsQueryable().ToList().Find(a => a.authorFullName == authorName);
            bookRepo = serviceProvider.GetService<IRepository<Book, string>>();
            await bookRepo.InsertNewAsync(Book.Create("Test Book", "Test description", (AuthorId)insertedAuthor.Id, authorName));

            Policy
                .Handle<TestException>()
                .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                .Execute(() =>
                {
                    insertedAuthor = context.GetCollection<Author>().AsQueryable().ToList().Find(a => a.authorFullName == authorName);
                    if (!insertedAuthor.Books.Any(a => a.Title == new BookTitle("Test Book")))
                    {
                        throw new TestException();
                    }
                });

            var updatedName = new AuthorFullName("Test", "Author 2");
            await authorRepo.ModifyAsync
                (author => author.UpdateAuthorFullName(updatedName), insertedAuthor.Id);

            insertedAuthor = context.GetCollection<Author>().AsQueryable().ToList().Find(a => a.authorFullName == updatedName);
            Assert.That(insertedAuthor.Books.Any(a => a.Title == new BookTitle("Test Book")));

            var foundBook = context.GetCollection<Book>().AsQueryable().ToList().Find(a => a.Title == new BookTitle("Test Book"));
            Assert.Equals(foundBook.AuthorName, updatedName);
        }
    }

    internal class TestException : Exception
    {
    }
}
