﻿using System.Net;

namespace UnivMVC.Domain.Academico
{
    public class Estudiante
    {
        public int id { get; set; }
        public string sede { get; set; } = "";
        public string facultad { get; set; } = "";
        public string programa { get; set; } = "";
        public int plan { get; set; }
        public int curricula_id { get; set; }
        public int categoria_id { get; set; }
        public int semestre_id { get; set; }
        public string semestre { get; set; } = "";
        public int matricula_id { get; set; }

        public HttpStatusCode status { get; set; }
        public string mensaje { get; set; } = "";
    }
}
