using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampEventos.Domain
{
    public class ApresentadorEvento
    {
        public int ApresentadorId { get; set; }
        public Apresentador Apresentador  { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; set; }


    }
}