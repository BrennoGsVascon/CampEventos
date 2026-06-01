using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampEventos.Application.Dtos
{
    public class ApresentadorUpdateDto
    {
        public int Id { get; set; }
        public string MiniCurriculo { get; set; }
        public int UserId { get; set; }
    }
}