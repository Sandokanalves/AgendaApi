using agenda.Domain.Entities;
using Agenda.Domain.Entities;

namespace agenda.Application.Interfaces;

public interface IAuthService
{
    Task<bool> ValidarUsuario(string email, string senha);
    Task RegistrarUsuario(Usuario usuario);
    string GerarToken(Usuario usuario);
    Task<Usuario> GetUsuarioByEmailAsync(string email);
}
