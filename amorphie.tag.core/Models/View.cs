using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

[PrimaryKey("TagName", "ViewTemplateName")]
public class View : EntityBaseWithOutId
{
    [ForeignKey("Tag")]
    public Guid TagId { get; set; }
    public string TagName { get; set; } = string.Empty;
    public Tag? Tag { get; set; }

    public string ViewTemplateName { get; set; } = string.Empty;
    public Enums.ViewType Type { get; set; }
}