using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viatecla.Factory.Scriptor;
using Viatecla.Factory.Web;
using ScriptorModel = Viatecla.Factory.Scriptor.ModularSite.Models;
using BusinessEntities;
using BusinessEntities.DTO;
using Common;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace DataLayer
{
    public class RTipoCapexDAL
    {
        public List<RTipoCapexBE> ObtenerTipoCapex()
        {
            ScriptorChannel canalTipoCapex = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoCapex));

            
            List<ScriptorContent> TiposCapex = canalTipoCapex.QueryContents("#Id", Guid.NewGuid(), "<>").ToList();

            List<RTipoCapexBE> oListaTipoCapex = new List<RTipoCapexBE>();

            RTipoCapexBE oTipoCapex;

            foreach (ScriptorContent item in TiposCapex)
            {
                oTipoCapex = new RTipoCapexBE();

                oTipoCapex.Id = item.Id;
                oTipoCapex.Codigo = item.Parts.Codigo;
                oTipoCapex.Descripcion = item.Parts.Descripcion;
                oListaTipoCapex.Add(oTipoCapex);
            }

            return oListaTipoCapex;
        }

        public List<ListarCapexPublicadosDTO> ObtenerTipoCapexDTO()
        {
            ScriptorChannel canalTipoCapex = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoCapex));


            List<ScriptorContent> ListaTiposCapex = canalTipoCapex.QueryContents("#Id", Guid.NewGuid(), "<>").ToList();

            List<ListarCapexPublicadosDTO> oListaTipoCapex = new List<ListarCapexPublicadosDTO>();

            ListarCapexPublicadosDTO oTipoCapex;

            foreach (ScriptorContent item in ListaTiposCapex)
            {
                oTipoCapex = new ListarCapexPublicadosDTO();

                oTipoCapex.Id = item.Id.ToString().ToLower();
                oTipoCapex.Codigo = item.Parts.Codigo;
                oTipoCapex.Descripcion = item.Parts.Descripcion;

                if ((oTipoCapex.Id).ToString().ToUpper() == TiposCapex.Ahorro.ToUpper() || (oTipoCapex.Id).ToString().ToUpper() == TiposCapex.Ingreso.ToUpper())
                {
                    oTipoCapex.FlagEvaluacionInversion = 1;
                }
                else
                {
                    oTipoCapex.FlagEvaluacionInversion = 0; 
                }
                oListaTipoCapex.Add(oTipoCapex);
            }

            return oListaTipoCapex;
        }

        public int ValidarEvaluacionEconomica(string CodigoInversion)
        {
            int res = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ValidarEvaluacionEconomica";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@CodigoInversion", CodigoInversion);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        res = Convert.ToInt32(dataReader["Respuesta"]);

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
            return res;
        }

        public RTipoCapexBE ExisteTipoCapexBD(string descripcion)
        {
            RTipoCapexBE TipoCapexBE = new RTipoCapexBE();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerTipoCapexPorDescripcion";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@Descripcion", descripcion);
                cmd.Parameters.Add(par1);
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        LlenarEntidadTipoCapex(TipoCapexBE, dataReader);
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
            return TipoCapexBE;
        }

        private void LlenarEntidadTipoCapex(RTipoCapexBE item, IDataReader iDataReader)
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
