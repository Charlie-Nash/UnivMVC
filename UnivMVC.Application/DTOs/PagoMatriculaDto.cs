using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnivMVC.Application.DTOs
{
    public class PagoMatriculaDto
    {
        public int estudiante_id { get; set; }
        public int semestre_id { get; set; }
        public int categoria_id { get; set; }
    }
}
