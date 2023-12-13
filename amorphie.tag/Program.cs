using amorphie.core.Extension;
using amorphie.tag.Validator;
using FluentValidation;
using amorphie.core.Identity;
using System.Reflection;
using amorphie.tag.core.Mapper;
using amorphie.core.Swagger;
using amorphie.tag.data;

var builder = WebApplication.CreateBuilder(args);
using var client = new DaprClientBuilder().Build();
await VaultConfigExtension.AddVaultSecrets(builder.Configuration, "amorphie-tag", new string[] { "amorphie-tag" });
var postgreSql = builder.Configuration["PostgreSql"];
var postgreDb = builder.Configuration["PostgreDB"];
//await builder.Configuration.AddVaultSecrets("amorphie-secretstore", new string[] { "amorphie-secretstore" });

var assemblies = new Assembly[] { typeof(DomainValidator).Assembly, typeof(DomainMapper).Assembly };
builder.Services.AddScoped<IValidator<Domain>, DomainValidator>();

builder.Services.AddScoped<IBBTIdentity, FakeIdentity>();
builder.Services.AddValidatorsFromAssemblies(assemblies);
builder.Services.AddAutoMapper(assemblies);
builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole();
builder.Services.AddDaprClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<AddSwaggerParameterFilter>();

});


builder.Configuration.AddEnvironmentVariables();

// Console.WriteLine("Environment: " + builder.Environment.EnvironmentName);
// Console.WriteLine("State Store: " + builder.Configuration["STATE_STORE"]);
// Console.WriteLine("Vault PostgreSql: " + postgreSql);


builder.Services.AddDbContext<TagDBContext>
    (options => options.UseNpgsql(postgreSql, b => b.MigrationsAssembly("amorphie.tag.data")));

builder.Services.AddValidatorsFromAssemblyContaining<DomainValidator>(includeInternalTypes: true);
builder.Services.AddAutoMapper(typeof(Program).Assembly);

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
DbInitializer.Initialize(db);

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
