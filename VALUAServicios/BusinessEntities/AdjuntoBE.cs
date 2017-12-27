using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class AdjuntoBE:BEBase
    {
        public string Id { get; set; }
        public string IdCarga { get; set; }
        public string NombreAdjunto { get; set; }
        public byte[] AdjuntoFisico { get; set; }
        public string RutaAdjunto { get; set; }
        public string AdjuntoFileType { get; set; }
        public int AdjuntoFileSize { get; set; }
        public int IdAdjunto { get; set; }
    }
}
