using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using amorphie.core.Base;

public class EntityData : EntityBase
{
    public Guid EntityId { get; set; }
    public Entity? Entity { get; set; }
    public string Field { get; set; } = string.Empty;
    public int? Ttl { get; set; }
   // public List<EntityDataSource> Sources = new List<EntityDataSource>();
}