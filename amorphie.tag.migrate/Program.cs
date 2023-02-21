using Microsoft.EntityFrameworkCore;
using amorphie.tag.data;
using amorphie.tag.migrate;
using Dapr.Client;

var builder = Host.CreateApplicationBuilder(args);

var daprClient = new DaprClientBuilder().Build();
var secrets = await daprClient.GetSecretAsync("amorphie-secretstore", "amorphie-secretstore");

builder.Services.AddHostedService<Migrate>();
builder.Services.AddDbContext<TagDBContext>(c => c.UseNpgsql(secrets["PostgreSql"], b => b.MigrationsAssembly("amorphie.tag.data")),ServiceLifetime.Singleton);

var app = builder.Build();

app.Run();
