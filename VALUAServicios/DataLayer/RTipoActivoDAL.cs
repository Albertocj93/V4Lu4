using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viatecla.Factory.Scriptor;
using Viatecla.Factory.Web;
using ScriptorModel = Viatecla.Factory.Scriptor.ModularSite.Models;
using BusinessEntities;
using Common;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataLayer
{
    public class RTipoActivoDAL
    {
        public List<RTipoActivoBE> ObtenerTiposActivo()
        {
            List<RTipoActivoBE> salida = new List<RTipoActivoBE>();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerTipoActivo";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        RTipoActivoBE TipoActivoBE = new RTipoActivoBE();
                        LlenarEntidadTipoActivo(TipoActivoBE, dataReader);
                        salida.Add(TipoActivoBE);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return salida;
        }

        public RTipoActivoBE ExisteTipoActivo(string descripcion)
        {
            
            RTipoActivoBE resultado = new RTipoActivoBE();
            ScriptorChannel canalTipoActivo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoActivo));
            List<ScriptorContent> TiposActivo = canalTipoActivo.QueryContents("#Id", Guid.NewGuid(), "<>").QueryContents("Descripcion", descripcion, "==").ToList();
            if (TiposActivo.Count > 0)
            {
                ScriptorContent item = TiposActivo[0];
                resultado.Id = item.Id;
                resultado.Descripcion = item.Parts.Descripcion;
                resultado.Codigo = item.Parts.Codigo;
            }
            return resultado;
        }
        public RTipoActivoBE ExisteTipoActivoBD(string descripcion)
        {
            RTipoActivoBE TipoActivoBE = new RTipoActivoBE();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerTipoActivoPorDescripcion";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@Descripcion", descripcion);
                cmd.Parameters.Add(par1);
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        LlenarEntidadTipoActivo(TipoActivoBE, dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return TipoActivoBE;
        }
        private void LlenarEntidadTipoActivo(RTipoActivoBE item, IDataReader iDataReader)
        {
            if (!Convert.IsDBNull(iDataReader["Id"]))
            {
                item.Id = (Guid)(iDataReader["Id"]);
            }
            if (!Convert.IsDBNull(iDataReader["Codigo"]))
            {
                item.Codigo = Convert.ToString(iDataReader["Codigo"]);
            }
            if (!Convert.IsDBNull(iDataReader["Descripcion"]))
            {
                item.Descripcion = Convert.ToString(iDataReader["Descripcion"]);
            }
        }
    }
}
