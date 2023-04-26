using Contracts;
using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GettingStarted
{
    public class Worker : BackgroundService
    {
        readonly IBus _bus;
        public Worker(IBus bus)
        {
            _bus = bus;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _bus.Publish(message: new Hello 
                    { 
                        Name = "World"
                    }, stoppingToken); // Task

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
