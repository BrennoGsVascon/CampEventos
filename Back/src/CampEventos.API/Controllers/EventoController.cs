using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampEventos.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CampEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly DataContext context;
        public EventosController(DataContext context)
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
        var evento = context.Eventos.FirstOrDefault(e => e.EventoId == id);

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
