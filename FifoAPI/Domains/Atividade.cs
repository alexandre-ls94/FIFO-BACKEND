using System;
using System.Collections.Generic;

namespace FifoAPI.Domains
{
    public partial class Atividade
    {
        public Atividade()
        {
            Fila = new HashSet<Fila>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public int? JogadoresPorVez { get; set; }

        public ICollection<Fila> Fila { get; set; }
    }
}
