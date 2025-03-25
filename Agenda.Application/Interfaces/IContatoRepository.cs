using Agenda.Domain.Entities;


namespace Agenda.Application.Interfaces
{
    public interface IContatoRepository
    {
       
        Task<IEnumerable<Contato>> GetAllAsync();
        Task<Contato> GetByIdAsync(int id);
        Task<Contato> AddAsync(Contato contato); 
        Task<Contato> UpdateAsync(Contato contato);
        Task<bool> DeleteAsync(int id);
    }
}
