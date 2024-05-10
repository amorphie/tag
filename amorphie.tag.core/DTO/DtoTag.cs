using System.ComponentModel.DataAnnotations;
using amorphie.core.Base;

public class DtoTag : DtoBase
{
    [Key]
    public string? Name { get; set; }
    public string? Status { get; set; }
    public string? Url { get; set; }
    public int? Ttl { get; set; }
    // public List<DtoTagRelation> TagsRelations { get; set; } = new List<DtoTagRelation>();
}

public class DtoUrl
{
    public string Url { get; set; }
}
public class JsonData
{
    public string Data { get; set; }
}