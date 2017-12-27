using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using Viatecla.Factory.Scriptor;
using Viatecla.Factory.Web;
using ScriptorModel = Viatecla.Factory.Scriptor.ModularSite.Models;
using BusinessEntities.DTO;
using Common;
using System.Data.SqlClient;
using System.Configuration;

namespace DataLayer
{
    public class REstadoDAL
    {
        public List<REstadoBE> ObtenerEstados()
        {
            ScriptorChannel canalEstado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Estado));

            //Obtener CeCos de la ultima version de la sociedad especificada
            List<ScriptorContent> listaEstado = canalEstado.QueryContents("#Id", Guid.NewGuid(), "<>").ToList();

            List<REstadoBE> oListaEstado = new List<REstadoBE>();
            REstadoBE oEstado;

            foreach (ScriptorContent item in listaEstado)
            {
                oEstado = new REstadoBE();

                oEstado.Id = item.Id;                
                oEstado.Descripcion = item.Parts.Descripcion;

                oListaEstado.Add(oEstado);

            }

            return oListaEstado;


        }

        public List<REstadoBE> ObtenerEstadosaAdministracion(bool EsContabilidad, bool EsAdministrador)
        {
            ScriptorChannel canalEstado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Estado));

            //Obtener CeCos de la ultima version de la sociedad especificada
            List<ScriptorContent> listaEstado = canalEstado.QueryContents("#Id", Guid.NewGuid(), "<>").ToList();

            List<REstadoBE> oListaEstado = new List<REstadoBE>();
            REstadoBE oEstado;

            foreach (ScriptorContent item in listaEstado)
            {
                oEstado = new REstadoBE();
                if (EsContabilidad && (item.Id.ToString().ToLower() == Estados.Aprobado.ToLower() || item.Id.ToString().ToLower() == Estados.Cerrado.ToLower()) && !EsAdministrador)
                {
                    
                    oEstado.Id = item.Id;
                    oEstado.Descripcion = item.Parts.Descripcion;
                    oListaEstado.Add(oEstado);
                }

                if (EsAdministrador)
                {
                    oEstado.Id = item.Id;
                    oEstado.Descripcion = item.Parts.Descripcion;
                    oListaEstado.Add(oEstado);
                }
                

            }

            return oListaEstado;


        }

        public List<REstado2BE> ObtenerEstado2(int rol)
        {
            List<REstado2BE> oLista = new List<REstado2BE>();
            REstado2BE oEstado2;

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerIdRol";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@Rol", rol);
                cmd.Parameters.Add(par1);

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        oEstado2 = new REstado2BE();
                        oEstado2.Id = new Guid(dataReader["Id"].ToString());
                        oEstado2.IdRol = dataReader["IdRol"].ToString();
                        oEstado2.Descripcion = dataReader["Descripcion"].ToString();

                        oLista.Add(oEstado2);

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
            return oLista;

        }
    }
}
