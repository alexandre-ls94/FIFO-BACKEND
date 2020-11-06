using System;
using System.Collections.Generic;

namespace FifoAPI.Domains
{
    public partial class Usuario
    {
        public Usuario()
        {
            Fila = new HashSet<Fila>();
        }

        public int Id { get; set; }
        public string Nickname { get; set; }

        public ICollection<Fila> Fila { get; set; }
    }
}
