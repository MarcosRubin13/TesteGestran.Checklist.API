using FluentValidation;
using TesteGestran.Checklist.Domain.Checklist;

public class ChecklistUpdateStatusCommandValidator : AbstractValidator<ChecklistUpdateStatusCommand>
{
    public ChecklistUpdateStatusCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Identificador inválido.");

        RuleFor(x => x.Etapa)
            .NotEmpty().WithMessage("A situação é obrigatória.")
            .Must(s => s == "ABERTO" || s == "INICIADO" || s == "FINALIZADO")
            .WithMessage("Situação inválida.");
    }
}
