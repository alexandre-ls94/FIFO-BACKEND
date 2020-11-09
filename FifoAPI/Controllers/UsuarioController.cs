﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FifoAPI.Domains;
using FifoAPI.Interfaces;
using FifoAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FifoAPI.Controllers
{
    public class UsuarioController : Controller
    {
        private IUsuarioRepository _usuarioRepository;

        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpGet]
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
                _usuarioRepository.Cadastrar(novoUsuario);

                return StatusCode(201, novoUsuario);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete]
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
