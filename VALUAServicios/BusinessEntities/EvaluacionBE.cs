using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class EvaluacionBE
    {
        public CompetenciaTBE CompetenciaT { get; set; }
        public CompetenciaGBE CompetenciaG { get; set; }
        public CompetenciaRHBE CompetenciaRH { get; set; }
        public int CompetenciaPTS { get; set; }
        public SolucionABE SolucionA { get; set; }
        public SolucionDBE SolucionD { get; set; }
        public int SolucionPorc { get; set; }
        public int SolucionPTS { get; set; }
        public ResponsabilidadABE ResponsabilidadA { get; set; }
        public ResponsabilidadMBE ResponsabilidadM { get; set; }
        public ResponsabilidadIBE ResponsabilidadI { get; set; }
        public int ResponsabilidadPTS { get; set; }
        public int Total { get; set; }
        public string Perfil { get; set; }
        public int Grado { get; set; }
        public int PuntoMedio { get; set; }

    }
}
