using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using SteelDoorRecipeAPIOdata.Models;
using System.IdentityModel.Tokens.Jwt;

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<AccountManager>("AccountManager");
    builder.EntitySet<AccountType>("AccountType");
    builder.EntitySet<Course>("Course");
    builder.EntitySet<CourseList>("CourseList");
    builder.EntitySet<Cuisine>("Cuisine");
    builder.EntitySet<Diet>("Diet");
    builder.EntitySet<DietList>("DietList");
    builder.EntitySet<IngredientList>("IngredientList");
    builder.EntitySet<Instruction>("Instruction");
    builder.EntitySet<Person>("Person");
    //builder.EntitySet<PersonReview>("PersonReview");
    builder.EntitySet<PublishState>("PublishState");
    builder.EntitySet<Recipe>("Recipe");
    builder.EntitySet<Review>("Review");
    builder.EntitySet<Season>("Season");
    builder.EntitySet<SeasonList>("SeasonList");
    builder.EntitySet<Timing>("Timing");
    builder.EntitySet<Unit>("Unit");
    return builder.GetEdmModel();
}

var MyCorsSettings = "_MyCorsSettings";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyCorsSettings,
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader();
        });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>{c.SwaggerDoc("v1", new() { Title = "OData Recipes", Version = "v1" });});

builder.Services.AddDbContext<CapstoneRecipeDatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers().AddOData(
    opt => opt.AddRouteComponents("v1", GetEdmModel()).Filter().Select().Expand().Count());

// 1. Add Authentication Services
/*
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = "https://dev-74yzy8-6.us.auth0.com/";
    options.Audience = "https://rrr-login-api/";
});
*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OData SteelDoor v1"));
    //app.UseDeveloperExceptionPage();
}

app.MapGet("/helloWorld", () => "Hello World");

app.UseHttpsRedirection();

app.UseCors(MyCorsSettings);

app.UseAuthorization();
//app.UseAuthentication();

app.MapControllers();

app.Run();
