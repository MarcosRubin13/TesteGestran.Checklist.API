﻿
namespace TesteGestran.Checklist.Domain.Checklist
{
    public class ChecklistItemCommand
    {
        public bool Verificado { get; set; }
        public string Observacao { get; set; }
        public int ChecklistId { get; set; }
        public string Item { get; set; }
    }
}