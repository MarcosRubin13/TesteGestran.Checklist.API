using TesteGestran.Checklist.Domain.Checklist;

namespace TesteGestran.Checklist.Domain.Interfaces.Service
{
    public interface IChecklistService
    {
        List<ChecklistDto> GetAllChecklists();
        ChecklistDto GetChecklistById(int id);
        ChecklistDto CreateChecklist(ChecklistCreateCommand command);
        bool UpdateChecklistStatus(ChecklistUpdateStatusCommand command);
        bool VerificarItem(int checklistId, int itemId, VerificarItemChecklistCommand command);
        bool SetChecklistToAguardandoFinalizacao(int id);
    }
}
