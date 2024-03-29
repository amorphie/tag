using System.ComponentModel.DataAnnotations;
using amorphie.core.Base;
using NpgsqlTypes;

public class Domain : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Entity> Entities = new List<Entity>();
    public NpgsqlTsVector? SearchVector { get; set; }

}