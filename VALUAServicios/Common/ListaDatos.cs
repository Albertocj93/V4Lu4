using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    [Serializable]
    public class ListaDatos
    {
        public string Tipo { get; set; }
        public char EstadoCarga { get; set; }
        public List<ExcelDataDTO> RegistroDeArchivo { get; set; }
        public RespuestaDTO Respuestas { get; set; }
        public int NroColumnas { get; set; }//nuevo
    }
    [Serializable]
    public class RespuestaDTO
    {
        public RespuestaDTO()
        {
            this.ListaMensajes = new List<string>();
            this.ListaErrores = new List<string>();
        }
        public List<string> ListaMensajes { get; set; }
        public List<string> ListaErrores { get; set; }
    }
    [Serializable]
    public class ExcelDataDTO
    {
        public string Columna0 { get; set; }
        public string Columna1 { get; set; }
        public string Columna2 { get; set; }
        public string Columna3 { get; set; }
        public string Columna4 { get; set; }
        public string Columna5 { get; set; }
        public string Columna6 { get; set; }
        public string Columna7 { get; set; }
        public string Columna8 { get; set; }
        public string Columna9 { get; set; }
        public string Columna10 { get; set; }
        public string Columna11 { get; set; }
        public string Columna12 { get; set; }
        public string Columna13 { get; set; }
        public string Columna14 { get; set; }
        public string Columna15 { get; set; }
        public string Columna16 { get; set; }
        public string Columna17 { get; set; }
        public string Columna18 { get; set; }
        public string Columna19 { get; set; }
        public string Columna20 { get; set; }

    }
}
