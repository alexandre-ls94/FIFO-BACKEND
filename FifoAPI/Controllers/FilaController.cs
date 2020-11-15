using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FifoAPI.Domains;
using FifoAPI.Interfaces;
using FifoAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FifoAPI.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]
    
    [ApiController]
    public class FilaController : ControllerBase
    {
        private IFilaRepository _filaRepository;

        public FilaController()
        {
            _filaRepository = new FilaRepository();
        }

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
