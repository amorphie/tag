using System.ComponentModel.DataAnnotations;
using amorphie.core.Base;

public class Domain : EntityBaseWithOutId
{
    [Key]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Entity> Entities = new List<Entity>();
}