using amorphie.core.Base;

public class EntityAllDto : DtoBase
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public Guid DomainId { get; set; }
    public List<GetEntityDataResponseDto> EntityData = new();
    public List<GetEntityDataSourcesResponseDto> Sources = new();
}