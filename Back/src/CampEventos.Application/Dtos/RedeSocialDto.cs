using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampEventos.Application.Dtos
{
    public class RedeSocialDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string URL { get; set; }
        public int? EventoId { get; set; }
        public EventoDto Evento { get; set; }
        public int? ApresentadorId { get; set; }
        public ApresentadorDto Apresentador { get; set; }
    }
}