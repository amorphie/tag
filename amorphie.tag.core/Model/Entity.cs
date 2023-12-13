using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using amorphie.core.Base;
using NpgsqlTypes;

public class Entity : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public Guid DomainId { get; set; }
    public string? DomainName { get; set; } = string.Empty;
    public Domain? Domain { get; set; }
    public List<EntityData> EntityData = new List<EntityData>();
    public NpgsqlTsVector? SearchVector { get; set; }

}