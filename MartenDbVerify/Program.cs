using Marten;
using MartenDbVerify;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=yourpassword;";
builder.Services.AddMarten(options =>
{
    options.Connection(connectionString);
    options.Events.StreamIdentity = Marten.Events.StreamIdentity.AsGuid;
});
builder.Services.AddScoped<ProcessCommandHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
