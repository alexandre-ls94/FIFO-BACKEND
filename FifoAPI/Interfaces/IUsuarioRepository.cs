using FifoAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FifoAPI.Interfaces
{
    interface IUsuarioRepository
    {
        List<Usuario> Listar();

        Usuario BuscarPorId(int id);

        Usuario BuscarPorNicknameSenha(string nickname, string senha);

        void Cadastrar(Usuario usuario);

        void Deletar(int id);
    }
}
