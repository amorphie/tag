﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace amorphie.tag.data;

class TagDbContextFactory : IDesignTimeDbContextFactory<TagDBContext>
{

    //lazy loading true
    //lazy loading false, eğer alt bileşenleri getirmek istiyorsak include kullanmamız lazım,eager loading
    private readonly IConfiguration _configuration;
    public TagDbContextFactory()
    {

    }
    public TagDbContextFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public TagDBContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<TagDBContext>();
        // var test = _configuration["STATE_STORE"];
        // System.Console.WriteLine("Test: " + test);


        var connStr = "Host=localhost:5432;Database=tags;Username=postgres;Password=postgres";
        builder.UseNpgsql(connStr);
        return new TagDBContext(builder.Options);
    }
}

public class TagDBContext : DbContext
{
    public DbSet<Tag>? Tags { get; set; }
    public DbSet<TagRelation>? TagRelations { get; set; }
    public DbSet<View>? Views { get; set; }
    public DbSet<Domain>? Domains { get; set; }
    public DbSet<Entity>? Entities { get; set; }
    public DbSet<EntityData>? EntityData { get; set; }
    public DbSet<EntityDataSource>? EntityDataSource { get; set; }

    public TagDBContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain>()
            .HasMany(b => b.Entities)
            .WithOne(o => o.Domain);

        modelBuilder.Entity<Entity>()
           .HasMany(b => b.Data)
           .WithOne(o => o.Entity);

        modelBuilder.Entity<EntityData>()
            .HasMany(b => b.Sources)
            .WithOne(o => o.EntityData);

        // modelBuilder.Entity<Tag>().HasData(new { Name = "retail-loan" });
        // modelBuilder.Entity<Tag>().HasData(new { Name = "idm" });
        // modelBuilder.Entity<Tag>().HasData(new { Name = "retail-customer", Url = "http://localhost:3001/cb.customers?reference=@reference", Ttl = 5, CreatedDate = System.DateTime.Now.ToUniversalTime() });
        // modelBuilder.Entity<Tag>().HasData(new { Name = "corporate-customer", Url = "http://localhost:3001/cb.customers?reference=@reference", Ttl = 10 });
        // modelBuilder.Entity<Tag>().HasData(new { Name = "loan-partner", Url = "http://localhost:3001/cb.partner/@reference", Ttl = 10 });
        // modelBuilder.Entity<Tag>().HasData(new { Name = "loan-partner-staff", Url = "http://localhost:3001/cb.partner/@partner/staff/@reference", Ttl = 10 });
        // modelBuilder.Entity<Tag>().HasData(new { Name = "burgan-staff", Url = "http://localhost:3001/cb.staff/@reference", Ttl = 10 });
        // modelBuilder.Entity<Tag>().HasData(new { Name = "burgan-bank-turkey", Url = "http://localhost:3001/cb.bankInfo", Ttl = 10 });

        // modelBuilder.Entity<TagRelation>().HasData(new { TagName = "corporate-customer", OwnerName = "idm" });
        // modelBuilder.Entity<TagRelation>().HasData(new { TagName = "retail-customer", OwnerName = "idm" });
        // modelBuilder.Entity<TagRelation>().HasData(new { TagName = "loan-partner", OwnerName = "idm" });
        // modelBuilder.Entity<TagRelation>().HasData(new { TagName = "loan-partner-staff", OwnerName = "idm" });
        // modelBuilder.Entity<TagRelation>().HasData(new { TagName = "burgan-staff", OwnerName = "idm" });
        // modelBuilder.Entity<TagRelation>().HasData(new { TagName = "burgan-bank-turkey", OwnerName = "idm" });

        // modelBuilder.Entity<View>().HasData(new { TagName = "retail-customer", ViewTemplateName = "retail-customer-mini-html", Type = ViewType.Html });
        // modelBuilder.Entity<View>().HasData(new { TagName = "retail-customer", ViewTemplateName = "retail-customer-flutter", Type = ViewType.Flutter });

        // modelBuilder.Entity<TagRelation>().HasData(new { TagName = "retail-customer", OwnerName = "retail-loan" });
        // modelBuilder.Entity<TagRelation>().HasData(new { TagName = "loan-partner", OwnerName = "retail-loan" });

        modelBuilder.Entity<Domain>().HasData(new { Name = "idm", Description = "Identity Management Platform" });

        modelBuilder.Entity<Entity>().HasData(new { Name = "user", Description = "User repository", DomainName = "idm" });
        modelBuilder.Entity<Entity>().HasData(new { Name = "scope", Description = "Scope repository", DomainName = "idm" });

        modelBuilder.Entity<EntityData>().HasData(new { Id = new Guid("107f4644-57cd-46ff-80de-004c6cd44704"), EntityName = "user", Field = "firstname" });
        modelBuilder.Entity<EntityData>().HasData(new { Id = new Guid("207f4644-57cd-46ff-80de-004c6cd44704"), EntityName = "user", Field = "lastname" });
        modelBuilder.Entity<EntityData>().HasData(new { Id = new Guid("307f4644-57cd-46ff-80de-004c6cd44704"), EntityName = "scope", Field = "title" });

        modelBuilder.Entity<EntityDataSource>().HasData(new { EntityDataId = new Guid("107f4644-57cd-46ff-80de-004c6cd44704"), Order = 1, TagName = "burgan-staff", DataPath = "$.firstname" });
        modelBuilder.Entity<EntityDataSource>().HasData(new { EntityDataId = new Guid("107f4644-57cd-46ff-80de-004c6cd44704"), Order = 2, TagName = "loan-partner-staff", DataPath = "$.partner-staff.fullname" });
        modelBuilder.Entity<EntityDataSource>().HasData(new { EntityDataId = new Guid("107f4644-57cd-46ff-80de-004c6cd44704"), Order = 3, TagName = "retail-customer", DataPath = "$.firstname" });

        modelBuilder.Entity<EntityDataSource>().HasData(new { EntityDataId = new Guid("207f4644-57cd-46ff-80de-004c6cd44704"), Order = 1, TagName = "burgan-staff", DataPath = "$.lastname" });
        modelBuilder.Entity<EntityDataSource>().HasData(new { EntityDataId = new Guid("207f4644-57cd-46ff-80de-004c6cd44704"), Order = 2, TagName = "loan-partner-staff", DataPath = "$.partner-staff.fullname" });
        modelBuilder.Entity<EntityDataSource>().HasData(new { EntityDataId = new Guid("207f4644-57cd-46ff-80de-004c6cd44704"), Order = 3, TagName = "retail-customer", DataPath = "$.lastname" });

        modelBuilder.Entity<EntityDataSource>().HasData(new { EntityDataId = new Guid("307f4644-57cd-46ff-80de-004c6cd44704"), Order = 1, TagName = "burgan-staff", DataPath = "$.firstname" });
        modelBuilder.Entity<EntityDataSource>().HasData(new { EntityDataId = new Guid("307f4644-57cd-46ff-80de-004c6cd44704"), Order = 2, TagName = "loan-partner-staff", DataPath = "$.partner-staff.fullname" });
    }

}

// public class Tag
// {
//     [Key]
//     public string Name { get; set; } = string.Empty;
//     public string? Url { get; set; }
//     public int? Ttl { get; set; }
//     public DateTime? CreatedDate { get; set; }
//     [JsonIgnore]
//     public DateTime? LastModifiedDate { get; set; }
//     [InverseProperty("Owner")]
//     public List<TagRelation> TagsRelations { get; set; } = new List<TagRelation>();
//     public List<View> Views { get; set; } = new List<View>();

// }

// [PrimaryKey("OwnerName", "TagName")]
// public class TagRelation
// {
//     [ForeignKey("Owner")]
//     public string OwnerName { get; set; } = string.Empty;
//     public Tag? Owner { get; set; }

//     [ForeignKey("Tag")]
//     public string TagName { get; set; } = string.Empty;
//     public Tag? Tag { get; set; }
// }

// [PrimaryKey("TagName", "ViewTemplateName")]
// public class View
// {
//     [ForeignKey("Tag")]
//     public string TagName { get; set; } = string.Empty;
//     public Tag? Tag { get; set; }

//     public string ViewTemplateName { get; set; } = string.Empty;
//     public ViewType Type { get; set; }
// }

// public enum ViewType
// {
//     Html,
//     MobileHtml,
//     Flutter,
//     NativeIOS,
//     NativeAndroid,
//     Json
// }

// public class Domain
// {
//     [Key]
//     public string Name { get; set; } = string.Empty;
//     public string Description { get; set; } = string.Empty;
//     public List<Entity> Entities = new List<Entity>();
// }

// public class Entity
// {
//     [Key]
//     public string Name { get; set; } = string.Empty;
//     public string? Description { get; set; } = string.Empty;
//     public DateTime? CreatedDate { get; set; }
//     [JsonIgnore]
//     public DateTime? LastModifiedDate { get; set; }
//     [ForeignKey("Domain")]
//     public string DomainName { get; set; } = string.Empty;
//     public Domain? Domain { get; set; }
//     public List<EntityData> Data = new List<EntityData>();
// }

// public class EntityData
// {
//     [Key]
//     public Guid Id { get; set; }

//     public Entity? Entity { get; set; }
//     public String EntityName { get; set; } = string.Empty;
//     public DateTime? CreatedDate { get; set; }
//     [JsonIgnore]
//     public DateTime? LastModifiedDate { get; set; }
//     public string Field { get; set; } = string.Empty;
//     public int? Ttl { get; set; }
//     public List<EntityDataSource> Sources = new List<EntityDataSource>();
// }

// [PrimaryKey("EntityDataId", "Order")]
// public class EntityDataSource
// {
//     [ForeignKey("EntityData")]
//     public Guid EntityDataId { get; set; }
//     public EntityData? EntityData { get; set; }

//     public int Order { get; set; }
//     public string TagName { get; set; } = string.Empty;
//     public Tag? Tag { get; set; }
//     public string DataPath { get; set; } = string.Empty;
// }

public class RenderRequestDefinition
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [JsonPropertyNameAttribute("name")]
    public string? Name { get; set; }

    [JsonPropertyNameAttribute("render-id")]
    public Guid RenderID { get; set; }

    [JsonPropertyNameAttribute("render-data")]
    public dynamic? RenderData { get; set; }

    [JsonPropertyNameAttribute("render-data-for-log")]
    public dynamic? RenderDataForLog { get; set; }
    [JsonPropertyNameAttribute("semantic-version")]
    public string? SemVer { get; set; }
    [JsonPropertyNameAttribute("process-name")]
    public string? ProcessName { get; set; }

    [JsonPropertyNameAttribute("item-id")]
    public string? ItemId { get; set; }

    [JsonPropertyNameAttribute("action")]
    public string? Action { get; set; }
    [JsonPropertyNameAttribute("identity")]
    public string? Identity { get; set; }
    [JsonPropertyNameAttribute("customer")]
    public string? Customer { get; set; }
}
