using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using amorphie.core.Base;

public class Entity : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public Guid DomainId { get; set; }
    public Domain? Domain { get; set; }
    public List<EntityData> Data = new List<EntityData>();
}