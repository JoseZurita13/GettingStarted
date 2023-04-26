namespace Company.Consumers
{
    using System.Threading.Tasks;
    using MassTransit;
    using Contracts;
    using Microsoft.Extensions.Logging;

    public class HelloConsumer :
        IConsumer<Hello>
    {
        readonly ILogger<HelloConsumer> _logger;

        public HelloConsumer(ILogger<HelloConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Hello> context)
        {
            _logger.LogInformation(message: "Hello {Name}", context.Message.Name);
            return Task.CompletedTask;
        }
    }
}