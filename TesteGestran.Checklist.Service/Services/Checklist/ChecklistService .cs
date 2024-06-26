using AutoMapper;
using Microsoft.Extensions.Logging;
using TesteGestran.Checklist.Domain.Checklist;
using TesteGestran.Checklist.Domain.Entities;
using TesteGestran.Checklist.Domain.Interfaces.Infra;
using TesteGestran.Checklist.Domain.Interfaces.Service;

namespace TesteGestran.Checklist.Services
{
    public class ChecklistService : IChecklistService
    {
        private readonly IChecklistRepository _checklistRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ChecklistService> _logger;

        public ChecklistService(IChecklistRepository checklistRepository, IMapper mapper, ILogger<ChecklistService> logger)
        {
            _checklistRepository = checklistRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public List<ChecklistDto> GetAllChecklists()
        {
            try
            {
                var checklists = _checklistRepository.GetAll();
                return _mapper.Map<List<ChecklistDto>>(checklists);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter lista de Checklists");
                throw;
            }
        }

        public ChecklistDto GetChecklistById(int id)
        {

            try
            {
                var checklist = _checklistRepository.GetById(id);
                if (checklist == null)
                {
                    _logger.LogWarning($"Checklist com ID {id} não encontrado.");
                    return null;
                }

                return _mapper.Map<ChecklistDto>(checklist);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter Checklist");
                throw;
            }
        }

        public ChecklistDto CreateChecklist(ChecklistCreateCommand command)
        {
            try
            {
                var checklist = ChecklistFactory.Create(command);
                _checklistRepository.Add(checklist);
                return _mapper.Map<ChecklistDto>(checklist);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating checklist");
                throw;
            }
        }

        public bool UpdateChecklistStatus(ChecklistUpdateStatusCommand command)
        {
            try
            {
                var checklist = _checklistRepository.GetById(command.Id);
                if (checklist == null)
                {
                    _logger.LogWarning($"Checklist ID {command.Id} não encontrado.");
                    return false;
                }

                if (checklist.Etapa.Equals("INICIADO") && command.Etapa.Equals("INICIADO"))
                {
                    _logger.LogWarning($"Checklist ID {command.Id} ja iniciado por outro usuário.");
                    return false;
                }

                if (command.Etapa.Equals("FINALIZADO"))
                {
                    checklist.Situacao = command.Situacao;
                    checklist.DataFinalizacao = DateTime.Now;
                }

                checklist.Situacao = checklist.Etapa == "ABERTO" ? null : command.Situacao;
                checklist.Etapa = command.Etapa;
                _checklistRepository.Update(checklist);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar o status do checklist com ID {command.Id}");
                throw;
            }
        }

        public bool VerificarItem(int checklistId, int itemId, VerificarItemChecklistCommand command)
        {
            var checklist = _checklistRepository.GetById(checklistId);
            if (checklist == null)
            {
                _logger.LogWarning($"Checklist ID {checklistId} não encontrado.");
                return false;
            }

            if (checklist.Etapa != "INICIADO")
            {
                _logger.LogWarning($"Checklist ID {checklistId} não está em etapa de ATENDIMENTO.");
                return false;
            }

            var item = checklist.Itens.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
            {
                _logger.LogWarning($"Item com ID {itemId} não encontrado no Checklist ID {checklistId}.");
                return false;
            }

            if (item.Verificado)
            {
                _logger.LogWarning($"Item com ID {itemId} do Checklist ID {checklistId} já foi verificado.");
                return false;
            }

            item.Verificado = true;
            item.Observacao = command.Observacao;
            _checklistRepository.UpdateItem(item);
            _logger.LogInformation($"Item com ID {itemId} verificado com sucesso no Checklist ID {checklistId}.");
            return true;
        }

        public bool SetChecklistToAguardandoFinalizacao(int id)
        {
            var checklist = _checklistRepository.GetById(id);
            if (checklist == null)
            {
                _logger.LogWarning($"Checklist com ID {id} não encontrado.");
                throw new Exception("Checklist não encontrado.");
            }

            if (checklist.Itens.Any(item => item.Verificado == false))
            {
                _logger.LogWarning($"Nem todos os itens do Checklist ID {id} foram verificados.");
                return false;
            }

            checklist.Etapa = "AGUARDANDO_FINALIZAÇÃO";
            _checklistRepository.Update(checklist);
            return true;
        }

    }
}
