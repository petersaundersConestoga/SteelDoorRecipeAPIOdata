using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using SteelDoorRecipeAPIOdata.Models;

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<Recipe>("Recipe");
    builder.EntitySet<Course>("Course");
    return builder.GetEdmModel();
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>{c.SwaggerDoc("v1", new() { Title = "OData Recipes", Version = "v1" });});

builder.Services.AddDbContext<CapstoneRecipeDatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers().AddOData(
    opt => opt.AddRouteComponents("v1", GetEdmModel()).Filter().Select().Expand().Count());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OData SteelDoor v1"));
}

app.MapGet("/helloWorld", () => "Hello World");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
