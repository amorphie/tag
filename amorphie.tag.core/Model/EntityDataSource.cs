using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;


public class EntityDataSource : EntityBase
{

    public Guid EntityDataId { get; set; }
    public EntityData? EntityData { get; set; }
    public int Order { get; set; }
    public Tag? Tag { get; set; }
    public Guid TagId { get; set; }
    public string TagName { get; set; } = string.Empty;
    public string DataPath { get; set; } = string.Empty;
}
