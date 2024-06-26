using Microsoft.EntityFrameworkCore;
using TesteGestran.Checklist.Domain.Entities;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Checklist> Checklists { get; set; }
    public DbSet<ChecklistItem> ChecklistItens { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
