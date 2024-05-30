using amorphie.core.Extension;
using amorphie.tag.Validator;
using FluentValidation;
using amorphie.core.Identity;
using System.Reflection;
using amorphie.tag.core.Mapper;
using amorphie.core.Swagger;
using amorphie.tag.data;
using amorphie.core.Middleware.Logging;
using Elastic.Apm.NetCoreAll;
using Serilog;

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

builder.Services.AddDaprClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<AddSwaggerParameterFilter>();

});
builder.Logging.ClearProviders();
builder.Host.UseSerilog((_, serviceProvider, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(builder.Configuration);

});


builder.Configuration.AddEnvironmentVariables();

builder.AddSeriLogWithHttpLogging<AmorphieLogEnricher>();


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
builder.Services.AddHealthChecks();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseAllElasticApm(app.Configuration);
}
app.UseLoggingHandlerMiddlewares();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<TagDBContext>();

db.Database.Migrate();
DbInitializer.Initialize(db);
app.MapHealthChecks("/health");
db.Database.Migrate();
app.UseCloudEvents();
app.UseRouting();
app.MapSubscribeHandler();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAllElasticApm(app.Configuration);
app.UseHttpLogging();
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
