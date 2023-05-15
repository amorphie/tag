using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

[PrimaryKey("OwnerName", "TagName")]
public class TagRelation : EntityBaseWithOutId
{
    [ForeignKey("Owner")]
    public Guid OwnerId { get; set; }
    public string OwnerName { get; set; } = string.Empty;
    public Tag? Owner { get; set; }

    [ForeignKey("Tag")]
    public Guid TagId { get; set; }
    public string TagName { get; set; } = string.Empty;
    public Tag? Tag { get; set; }
}