using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Module.minimal_api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using amorphie.core.Swagger;
using Microsoft.OpenApi.Models;
using amorphie.tag.data;
using System.Text.Json;
using amorphie.tag.data;

namespace amorphie.domain.Module;

public class EntityDataSourceModule : BaseBBTRoute<DtoEntityDataSource, EntityDataSource, TagDBContext>
{
    public EntityDataSourceModule(WebApplication app)
        : base(app) { }

    public override string[]? PropertyCheckList => new string[] { "Name" };

    public override string? UrlFragment => "entityDataSource";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
    }



}


// protected async ValueTask<IResult> SearchMethod(
//     [FromServices] DomainDbContext context,
//     [FromServices] IMapper mapper,
//     [AsParameters] DomainModuleSearch userSearch,
//     HttpContext httpContext,
//     CancellationToken token
// )
// {
//     IList<DomainModule> resultList = await context
//         .Set<DomainModule>()
//         .AsNoTracking()
//         .Where(
//             x =>
//                 x.FirstMidName.Contains(userSearch.Keyword!)
//                 || x.LastName.Contains(userSearch.Keyword!)
//         )
//         .Skip(userSearch.Page)
//         .Take(userSearch.PageSize)
//         .ToListAsync(token);

//     return (resultList != null && resultList.Count > 0)
//         ? Results.Ok(mapper.Map<IList<DomainModuleDTO>>(resultList))
//         : Results.NoContent();
// }

