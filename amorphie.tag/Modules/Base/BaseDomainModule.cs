using amorphie.core.Module.minimal_api;
using amorphie.core.Repository;
using FluentValidation;
using amorphie.core.Base;

namespace amorphie.tag.Modules.Base;

public abstract class BaseDomainModule<TDTOModel, TDBModel, TValidator>
    : BaseBBTRouteRepository<TDTOModel, TDBModel, TValidator, TagDBContext, IBBTRepository<TDBModel, TagDBContext>>
    where TDTOModel : class, new()
    where TDBModel : EntityBase
    where TValidator : AbstractValidator<TDBModel>
{
    protected BaseDomainModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string [] { "Name", "Description"};

    public override string? UrlFragment => throw new NotImplementedException();

}