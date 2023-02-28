using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;
using Microsoft.EntityFrameworkCore;

[PrimaryKey("EntityDataId", "Order")]
public class EntityDataSource : EntityBaseWithOutId
{
    [ForeignKey("EntityData")]
    public Guid EntityDataId { get; set; }
    public EntityData? EntityData { get; set; }

    public int Order { get; set; }
    public string TagName { get; set; } = string.Empty;
    public Tag? Tag { get; set; }
    public string DataPath { get; set; } = string.Empty;
}
