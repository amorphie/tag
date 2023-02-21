using System.ComponentModel.DataAnnotations;

public class Domain
{
    [Key]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Entity> Entities = new List<Entity>();
}