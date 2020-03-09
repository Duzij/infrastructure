using Infrastructure.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Library.Tests
{
    public class ValueIncrementedEventHandler : IEventHandler<ValueIncremented>
    {
        private readonly ILogger<ValueIncrementedEventHandler> logger;

        public ValueIncrementedEventHandler(ILogger<ValueIncrementedEventHandler> logger)
        {
            this.logger = logger;
        }

        public Task Handle(ValueIncremented @event)
        {
            logger.LogInformation("Motified");
            return Task.CompletedTask;
        }
    }
}
