using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using amorphie.core.Base;

public class Entity : EntityBaseWithOutId
{
    [Key]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    [ForeignKey("Domain")]
    public string DomainName { get; set; } = string.Empty;
    public Domain? Domain { get; set; }
    public List<EntityData> Data = new List<EntityData>();
}