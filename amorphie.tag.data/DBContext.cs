using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace amorphie.tag.data;

public class TagDBContext : DbContext
{
    public DbSet<Tag>? Tags { get; set; }

    public TagDBContext(DbContextOptions options) : base(options) {}

    
    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseNpgsql("Host=localhost:5432;Database=tags;Username=postgres;Password=example");
        => optionsBuilder.UseNpgsql("Host=localhost:5432;Database=tags;Username=postgres;Password=example", b => b.MigrationsAssembly("amorphie.tag"));
    */
    
        
}

public class Tag
{
    [Key]
    public string Name { get; set; } = string.Empty;
    public string? Url { get; set; }
    public int TTL { get; set; }

    [InverseProperty("Owner")]
    public List<TagRelation> Tags { get; set; } = new List<TagRelation>();
}

[PrimaryKey("OwnerName", "TagName") ] 
public class TagRelation
{
    [ForeignKey("Owner")]
    public string OwnerName { get; set; } = string.Empty;
    public Tag? Owner { get; set; }

    [ForeignKey("Tag")]
    public string TagName { get; set; } = string.Empty;
    public Tag? Tag { get; set; }
}