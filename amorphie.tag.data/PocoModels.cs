namespace amorphie.tag.data;

public record GetTagResponse(string Name, string? Url, int? Ttl, string[] Tags);

public record SaveTagRequest(string Name, string? Url, int? Ttl);
public record SaveDomainRequest(string Name, string? Description);
public record SaveEntityRequest(string Name, string? Description, Guid DomainId);
public record SaveEntiyWithDataAndSource(string Name, string? Description, string DomainName, SaveEntityDataRequest[]? Data);
public record SaveEntityDataRequest(string Field, int? Ttl, Guid Id, Guid EntityId, string EntityName, DateTime? CreatedAt, DateTime? LastModifiedDate, SaveEntityDataSourcesRequest[]? Source);
public record SaveEntityDataSourcesRequest(int Order, string TagName, string DataPath)
{
    public Guid TagId { get; set; }
}

public record GetDomainResponse(string Name, string Description, GetDomainEntityResponse[] Entities);
public record GetDomainEntityResponse(string Name, string Description);

public record GetEntityResponse(string Name, string Description, GetEntityDataResponse[] Data);
public record GetEntityDataResponse(string Field, int? Ttl, GetEntityDataSourcesResponse[] Sources);
public record GetEntityDataSourcesResponse(int Order, string Tag, string Path);

public record Test
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid DomainId { get; set; } = Guid.Empty;
    public SaveEntityDataRequest[]? Data { get; set; }
}
