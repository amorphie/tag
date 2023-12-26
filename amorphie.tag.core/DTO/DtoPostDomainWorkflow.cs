using System.ComponentModel.DataAnnotations;

public class DtoPostDomainWorkflow
{
    public Guid recordId { get; set; }
    public DtoDomain? entityData { get; set; }
    [Required]
    public string newStatus { get; set; } = default!;
    public Guid? user { get; set; }
    public Guid? behalfOfUser { get; set; }
    [Required]
    public string workflowName { get; set; } = default!;
}