using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AspNetCoreRateLimit;
using MongoDB.Driver;
using MongoDB.Bson;
using ProductCatalogService.Infrastructure.Data;
using ProductCatalogService.Core.Domain.Entities;
using Asp.Versioning;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ProductContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductContext")));


builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(
                   builder.Configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());


builder.Services.AddControllers();

// Add API versioning.
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

// Implement RateLimiting for Products.
builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.EnableEndpointRateLimiting = true;
    options.StackBlockedRequests = false;
    options.HttpStatusCode = 429;
    options.RealIpHeader = "X-Real-IP";
    options.ClientIdHeader = "X-ClientId";
    options.GeneralRules =
    [
        new RateLimitRule
        {
            Endpoint = "*/getAll",
            Period = "10s",
            Limit = 3
        }
    ];       
});
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.AddInMemoryRateLimiting();
//builder.Services.AddScoped<IProductService, ProductService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

// Next is Mongo.
const string connectionUri = "mongodb+srv://tmproduct:F17n6fppU1iGCaSy@cluster0.asw8qts.mongodb.net/?retryWrites=true&w=majority";
var settings = MongoClientSettings.FromConnectionString(connectionUri);

// Set the ServerApi field of the settings object to set the version of the Stable API on the client
settings.ServerApi = new ServerApi(ServerApiVersion.V1);
// Create a new client and connect to the server
var client = new MongoClient(settings);
// Send a ping to confirm a successful connection
try
{
    var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
    Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");

    var playlistCollection = client.GetDatabase("sample_mflix").GetCollection<Product2>("product2");

    List<string> movieList = ["1234"];

    playlistCollection.InsertOne(new Product2("nraboy", movieList));

    FilterDefinition<Product2> filter = Builders<Product2>.Filter.Eq("Title", "nraboy");


    UpdateDefinition<Product2> update = Builders<Product2>.Update.AddToSet<string>("Features", "5678");

    playlistCollection.UpdateOne(filter, update);

    List<Product2> results = playlistCollection.Find(filter).ToList();


    String names = "";
    foreach (Product2 res in results)
    {
        names = names + res.Features;
        Console.WriteLine(string.Join(", ", res.Features));
    }

    List<string> databases = client.ListDatabaseNames().ToList();

    foreach (string database in databases)
    {
        Console.WriteLine(database);
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}
// Mongo End.


app.Run();