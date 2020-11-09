using FifoAPI.Domains;
using FifoAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FifoAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        FifoContext ctx = new FifoContext();

        public Usuario BuscarPorId(int id)
        {
            return ctx.Usuario.FirstOrDefault(u => u.Id == id);
        }

        public void Cadastrar(Usuario usuario)
        {
            ctx.Usuario.Add(usuario);

            ctx.SaveChanges();
        }

        public void Deletar(int id)
        {
            Usuario usuarioBuscado = ctx.Usuario.Find(id);

            ctx.Usuario.Remove(usuarioBuscado);

            ctx.SaveChanges();
        }

        public List<Usuario> Listar()
        {
            return ctx.Usuario.ToList();
        }
    }
}
