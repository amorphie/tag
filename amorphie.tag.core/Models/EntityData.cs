using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using amorphie.core.Base;

public class EntityData : EntityBase
{
    public Entity? Entity { get; set; }
    public String EntityName { get; set; } = string.Empty;
    public string Field { get; set; } = string.Empty;
    public int? Ttl { get; set; }
    public List<EntityDataSource> Sources = new List<EntityDataSource>();
}