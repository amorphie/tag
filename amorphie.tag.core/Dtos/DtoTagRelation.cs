using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;

public class DtoTagRelation : DtoEntityBaseWithOutId
{
    [ForeignKey("Owner")]
    public string OwnerName { get; set; } = string.Empty;

    [ForeignKey("Tag")]
    public string TagName { get; set; } = string.Empty;
}