using Exo.WebApi.Contexts;
using Exo.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exo.WebApi.Repositories
{

    public class ProjetoRepository
    {

        private readonly ExoContext _context;

        public ProjetoRepository(ExoContext context)
        {

            _context = context;

        }

        public List<Projeto> Listar()
        {

            return _context.Projetos.ToList();

        }

       public Projeto BuscarporId(int Id)
        {

            return _context.Projetos.Find(Id);

        }

        public void Cadastrar(Projeto projeto)
        {

            _context.Projetos.Add(projeto);
            _context.SaveChanges();

        }

        public void Atualizar(int Id, Projeto projeto)
        {

            Projeto projetoBuscado = _context.Projetos.Find(Id);

            if (projetoBuscado != null)
            {

                projetoBuscado.NomeDoProjeto = projeto.NomeDoProjeto;
                projetoBuscado.Area = projeto.Area;
                projetoBuscado.Status = projeto.Status;

            }

            _context.Projetos.Update(projetoBuscado);
            _context.SaveChanges();

        }

        public void Deletar(int Id)
        {

            Projeto projetoBuscado = _context.Projetos.Find(Id);
            _context.Projetos.Remove(projetoBuscado);
            _context.SaveChanges();

        }

    }

}