using amorphie.core.Base;
[Serializable]
public class DtoEntityDataSource : DtoBase
{

    public Guid EntityDataId { get; set; }
    public int Order { get; set; }
    public Guid TagId { get; set; }
    public string TagName { get; set; } = string.Empty;
    public string DataPath { get; set; } = string.Empty;
}