using amorphie.core.Base;

public class DtoEntity : DtoBase
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public Guid DomainId { get; set; }
    public Domain? Domain { get; set; }
    public List<EntityData> Data = new List<EntityData>();
}