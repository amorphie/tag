namespace amorphie.tag.data;

public record GetTagResponse(string Name, string Url, int Ttl, string[] Tags);
public record SaveTagRequest(string Name, string? Url, int? Ttl);