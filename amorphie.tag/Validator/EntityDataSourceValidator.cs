using System;
using FluentValidation;

namespace amorphie.tag.Validator
{
    public sealed class EntityDataSourceValidator : AbstractValidator<EntityDataSource>
    {
        public EntityDataSourceValidator()
        {
            // RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            // RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        }
    }
}


