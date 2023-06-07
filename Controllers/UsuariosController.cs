using Exo.WebApi.Models;
using Exo.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Exo.WebApi.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]

    public class UsuariosController : ControllerBase
    {

        private readonly UsuarioRepository _usuarioRepository;

        public UsuariosController (UsuarioRepository usuarioRepository)
        {

            _usuarioRepository = usuarioRepository;

        }

        [HttpGet]
        public IActionResult Listar()
        {

            return Ok(_usuarioRepository.Listar());

        }

        [HttpGet("{Id}")]
        public IActionResult BuscaPorId(int Id)
        {

            Usuario usuario = _usuarioRepository.BuscaPorId(Id);

            if (usuario == null)
            {

                return NotFound();

            }
            else
            {

                return Ok(usuario);

            }

        }

        // [HttpPost]
        // public IActionResult Cadastrar(Usuario usuario)
        // {

        //     _usuarioRepository.Cadastrar(usuario);
        //     return StatusCode(201);

        // }

        public IActionResult Post(Usuario usuario)
        {

            Usuario usuarioBuscado = _usuarioRepository.Login(usuario.Email, usuario.Senha);

            if (usuarioBuscado == null)
            {

                return NotFound("Email ou Senha Inválidos");

            }

            var claims = new[]
            {
            
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),

                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.Id.ToString()),
            
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("exoapi-chave-autenticacao"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "exoapi.webapi",
                audience: "exoapi.webapi",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return Ok(
                new { token = new JwtSecurityTokenHandler().WriteToken(token) }
            );

        }

        [Authorize]
        [HttpPut("{Id}")]
        public IActionResult Atualizar(int Id, Usuario usuario)
        {

            _usuarioRepository.Atualizar(Id, usuario);
            return StatusCode(204);

        }

        [Authorize]
        [HttpDelete("{Id}")]
        public IActionResult Deletar(int Id)
        {

            try
            {

                _usuarioRepository.Deletar(Id);
                return StatusCode(204);

            }
            catch (Exception e)
            {

                return BadRequest();

            }

        }

    }

}