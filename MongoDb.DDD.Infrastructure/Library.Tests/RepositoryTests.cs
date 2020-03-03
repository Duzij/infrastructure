using Infrastructure.Core;
using Infrastructure.MongoDb;
using Library.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests
{
    public class RepositoryTests
    {
        private MongoDbContext context;
        private Repository<Counter, string> repository;
        private string id;

        [SetUp]
        public void Setup()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILogger<Repository<Counter, string>>>();
            var settings = new MongoDbSettings(MongoDefaultSettings.ConnectionString, "Tests");
            context = new MongoDbContext(settings);
            repository = new Repository<Counter, string>(context, settings, logger);
            id = Guid.NewGuid().ToString();
            repository.InsertNewAsync(Counter.Create(id,1)).GetAwaiter().GetResult();
        }

        [Test]
        public async Task ModifyAsync()
        {
            var tasks = Enumerable.Range(0, 100).Select(async i =>
            {
                await repository.ModifyAsync(counter => {
                    counter.IncrementValue();
                }, id);
            }).ToList();

            tasks.ForEach(a => a.Wait());
            Assert.IsTrue(tasks.All(t => t.IsCompletedSuccessfully));
        }

        [Test]
        public async Task ModifyWithOptimisticConcurrencyAsync()
        {
            var tasks = Enumerable.Range(0, 100).Select(async i =>
            {
                await repository.ModifyWithOptimisticConcurrencyAsync(counter => {
                    counter.IncrementValue();
                }, id);
            }).ToList();

            tasks.ForEach(a => a.Wait());
            Assert.IsTrue(tasks.All(t => t.IsCompletedSuccessfully));
        }

    }

    public class Counter : Entity<string>
    {
        public int CounterValue { get; private set; }

        public static Counter Create(string idValue, int value)
        {
            return new Counter(new CounterId(idValue), value);
        }

        public void IncrementValue()
        {
            this.CounterValue++;
        }

        public Counter(CounterId counterId, int value)
        {
            this.Id = counterId;
            this.CounterValue = value;
        }

        public override void CheckState()
        {
            throw new NotImplementedException();
        }

    }

    public class CounterId : EntityId<string>
    {
        public CounterId(string value) : base(value)
        {
        }
    }
}
