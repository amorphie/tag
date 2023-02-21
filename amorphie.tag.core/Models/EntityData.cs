using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class EntityData
{
    [Key]
    public Guid Id { get; set; }

    public Entity? Entity { get; set; }
    public String EntityName { get; set; } = string.Empty;
    public DateTime? CreatedDate { get; set; }
    [JsonIgnore]
    public DateTime? LastModifiedDate { get; set; }
    public string Field { get; set; } = string.Empty;
    public int? Ttl { get; set; }
    public List<EntityDataSource> Sources = new List<EntityDataSource>();
}