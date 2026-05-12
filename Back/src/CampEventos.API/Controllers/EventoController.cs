using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampEventos.Domain;
using CampEventos.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CampEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly CampEventosContext context;
        public EventosController(CampEventosContext context)
        {
            this.context = context;
        
        }

        [HttpGet]
    public ActionResult<IEnumerable<Evento>> Get()
    {
        return Ok(context.Eventos);
    }

    [HttpGet("{id}")]
    public ActionResult<Evento> GetById(int id)
    {
        var evento = context.Eventos.FirstOrDefault(e => e.Id == id);

        if (evento == null)
            return NotFound();

        return Ok(evento);
    }

        [HttpPost]
        public string Post()
        {
            return "Exemplo de Post";
        }

        [HttpPut("{id}")]
        public string Put()
        {
            return "Exemplo de Put";
        }
    }
}
