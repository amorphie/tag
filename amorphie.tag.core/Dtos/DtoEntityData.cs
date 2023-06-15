using amorphie.core.Base;

public class DtoEntityData : DtoBase
{
    public Guid EntityId { get; set; }
    public string Field { get; set; } = string.Empty;
    public string EntityName { get; set; } = string.Empty;
    public int? Ttl { get; set; }
    public List<EntityDataSource>? Sources = new List<EntityDataSource>();
}