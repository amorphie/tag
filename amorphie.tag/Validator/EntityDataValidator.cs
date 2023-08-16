using System;
using FluentValidation;

namespace amorphie.tag.Validator
{
    public sealed class EntityDataValidator : AbstractValidator<EntityData>
    {
        public EntityDataValidator()
        {
            // RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            // RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        }
    }
}


