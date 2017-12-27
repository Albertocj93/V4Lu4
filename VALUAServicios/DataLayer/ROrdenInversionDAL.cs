using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Viatecla.Factory.Scriptor;
using Viatecla.Factory.Web;
using ScriptorModel = Viatecla.Factory.Scriptor.ModularSite.Models;
using BusinessEntities;
using Common;
using Microsoft.SqlServer.Server;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace DataLayer
{
    public class ROrdenInversionDAL
    {
        public List<ROrdenInversionBE> ObtenerOrdenInversion()
        {
            ScriptorChannel canalOrdenInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.IdCanalOrdenInversionTemp));
            //Obtener CeCos de la ultima version de la sociedad especificada
            List<ScriptorContent> listaOrdenInversion = canalOrdenInversion.QueryContents("#Id", Guid.NewGuid(), "<>")
                                                       .QueryContents("Version", ObtenerUltimaVersionMaestro(Canales.IdCanalOrdenInversionTemp), "=").ToList();
            List<ROrdenInversionBE> oListaOrdenInversion = new List<ROrdenInversionBE>();
            ROrdenInversionBE oOrdenInversion;
            foreach (ScriptorContent item in listaOrdenInversion)
            {
                List<ScriptorContentInsertContent> lstpopups = ((ScriptorContentInsert)item.Parts.SeccionesPopup).ToList();
                
                oOrdenInversion = new ROrdenInversionBE();
                oOrdenInversion.Id = item.Id;
                oOrdenInversion.NroAPI = item.Parts.NroAPI;
                oOrdenInversion.NroOI = item.Parts.NroOI;
                oOrdenInversion.DescripcionOI = item.Parts.DescripcionOI;
                oOrdenInversion.TipoActivo = item.Parts.TipoActivo;
                oOrdenInversion.Monto = item.Parts.Monto;
                oOrdenInversion.IdSolicitudInversion = item.Parts.IdSolicitudInversion;
                oListaOrdenInversion.Add(oOrdenInversion);
            }
            return oListaOrdenInversion;
        }
        public List<ROrdenInversionBE> ObtenerOrdenInversionBD()
        {
            List<ROrdenInversionBE> oListaOrdenInversion = new List<ROrdenInversionBE>();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerListaOrdenInversionTemp";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        ROrdenInversionBE OrdenInversionBE = new ROrdenInversionBE();
                        LlenarEntidadOrdenInversion(OrdenInversionBE, dataReader);
                        oListaOrdenInversion.Add(OrdenInversionBE);
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
            return oListaOrdenInversion;
        }
        public List<ROrdenInversionBE> ObtenerOrdenInversionPorNroOIBD(string nroOI)
        {
            List<ROrdenInversionBE> oListaOrdenInversion = new List<ROrdenInversionBE>();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerOrdenInversionPorOI";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@NroOI", nroOI);
                cmd.Parameters.Add(par1);
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        ROrdenInversionBE OrdenInversionBE = new ROrdenInversionBE();
                        LlenarEntidadOrdenInversion(OrdenInversionBE, dataReader);
                        oListaOrdenInversion.Add(OrdenInversionBE);
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
            return oListaOrdenInversion;
        }
        public bool GrabarOrdenInversion(List<Dictionary<string,string>> lstDatos)
        {
            bool resultado = true;
            var datos = new TipoListaOrdenInversionCollection();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            foreach (Dictionary<string,string> item in lstDatos)
            {
                datos.Add(new ListaOrdenInversion(item["NroAPI"],item["TipoActivo"],item["NroOI"],item["DescripcionOI"],item["Monto"],
                                                  item["IdSolicitudInversion"],item["CodProyecto"],new Guid(Canales.IdEsquemaOrdenInversionTemp),
                            new Guid(Canales.IdCreatorOrdenInversionTemp), ConfigurationManager.AppSettings["EstadoFlujo"].ToString()
                            , new Guid(Canales.IdCanalOrdenInversionTemp)));
            }
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "InsertarRegistrosOrdenInversion";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@ListaOrdenInversion", SqlDbType.Structured);//, SqlDbType.Structured,);
                par1.TypeName = "dbo.ListaOrdenInversion";
                par1.Value = datos;
                cmd.Parameters.Add(par1);
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    resultado = true;
                    while (dataReader.Read())
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                throw ex;
            }

            finally
            {
                con.Close();
            }
            return resultado;
        }

        public bool ActualizarOrdenInversion(List<ROrdenInversionBE> lstDatos)
        {
            bool resultado = true;
            var datos = new TipoListaOrdenInversionCollection();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            foreach (ROrdenInversionBE item in lstDatos)
            {
                //datos.Add(new ListaCoordinadorTemp(item["CodCeCo"],item["DescCeCo"],item["Coordinador"],new Guid(CannotUnloadAppDomainException)
                datos.Add(new ListaOrdenInversion(item.NroAPI, item.TipoActivo, item.NroOI, item.DescripcionOI, item.Monto,
                                                    item.IdSolicitudInversion, item.CodProyecto, new Guid(Canales.IdEsquemaOrdenInversionTemp),
                            new Guid(Canales.IdCreatorOrdenInversionTemp), ConfigurationManager.AppSettings["EstadoFlujo"].ToString()
                            , new Guid(Canales.IdCanalOrdenInversionTemp)));
            }
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ActualizarSolicitudInversion";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@ListaOrdenInversion", SqlDbType.Structured);//, SqlDbType.Structured,);
                par1.TypeName = "dbo.ListaOrdenInversion";
                par1.Value = datos;
                cmd.Parameters.Add(par1);
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    resultado = true;
                    while (dataReader.Read())
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return resultado;
        }
        public bool ActualizarEstadoOrdenInversion(List<ROrdenInversionBE> lstDatos)
        {
            bool resultado = true;
            var datos = new TipoListaOrdenInversionCollection();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            foreach (ROrdenInversionBE item in lstDatos)
            {
                //datos.Add(new ListaCoordinadorTemp(item["CodCeCo"],item["DescCeCo"],item["Coordinador"],new Guid(CannotUnloadAppDomainException)
                datos.Add(new ListaOrdenInversion(item.NroAPI, item.TipoActivo, item.NroOI, item.DescripcionOI, item.Monto,
                                                    item.IdSolicitudInversion, item.CodProyecto, new Guid(Canales.IdEsquemaOrdenInversionTemp),
                            new Guid(Canales.IdCreatorOrdenInversionTemp), ConfigurationManager.AppSettings["EstadoFlujo"].ToString()
                            , new Guid(Canales.IdCanalOrdenInversionTemp)));
            }
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ActualizarEstadoSolicitudInversion";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@ListaOrdenInversion", SqlDbType.Structured);//, SqlDbType.Structured,);
                par1.TypeName = "dbo.ListaOrdenInversion";
                par1.Value = datos;
                cmd.Parameters.Add(par1);
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    resultado = true;
                    while (dataReader.Read())
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return resultado;
        }
        public class TipoListaOrdenInversionCollection : List<ListaOrdenInversion>, IEnumerable<SqlDataRecord>
        {
            IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
            {

                var sqlDataRecord = new SqlDataRecord(  new SqlMetaData("NroAPI", SqlDbType.Text),
                new SqlMetaData("TipoActivo", SqlDbType.Text),  new SqlMetaData("NroOI", SqlDbType.Text),
                new SqlMetaData("DescripcionOI", SqlDbType.Text), new SqlMetaData("Monto", SqlDbType.Text),
                new SqlMetaData("CodProyecto", SqlDbType.Text),new SqlMetaData("IdSolicitudInversion", SqlDbType.Text),
                new SqlMetaData("IdEsquema", SqlDbType.UniqueIdentifier), new SqlMetaData("IdCreator", SqlDbType.UniqueIdentifier),
                new SqlMetaData("EstadoFlujo", SqlDbType.Text), new SqlMetaData("IdCanal", SqlDbType.UniqueIdentifier));

                foreach (ListaOrdenInversion ListaMaestroTempitem in this)
                {
                    sqlDataRecord.SetString(0, ListaMaestroTempitem.NroAPI);
                    sqlDataRecord.SetString(1, ListaMaestroTempitem.TipoActivo);
                    sqlDataRecord.SetString(2, ListaMaestroTempitem.NroOI);
                    sqlDataRecord.SetString(3, ListaMaestroTempitem.DescripcionOI);
                    sqlDataRecord.SetString(4, ListaMaestroTempitem.Monto);
                    sqlDataRecord.SetString(5, ListaMaestroTempitem.CodProyecto);
                    sqlDataRecord.SetString(6, ListaMaestroTempitem.IdSolicitudInversion);
                    sqlDataRecord.SetGuid(7, ListaMaestroTempitem.IdEsquema);
                    sqlDataRecord.SetGuid(8, ListaMaestroTempitem.IdCreator);
                    sqlDataRecord.SetString(9, ListaMaestroTempitem.EstadoFlujo);
                    sqlDataRecord.SetGuid(10, ListaMaestroTempitem.IdCanal);

                    yield return sqlDataRecord;
                }
            }
        }
        public int ObtenerUltimaVersionMaestro(string idCanal)
        {
            ScriptorChannel canalPlanBase = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(idCanal));

            //Obtener ultima version del canal de maestros
            int maxVersion = 1;
            var versionMax = canalPlanBase.QueryContents("#Id", Guid.NewGuid(), "<>").OrderByDescending(x => x.Parts.Version).Select(x => x.Parts.Version).ToList().FirstOrDefault();
            if (versionMax != null)
            {
                maxVersion = versionMax;
            }
            return maxVersion;
        }
        private void LlenarEntidadOrdenInversion(ROrdenInversionBE item, IDataReader iDataReader)
        {
            if (!Convert.IsDBNull(iDataReader["Id"]))
            {
                item.Id = (Guid)(iDataReader["Id"]);
            }
            if (!Convert.IsDBNull(iDataReader["NroAPI"]))
            {
                item.NroAPI = Convert.ToString(iDataReader["NroAPI"]);
            }
            if (!Convert.IsDBNull(iDataReader["NroOI"]))
            {
                item.NroOI = Convert.ToString(iDataReader["NroOI"]);
            }
            if (!Convert.IsDBNull(iDataReader["DescripcionOI"]))
            {
                item.DescripcionOI = Convert.ToString(iDataReader["DescripcionOI"]);
            }
            if (!Convert.IsDBNull(iDataReader["TipoActivo"]))
            {
                item.TipoActivo = Convert.ToString(iDataReader["TipoActivo"]);
            }
            if (!Convert.IsDBNull(iDataReader["Monto"]))
            {
                item.Monto = Convert.ToString(iDataReader["Monto"]);
            }
            if (!Convert.IsDBNull(iDataReader["IdSolicitudInversion"]))
            {
                item.IdSolicitudInversion = Convert.ToString(iDataReader["IdSolicitudInversion"]);
            }
            if (!Convert.IsDBNull(iDataReader["CodProyecto"]))
            {
                item.CodProyecto = Convert.ToString(iDataReader["CodProyecto"]);
            }
        }
    }
}
