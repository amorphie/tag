using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[PrimaryKey("TagName", "ViewTemplateName")]
public class View : EntityBase
{
    [ForeignKey("Tag")]
    public string TagName { get; set; } = string.Empty;
    public Tag? Tag { get; set; }

    public string ViewTemplateName { get; set; } = string.Empty;
    public Enums.ViewType Type { get; set; }
}