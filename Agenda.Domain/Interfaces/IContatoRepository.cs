using Agenda.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Domain.Interfaces
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
