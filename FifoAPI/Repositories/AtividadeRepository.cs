using FifoAPI.Domains;
using FifoAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FifoAPI.Repositories
{
    public class AtividadeRepository : IAtividadeRepository
    {
        FifoContext ctx = new FifoContext();

        FilaRepository _filaRepository = new FilaRepository();

        public void Atualizar(int id, Atividade atividadeAtualizada)
        {
            Atividade atividadeAtual = BuscarPorId(id);

            if (atividadeAtual != null)
            {
                atividadeAtual.Titulo = atividadeAtualizada.Titulo;
            }

            ctx.Atividade.Update(atividadeAtual);

            ctx.SaveChanges();
        }

        public Atividade BuscarPorId(int id)
        {
            return ctx.Atividade.FirstOrDefault(a => a.Id == id);
        }

        public void Cadastrar(Atividade atividade)
        {
            ctx.Atividade.Add(atividade);

            ctx.SaveChanges();
        }

        public void Deletar(int id)
        {
            Atividade atividadeBuscada = ctx.Atividade.Find(id);

            _filaRepository.DeletarRegistrosPorAtividade(id);

            ctx.Atividade.Remove(atividadeBuscada);

            ctx.SaveChanges();
        }

        public List<Atividade> Listar()
        {
            return ctx.Atividade.ToList();
        }
    }
}
