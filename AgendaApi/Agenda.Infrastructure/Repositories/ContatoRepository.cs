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
            return await _agendaContext.Contatos.ToListAsync();
        }

        public async Task<Contato> GetByIdAsync(int id)
        {
            return await _agendaContext.Contatos.FindAsync(id);
        }

        public async Task AddAsync(Contato contato)
        {
            _agendaContext.Contatos.Add(contato);
            await _agendaContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Contato contato)
        {
            _agendaContext.Contatos.Update(contato);
            await _agendaContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var contato = await _agendaContext.Contatos.FindAsync(id);
            if (contato != null)
            {
                _agendaContext.Contatos.Remove(contato);
                await _agendaContext.SaveChangesAsync();
            }
        }
    }
}