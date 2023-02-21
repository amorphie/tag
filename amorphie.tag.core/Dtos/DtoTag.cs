using System.ComponentModel.DataAnnotations;

public class DtoTag : DtoBase
{
    [Key]
    public string? Name { get; set; }
    public string? Url { get; set; }
    public int? Ttl { get; set; }
    public List<DtoTagRelation> TagsRelations { get; set; } = new List<DtoTagRelation>();
}