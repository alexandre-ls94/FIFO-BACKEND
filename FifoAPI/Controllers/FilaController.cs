using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FifoAPI.Domains;
using FifoAPI.Interfaces;
using FifoAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FifoAPI.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]
    
    [ApiController]
    [Authorize]
    public class FilaController : ControllerBase
    {
        private IFilaRepository _filaRepository;

        public FilaController()
        {
            _filaRepository = new FilaRepository();
        }

        /// <summary>
        /// Lista todos os itens da fila
        /// </summary>
        /// <returns> Retorna uma lista de itens da fila </returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_filaRepository.Listar());
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Busca um item da fila através do Id
        /// </summary>
        /// <param name="id"> Id do item buscado </param>
        /// <returns> Retorna um item da fila </returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                Fila registro = _filaRepository.BuscarPorId(id);

                if (registro != null)
                {
                    return Ok(registro);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return NotFound("Registro não encontrado");
        }

        /// <summary>
        /// Cadastra um item da fila
        /// </summary>
        /// <param name="novoRegistro"></param>
        /// <returns> Retorna um Status Code 201 - Created e o item cadastrado </returns>
        [HttpPost]
        public IActionResult Post(Fila novoRegistro)
        {
            try
            {
                novoRegistro.CreatedAt = DateTime.Now;

                _filaRepository.Cadastrar(novoRegistro);

                return StatusCode(201, novoRegistro);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Atualiza um item existente
        /// </summary>
        /// <param name="id"> Id do item a ser atualizado </param>
        /// <param name="registroAtualizado"></param>
        /// <returns> Retorna um Status Code 204 - No Content e o item atualizado </returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, Fila registroAtualizado)
        {
            try
            {
                Fila registroBuscado = _filaRepository.BuscarPorId(id);

                if (registroBuscado != null)
                {
                    _filaRepository.Atualizar(id, registroAtualizado);

                    return StatusCode(204, registroAtualizado);
                }

                return NotFound("Nenhum registro encontrado para o ID informado.");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Deleta um item da fila
        /// </summary>
        /// <param name="id"> Id do item a ser deletado </param>
        /// <returns> Retorna um Status Code 202 - Accepted e o item deletado </returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Fila registro = _filaRepository.BuscarPorId(id);

                if (registro != null)
                {
                    _filaRepository.Deletar(id);

                    return StatusCode(202, registro);
                }

                return NotFound("Nenhum registro encontrado para o ID informado.");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
