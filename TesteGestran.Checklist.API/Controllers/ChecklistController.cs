using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TesteGestran.Checklist.API.Controllers;
using TesteGestran.Checklist.API.Extensions;
using TesteGestran.Checklist.Domain.Checklist;
using TesteGestran.Checklist.Domain.Interfaces.Service;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ChecklistController : BaseController
{
    private readonly IChecklistService _checklistService;

    public ChecklistController(IChecklistService checklistService)
    {
        _checklistService = checklistService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var checklists = _checklistService.GetAllChecklists();
        return Response(checklists);
    }

    [HttpGet("{id}")]
    public IActionResult GetChecklistById(int id)
    {
        var checklistDto = _checklistService.GetChecklistById(id);
        if (checklistDto == null)
        {
            return NotFound(new { message = "Checklist não encontrado." });
        }

        return Response(checklistDto);
    }

    [HttpPost]
    public IActionResult Create([FromBody] ChecklistCreateCommand command)
    {
        command.Usuario = User.GetUsername();
        var checklist = _checklistService.CreateChecklist(command);
        return Response(new { Id = checklist.Id });
    }

    [HttpPut("iniciar")]
    public IActionResult IniciarProcesso([FromBody] ChecklistUpdateStatusCommand command)
    {
        command.Etapa = "INICIADO";
        var success = _checklistService.UpdateChecklistStatus(command);
        if (!success) return NotFound(new { message = "Checklist não encontrado ou já iniciado." });
        return Response(new { message = "Checklist iniciado com sucesso!" });
    }

    [HttpPut("{checklistId}/item/{itemId}/verificar")]
    public IActionResult VerificarItemChecklist(int checklistId, int itemId, [FromBody] VerificarItemChecklistCommand command)
    {
        var success = _checklistService.VerificarItem(checklistId, itemId, command);
        if (!success)
            return NotFound(new { message = "Item ou checklist não encontrado." });

        return Ok(new { message = "Item verificado com sucesso." });
    }

    [HttpPut("{id}/status/aguardando-finalizacao")]
    public IActionResult SetChecklistToAguardandoFinalizacao(int id)
    {

        var result = _checklistService.SetChecklistToAguardandoFinalizacao(id);
        if (result == false)
        {
            return BadRequest(new { message = "Alguns itens ainda não foram verificados. Por favor, verifique todos os itens para prosseguir." });
        }
        return Response(new { message = "Status do checklist atualizado para AGUARDANDO_FINALIZAÇÃO." });

    }

    [HttpPut("finalizar")]
    [Authorize(Roles = "Supervisor")]
    public IActionResult FinalizarProcessor([FromBody] ChecklistUpdateStatusCommand command)
    {
        command.Etapa = "FINALIZADO";
        var success = _checklistService.UpdateChecklistStatus(command);
        if (!success) return NotFound();
        return Response(new { message = "Checklist finalizado com sucesso!" });
    }

}

