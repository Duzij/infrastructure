using Infrastructure.MongoDB;
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
        private Repository<TestCounter, string> repository;
        private CounterId id;

        [SetUp]
        public void Setup()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILogger<Repository<TestCounter, string>>>();
            var settings = new MongoDbSettings(MongoDefaultSettings.ConnectionString, "Tests");
            context = new MongoDbContext(settings);
            repository = new Repository<TestCounter, string>(context, settings, logger);
            id = TypedId.GetNewId<CounterId>();
            repository.InsertNewAsync(TestCounter.Create(id.Value,0)).GetAwaiter().GetResult();
        }

        [Test]
        public async Task ReplaceAsync()
        {
            int value = 0;

            var tasks = Enumerable.Range(0, 100).Select(async i =>
            {
                var counter =  await repository.GetByIdAsync(id);
                counter.UpdateCounterWithValue(value++);
                await repository.ReplaceAsync(counter);
            }).ToList();


            tasks.ForEach(a => a.Wait());

            var counter = await repository.GetByIdAsync(id);
            counter.UpdateCounterWithValue(value++);
            await repository.ReplaceAsync(counter);

            Assert.IsTrue(tasks.All(t => t.IsCompletedSuccessfully));
        }

        [Test]
        public async Task ModifyAsync()
        {
            var tasks = Enumerable.Range(0, 1500).Select(async i =>
            {
                await repository.ModifyAsync(counter => {
                    counter.IncrementValue();
                }, id);
            }).ToList();

            tasks.ForEach(a => a.Wait());
            Assert.IsTrue(tasks.All(t => t.IsCompletedSuccessfully));
        }

        [Test]
        public async Task ModifyWithEventAsync()
        {
            var tasks = Enumerable.Range(0, 1000).Select(async i =>
            {
                await repository.ModifyAsync(counter => {
                    counter.IncrementValueWithEvent();
                }, id);
            }).ToList();

            tasks.ForEach(a => a.Wait());
            Assert.IsTrue(tasks.All(t => t.IsCompletedSuccessfully));
        }

    }
}
