using amorphie.core.Base;
[Serializable]
public class DtoEntity : DtoBase
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string DomainName { get; set; } = string.Empty;
    public Guid DomainId { get; set; }
    public List<EntityData> EntityData = new();
    public List<EntityDataSource> Sources = new();
}