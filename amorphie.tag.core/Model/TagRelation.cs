using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

public class TagRelation : EntityBase
{

    public string OwnerName { get; set; } = string.Empty;
    [ForeignKey("Tag")]
    public Guid TagId { get; set; }
    public string TagName { get; set; } = string.Empty;
    public Tag? Tag { get; set; }
}