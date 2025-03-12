using HotChocolate.Subscriptions;

namespace GraphQlSubscription;

public class SubscriptionBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    
    public SubscriptionBackgroundService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
            using var scope = _scopeFactory.CreateScope();
            var sender = scope.ServiceProvider.GetRequiredService<ITopicEventSender>();
                
            await sender.SendAsync("NumberGenerated", new NumberEvent()
            {
                Number = Random.Shared.Next(1000, 9999)
            }, stoppingToken);
        }
    }
}