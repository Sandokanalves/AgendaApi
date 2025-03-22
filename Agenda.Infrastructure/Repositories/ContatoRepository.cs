using Agenda.Domain.Entities;
using Agenda.Domain.Interfaces;
using Agenda.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Infrastructure.Repositories
{
    public class ContatoRepository: IContatoRepository

    {    private readonly AgendaDbContext _agendaContext;
        public ContatoRepository(AgendaDbContext contato) 
        {
            _agendaContext = contato;
        
        }

        public async Task<IEnumerable<Contato>> GetAllAsync()
        {
            return await _agendaContext.Contatos.AsNoTracking().ToListAsync();
        }

        public async Task<Contato> GetByIdAsync(int id)
        {
            return await _agendaContext.Contatos.FindAsync(id);
        }

        public async Task<Contato> AddAsync(Contato contato)
        {
            _agendaContext.Contatos.Add(contato);
            await _agendaContext.SaveChangesAsync();
            return contato;
        }

        public async Task<Contato> UpdateAsync(Contato contato)
        {
            _agendaContext.Contatos.Update(contato);
            await _agendaContext.SaveChangesAsync();
            return contato;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var contato = await GetByIdAsync(id);
            if (contato == null) return false;

            _agendaContext.Contatos.Remove(contato);
            await _agendaContext.SaveChangesAsync();
            return true;
        }
    }
}