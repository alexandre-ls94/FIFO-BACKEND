using FifoAPI.Domains;
using FifoAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FifoAPI.Repositories
{
    public class FilaRepository : IFilaRepository
    {
        FifoContext ctx = new FifoContext();

        public void Atualizar(int id, Fila registroAtualizado)
        {
            Fila registroAtual = BuscarPorId(id);

            if (registroAtual != null)
            {
                registroAtual.Estado = registroAtualizado.Estado;
                registroAtual.IdAtividade = registroAtualizado.IdAtividade;
                registroAtual.IdUsuario = registroAtualizado.IdUsuario;
            }

            ctx.Fila.Update(registroAtual);

            ctx.SaveChanges();
        }

        public Fila BuscarPorId(int id)
        {
            return ctx.Fila.FirstOrDefault(f => f.Id == id);
        }

        public void Cadastrar(Fila fila)
        {
            ctx.Fila.Add(fila);

            ctx.SaveChanges();
        }

        public void Deletar(int id)
        {
            Fila registroBuscado = ctx.Fila.Find(id);

            ctx.Fila.Remove(registroBuscado);

            ctx.SaveChanges();
        }

        public List<Fila> Listar()
        {
            return ctx.Fila
                .Include(u => u.IdUsuarioNavigation)
                .Include(a => a.IdAtividadeNavigation)
                .OrderBy(f => f.CreatedAt)
                .ToList();
        }
    }
}
