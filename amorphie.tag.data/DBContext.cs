using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;

namespace amorphie.tag.data;

 class TagDbContextFactory : IDesignTimeDbContextFactory<TagDBContext>
{
    private readonly IConfiguration _configuration;

    public TagDbContextFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public TagDbContextFactory()
    {

    }
    public TagDBContext CreateDbContext(string[] args)
    {

        var connStr = "Host=localhost:5432;Database=TagDb;Username=postgres;Password=postgres";
        var builder = new DbContextOptionsBuilder<TagDBContext>()
            .EnableSensitiveDataLogging()
            .UseNpgsql(connStr);

        return new TagDBContext(builder.Options);
    }
}

public class TagDBContext : DbContext
{

    public DbSet<Tag>? Tags { get; set; }
    public DbSet<TagRelation>? TagRelations { get; set; }
    public DbSet<View>? Views { get; set; }
    public DbSet<EntityDataSource>? EntityDataSource { get; set; }
    public DbSet<Domain>? Domains { get; set; }
    public DbSet<Entity>? Entities { get; set; }
    public DbSet<EntityData>? EntityData { get; set; }


    public TagDBContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain>()
            .HasMany(b => b.Entities)
            .WithOne(o => o.Domain);

        modelBuilder.Entity<Entity>()
           .HasMany(b => b.EntityData)
           .WithOne(o => o.Entity);

        modelBuilder.Entity<EntityData>()
            .HasMany(b => b.Sources)
            .WithOne(o => o.EntityData);

        modelBuilder.Entity<Domain>().HasIndex(item => item.SearchVector).HasMethod("GIN");
        modelBuilder.Entity<Domain>().Property(item => item.SearchVector).HasComputedColumnSql(FullTextSearchHelper.GetTsVectorComputedColumnSql("english", new[] { "Name" }), true);
        modelBuilder.Entity<Tag>().HasIndex(item => item.SearchVector).HasMethod("GIN");
        modelBuilder.Entity<Tag>().Property(item => item.SearchVector).HasComputedColumnSql(FullTextSearchHelper.GetTsVectorComputedColumnSql("english", new[] { "Name", "Status" }), true);
        modelBuilder.Entity<Entity>().HasIndex(item => item.SearchVector).HasMethod("GIN");
        modelBuilder.Entity<Entity>().Property(item => item.SearchVector).HasComputedColumnSql(FullTextSearchHelper.GetTsVectorComputedColumnSql("english", new[] { "Name", "DomainName" }), true);
        modelBuilder.Entity<EntityData>().HasIndex(item => item.SearchVector).HasMethod("GIN");
        modelBuilder.Entity<EntityData>().Property(item => item.SearchVector).HasComputedColumnSql(FullTextSearchHelper.GetTsVectorComputedColumnSql("english", new[] { "Field", "EntityName" }), true);

    }


}

