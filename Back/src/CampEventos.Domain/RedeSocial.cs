using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampEventos.Domain
{
    public class RedeSocial
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string URL { get; set; }
        public int? EventoId { get; set; }
        public Evento Evento { get; set; }
        public int? ApresentadorId { get; set; }
        public Apresentador Apresentador { get; set; }
    }
}