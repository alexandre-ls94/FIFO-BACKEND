using FifoAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FifoAPI.Interfaces
{
    interface IFilaRepository
    {
        List<Fila> Listar();

        Fila BuscarPorId(int id);

        void Cadastrar(Fila fila);

        void Deletar(int id);

        void Atualizar(int id, Fila filaAtualizada);
    }
}
