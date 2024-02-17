using Microsoft.OpenApi.Models;
using Rainfall.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "Rainfall Api",
		Version = "1.0",
		Contact = new OpenApiContact { Name = "Sorted ", Url = new Uri("https://www.sorted.com") },
		Description = "An API which provides rainfall reading data"
	});

	c.AddServer(new OpenApiServer { Url = "http://localhost:3000", Description = "Rainfall Api" });
});

builder.Services.AddHttpClient<IRainfallRepository, RainfallRepository>();

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
