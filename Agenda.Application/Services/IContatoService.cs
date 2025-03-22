using Agenda.Application.DTOS.ViewModels;
using Agenda.Application.DTOS.InputModels;


namespace Agenda.Application.Services
{
    public interface IContatoService
    {

        Task<IEnumerable<ContatoViewModel>> GetAllAsync();
        Task<ContatoDetailsViewModel> GetByIdAsync(int id);
        Task<ContatoViewModel> AddAsync(CreateContatoInputModel contatoInput);
        Task<ContatoViewModel> UpdateAsync(int id, UpdateContatoInput contatoInput);
        Task<bool> DeleteAsync(int id);

    }
}
