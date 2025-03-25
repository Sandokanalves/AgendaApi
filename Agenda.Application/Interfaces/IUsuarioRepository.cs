using Agenda.Domain.Entities;

namespace Agenda.Application.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario> GetUsuarioByEmailAsync(string email);
    Task AdicionarUsuarioAsync(Usuario usuario);
}
