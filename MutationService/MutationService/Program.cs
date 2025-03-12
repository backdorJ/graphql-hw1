using HotChocolate.Execution;
using MutationService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddLogging()
    .AddRouting()
    .AddGraphQLCore()
    .AddGraphQLServer(disableDefaultSecurity: true)
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddMutationConventions()
    .AddFiltering()
    .AddSorting()
    .AddProjections()
    ;
builder.Services.AddMemoryCache();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapGraphQL();

app.RunWithGraphQLCommands(args);
