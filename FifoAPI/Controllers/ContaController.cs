using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FifoAPI.Domains;
using FifoAPI.Interfaces;
using FifoAPI.Repositories;
using FifoAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FifoAPI.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }
        private static JwtSecurityToken token;

        public ContaController()
        {
            _usuarioRepository = new UsuarioRepository();
        }
        
        [HttpPost("Login")]
        public IActionResult Login(LoginViewModel login)
        {
            Usuario usuarioBuscado = _usuarioRepository.BuscarPorNicknameSenha(login.Nickname, login.Senha);

            if (usuarioBuscado == null)
            {
                return NotFound("Nickname ou senha inválidos");
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, usuarioBuscado.Nickname),
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.Id.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("fifo-chave-autenticacao"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            token = new JwtSecurityToken(
            issuer: "FifoAPI",
            audience: "FifoAPI",
            claims: claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        [HttpPost("Logout")]
        public ActionResult Logout()
        {
            try
            {
                token = null;

                return Ok("Logout realizado com sucesso");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
