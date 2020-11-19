using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FifoAPI.Domains;
using FifoAPI.Interfaces;
using FifoAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FifoAPI.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]

    [ApiController]
    public class UsuarioController : Controller
    {
        private IUsuarioRepository _usuarioRepository;

        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            try
            {
                return Ok(_usuarioRepository.Listar());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            try
            {
                Usuario usuario = _usuarioRepository.BuscarPorId(id);

                if (usuario != null)
                {
                    return Ok(usuario);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return NotFound("Usuário não encontrado");
        }

        [HttpPost]
        public IActionResult Post(Usuario novoUsuario)
        {
            try
            {
                if (novoUsuario.Senha != null && novoUsuario.Nickname != null)
                {
                    _usuarioRepository.Cadastrar(novoUsuario);

                    return StatusCode(201, novoUsuario);
                }

                return BadRequest("Todos as informações são obrigatórias");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                Usuario usuario = _usuarioRepository.BuscarPorId(id);

                if (usuario != null)
                {
                    _usuarioRepository.Deletar(id);

                    return StatusCode(202, usuario);
                }

                return NotFound("Nenhum usuário encontrado para o ID informado.");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
