using FluentValidation;
using TesteGestran.Checklist.Domain.Checklist;

public class ChecklistCreateCommandValidator : AbstractValidator<ChecklistCreateCommand>
{
    public ChecklistCreateCommandValidator()
    {
        RuleFor(x => x.Usuario)
            .NotEmpty().WithMessage("O campo 'Usuário' é obrigatório.");
    }
}

public class ChecklistItemCommandValidator : AbstractValidator<ChecklistItemCommand>
{
    public ChecklistItemCommandValidator()
    {

        RuleFor(x => x.Verificado)
            .NotNull().WithMessage("A verificação do item é obrigatória.");
    }
}
