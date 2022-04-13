//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using SteelDoorRecipeAPIOdata.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using SteelDoorRecipeAPIOdata;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ml;
using Microsoft.Extensions.Configuration;

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
    builder.EntitySet<ImagePersonImplementation>("ImagePersonImplementation");
    builder.EntitySet<ImageRecipeImplementation>("ImageRecipeImplementation");
    builder.EntitySet<ImageRecipe>("ImageRecipe");
    builder.EntitySet<IngredientList>("IngredientList");
    builder.EntitySet<Instruction>("Instruction");
    builder.EntitySet<Person>("Person");
    builder.EntitySet<PersonReview>("PersonReview");
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

if (builder.Environment.IsProduction())
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyCorsSettings,
            builder =>
            {
                builder.AllowAnyHeader();
            });
    });
} else
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyCorsSettings,
            builder =>
            {
                builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
            });
    });
}

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>{c.SwaggerDoc("v1", new() { Title = "OData Recipes", Version = "v1" });});

builder.Services.AddDbContext<rrrdbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers().AddOData(
    // odata commands
    opt => opt.AddRouteComponents("v1", GetEdmModel())
        .Filter()
        .Select()
        .Expand()
        .Count()
        .SkipToken()
        .OrderBy()
    );

// we need an http client to talk to the local flask server
// review here https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-6.0
/*
builder.Services.AddHttpClient("Flask", httpClient =>
{
    httpClient.BaseAddress = new Uri("localhost:8086");

    // may need to add headers;
    httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
    httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "rrr-api");
});

// 1. Add Authentication Services
/*
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    
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

app.UseHttpsRedirection();

app.UseCors(MyCorsSettings);

//app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/helloWorld", () => "Hello World!");
app.MapGet("/generate/Trump", async () => { 
    using var client = new HttpClient();
    var flask = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["Flask"];
    return await client.GetStringAsync(flask + "/trump");
});

app.MapPost("/generate/Trump", async (HttpRequest request) => { 
    var flask = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["Flask"];
    using var client = new HttpClient();
    var req = request.Body;
    req.Seek(0, System.IO.SeekOrigin.Begin);
    string rawjson = "";
    try
    {
        rawjson = await new StreamReader(req).ReadToEndAsync();
    } catch (Exception ex)
    {
        Console.WriteLine(ex.InnerException);
    }
        
    var json = JsonSerializer.Deserialize<Story>(rawjson);
    string jsonstring = JsonSerializer.Serialize(json);
    StringContent httpcontent = new StringContent(jsonstring, System.Text.Encoding.UTF8, "text/plain");

    HttpResponseMessage respraw = null;
    try
    {
        respraw = await client.PostAsync(flask + "/trump", httpcontent);
    } catch (Exception e)
    {
        Console.WriteLine(e.InnerException);
    }
    var respcontent = await respraw.Content.ReadAsStringAsync();
    Console.WriteLine(respcontent);
    return respcontent;
});

app.MapControllers();

app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();
    await next(context);
});

app.Run();
