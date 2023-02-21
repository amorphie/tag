using Microsoft.EntityFrameworkCore;
using amorphie.tag.data;

namespace amorphie.tag.migrate
{
    public class Migrate : BackgroundService
    {
        private readonly ILogger<Migrate> _logger;
        private readonly IHostApplicationLifetime _host;
        private readonly TagDBContext _databaseContext;
        public Migrate(ILogger<Migrate> logger,IHostApplicationLifetime host,TagDBContext databaseContext)
        {
            _logger = logger;
            _host = host;
            _databaseContext = databaseContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _databaseContext.Database.MigrateAsync();
        }
    }
}