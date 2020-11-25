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
    //[Authorize]
    public class AtividadeController : ControllerBase
    {
        private IAtividadeRepository _atividadeRepository;

        public AtividadeController()
        {
            _atividadeRepository = new AtividadeRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_atividadeRepository.Listar());
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
                Atividade atividade = _atividadeRepository.BuscarPorId(id);

                if (atividade != null)
                {
                    return Ok(atividade);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return NotFound("Atividade não encontrada");
        }

        [HttpPost]
        public IActionResult Post(Atividade novaAtividade)
        {
            try
            {
                _atividadeRepository.Cadastrar(novaAtividade);

                return StatusCode(201, novaAtividade);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("{id}")]

        public IActionResult Put(int id, Atividade atividadeAtualizada)
        {
            try
            {
                Atividade atividadeBuscada = _atividadeRepository.BuscarPorId(id);

                if (atividadeBuscada != null)
                {
                    _atividadeRepository.Atualizar(id, atividadeAtualizada);

                    return StatusCode(204, atividadeAtualizada);
                }

                return NotFound("Nenhuma atividade encontrada para o ID informado.");
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
                Atividade atividade = _atividadeRepository.BuscarPorId(id);

                if (atividade != null)
                {
                    _atividadeRepository.Deletar(id);

                    return StatusCode(202, atividade);
                }

                return NotFound("Nenhuma atividade encontrada para o ID informado.");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
