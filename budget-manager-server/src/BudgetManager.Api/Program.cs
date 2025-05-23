var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseInfrastructure();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}

await app.RunAsync();