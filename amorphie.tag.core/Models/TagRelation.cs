using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

[PrimaryKey("OwnerName", "TagName")]
public class TagRelation : EntityBaseWithOutId
{
    [ForeignKey("Owner")]
    public string OwnerName { get; set; } = string.Empty;
    public Tag? Owner { get; set; }

    [ForeignKey("Tag")]
    public string TagName { get; set; } = string.Empty;
    public Tag? Tag { get; set; }
}