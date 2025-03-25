using agenda.Application.Interfaces;
using agenda.Domain.Entities;
using Agenda.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace agenda.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Usuario usuario)
    {
        var existingUser = await _authService.ValidarUsuario(usuario.Email, usuario.Senha);

        if (existingUser)
            return BadRequest("Usuário já existe.");

        await _authService.RegistrarUsuario(usuario);
        return Ok(new { message = "Usuário registrado com sucesso!" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var usuarioValido = await _authService.ValidarUsuario(request.Email, request.Senha);

        if (!usuarioValido)
            return Unauthorized(new { message = "Credenciais inválidas." });

        var usuario = await _authService.GetUsuarioByEmailAsync(request.Email);

        var token = _authService.GerarToken(usuario);

        return Ok(new { token });
    }
}
