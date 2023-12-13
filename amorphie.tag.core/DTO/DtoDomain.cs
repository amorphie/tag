using amorphie.core.Base;

public class DtoDomain : DtoBase
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Entity> Entities = new List<Entity>();
}