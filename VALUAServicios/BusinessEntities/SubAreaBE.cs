using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class SubAreaBE:BEBase
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Sigla { get; set; }
        public int IdDepartamento { get; set; }
        public string Departamento { get; set; }
        public int IdArea { get; set; }
        public string Area { get; set; }
        public int IdEmpresa { get; set; }

    }
}
