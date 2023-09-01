using amorphie.core.Module.minimal_api;
using amorphie.core.Repository;
using FluentValidation;
using amorphie.core.Base;

namespace amorphie.tag.Modules.Base;

public abstract class BaseEntityDataSourceModule<TDTOModel, TDBModel, TValidator>
    : BaseBBTRouteRepository<TDTOModel, TDBModel, TValidator, TagDBContext, IBBTRepository<TDBModel, TagDBContext>>
    where TDTOModel : class, new()
    where TDBModel : EntityBase
    where TValidator : AbstractValidator<TDBModel>
{
    protected BaseEntityDataSourceModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Order", "EntityDataId", "TagId", "TagName", "DataPath" };

    public override string? UrlFragment => "entityDataSource";

}