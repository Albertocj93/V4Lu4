using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{

    public class ReporteBE
    {
        public string text { get; set; }
        public List<ReporteChildBE> children { get; set; }
    }

    public class ReporteChildBE
    {
        public string task { get; set; }
        public string duration { get; set; }
        public string iconCls { get; set; }
        public string expanded { get; set; }
        public bool leaf { get; set; }
        public int anho { get; set; }
        public int semestre { get; set; }
        public int trimestre { get; set; }
        public int mes { get; set; }
        public int dia { get; set; }
        public int idEmpresa { get; set; }
        public int idUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public List<ReporteChildBE> children { get; set; }
    }

}
