using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace amorphie.tag.data;

class TagDbContextFactory : IDesignTimeDbContextFactory<TagDBContext>
{
    public TagDBContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<TagDBContext>();

        var connStr = "Host=localhost:5432;Database=tags;Username=postgres;Password=example";
        builder.UseNpgsql(connStr);
        return new TagDBContext(builder.Options);
    }

}

public class TagDBContext : DbContext
{
    public DbSet<Tag>? Tags { get; set; }
    public DbSet<TagRelation>? TagRelations { get; set; }

    public TagDBContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tag>().HasData(new { Name = "retail-loan" });
        modelBuilder.Entity<Tag>().HasData(new { Name = "idm" });
        modelBuilder.Entity<Tag>().HasData(new { Name = "retail-customer", Url = "http://localhost:3000/cb.customers/@param1?dbid=@param2&loveid=@param3", Ttl = 100 });
        modelBuilder.Entity<Tag>().HasData(new { Name = "corporate-customer", Url = "http://localhost:3000/cb.customers/@param1", Ttl = 10 });
        modelBuilder.Entity<Tag>().HasData(new { Name = "loan-partner", Url = "http://localhost:3000/cb.partner/@partner", Ttl = 10 });

        modelBuilder.Entity<TagRelation>().HasData(new { TagName = "corporate-customer", OwnerName = "idm" });
        modelBuilder.Entity<TagRelation>().HasData(new { TagName = "retail-customer", OwnerName = "idm" });
        modelBuilder.Entity<TagRelation>().HasData(new { TagName = "loan-partner", OwnerName = "idm" });

        modelBuilder.Entity<TagRelation>().HasData(new { TagName = "retail-customer", OwnerName = "retail-loan" });
        modelBuilder.Entity<TagRelation>().HasData(new { TagName = "loan-partner", OwnerName = "retail-loan" });
    }

}

public class Tag
{
    [Key]
    public string Name { get; set; } = string.Empty;
    public string? Url { get; set; }
    public int? Ttl { get; set; }

    [InverseProperty("Owner")]
    public List<TagRelation> Tags { get; set; } = new List<TagRelation>();
}

[PrimaryKey("OwnerName", "TagName")]
public class TagRelation
{
    [ForeignKey("Owner")]
    public string OwnerName { get; set; } = string.Empty;
    public Tag? Owner { get; set; }

    [ForeignKey("Tag")]
    public string TagName { get; set; } = string.Empty;
    public Tag? Tag { get; set; }
}

