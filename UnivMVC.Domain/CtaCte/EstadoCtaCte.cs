using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnivMVC.Domain.CtaCte
{
    public class EstadoCtaCte
    {
        public string semestre { get; set; } = "";
        public int nro { get; set; }
        public DateTime fecha_vencimiento { get; set; }
        public decimal importe { get; set; }
        public string pagado { get; set; } = "";
        public int cuenta_id { get; set; }
        public int compromiso_id { get; set; }
        public int costo_id { get; set; }
        public string concepto { get; set; } = "";
        public string categoria { get; set; } = "";
    }
}
