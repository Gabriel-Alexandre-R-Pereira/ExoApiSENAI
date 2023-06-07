using Exo.WebApi.Models;
using Exo.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Exo.WebApi.Controllers
{

    [Route("api/[controller]")]

    [ApiController]

    public class ProjetosController : ControllerBase

    {

        private readonly ProjetoRepository _projetoRepository;

        public ProjetosController(ProjetoRepository projetoRepository)
        {

            _projetoRepository = projetoRepository;

        }

        [HttpGet]
        public IActionResult Listar()
        {

            return Ok(_projetoRepository.Listar());

        }

        [HttpGet("{Id}")]
        public IActionResult BuscarporId(int Id)
        {

            Projeto projeto = _projetoRepository.BuscarporId(Id);

            if (projeto == null)
            {

                return NotFound();

            }

            return Ok(projeto);

        }

        [HttpPost]
        public IActionResult Cadastrar(Projeto projeto)
        {

            _projetoRepository.Cadastrar(projeto);
            return StatusCode(201);

        }

        [HttpPut("{Id}")]
        public IActionResult Atualizar(int Id, Projeto projeto)
        {

            _projetoRepository.Atualizar(Id, projeto);
            return StatusCode(204);

        }

        [HttpDelete("{Id}")]
        public IActionResult Deletar(int Id)
        {

            try
            {

                _projetoRepository.Deletar(Id);
                return StatusCode(204);

            }
            catch (Exception e)
            {

                return BadRequest();

            }

        }

    }

}