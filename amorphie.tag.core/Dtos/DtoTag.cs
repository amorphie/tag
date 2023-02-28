using System.ComponentModel.DataAnnotations;
using amorphie.core.Base;

public class DtoTag
{
    [Key]
    public string? Name { get; set; }
    public string? Url { get; set; }
    public int? Ttl { get; set; }
    public List<DtoTagRelation> TagsRelations { get; set; } = new List<DtoTagRelation>();
}