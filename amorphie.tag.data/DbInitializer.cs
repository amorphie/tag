using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.tag.data;

namespace amorphie.tag.data;

public static class DbInitializer
{
    public static void Initialize(TagDBContext context)
    {
        context.Database.EnsureCreated();

        // Look for any students.
        if (context.Tags.Any())
        {
            return; // DB has been seeded
        }

        var tags = new Tag[]{
            new Tag{
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = new Guid("107f4644-57cd-46ff-80de-004c6cd44778"),
                Name = "retail-loan",
                Status="Active"
            },
            new Tag{
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                Name = "idm",
                Status="Active"
                },
                new Tag{
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                Name = "retail-customer",
                Url = "http://localhost:3000/cb.customers?reference=@reference",
                Ttl = 5,
                Status="Active"
                }
        };
        foreach (Tag c in tags)
        {
            context.Tags.Add(c);
        }
        context.SaveChanges();

        var tagRelations = new TagRelation[]{
            new TagRelation{
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                TagName = "retail-customer",
                OwnerName = "idm"
            },
            new TagRelation{
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                TagName = "loan-partner",
                OwnerName = "idm"
            }
        };
        foreach (TagRelation c in tagRelations)
        {
            context.TagRelations!.Add(c);
        }
        context.SaveChanges();

        var views = new View[]{
            new View{
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                TagName = "retail-customer",
                ViewTemplateName = "retail-customer-mini-html",
                Type = ViewType.MobileHtml
            },
            new View{
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                TagName = "retail-customer",
                ViewTemplateName = "retail-customer-flutter",
                Type = ViewType.Flutter
            }
        };

        foreach (View c in views)
        {
            context.Views!.Add(c);
        }
        context.SaveChanges();

        var domain = new Domain
        {
            ModifiedBy = Guid.NewGuid(),
            ModifiedAt = DateTime.UtcNow,
            CreatedBy = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Id = new Guid("107f4644-57cd-46ff-80de-004c6cd44712"),
            Name = "idm",
            Description = "Identity Management Platform"
        };
        context.Domains!.Add(domain);
        context.SaveChanges();

        var entities = new Entity[]{
            new Entity{
                 ModifiedBy = Guid.NewGuid(), ModifiedAt = DateTime.UtcNow,
                 CreatedBy = Guid.NewGuid(), 
                 CreatedAt = DateTime.UtcNow, 
                 Id = new Guid("107f4644-57cd-46ff-80de-004c6cd44714"), 
                 DomainId = new Guid("107f4644-57cd-46ff-80de-004c6cd44712"), 
                 Name = "user", 
                 Description = "User repository", 
                 DomainName = "idm",
            },
            new Entity{
                 ModifiedBy = Guid.NewGuid(), ModifiedAt = DateTime.UtcNow,
                 CreatedBy = Guid.NewGuid(), 
                 CreatedAt = DateTime.UtcNow, 
                 Id = new Guid("107f4644-57cd-46ff-80de-004c6cd44715"), 
                 DomainId = new Guid("107f4644-57cd-46ff-80de-004c6cd44712"), 
                 Name = "scope", 
                 Description = "Scope repository", 
                 DomainName = "idm",
            }
        };
        foreach (Entity c in entities)
        {
            context.Entities!.Add(c);
        }
        context.SaveChanges();

        var entityData=new EntityData[]{
            new EntityData{
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Id = new Guid("107f4644-57cd-46ff-80de-004c6cd44734"),
                EntityId = new Guid("107f4644-57cd-46ff-80de-004c6cd44715"),
                EntityName = "user",
                Field = "firstname"
            },
            new EntityData{
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                // TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                Id = new Guid("107f4644-57cd-46ff-80de-004c6cd44722"),
                EntityId = new Guid("107f4644-57cd-46ff-80de-004c6cd44715"),
                EntityName = "user",
                Field = "lastname"
            },
            new EntityData{
                ModifiedBy = Guid.NewGuid(),
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                // TagId = new Guid("107f4644-57cd-46ff-80de-004c6cd44756"),
                Id = new Guid("107f4644-57cd-46ff-80de-004c6cd44755"),
                EntityId = new Guid("107f4644-57cd-46ff-80de-004c6cd44714"),
                EntityName = "scope",
                Field = "title"
            }
        };

        foreach (EntityData c in entityData)
        {
            context.EntityData!.Add(c);
        }
        context.SaveChanges();

        var entityDataSource=new EntityDataSource[]{
            new EntityDataSource{
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
            },
            new EntityDataSource{
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
            },
            new EntityDataSource{
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
            },
            new EntityDataSource{
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
            },
            new EntityDataSource{
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
            }
        };

        foreach (EntityDataSource c in entityDataSource)
        {
            context.EntityDataSource!.Add(c);
        }
        context.SaveChanges();

    }
}
