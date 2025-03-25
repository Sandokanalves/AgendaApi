using Agenda.Application.Interfaces;
using Agenda.Domain.Entities;
using Agenda.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AgendaDbContext _context;

    public UsuarioRepository(AgendaDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario> GetUsuarioByEmailAsync(string email)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task AdicionarUsuarioAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
    }
   
}