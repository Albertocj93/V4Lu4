using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class ArchivoDTO
    {
        public string Tipo { get; set; }
        public Stream FulArchivo { get; set; }
        public string Nombre { get; set; }
    }
}
