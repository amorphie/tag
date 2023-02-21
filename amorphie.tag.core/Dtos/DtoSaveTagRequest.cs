using System.ComponentModel.DataAnnotations;

public class DtoSaveTagRequest : DtoBase
{
    [Key]
    public string Name { get; set; } = string.Empty;
    public string? Url { get; set; }
    public int? Ttl { get; set; }
}