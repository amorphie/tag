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
    public DbSet<Domain>? Domains { get; set; }
    public DbSet<Entity>? Entities { get; set; }
    public DbSet<EntityData>? EntityData { get; set; }
    public DbSet<EntityDataSource>? EntityDataSource { get; set; }

    public TagDBContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var domains = new List<Domain>
        {
            new Domain { Id = Guid.NewGuid(), Name = "Domain 1", Description = "Domain 1 Description" },
            new Domain { Id = Guid.NewGuid(), Name = "Domain 2", Description = "Domain 2 Description" }
        };
        modelBuilder.Entity<Domain>().HasData(domains);

        // Entity verileri
        var entities = new List<Entity>
        {
            new Entity { Id = Guid.NewGuid(), Name = "Entity 1", Description = "Entity 1 Description",DomainId=domains[0].Id },
            new Entity { Id = Guid.NewGuid(), Name = "Entity 2", Description = "Entity 2 Description",DomainId=domains[1].Id }
        };
        modelBuilder.Entity<Entity>().HasData(entities);

        // EntityData verileri
        var entityData = new List<EntityData>
        {
            new EntityData { Id = Guid.NewGuid(), EntityId = entities[0].Id, EntityName = entities[0].Name, Field = "Field 1", Ttl = 10},
            new EntityData { Id = Guid.NewGuid(), EntityId = entities[1].Id, EntityName = entities[1].Name, Field = "Field 2", Ttl = 20 }
        };
        modelBuilder.Entity<EntityData>().HasData(entityData);

        // EntityDataSource verileri
        var entityDataSources = new List<EntityDataSource>
        {
            new EntityDataSource { Id = Guid.NewGuid(), EntityDataId = entityData[0].Id, Order = 1, TagId = Guid.NewGuid(), Tag = new Tag { Id = Guid.NewGuid(), Name = "Tag 1", Url = "URL 1" }, DataPath = "Path 1" },
            new EntityDataSource { Id = Guid.NewGuid(), EntityDataId = entityData[1].Id, Order = 2, TagId = Guid.NewGuid(), Tag = new Tag { Id = Guid.NewGuid(), Name = "Tag 2", Url = "URL 2" }, DataPath = "Path 2" }
        };
        modelBuilder.Entity<EntityData>().HasData(entityDataSources);

        // Tag verileri
        var tags = new List<Tag>
        {
            new Tag { Id = Guid.NewGuid(), Name = "Tag 1", Url = "URL 1", Ttl = 30, CreatedDate = DateTime.Now},
            new Tag { Id = Guid.NewGuid(), Name = "Tag 2", Url = "URL 2", Ttl = 40, CreatedDate = DateTime.Now}
        };
        modelBuilder.Entity<Tag>().HasData(tags);

        // TagRelation verileri
        var tagRelations = new List<TagRelation>
        {
            new TagRelation { Id = Guid.NewGuid(), OwnerName = "Owner 1", TagId = tags[0].Id, Tag = tags[0] },
            new TagRelation { Id = Guid.NewGuid(), OwnerName = "Owner 2", TagId = tags[1].Id, Tag = tags[1] }
        };
        modelBuilder.Entity<TagRelation>().HasData(tagRelations);

        // View verileri
        var views = new List<View>
        {
            new View { Id = Guid.NewGuid(), TagId = tags[0].Id, Tag = tags[0], ViewTemplateName = "View 1", Type = Enums.ViewType.Html },
            new View { Id = Guid.NewGuid(), TagId = tags[1].Id, Tag = tags[1], ViewTemplateName = "View 2", Type = Enums.ViewType.Json }
        };
        modelBuilder.Entity<View>().HasData(views);

    }
}


public static class SeedDataGenerator
{

    public static void GenerateSeedData(ModelBuilder modelBuilder)
    {
        // Domain verileri
        var domains = new List<Domain>
        {
            new Domain { Id = Guid.NewGuid(), Name = "Domain 1", Description = "Domain 1 Description" },
            new Domain { Id = Guid.NewGuid(), Name = "Domain 2", Description = "Domain 2 Description" }
        };
        modelBuilder.Entity<Domain>().HasData(domains);

        // Entity verileri
        var entities = new List<Entity>
        {
            new Entity { Id = Guid.NewGuid(), Name = "Entity 1", Description = "Entity 1 Description",DomainId=domains[0].Id, Domain = domains[0] },
            new Entity { Id = Guid.NewGuid(), Name = "Entity 2", Description = "Entity 2 Description",DomainId=domains[1].Id, Domain = domains[1] }
        };
        modelBuilder.Entity<Entity>().HasData(entities);

        // EntityData verileri
        var entityData = new List<EntityData>
        {
            new EntityData { Id = Guid.NewGuid(), EntityId = entities[0].Id, Entity = entities[0], EntityName = entities[0].Name, Field = "Field 1", Ttl = 10 },
            new EntityData { Id = Guid.NewGuid(), EntityId = entities[1].Id, Entity = entities[1], EntityName = entities[1].Name, Field = "Field 2", Ttl = 20 }
        };
        modelBuilder.Entity<EntityData>().HasData(entityData);

        // EntityDataSource verileri
        var entityDataSources = new List<EntityDataSource>
        {
            new EntityDataSource { Id = Guid.NewGuid(), EntityDataId = entityData[0].Id, EntityData = entityData[0], Order = 1, TagId = Guid.NewGuid(), Tag = new Tag { Id = Guid.NewGuid(), Name = "Tag 1", Url = "URL 1" }, DataPath = "Path 1" },
            new EntityDataSource { Id = Guid.NewGuid(), EntityDataId = entityData[1].Id, EntityData = entityData[1], Order = 2, TagId = Guid.NewGuid(), Tag = new Tag { Id = Guid.NewGuid(), Name = "Tag 2", Url = "URL 2" }, DataPath = "Path 2" }
        };
        modelBuilder.Entity<EntityData>().HasData(entityDataSources);

        // Tag verileri
        var tags = new List<Tag>
        {
            new Tag { Id = Guid.NewGuid(), Name = "Tag 1", Url = "URL 1", Ttl = 30, CreatedDate = DateTime.Now },
            new Tag { Id = Guid.NewGuid(), Name = "Tag 2", Url = "URL 2", Ttl = 40, CreatedDate = DateTime.Now }
        };
        modelBuilder.Entity<Tag>().HasData(tags);

        // TagRelation verileri
        var tagRelations = new List<TagRelation>
        {
            new TagRelation { Id = Guid.NewGuid(), OwnerName = "Owner 1", TagId = tags[0].Id, Tag = tags[0] },
            new TagRelation { Id = Guid.NewGuid(), OwnerName = "Owner 2", TagId = tags[1].Id, Tag = tags[1] }
        };
        modelBuilder.Entity<TagRelation>().HasData(tagRelations);

        // View verileri
        var views = new List<View>
        {
            new View { Id = Guid.NewGuid(), TagId = tags[0].Id, Tag = tags[0], ViewTemplateName = "View 1", Type = Enums.ViewType.Html },
            new View { Id = Guid.NewGuid(), TagId = tags[1].Id, Tag = tags[1], ViewTemplateName = "View 2", Type = Enums.ViewType.Json }
        };
        modelBuilder.Entity<View>().HasData(views);

    }

}

//         modelBuilder.Entity<Tag>().HasData(new
//         {
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             Id = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//             Name = "idm"
//         });
//         modelBuilder.Entity<Tag>().HasData(new
//         {
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//             Id = new Guid("b3f9a3a0-0b0a-4b0a-8b0a-0b0a0b0a0b5a"),
//             Name = "retail-customer",
//             Url = "http://localhost:3001/cb.customers?reference=@reference",
//             Ttl = 5,
//             CreatedDate = System.DateTime.Now.ToUniversalTime()
//         });
//         modelBuilder.Entity<Tag>().HasData(new
//         {
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             Id = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//             Name = "corporate-customer",
//             Url = "http://localhost:3001/cb.customers?reference=@reference",
//             Ttl = 10
//         });
//         modelBuilder.Entity<Tag>().HasData(new
//         {
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//             Id = Guid.NewGuid(),
//             Name = "loan-partner",
//             Url = "http://localhost:3001/cb.partner/@reference",
//             Ttl = 10
//         });
//         modelBuilder.Entity<Tag>().HasData(new
//         {
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//             Id = new Guid("b3f9a3a0-0b1a-4b0e-8b0a-0b9b5b0a0b04"),
//             Name = "loan-partner-staff",
//             Url = "http://localhost:3001/cb.partner/@partner/staff/@reference",
//             Ttl = 10
//         });
//         modelBuilder.Entity<Tag>().HasData(new
//         {
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//             Id = new Guid("b3f9a3a0-0b1a-4b0e-8b0a-0b9b5b0a0b0b"),
//             Name = "burgan-staff",
//             Url = "http://localhost:3001/cb.staff/@reference",
//             Ttl = 10
//         });
//         modelBuilder.Entity<Tag>().HasData(new
//         {
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             Id = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//             Name = "burgan-bank-turkey",
//             Url = "http://localhost:3001/cb.bankInfo",
//             Ttl = 10
//         });

//         modelBuilder.Entity<TagRelation>().HasData(new
//         {
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//             OwnerId = Guid.NewGuid(),
//             TagId = Guid.NewGuid(),
//             Id = Guid.NewGuid(),
//             TagName = "corporate-customer",
//             OwnerName = "idm"
//         });
//         modelBuilder.Entity<TagRelation>().HasData(new
//         {
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//             OwnerId = Guid.NewGuid(),
//             TagId = Guid.NewGuid(),
//             Id = Guid.NewGuid(),
//             TagName = "retail-customer",
//             OwnerName = "idm"
//         });
//         modelBuilder.Entity<TagRelation>().HasData(new
//         {
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//             OwnerId = Guid.NewGuid(),
//             TagId = Guid.NewGuid(),
//             Id = Guid.NewGuid(),
//             TagName = "loan-partner",
//             OwnerName = "idm"
//         });
//         modelBuilder.Entity<TagRelation>().HasData(new
//         {
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//             OwnerId = Guid.NewGuid(),
//             TagId = Guid.NewGuid(),
//             Id = Guid.NewGuid(),
//             TagName = "loan-partner-staff",
//             OwnerName = "idm"
//         });
//         modelBuilder.Entity<TagRelation>().HasData(new
//         {
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//             OwnerId = Guid.Empty,
//             TagId = Guid.NewGuid(),
//             TagName = "burgan-staff",
//             OwnerName = "idm",
//             Id = Guid.NewGuid(),
//         });
//         modelBuilder.Entity<TagRelation>().HasData(new
//         {
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//             OwnerId = Guid.NewGuid(),
//             TagId = Guid.NewGuid(),
//             TagName = "burgan-bank-turkey",
//             OwnerName = "idm",
//             Id = Guid.NewGuid(),
//         });

//         modelBuilder.Entity<View>().HasData(new
//         {
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//             TagName = "retail-customer",
//             ViewTemplateName = "retail-customer-mini-html",
//             TagId = Guid.NewGuid(),

//             Type = Enums.ViewType.Html
//         });
//         modelBuilder.Entity<View>().HasData(new
//         {
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//             TagName = "retail-customer",
//             ViewTemplateName = "retail-customer-flutter",
//             TagId = Guid.NewGuid(),
//             Type = Enums.ViewType.Flutter
//         });

//         modelBuilder.Entity<TagRelation>().HasData(new
//         {
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//             TagName = "retail-customer",
//             TagId = Guid.NewGuid(),
//             Id = Guid.NewGuid(),
//             OwnerName = "retail-loan"
//         });
//         modelBuilder.Entity<TagRelation>().HasData(new
//         {
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//             TagName = "loan-partner",
//             TagId = Guid.NewGuid(),
//             Id = Guid.NewGuid(),
//             OwnerName = "retail-loan"
//         });

//         modelBuilder.Entity<Domain>().HasData(new
//         {
//             Name = "idm",
//             Id = new Guid("9ef36432-7078-412c-bf5f-64d33a8f3fc8"),
//             Description = "Identity Management Platform",
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//         });

//         modelBuilder.Entity<Entity>().HasData(new
//         {
//             Name = "user",
//             Id = new Guid("9ef36432-7078-413c-bf5f-64d33a8f3fc8"),
//             Description = "User repository",
//             DomainId = new Guid("9ef36432-7078-412c-bf5f-64d33a8f3fc8"),
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//         });
//         modelBuilder.Entity<Entity>().HasData(new
//         {
//             Name = "scope",
//             Id = new Guid("9ef36433-7078-412c-bf5d-64d33a8f3fc8"),
//             Description = "Scope repository",
//             DomainId = new Guid("9ef36433-7078-412c-bf5f-64d33a8f3fc8"),
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//         });

//         modelBuilder.Entity<EntityData>().HasData(new
//         {
//             Id = new Guid("107f4644-57cd-46ff-80de-004c6cd44704"),
//             EntityId = new Guid("9ef36432-7078-413c-bf5f-64d33a8f3fc8"),
//             EntityName = "user",
//             Field = "firstname",
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//         });
//         modelBuilder.Entity<EntityData>().HasData(new
//         {
//             Id = new Guid("207f4644-57cd-46ff-80de-004c6cd44704"),
//             EntityId = new Guid("9ef36432-7078-413c-bf5f-64d33a8f3fc8"),
//             EntityName = "user",
//             Field = "lastname",
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//         });
//         modelBuilder.Entity<EntityData>().HasData(new
//         {
//             Id = new Guid("307f4644-57cd-46ff-80de-004c6cd44704"),
//             EntityId = new Guid("9ef36433-7078-412c-bf5d-64d33a8f3fc8"),
//             EntityName = "scope",
//             Field = "title",
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//         });

//         modelBuilder.Entity<EntityDataSource>().HasData(new
//         {
//             EntityDataId = new Guid("107f4644-57cd-46ff-80de-004c6cd44704"),
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//             Order = 1,
//             TagName = "burgan-staff",
//             TagId = new Guid("b3f9a3a0-0b1a-4b0e-8b0a-0b9b5b0a0b0b"),
//             DataPath = "$.firstname"
//         });
//         modelBuilder.Entity<EntityDataSource>().HasData(new
//         {
//             EntityDataId = new Guid("107f4644-57cd-46ff-80de-004c6cd44704"),
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//             Order = 2,
//             TagId = new Guid("b3f9a3a0-0b1a-4b0e-8b0a-0b9b5b0a0b04"),
//             TagName = "loan-partner-staff",
//             DataPath = "$.partner-staff.fullname"
//         });
//         modelBuilder.Entity<EntityDataSource>().HasData(new
//         {
//             EntityDataId = new Guid("107f4644-57cd-46ff-80de-004c6cd44704"),
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//             Order = 3,
//             TagId = new Guid("b3f9a3a0-0b0a-4b0a-8b0a-0b0a0b0a0b5a"),
//             TagName = "retail-customer",
//             DataPath = "$.firstname"
//         });

//         modelBuilder.Entity<EntityDataSource>().HasData(new
//         {
//             EntityDataId = new Guid("207f4644-57cd-46ff-80de-004c6cd44704"),
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//             Order = 1,
//             TagId = new Guid("b3f9a3a0-0b1a-4b0e-8b0a-0b9b5b0a0b0b"),
//             TagName = "burgan-staff",
//             DataPath = "$.lastname"
//         });
//         modelBuilder.Entity<EntityDataSource>().HasData(new
//         {
//             EntityDataId = new Guid("207f4644-57cd-46ff-80de-004c6cd44704"),
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//             Order = 2,
//             TagId = new Guid("b3f9a3a0-0b1a-4b0e-8b0a-0b9b5b0a0b04"),
//             TagName = "loan-partner-staff",
//             DataPath = "$.partner-staff.fullname"
//         });
//         modelBuilder.Entity<EntityDataSource>().HasData(new
//         {
//             EntityDataId = new Guid("207f4644-57cd-46ff-80de-004c6cd44704"),
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//             Order = 3,
//             TagId = new Guid("b3f9a3a0-0b0a-4b0a-8b0a-0b0a0b0a0b5a"),
//             TagName = "retail-customer",
//             DataPath = "$.lastname"
//         });

//         modelBuilder.Entity<EntityDataSource>().HasData(new
//         {
//             EntityDataId = new Guid("307f4644-57cd-46ff-80de-004c6cd44704"),
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//             Order = 1,
//             TagId = new Guid("b3f9a3a0-0b1a-4b0e-8b0a-0b9b5b0a0b0b"),
//             TagName = "burgan-staff",
//             DataPath = "$.firstname"
//         });
//         modelBuilder.Entity<EntityDataSource>().HasData(new
//         {
//             EntityDataId = new Guid("307f4644-57cd-46ff-80de-004c6cd44704"),
//             CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
//             CreatedBy = Guid.NewGuid(),
//             ModifiedAt = DateTime.UtcNow,
//             ModifiedBy = Guid.NewGuid(),
//             ModifiedByBehalfOf = Guid.NewGuid(),
//             Order = 2,
//             TagId = new Guid("b3f9a3a0-0b1a-4b0e-8b0a-0b9b5b0a0b04"),
//             TagName = "loan-partner-staff",
//             DataPath = "$.partner-staff.fullname"
//         });
//     }

// }

// // public class Tag
// // {
// //     [Key]
// //     public string Name { get; set; } = string.Empty;
// //     public string? Url { get; set; }
// //     public int? Ttl { get; set; }
// //     public DateTime? CreatedDate { get; set; }
// //     [JsonIgnore]
// //     public DateTime? LastModifiedDate { get; set; }
// //     [InverseProperty("Owner")]
// //     public List<TagRelation> TagsRelations { get; set; } = new List<TagRelation>();
// //     public List<View> Views { get; set; } = new List<View>();

// // }

// // [PrimaryKey("OwnerName", "TagName")]
// // public class TagRelation
// // {
// //     [ForeignKey("Owner")]
// //     public string OwnerName { get; set; } = string.Empty;
// //     public Tag? Owner { get; set; }

// //     [ForeignKey("Tag")]
// //     public string TagName { get; set; } = string.Empty;
// //     public Tag? Tag { get; set; }
// // }

// // [PrimaryKey("TagName", "ViewTemplateName")]
// // public class View
// // {
// //     [ForeignKey("Tag")]
// //     public string TagName { get; set; } = string.Empty;
// //     public Tag? Tag { get; set; }

// //     public string ViewTemplateName { get; set; } = string.Empty;
// //     public ViewType Type { get; set; }
// // }

// // public enum ViewType
// // {
// //     Html,
// //     MobileHtml,
// //     Flutter,
// //     NativeIOS,
// //     NativeAndroid,
// //     Json
// // }

// // public class Domain
// // {
// //     [Key]
// //     public string Name { get; set; } = string.Empty;
// //     public string Description { get; set; } = string.Empty;
// //     public List<Entity> Entities = new List<Entity>();
// // }

// // public class Entity
// // {
// //     [Key]
// //     public string Name { get; set; } = string.Empty;
// //     public string? Description { get; set; } = string.Empty;
// //     public DateTime? CreatedDate { get; set; }
// //     [JsonIgnore]
// //     public DateTime? LastModifiedDate { get; set; }
// //     [ForeignKey("Domain")]
// //     public string DomainName { get; set; } = string.Empty;
// //     public Domain? Domain { get; set; }
// //     public List<EntityData> Data = new List<EntityData>();
// // }

// // public class EntityData
// // {
// //     [Key]
// //     public Guid Id { get; set; }

// //     public Entity? Entity { get; set; }
// //     public String EntityName { get; set; } = string.Empty;
// //     public DateTime? CreatedDate { get; set; }
// //     [JsonIgnore]
// //     public DateTime? LastModifiedDate { get; set; }
// //     public string Field { get; set; } = string.Empty;
// //     public int? Ttl { get; set; }
// //     public List<EntityDataSource> Sources = new List<EntityDataSource>();
// // }

// // [PrimaryKey("EntityDataId", "Order")]
// // public class EntityDataSource
// // {
// //     [ForeignKey("EntityData")]
// //     public Guid EntityDataId { get; set; }
// //     public EntityData? EntityData { get; set; }

// //     public int Order { get; set; }
// //     public string TagName { get; set; } = string.Empty;
// //     public Tag? Tag { get; set; }
// //     public string DataPath { get; set; } = string.Empty;
// // }

// public class RenderRequestDefinition
// {
//     [DatabaseGenerated(DatabaseGeneratedOption.None)]
//     [JsonPropertyNameAttribute("name")]
//     public string? Name { get; set; }

//     [JsonPropertyNameAttribute("render-id")]
//     public Guid RenderID { get; set; }

//     [JsonPropertyNameAttribute("render-data")]
//     public dynamic? RenderData { get; set; }

//     [JsonPropertyNameAttribute("render-data-for-log")]
//     public dynamic? RenderDataForLog { get; set; }
//     [JsonPropertyNameAttribute("semantic-version")]
//     public string? SemVer { get; set; }
//     [JsonPropertyNameAttribute("process-name")]
//     public string? ProcessName { get; set; }

//     [JsonPropertyNameAttribute("item-id")]
//     public string? ItemId { get; set; }

//     [JsonPropertyNameAttribute("action")]
//     public string? Action { get; set; }
//     [JsonPropertyNameAttribute("identity")]
//     public string? Identity { get; set; }
//     [JsonPropertyNameAttribute("customer")]
//     public string? Customer { get; set; }
// }
