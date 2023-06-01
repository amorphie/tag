
using amorphie.core.Module.minimal_api;
using amorphie.core.Repository;
using amorphie.tag.Modules.Base;
using amorphie.tag.Validator;
using AutoMapper;
using FluentValidation;

public sealed class DomainModule : BaseDomainModule<DtoDomain, Domain, DomainValidator>
{
    public DomainModule(WebApplication app) : base(app)
    {
    }

    public override string? UrlFragment => "domain";


    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);

    }

}