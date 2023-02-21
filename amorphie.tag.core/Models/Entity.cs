using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Entity
{
    [Key]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public DateTime? CreatedDate { get; set; }
    [JsonIgnore]
    public DateTime? LastModifiedDate { get; set; }
    [ForeignKey("Domain")]
    public string DomainName { get; set; } = string.Empty;
    public Domain? Domain { get; set; }
    public List<EntityData> Data = new List<EntityData>();
}