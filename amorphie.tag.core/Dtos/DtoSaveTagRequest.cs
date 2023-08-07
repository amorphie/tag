using System.ComponentModel.DataAnnotations;
using amorphie.core.Base;

public class DtoSaveTagRequest
{
    [Key]
    public string Name { get; set; } = string.Empty;
    public string? Url { get; set; }
    public int? Ttl { get; set; }
}