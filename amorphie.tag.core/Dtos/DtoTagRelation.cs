using System.ComponentModel.DataAnnotations.Schema;


public class DtoTagRelation : DtoBase
{
    [ForeignKey("Owner")]
    public string OwnerName { get; set; } = string.Empty;

    [ForeignKey("Tag")]
    public string TagName { get; set; } = string.Empty;
}