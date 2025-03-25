
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using agenda.Application.Interfaces;
using Agenda.Application.Interfaces;
using Agenda.Domain.Entities;
using Castle.Core.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;


namespace agenda.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

    public AuthService(IUsuarioRepository usuarioRepository, Microsoft.Extensions.Configuration.IConfiguration configuration)
    {
        _usuarioRepository = usuarioRepository;
        _configuration = configuration;
    }

    public async Task<bool> ValidarUsuario(string email, string senha)
    {
        var usuario = await _usuarioRepository.GetUsuarioByEmailAsync(email);

        if (usuario == null) return false;

        
        var senhaHash = GerarHashSenha(senha);
        return usuario.Senha == senhaHash;
    }
    public async Task<Usuario> GetUsuarioByEmailAsync(string email)
    {
        var usuario = await _usuarioRepository.GetUsuarioByEmailAsync(email);

        if (usuario == null)
        {
          
            throw new Exception($"Usuário não encontrado com o e-mail: {email}");
        }
        return usuario;
    }
    public async Task RegistrarUsuario(Usuario usuario)
    {
        
        usuario.Senha = GerarHashSenha(usuario.Senha);
        await _usuarioRepository.AdicionarUsuarioAsync(usuario);
    }

    public string GerarToken(Usuario usuario)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Email, usuario.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings")["Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration.GetSection("JwtSettings")["Issuer"],
            audience: _configuration.GetSection("JwtSettings")["Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );



        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GerarHashSenha(string senha)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
            return Convert.ToBase64String(bytes);
        }
    }
}
