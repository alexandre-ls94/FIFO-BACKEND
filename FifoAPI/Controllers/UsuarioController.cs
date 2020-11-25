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

        /// <summary>
        /// Lista todos os usuários
        /// </summary>
        /// <returns> Retorna uma lista de usuários </returns>
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

        /// <summary>
        /// Busca um usuário através do Id
        /// </summary>
        /// <param name="id"> Id do usuário que será buscado </param>
        /// <returns> Retorna um usuário buscado </returns>
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

        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="novoUsuario"></param>
        /// <returns> Retorna um Status Code 201 - Created </returns>
        [HttpPost]
        public IActionResult Post(Usuario novoUsuario)
        {
            try
            {
                Usuario usuarioBuscado = _usuarioRepository.Listar().FirstOrDefault(u => u.Nickname == novoUsuario.Nickname);

                if (novoUsuario.Senha != null && novoUsuario.Nickname != null)
                {
                    if (usuarioBuscado == null)
                    {
                        _usuarioRepository.Cadastrar(novoUsuario);

                        return StatusCode(201, novoUsuario); 
                    }

                    return BadRequest("Usuário já existe");
                }

                return BadRequest("Todos as informações são obrigatórias");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Deleta um usuário existente
        /// </summary>
        /// <param name="id"> Id do usuário a ser deletado </param>
        /// <returns> Retorna um Status Code 202 - Accepted e o usuário deletado </returns>
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
