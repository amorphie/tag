
var builder = WebApplication.CreateBuilder(args);

var client = new DaprClientBuilder().Build();
#pragma warning disable 618
var configurations = await client.GetConfiguration("configstore", new List<string>() { "config-amorphie-tag-db" });
#pragma warning restore 618

builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TagDBContext>
    (options => options.UseNpgsql(configurations.Items["config-amorphie-tag-db"].Value, b => b.MigrationsAssembly("amorphie.tag")));

var app = builder.Build();

app.UseCloudEvents();
app.UseRouting();
app.MapSubscribeHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Logger.LogInformation("Registering Routes");

app.MapDomainEndpoints();
app.MapTagEndpoints();

try
{
    app.Logger.LogInformation("Starting application...");
    app.Run();

}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Aplication is terminated unexpectedly ");
}
