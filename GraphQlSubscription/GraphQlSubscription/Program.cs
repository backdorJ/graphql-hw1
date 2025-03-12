using GraphQlSubscription;
using HotChocolate.Execution;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions();
builder.Services.AddHostedService<SubscriptionBackgroundService>();

var schema = builder.Services.BuildServiceProvider()
    .GetRequiredService<IRequestExecutorResolver>()
    .GetRequestExecutorAsync().Result.Schema;

File.WriteAllText("schema.graphql", schema.ToDocument().ToString());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseWebSockets();
app.UseHttpsRedirection();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);
