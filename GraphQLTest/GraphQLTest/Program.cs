using BLL;
using BLL.Queries;
using Dal;
using HotChocolate.Execution;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<PostgresConfig>(builder.Configuration.GetSection(nameof(PostgresConfig)));
var postgresConfig = builder.Configuration.GetSection(nameof(PostgresConfig)).Get<PostgresConfig>()!;
builder.Services.AddSingleton(postgresConfig);

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddPagingArguments()
    .AddFiltering()
    .AddSorting();

builder.Services.AddDal(postgresConfig);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGraphQL();

app.RunWithGraphQLCommands(args);
