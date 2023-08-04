using System.Text.Json.Serialization;
using amorphie.core.security.Extensions;
using amorphie.core.Extension;
using amorphie.tag.Validator;
using FluentValidation;
using static amorphie.tag.data.TagDBContext;
using amorphie.core.Identity;
using amorphie.core.Repository;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);
using var client = new DaprClientBuilder().Build();
await builder.Configuration.AddVaultSecrets("amorphie-secretstore", new string[] { "amorphie-secretstore" });
var postgreSql = builder.Configuration["PostgreSql"];
var postgreDb = builder.Configuration["PostgreDB"];

//var client = new DaprClientBuilder().Build();
#pragma warning disable 618
//var configurations = await client.GetConfiguration("amorphie-config", new List<string>() { "PostgreDB" });

#pragma warning restore 618
var assemblies = new Assembly[] { typeof(DomainValidator).Assembly, typeof(DomainMapper).Assembly };
builder.Services.AddScoped<IValidator<Domain>, DomainValidator>();

builder.Services.AddScoped<IBBTIdentity, FakeIdentity>();
builder.Services.AddScoped(typeof(IBBTRepository<,>), typeof(BBTRepository<,>));
builder.Services.AddValidatorsFromAssemblies(assemblies);
builder.Services.AddAutoMapper(assemblies);
builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole();
builder.Services.AddDaprClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddEnvironmentVariables();

// Console.WriteLine("Environment: " + builder.Environment.EnvironmentName);
// Console.WriteLine("State Store: " + builder.Configuration["STATE_STORE"]);
// Console.WriteLine("Vault PostgreSql: " + postgreSql);


builder.Services.AddDbContext<TagDBContext>
    (options => options.UseNpgsql(postgreSql, b => b.MigrationsAssembly("amorphie.tag.data")));


builder.Services.AddCors(options =>

{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
var app = builder.Build();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<TagDBContext>();

db.Database.Migrate();
app.UseCloudEvents();
app.UseRouting();
app.MapSubscribeHandler();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();



app.UseSwagger();
app.UseSwaggerUI();


app.Logger.LogInformation("Registering Routes");

//app.MapTagEndpoints();
//app.MapEntityEndpoints();

app.AddRoutes();

try
{
    app.Logger.LogInformation("Starting application...");
    app.Run();
}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Aplication is terminated unexpectedly ");
}
