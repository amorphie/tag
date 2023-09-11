using amorphie.core.Base;

public class GetEntityDataResponseDto:DtoBase
{
    public required string Field { get; set; }
    public int? Ttl { get; set; }
    public GetEntityDataSourcesResponseDto[]? Sources { get; set; }
}
