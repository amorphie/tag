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
        builder.EnableSensitiveDataLogging();
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
           .HasMany(b => b.Data)
           .WithOne(o => o.Entity);

        modelBuilder.Entity<EntityData>()
            .HasMany(b => b.Sources)
            .WithOne(o => o.EntityData);

        SeedDataGenerator.GenerateSeedData(modelBuilder);
    }
    public static class SeedDataGenerator
    {
        public static void GenerateSeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = new Guid("107f4644-57cd-46ff-80de-004c6cd44778"),
                Name = "retail-loan"
            });
            modelBuilder.Entity<Tag>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                Name = "idm"
            });
            modelBuilder.Entity<Tag>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                Name = "retail-customer",
                Url = "http://localhost:3000/cb.customers?reference=@reference",
                Ttl = 5
            });
            modelBuilder.Entity<Tag>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                Name = "corporate-customer",
                Url = "http://localhost:3000/cb.customers?reference=@reference",
                Ttl = 10
            });
            modelBuilder.Entity<Tag>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                Name = "loan-partner",
                Url = "http://localhost:3000/cb.partner/@reference",
                Ttl = 10
            });
            modelBuilder.Entity<Tag>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                Name = "loan-partner-staff",
                Url = "http://localhost:3000/cb.partner/@partner/staff/@reference",
                Ttl = 10
            });
            modelBuilder.Entity<Tag>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                Name = "burgan-staff",
                Url = "http://localhost:3000/cb.staff/@reference",
                Ttl = 10
            });
            modelBuilder.Entity<Tag>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                Name = "burgan-bank-turkey",
                Url = "http://localhost:3000/cb.bankInfo",
                Ttl = 10
            });

            modelBuilder.Entity<TagRelation>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                Id = Guid.NewGuid(),
                TagName = "corporate-customer",
                OwnerName = "idm"
            });
            modelBuilder.Entity<TagRelation>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                TagName = "retail-customer",
                OwnerName = "idm"
            });
            modelBuilder.Entity<TagRelation>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                TagName = "loan-partner",
                OwnerName = "idm"
            });
            modelBuilder.Entity<TagRelation>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                TagName = "loan-partner-staff",
                OwnerName = "idm"
            });
            modelBuilder.Entity<TagRelation>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                TagName = "burgan-staff",
                OwnerName = "idm"
            });
            modelBuilder.Entity<TagRelation>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                TagName = "burgan-bank-turkey",
                OwnerName = "idm"
            });

            modelBuilder.Entity<View>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                TagName = "retail-customer",
                ViewTemplateName = "retail-customer-mini-html",
                Type = ViewType.MobileHtml
            });
            modelBuilder.Entity<View>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                TagName = "retail-customer",
                ViewTemplateName = "retail-customer-flutter",
                Type = ViewType.Flutter
            });

            modelBuilder.Entity<TagRelation>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44778"),
                TagName = "retail-customer",
                OwnerName = "retail-loan"
            });
            modelBuilder.Entity<TagRelation>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44778"),
                TagName = "loan-partner",
                OwnerName = "retail-loan"
            });

            modelBuilder.Entity<Domain>().HasData(new { ModifiedBy = Guid.NewGuid(), ModifiedAt = DateTime.UtcNow, CreatedBy = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, Id = new Guid("107f4644-57cd-46ff-80de-004c6cd44712"), Name = "idm", Description = "Identity Management Platform" });

            modelBuilder.Entity<Entity>().HasData(new { ModifiedBy = Guid.NewGuid(), ModifiedAt = DateTime.UtcNow, CreatedBy = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, Id = new Guid("107f4644-57cd-46ff-80de-004c6cd44715"), DomainId = new Guid("107f4644-57cd-46ff-80de-004c6cd44712"), Name = "user", Description = "User repository", DomainName = "idm" });
            modelBuilder.Entity<Entity>().HasData(new { ModifiedBy = Guid.NewGuid(), ModifiedAt = DateTime.UtcNow, CreatedBy = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, Id = new Guid("107f4644-57cd-46ff-80de-004c6cd44710"), DomainId = new Guid("107f4644-57cd-46ff-80de-004c6cd44712"), Name = "scope", Description = "Scope repository", DomainName = "idm" });

            modelBuilder.Entity<EntityData>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                Id = new Guid("107f4644-57cd-46ff-80de-004c6cd44734"),
                EntityId = new Guid("107f4644-57cd-46ff-80de-004c6cd44715"),
                EntityName = "user",
                Field = "firstname"
            });
            modelBuilder.Entity<EntityData>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                Id = new Guid("107f4644-57cd-46ff-80de-004c6cd44722"),
                EntityId = new Guid("107f4644-57cd-46ff-80de-004c6cd44715"),
                EntityName = "user",
                Field = "lastname"
            });
            modelBuilder.Entity<EntityData>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                Id = new Guid("107f4644-57cd-46ff-80de-004c6cd44755"),
                EntityId = new Guid("107f4644-57cd-46ff-80de-004c6cd44710"),
                EntityName = "scope",
                Field = "title"
            });

            modelBuilder.Entity<EntityDataSource>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                EntityDataId = new Guid("107f4644-57cd-46ff-80de-004c6cd44734"),
                Order = 1,
                TagName = "burgan-staff",
                DataPath = "$.firstname"
            });
            modelBuilder.Entity<EntityDataSource>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                EntityDataId = new Guid("107f4644-57cd-46ff-80de-004c6cd44734"),
                Order = 2,
                TagName = "loan-partner-staff",
                DataPath = "$.partner-staff.fullname"
            });
            modelBuilder.Entity<EntityDataSource>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                EntityDataId = new Guid("107f4644-57cd-46ff-80de-004c6cd44734"),
                Order = 3,
                TagName = "retail-customer",
                DataPath = "$.firstname"
            });

            modelBuilder.Entity<EntityDataSource>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                EntityDataId = new Guid("107f4644-57cd-46ff-80de-004c6cd44734"),
                Order = 1,
                TagName = "burgan-staff",
                DataPath = "$.lastname"
            });
            modelBuilder.Entity<EntityDataSource>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                EntityDataId = new Guid("107f4644-57cd-46ff-80de-004c6cd44734"),
                Order = 2,
                TagName = "loan-partner-staff",
                DataPath = "$.partner-staff.fullname"
            });
            modelBuilder.Entity<EntityDataSource>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                EntityDataId = new Guid("107f4644-57cd-46ff-80de-004c6cd44734"),
                Order = 3,
                TagName = "retail-customer",
                DataPath = "$.lastname"
            });

            modelBuilder.Entity<EntityDataSource>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                EntityDataId = new Guid("107f4644-57cd-46ff-80de-004c6cd44734"),
                Order = 1,
                TagName = "burgan-staff",
                DataPath = "$.firstname"
            });
            modelBuilder.Entity<EntityDataSource>().HasData(new
            {
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                EntityDataId = new Guid("107f4644-57cd-46ff-80de-004c6cd44734"),
                Order = 2,
                TagName = "loan-partner-staff",
                DataPath = "$.partner-staff.fullname"
            });
        }

    }
}

