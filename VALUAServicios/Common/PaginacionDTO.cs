using System;
using System.Collections.Generic;
using System.Linq;


namespace Common
{
    /// <summary>
    /// Clase para paginar los resultados de una búsqueda
    /// </summary>
    public class PaginacionDTO
    {
        /// <summary>
        /// Número de página
        /// Tipo: Int
        /// </summary>
        public int? page { get; set; }

        private int? m_rows;

        /// <summary>
        /// Número de resultados por página
        /// Tipo: Int
        /// </summary>
        public int? rows { get; set; }

        /// <summary>
        /// Campo por el que se va a ordenar
        /// Tipo: String
        /// Longitud: Variable
        /// </summary>
        public String sidx { get; set; }

        /// <summary>
        /// Tipo de ordenamiento [asc|desc]
        /// Tipo: String
        /// Longitud: Variable
        /// </summary>
        public String sord { get; set; }
        public Guid IdGrilla { get; set; }

        /// <summary>
        /// Obtiene el número de página
        /// Tipo: Int
        /// </summary>
        /// <returns></returns>
        public int GetNroPagina()
        {
            return page.Value;
        }

        /// <summary>
        /// obtiene el número de filas
        /// Tipo: int
        /// </summary>
        /// <returns></returns>
        public int GetNroFilas()
        {
            return rows.Value;
        }

        /// <summary>
        /// Obtiene la sentencia de ordenamiento en formato texto
        /// Tipo: String
        /// Longitud: Variable
        /// </summary>
        /// <param name="CampoDefecto"></param>
        /// <returns></returns>
        public String GetOrdenamiento(String CampoDefecto = "")
        {
            var salida = string.Empty;
            if (String.IsNullOrEmpty(sidx))
            {
                sidx = CampoDefecto;
            }
            if (String.IsNullOrEmpty(sord))
            {
                sord = "asc";
            }

            if (Convert.ToString(string.Empty + sidx).Length > 0 && Convert.ToString(string.Empty + sord).Length > 0)
            {
                salida = String.Format("{0} {1}", sidx, sord);
            }
            return salida;
        }

        /// <summary>
        /// si los resultados se devolverán todos los resultados o solo los que correspondan a la página consultada
        /// Tipo: Boolean
        /// </summary>
        public bool HabilitarPaginacion { get; set; }
    }

    public class OrdenamientoDTO
    {
        public string ColumnaOrdenamiento { get; set; }
        public string ColumnaOrden { get; set; }

        public int PaginaActual { get; set; }
        public int TamanoPagina { get; set; }

    }
}