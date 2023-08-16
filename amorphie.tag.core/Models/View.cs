using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;
using static Enums;

public class View : EntityBase
{
    public Guid TagId { get; set; }
    public string TagName { get; set; } = string.Empty;
    public Tag? Tag { get; set; }

    public string ViewTemplateName { get; set; } = string.Empty;
    public ViewType Type { get; set; }
}

public enum ViewType
{
    Html,
    MobileHtml,
    Flutter,
    NativeIOS,
    NativeAndroid,
    Json
}