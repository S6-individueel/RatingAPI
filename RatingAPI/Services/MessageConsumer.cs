using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RatingAPI.Models;
using RatingAPI.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RatingAPI.Services
{
    public class MessageConsumer : BackgroundService, IHostedService
    {
        private readonly ISubscriptionClient _subscriptionClient;

        private readonly IServiceScopeFactory _scopeFactory;

        public MessageConsumer(ISubscriptionClient subscriptionClient, IServiceScopeFactory scopeFactory)
        {
            _subscriptionClient = subscriptionClient;
            _scopeFactory = scopeFactory;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var scope = _scopeFactory.CreateScope(); // this will use `IServiceScopeFactory` internally
            var scoped = scope.ServiceProvider.GetRequiredService<RatingsContext>();

            _subscriptionClient.RegisterMessageHandler((message, token) =>
            {
                User userDeleted = JsonSerializer.Deserialize<User>(Encoding.UTF8.GetString(message.Body));
                foreach (var rating in scoped.Ratings.Where(r => r.UserId == userDeleted.Id))
                {
                    scoped.Remove(rating);
                }
                scoped.SaveChanges();
                return _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
            }, new MessageHandlerOptions(args => Task.CompletedTask)
            {
                AutoComplete = false,
                MaxConcurrentCalls = 1
            });
            return Task.CompletedTask;
        }
    }
}
