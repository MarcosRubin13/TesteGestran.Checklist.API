namespace TesteGestran.Checklist.Domain.Interfaces.Infra
{
    public interface IChecklistRepository
    {
        List<Entities.Checklist> GetAll();
        Entities.Checklist GetById(int id);
        Entities.ChecklistItem GetItemById(int checklistId, int itemId);
        void Add(Entities.Checklist checklist);
        void Update(Entities.Checklist checklist);
        void UpdateItem(Entities.ChecklistItem item);
    }
}
