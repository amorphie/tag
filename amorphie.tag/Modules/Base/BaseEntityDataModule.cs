using amorphie.core.Module.minimal_api;
using amorphie.core.Repository;
using FluentValidation;
using amorphie.core.Base;

namespace amorphie.tag.Modules.Base;

public abstract class BaseEntityDataModule<TDTOModel, TDBModel, TValidator>
    : BaseBBTRouteRepository<TDTOModel, TDBModel, TValidator, TagDBContext, IBBTRepository<TDBModel, TagDBContext>>
    where TDTOModel : class, new()
    where TDBModel : EntityBase
    where TValidator : AbstractValidator<TDBModel>
{
    protected BaseEntityDataModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => throw new NotImplementedException();

    public override string? UrlFragment => throw new NotImplementedException();

}