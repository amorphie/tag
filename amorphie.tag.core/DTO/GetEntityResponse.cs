using amorphie.core.Base;

public class GetEntityResponseDto:DtoBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public GetEntityDataResponseDto[] Data { get; set; }
}
