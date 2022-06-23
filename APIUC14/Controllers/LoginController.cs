using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using APIUC14.Interfaces;
using APIUC14.Models;
using APIUC14.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace APIUC14.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _iUsuarioRepository;

        public LoginController(IUsuarioRepository iUsuarioRepository)
        {
            _iUsuarioRepository = iUsuarioRepository;
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            Usuario usuarioEncontrado = _iUsuarioRepository.Login(login.Email, login.Senha);

            if(usuarioEncontrado == null)
            {
                return Unauthorized(new { msg = "Usuário e/ou Senha Inválidos" });
            }

            var minhasClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, usuarioEncontrado.Email),
                new Claim(JwtRegisteredClaimNames.Jti, usuarioEncontrado.Id.ToString()),
                new Claim(ClaimTypes.Role, usuarioEncontrado.Tipo)
            };

            var chave = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chapter-chave-autenticacao"));

            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var meuToken = new JwtSecurityToken(
                issuer: "chapter.webapi",
                audience: "chapter.webapi",
                claims: minhasClaims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credenciais
            );

            return Ok(
                new {token = new JwtSecurityTokenHandler().WriteToken(meuToken)}
                );
        }

    }
}
