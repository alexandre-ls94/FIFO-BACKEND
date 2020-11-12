using FifoAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FifoAPI.Interfaces
{
    interface IAtividadeRepository
    {
        List<Atividade> Listar();

        Atividade BuscarPorId(int id);

        void Cadastrar(Atividade atividade);

        void Deletar(int id);

        void Atualizar(int id, Atividade atividadeAtualizada);
    }
}
