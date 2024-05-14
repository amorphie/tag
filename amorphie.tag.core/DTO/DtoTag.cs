using System.ComponentModel.DataAnnotations;
using amorphie.core.Base;
using Microsoft.AspNetCore.Http;

public class DtoTag : DtoBase
{
    [Key]
    public string? Name { get; set; }
    public string? Status { get; set; }
    public string? Url { get; set; }
    public int? Ttl { get; set; }
    public List<DtoTagRelation> TagsRelations { get; set; } = new List<DtoTagRelation>();
}

public class ResultData
{
    public ResultData(IResult result, dynamic data)
    {
        _result = result;
        _data = data;
    }
    public ResultData(IResult result)
    {
        _result = result;
    }
    private IResult _result;
    public IResult Result { get { return _result; } set { _result = value; } }
    private dynamic _data;
    public dynamic Data { get { return _data; } set { _data = value; } }
}