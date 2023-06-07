using Exo.WebApi.Contexts;
using Exo.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exo.WebApi.Repositories
{

    public class UsuarioRepository
    {

        private readonly ExoContext _context;

        public UsuarioRepository(ExoContext context)
        {

            _context = context;

        }

        public Usuario Login(string email, string senha)
        {

            return _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);

        }

        public List<Usuario> Listar()
        {

            return _context.Usuarios.ToList();

        }

        public Usuario BuscaPorId(int Id)
        {

            return _context.Usuarios.Find(Id);

        }

        public void Cadastrar(Usuario usuario)
        {

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

        }

        public void Atualizar(int Id, Usuario usuario)
        {

            Usuario usuarioBuscado = _context.Usuarios.Find(Id);

            if (usuarioBuscado != null)
            {

                usuarioBuscado.Email = usuario.Email;
                usuarioBuscado.Senha = usuario.Senha;

            }

            _context.Usuarios.Update(usuarioBuscado);
            _context.SaveChanges();

        }

        public void Deletar(int Id)
        {

            Usuario usuarioBuscado = _context.Usuarios.Find(Id);

            _context.Usuarios.Remove(usuarioBuscado);
            _context.SaveChanges();

        }

    }

}