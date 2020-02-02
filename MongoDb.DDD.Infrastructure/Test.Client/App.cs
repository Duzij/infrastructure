using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Test.Client
{
    public class App
    {
        private readonly ILogger<App> _logger;
        private readonly BookService bookService;

        public App(ILogger<App> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.bookService = new BookService("mongodb://localhost:27017", "TestDb", "Books");
        }

        public async Task Run()
        {
            Console.WriteLine();
            Console.WriteLine("Hello world!");
            Console.WriteLine();

            bookService.Create(new DAL.Book() { BookName = "Test", Author = "Test author" });

            _logger.LogInformation("sdaf");
            Console.WriteLine();

            await Task.CompletedTask;
        }
    }
}
