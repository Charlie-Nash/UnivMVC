using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UnivMVC.Domain.CtaCte
{
    public class PagoMatriculaResult
    {
        public int respuesta { get; set; }
        public string mensaje { get; set; } = "";

        public HttpStatusCode status { get; set; }
    }
}
