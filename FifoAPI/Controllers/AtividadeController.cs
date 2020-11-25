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
    public class AtividadeController : ControllerBase
    {
        private IAtividadeRepository _atividadeRepository;

        public AtividadeController()
        {
            _atividadeRepository = new AtividadeRepository();
        }

        /// <summary>
        /// Lista todas as atividades
        /// </summary>
        /// <returns> Retorna uma lista de atividades </returns>
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

        /// <summary>
        /// Busca uma atividade através do Id
        /// </summary>
        /// <param name="id"> Id da atividade que será buscada </param>
        /// <returns> Retorna uma atividade buscada </returns>
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

        /// <summary>
        /// Cadastra uma nova atividade
        /// </summary>
        /// <param name="novaAtividade"></param>
        /// <returns> Retorna um Status Code 201 - Created </returns>
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

        /// <summary>
        /// Atualiza uma atividade existente
        /// </summary>
        /// <param name="id"> Id da atividade a ser atualizada</param>
        /// <param name="atividadeAtualizada"></param>
        /// <returns> Retorna um Status Code 204 - No Content e a atividade atualizada </returns>
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

        /// <summary>
        /// Deleta uma atividade existente
        /// </summary>
        /// <param name="id"> Id da atividade a ser deletada </param>
        /// <returns> Retorna um Status Code 202 - Accepted e a atividade deletada </returns>
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
