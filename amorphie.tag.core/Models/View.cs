using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

public class View : EntityBase
{
    public Guid TagId { get; set; }
    public string TagName { get; set; } = string.Empty;
    public Tag? Tag { get; set; }

    public string ViewTemplateName { get; set; } = string.Empty;
    public Enums.ViewType Type { get; set; }
}