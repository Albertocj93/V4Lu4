using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class PuestoBE
    {
        public int Id { get; set; }
        public int IdEstado { get; set; }
        public string Estado { get; set; }
        public int IdAdjunto { get; set; }
        public string NombreAdjunto { get; set; }
        public byte[] AdjuntoFisico { get; set; }
        public int IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public int IdPais { get; set; }
        public string Pais { get; set; }
        public string TituloPuesto { get; set; }
        public int IdDepartamento { get; set; }
        public string Departamento { get; set; }
        public int IdArea { get; set; }
        public string Area { get; set; }
        public int IdSubArea { get; set; }
        public string SubArea { get; set; }
        public string NombreOcupante { get; set; }
        public string Grado { get; set; }
        public int IdCompetenciaT { get; set; }
        public string CompetenciaT { get; set; }
        public int IdCompetenciaG { get; set; }
        public string CompetenciaG { get; set; }
        public int IdCompetenciaRH { get; set; }
        public string CompetenciaRH { get; set; }
        public string CompetenciaPTS { get; set; }
        public int IdSolucionA { get; set; }
        public string SolucionA { get; set; }
        public int IdSolucionD { get; set; }
        public string SolucionD { get; set; }
        public string SolucionPorc { get; set; }
        public string SolucionPTS { get; set; }
        public int IdResponsabilidadA { get; set; }
        public string ResponsabilidadA { get; set; }
        public int IdResponsabilidadM { get; set; }
        public string ResponsabilidadM { get; set; }
        public int IdResponsabilidadI { get; set; }
        public string ResponsabilidadI { get; set; }
        public string ResponsabilidadPTS { get; set; }
        public string Total { get; set; }
        public string Perfil { get; set; }
        public string PuntoMedio { get; set; }
        public string Magnitud { get; set; }
        public string Comentario { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreador { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioElimino { get; set; }
        public DateTime FechaEliminacion { get; set; }
        public string CodigoFuncion { get; set; }
        public string CodigoOcupante { get; set; }
        public string CodigoValua { get; set; }
        public int Activo { get; set; }

    }
}
