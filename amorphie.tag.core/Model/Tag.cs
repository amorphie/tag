using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using amorphie.core.Base;

public class Tag : EntityBase
{

    public string Name { get; set; } = string.Empty;
    public string? Status { get; set; } = string.Empty;
    public string? Url { get; set; }
    public int? Ttl { get; set; }
    public DateTime? CreatedDate { get; set; }
    [JsonIgnore]
    public DateTime? LastModifiedDate { get; set; }
    [InverseProperty("Tag")]
    public List<TagRelation> TagsRelations { get; set; } = new List<TagRelation>();
    public List<View> Views { get; set; } = new List<View>();

}