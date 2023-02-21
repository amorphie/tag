using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Tag : EntityBase
{
    [Key]
    public string Name { get; set; } = string.Empty;
    public string? Url { get; set; }
    public int? Ttl { get; set; }
    public DateTime? CreatedDate { get; set; }
    [JsonIgnore]
    public DateTime? LastModifiedDate { get; set; }
    [InverseProperty("Owner")]
    public List<TagRelation> TagsRelations { get; set; } = new List<TagRelation>();
    public List<View> Views { get; set; } = new List<View>();

}