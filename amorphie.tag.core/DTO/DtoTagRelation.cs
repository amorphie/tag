using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;

public class DtoTagRelation : DtoBase
{
    public string OwnerName { get; set; } = string.Empty;

    public string TagName { get; set; } = string.Empty;

    public Guid TagId { get; set; }

}