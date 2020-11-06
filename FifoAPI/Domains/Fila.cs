using System;
using System.Collections.Generic;

namespace FifoAPI.Domains
{
    public partial class Fila
    {
        public int Id { get; set; }
        public string Estado { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? IdAtividade { get; set; }
        public int? IdUsuario { get; set; }

        public Atividade IdAtividadeNavigation { get; set; }
        public Usuario IdUsuarioNavigation { get; set; }
    }
}
