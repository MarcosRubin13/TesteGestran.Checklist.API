using Microsoft.EntityFrameworkCore;
using TesteGestran.Checklist.Domain.Interfaces.Infra;

namespace TesteGestran.Checklist.Infrastructure.Repositories
{
    public class ChecklistRepository : IChecklistRepository
    {
        private readonly ApplicationDbContext _context;

        public ChecklistRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Domain.Entities.Checklist> GetAll()
        {
            return _context.Checklists.Include(c => c.Itens).ToList();
        }

        public Domain.Entities.Checklist GetById(int id)
        {
            return _context.Checklists
                            .Include(c => c.Itens)
                            .SingleOrDefault(c => c.Id == id);
        }

        public Domain.Entities.ChecklistItem GetItemById(int checklistId, int itemId)
        {
            return _context.ChecklistItens
                           .FirstOrDefault(ci => ci.ChecklistId == checklistId && ci.Id == itemId);
        }

        public void Add(Domain.Entities.Checklist checklist)
        {
            _context.Checklists.Add(checklist);
            _context.SaveChanges();
        }

        public void Update(Domain.Entities.Checklist checklist)
        {
            _context.Checklists.Update(checklist);
            _context.SaveChanges();
        }

        public void UpdateItem(Domain.Entities.ChecklistItem item)
        {
            _context.ChecklistItens.Update(item);
            _context.SaveChanges();
        }
    }
}
