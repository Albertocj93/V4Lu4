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
    public class RPlanBaseDAL
    {
        public List<RPlanBaseBE> ObtenerPlanBase()
        {
            ScriptorChannel canalPlanBase = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Inversion));
            //Obtener CeCos de la ultima version de la sociedad especificada
            List<ScriptorContent> listaPlanBase = canalPlanBase.QueryContents("#Id", Guid.NewGuid(), "<>")
                                            .QueryContents("Version", ObtenerUltimaVersionMaestro(Canales.Inversion), "=").ToList();
            List<RPlanBaseBE> oListaPlanBase = new List<RPlanBaseBE>();
            RPlanBaseBE oPlanBase;
            foreach (ScriptorContent item in listaPlanBase)
            {
                oPlanBase = new RPlanBaseBE();
                ScriptorContent itemCeCo = ((ScriptorDropdownListValue)item.Parts.IdCeCo).Content;
                oPlanBase.CodCeCo = itemCeCo.Parts.CodCECO;
                oPlanBase.DescCeCo = itemCeCo.Parts.DescCECO;
                oPlanBase.CodProyecto = item.Parts.CodigoProyecto;
                oPlanBase.NomProyecto = item.Parts.NombreProyecto;
                oPlanBase.TipoCapex = ((ScriptorDropdownListValue)item.Parts.IdTipoCapex).Content.Parts.Descripcion;
                oPlanBase.TipoActivo = ((ScriptorDropdownListValue)item.Parts.IdTipoActivo).Content.Parts.Descripcion;
                oPlanBase.NroOI = item.Parts.CodigoOI;
                oPlanBase.DescOI = item.Parts.DescripcionOI;
                oPlanBase.MontoBase = item.Parts.MontoContable;
                oListaPlanBase.Add(oPlanBase);   
            }
            return oListaPlanBase;
        }
        public List<RPlanBaseBE> ObtenerPlanBaseBD()
        {
            List<RPlanBaseBE> oListaPlanBase = new List<RPlanBaseBE>();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerListaPlanBase";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        RPlanBaseBE PlanBaseBE = new RPlanBaseBE();
                        LlenarEntidadPlanBaseBD(PlanBaseBE, dataReader);
                        oListaPlanBase.Add(PlanBaseBE);
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
            return oListaPlanBase;
        }
        public List<RPlanBaseBE> ObtenerPlanBaseTemp()
        {
            ScriptorChannel canalPlanBaseTemp = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.PlanBaseTemp));
            //Obtener CeCos de la ultima version de la sociedad especificada
            List<ScriptorContent> listaPlanBaseTemp = canalPlanBaseTemp.QueryContents("#Id", Guid.NewGuid(), "<>").QueryContents("Version", ObtenerUltimaVersionMaestro(Canales.PlanBaseTemp), "=").ToList();
            List<RPlanBaseBE> oListaPlanBaseTemp = new List<RPlanBaseBE>();
            RPlanBaseBE oPlanBaseTemp;
            foreach (ScriptorContent item in listaPlanBaseTemp)
            {
                oPlanBaseTemp = new RPlanBaseBE();
                oPlanBaseTemp.CodCeCo = item.Parts.CodCeCo;
                oPlanBaseTemp.CodProyecto = item.Parts.CodProyecto;
                oPlanBaseTemp.NomProyecto = item.Parts.NomProyecto;
                oPlanBaseTemp.TipoCapex = item.Parts.TipoCapex;
                oPlanBaseTemp.TipoActivo = item.Parts.TipoActivo;
                oPlanBaseTemp.NroOI = item.Parts.NroOI;
                oPlanBaseTemp.DescOI = item.Parts.DescOI;
                oPlanBaseTemp.MontoBase = item.Parts.MontoBase;
                oPlanBaseTemp.CodigoSociedad = item.Parts.CodSociedad;
                oPlanBaseTemp.DescripcionSociedad = item.Parts.DescSociedad;
                oPlanBaseTemp.CodigoSector = item.Parts.CodSector;
                oPlanBaseTemp.DescripcionSector = item.Parts.DescSector;
                oPlanBaseTemp.CodigoMacroServicio = item.Parts.CodMacroservicio;
                oPlanBaseTemp.DescripcionMacroServicio = item.Parts.DescMacroservio;
                oListaPlanBaseTemp.Add(oPlanBaseTemp);
            }
            return oListaPlanBaseTemp;//oListaCentroCosto;
        }
        public List<RPlanBaseBE> ObtenerPlanBaseTempBD()
        {
            List<RPlanBaseBE> oListaPlanBase = new List<RPlanBaseBE>();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerListaPlanBaseTemp";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        RPlanBaseBE PlanBaseBE = new RPlanBaseBE();
                        LlenarEntidadPlanBaseTemp(PlanBaseBE, dataReader);
                        oListaPlanBase.Add(PlanBaseBE);
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
            return oListaPlanBase;
        }
        public List<RPlanBaseBE> ObtenerPlanBasePorCodigoProyecto(string codigoProyecto)
        { 
            ScriptorChannel canalPlanBase = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Inversion));
           
            List<ScriptorContent> listaPlanBase = canalPlanBase.QueryContents("#Id", Guid.NewGuid(), "<>")
                                                               .QueryContents("Version", ObtenerUltimaVersionMaestro(Canales.Inversion), "=")
                                                               .QueryContents("CodigoProyecto", codigoProyecto, "==").ToList();

            List<RPlanBaseBE> oListaPlanBase = new List<RPlanBaseBE>();
            RPlanBaseBE oPlanBase;

            foreach (ScriptorContent item in listaPlanBase)
            {
                oPlanBase = new RPlanBaseBE();
                oPlanBase.Id = item.Id;
                oPlanBase.CodCeCo = ((ScriptorDropdownListValue)item.Parts.IdCeCo).Content.Parts.CodCECO;
                oPlanBase.CodProyecto = item.Parts.CodProyecto;
                oPlanBase.NomProyecto = item.Parts.NomProyecto;
                oPlanBase.TipoCapex = item.Parts.TipoCapex;
                oPlanBase.TipoActivo = item.Parts.TipoActivo;
                oPlanBase.NroOI = item.Parts.CodigoOI;
                oPlanBase.DescOI = item.Parts.DescOI;
                oPlanBase.MontoBase = item.Parts.MontoContable;
                oListaPlanBase.Add(oPlanBase);   
            }
            return oListaPlanBase;
        }
        public List<RPlanBaseBE> ObtenerPlanBasePorCodigoProyectoBD(string codigoProyecto)
        {
            List<RPlanBaseBE> oListaPlanBase = new List<RPlanBaseBE>();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerListaPlanBasePorCodProyecto";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@CodigoProyecto", codigoProyecto);
                cmd.Parameters.Add(par1);
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        RPlanBaseBE PlanBaseBE = new RPlanBaseBE();
                        LlenarEntidadPlanBase(PlanBaseBE, dataReader);
                        oListaPlanBase.Add(PlanBaseBE);
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
            return oListaPlanBase;
        }
        public List<RPlanBaseBE> ObtenerPlanBasePorNroOI(string NroOI)
        {

            ScriptorChannel canalPlanBase = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Inversion));

            //Obtener CeCos de la ultima version de la sociedad especificada
            List<ScriptorContent> listaPlanBase = canalPlanBase.QueryContents("#Id", Guid.NewGuid(), "<>")
                                                               .QueryContents("Version", ObtenerUltimaVersionMaestro(Canales.Inversion), "=")
                                                               .QueryContents("CodigoOI", NroOI, "<>").ToList();

            List<RPlanBaseBE> oListaPlanBase = new List<RPlanBaseBE>();
            RPlanBaseBE oPlanBase;

            foreach (ScriptorContent item in listaPlanBase)
            {
                oPlanBase = new RPlanBaseBE();
                oPlanBase.Id = item.Id;
                oPlanBase.CodCeCo = ((ScriptorDropdownListValue)item.Parts.IdCeCo).Content.Parts.CodCECO;
                oPlanBase.CodProyecto = item.Parts.CodProyecto;
                oPlanBase.NomProyecto = item.Parts.NomProyecto;
                oPlanBase.TipoCapex = item.Parts.TipoCapex;
                oPlanBase.TipoActivo = item.Parts.TipoActivo;
                oPlanBase.NroOI = item.Parts.CodigoOI;
                oPlanBase.DescOI = item.Parts.DescOI;
                oPlanBase.MontoBase = item.Parts.MontoContable;
                oListaPlanBase.Add(oPlanBase);
            }
            return oListaPlanBase;
        }
        public List<RPlanBaseBE> ObtenerPlanBasePorNroOIBD(string NroOI)
        {

            List<RPlanBaseBE> oListaPlanBase = new List<RPlanBaseBE>();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerListaPlanBasePorCodigoOI";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@CodigoOI", NroOI);
                cmd.Parameters.Add(par1);
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        RPlanBaseBE PlanBaseBE = new RPlanBaseBE();
                        LlenarEntidadPlanBase(PlanBaseBE, dataReader);
                        oListaPlanBase.Add(PlanBaseBE);
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
            return oListaPlanBase;
        }

        public List<RPlanBaseBE> ValidarOI(string NroOI)
        {

            List<RPlanBaseBE> oListaPlanBase = new List<RPlanBaseBE>();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ValidarCodigoOI";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@CodigoOI", NroOI);
                cmd.Parameters.Add(par1);
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        RPlanBaseBE PlanBaseBE = new RPlanBaseBE();
                        LlenarEntidadPlanBase(PlanBaseBE, dataReader);
                        oListaPlanBase.Add(PlanBaseBE);
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
            return oListaPlanBase;
        }

        public List<RPlanBaseBE> ObtenerPlanBasePorCodCeco_CodProyecto_NroOI(Guid idCeCo,string codproyecto, string nroOI)
        {
            ScriptorChannel canalPlanBase = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Inversion));
            //Obtener CeCos de la ultima version de la sociedad especificada
            List<ScriptorContent> listaPlanBase = canalPlanBase.QueryContents("#Id", Guid.NewGuid(), "<>")
                                                               .QueryContents("Version", ObtenerUltimaVersionMaestro(Canales.Inversion), "=")
                                                               .QueryContents("IdCeCo", idCeCo, "==")
                                                               .QueryContents("CodigoProyecto", codproyecto, "==")
                                                               .QueryContents("CodigoOI", nroOI, "==").ToList();
            List<RPlanBaseBE> oListaPlanBase = new List<RPlanBaseBE>();
            RPlanBaseBE oPlanBase;

            foreach (ScriptorContent item in listaPlanBase)
            {
                oPlanBase = new RPlanBaseBE();
                oPlanBase.Id = item.Id;
                oPlanBase.CodCeCo = ((ScriptorDropdownListValue)item.Parts.IdCeCo).Content.Parts.CodCECO;
                oPlanBase.CodProyecto = item.Parts.CodProyecto;
                oPlanBase.NomProyecto = item.Parts.NomProyecto;
                oPlanBase.TipoCapex = item.Parts.TipoCapex;
                oPlanBase.TipoActivo = item.Parts.TipoActivo;
                oPlanBase.NroOI = item.Parts.CodigoOI;
                oPlanBase.DescOI = item.Parts.DescOI;
                oPlanBase.MontoBase = item.Parts.MontoContable;
                oListaPlanBase.Add(oPlanBase);
            }
            return oListaPlanBase;
        }
        public List<RPlanBaseBE> ObtenerPlanBasePorCodCeco_CodProyecto_NroOIBD(Guid idCeCo, string codproyecto, string nroOI)
        {
            List<RPlanBaseBE> oListaPlanBase = new List<RPlanBaseBE>();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerListaPlanBasePorCeCo_CodProyect_NroOI";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@IdCeCo", idCeCo);
                cmd.Parameters.Add(par1);
                SqlParameter par2 = new SqlParameter("@CodigoProyecto", codproyecto);
                cmd.Parameters.Add(par2);
                SqlParameter par3 = new SqlParameter("@CodigoOI", nroOI);
                cmd.Parameters.Add(par3);
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        RPlanBaseBE PlanBaseBE = new RPlanBaseBE();
                        LlenarEntidadPlanBase(PlanBaseBE, dataReader);
                        oListaPlanBase.Add(PlanBaseBE);
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
            return oListaPlanBase;
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

        public bool GrabarPlanBaseTemp(List<Dictionary<string,string>> lstDatos)
        {
            bool resultado = true;
            var datos = new TipoListaPlanBaseTempCollection();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            foreach (Dictionary<string, string> item in lstDatos)
            {
              
                datos.Add(new ListaPlanBaseTemp(item["CodCeCo"],item["CodProyecto"],item["NomProyecto"],item["TipoCapex"],item["TipoActivo"],item["NroOI"],
                                                item["DescOI"],item["MontoBase"],item["CodSociedad"],item["DescSociedad"],item["CodSector"],
                                                item["DescSector"],item["CodMacroservicio"],item["DescMacroservicio"],new Guid(Canales.IdEsquemaPlanBaseTemp),
                                                new Guid(Canales.IdCreatoPlanBaseTemp),
                                                ConfigurationManager.AppSettings["EstadoFlujo"].ToString(),
                                                new Guid(Canales.IdCanalPlanBaseTemp)));

            }

            
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "InsertarRegistrosPlanBaseTemp";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@ListaPlanBaseTemp", SqlDbType.Structured);//, SqlDbType.Structured,);
                par1.TypeName = "dbo.ListaPlanBaseTemp";
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
        public bool GrabarPlanBase(List<RPlanBaseBE> lstDatos)
        {
            bool resultado = true;
            var datos = new TipoListaPlanBaseCollection();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            foreach (RPlanBaseBE item in lstDatos)
            {
                datos.Add(new ListaPlanBase(item.CodCeCo, item.CodProyecto, item.NomProyecto, item.TipoCapex, item.TipoActivo, item.NroOI, item.DescOI, item.MontoBase.ToString(),
                    item.CodigoSociedad,item.DescripcionSociedad,item.CodigoSector,item.DescripcionSector,item.CodigoMacroServicio,
                    item.DescripcionMacroServicio, new Guid(Canales.IdEsquemaInversion), new Guid(Canales.IdCreator),
                    ConfigurationManager.AppSettings["EstadoFlujo"].ToString(),
                    new Guid(Canales.IdCanalInversion),new Guid(Canales.IdCanalMovimiento),new Guid(Canales.IdEsquemaMovimiento)));
            }
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "InsertarRegistrosPlanBase";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@ListaPlanBase", SqlDbType.Structured);//, SqlDbType.Structured,);
                par1.TypeName = "dbo.ListaPlanBase";
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
        public bool ActualizarPlanBase(List<RPlanBaseBE> lstDatos)
        {
            bool resultado = true;
            var datos = new TipoListaActualizarPlanBaseCollection();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            foreach (RPlanBaseBE item in lstDatos)
            {
                datos.Add(new ListaActualizarPlanBase(item.Id.Value,item.CodCeCo, item.CodProyecto, item.NomProyecto, item.TipoCapex, item.TipoActivo, item.NroOI, item.DescOI, item.MontoBase.ToString(),
                    item.CodigoSociedad, item.DescripcionSociedad, item.CodigoSector, item.DescripcionSector, item.CodigoMacroServicio,
                    item.DescripcionMacroServicio));
            }
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ActualizarRegistrosPlanBase";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@ListaIdsInversion", SqlDbType.Structured);//, SqlDbType.Structured,);
                par1.TypeName = "dbo.ListaActualizarPlanBase";
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

        public bool ActualizarPlanBaseVersion(List<RPlanBaseBE> lstDatos)
        {
            bool resultado = true;
            var datos = new TipoListaActualizarPlanBaseCollection();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            foreach (RPlanBaseBE item in lstDatos)
            {
                datos.Add(new ListaActualizarPlanBase(item.Id.Value, item.CodCeCo, item.CodProyecto, item.NomProyecto,item.TipoCapex, item.TipoActivo, item.NroOI, item.DescOI, item.MontoBase.ToString(),
                    item.CodigoSociedad, item.DescripcionSociedad, item.CodigoSector, item.DescripcionSector, item.CodigoMacroServicio,
                    item.DescripcionMacroServicio));
            }
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ActualizarRegistrosPlanBaseVersion";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@ListaIdsInversion", SqlDbType.Structured);//, SqlDbType.Structured,);
                par1.TypeName = "dbo.ListaActualizarPlanBase";
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
        public class TipoListaCodigosCollection : List<ListaCodigos>, IEnumerable<SqlDataRecord>
        {
            IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
            {
                var sqlDataRecord = new SqlDataRecord(new SqlMetaData("Codigo", SqlDbType.Text));
                
                foreach (ListaCodigos ListaCodigositem in this)
                {
                    sqlDataRecord.SetString(0, ListaCodigositem.Codigo);
                    yield return sqlDataRecord;
                }
            }
        }
        public class TipoListaPlanBaseTempCollection : List<ListaPlanBaseTemp>, IEnumerable<SqlDataRecord>
        {
            IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
            {
                var sqlDataRecord = new SqlDataRecord(new SqlMetaData("CodCeCo", SqlDbType.Text),
                new SqlMetaData("CodProyecto", SqlDbType.Text), new SqlMetaData("NomProyecto", SqlDbType.Text), new SqlMetaData("TipoCapex", SqlDbType.Text), new SqlMetaData("TipoActivo", SqlDbType.Text),
                new SqlMetaData("NroOI", SqlDbType.Text), new SqlMetaData("DescOI", SqlDbType.Text), new SqlMetaData("MontoBase", SqlDbType.Text),
                new SqlMetaData("CodSociedad", SqlDbType.Text), new SqlMetaData("DescSociedad", SqlDbType.Text), new SqlMetaData("CodSector", SqlDbType.Text),
                new SqlMetaData("DescSector", SqlDbType.Text), new SqlMetaData("CodMacroservicio", SqlDbType.Text),
                new SqlMetaData("DescMacroservicio", SqlDbType.Text), new SqlMetaData("IdEsquema", SqlDbType.UniqueIdentifier),
                new SqlMetaData("IdCreator", SqlDbType.UniqueIdentifier), new SqlMetaData("EstadoFlujo", SqlDbType.Text),
                new SqlMetaData("IdCanal", SqlDbType.UniqueIdentifier));
 
                foreach (ListaPlanBaseTemp ListaMaestroTempitem in this)
                {
                  
                    sqlDataRecord.SetString(0, ListaMaestroTempitem.CodCeCo);
                    sqlDataRecord.SetString(1, ListaMaestroTempitem.CodProyecto);
                    sqlDataRecord.SetString(2, ListaMaestroTempitem.NomProyecto);
                    sqlDataRecord.SetString(3, ListaMaestroTempitem.TipoCapex);
                    sqlDataRecord.SetString(4, ListaMaestroTempitem.TipoActivo);
                    sqlDataRecord.SetString(5, ListaMaestroTempitem.NroOI);
                    sqlDataRecord.SetString(6, ListaMaestroTempitem.DescOI);
                    sqlDataRecord.SetString(7, ListaMaestroTempitem.MontoBase);
                    sqlDataRecord.SetString(8, ListaMaestroTempitem.CodSociedad);
                    sqlDataRecord.SetString(9, ListaMaestroTempitem.DescSociedad);
                    sqlDataRecord.SetString(10, ListaMaestroTempitem.CodSector);
                    sqlDataRecord.SetString(11, ListaMaestroTempitem.DescSector);
                    sqlDataRecord.SetString(12, ListaMaestroTempitem.CodMacroservicio);
                    sqlDataRecord.SetString(13, ListaMaestroTempitem.DescMacroservicio);
                    sqlDataRecord.SetGuid(14, ListaMaestroTempitem.IdEsquema);
                    sqlDataRecord.SetGuid(15, ListaMaestroTempitem.IdCreator);
                    sqlDataRecord.SetString(16, ListaMaestroTempitem.EstadoFlujo);
                    sqlDataRecord.SetGuid(17, ListaMaestroTempitem.IdCanal);
                    yield return sqlDataRecord;
                }
            }
        }
        public class TipoListaPlanBaseCollection : List<ListaPlanBase>, IEnumerable<SqlDataRecord>
        {
            IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
            {
                var sqlDataRecord = new SqlDataRecord(new SqlMetaData("CodCeCo", SqlDbType.Text),
                new SqlMetaData("CodProyecto", SqlDbType.Text), new SqlMetaData("NomProyecto", SqlDbType.Text), new SqlMetaData("TipoCapex", SqlDbType.Text), new SqlMetaData("TipoActivo", SqlDbType.Text),
                new SqlMetaData("NroOI", SqlDbType.Text), new SqlMetaData("DescOI", SqlDbType.Text), new SqlMetaData("MontoBase", SqlDbType.Text),
                new SqlMetaData("CodSociedad", SqlDbType.Text), new SqlMetaData("DescSociedad", SqlDbType.Text), new SqlMetaData("CodSector", SqlDbType.Text),
                new SqlMetaData("DescSector", SqlDbType.Text), new SqlMetaData("CodMacroservicio", SqlDbType.Text),
                new SqlMetaData("DescMacroservicio", SqlDbType.Text), new SqlMetaData("IdEsquema", SqlDbType.UniqueIdentifier),
                new SqlMetaData("IdCreator", SqlDbType.UniqueIdentifier), new SqlMetaData("EstadoFlujo", SqlDbType.Text),
                new SqlMetaData("IdCanal", SqlDbType.UniqueIdentifier), new SqlMetaData("IdCanalMovimiento", SqlDbType.UniqueIdentifier),
                new SqlMetaData("IdEsquemaMovimiento", SqlDbType.UniqueIdentifier));

                foreach (ListaPlanBase ListaMaestroTempitem in this)
                {
                    sqlDataRecord.SetString(0, ListaMaestroTempitem.CodCeCo);
                    sqlDataRecord.SetString(1, ListaMaestroTempitem.CodProyecto);
                    sqlDataRecord.SetString(2, ListaMaestroTempitem.NomProyecto);
                    sqlDataRecord.SetString(3, ListaMaestroTempitem.TipoCapex);
                    sqlDataRecord.SetString(4, ListaMaestroTempitem.TipoActivo);
                    sqlDataRecord.SetString(5, ListaMaestroTempitem.NroOI);
                    sqlDataRecord.SetString(6, ListaMaestroTempitem.DescOI);
                    sqlDataRecord.SetString(7, ListaMaestroTempitem.MontoBase);
                    sqlDataRecord.SetString(8, ListaMaestroTempitem.CodSociedad);
                    sqlDataRecord.SetString(9, ListaMaestroTempitem.DescSociedad);
                    sqlDataRecord.SetString(10, ListaMaestroTempitem.CodSector);
                    sqlDataRecord.SetString(11, ListaMaestroTempitem.DescSector);
                    sqlDataRecord.SetString(12, ListaMaestroTempitem.CodMacroservicio);
                    sqlDataRecord.SetString(13, ListaMaestroTempitem.DescMacroservicio);
                    sqlDataRecord.SetGuid(14, ListaMaestroTempitem.IdEsquema);
                    sqlDataRecord.SetGuid(15, ListaMaestroTempitem.IdCreator);
                    sqlDataRecord.SetString(16, ListaMaestroTempitem.EstadoFlujo);
                    sqlDataRecord.SetGuid(17, ListaMaestroTempitem.IdCanal);
                    sqlDataRecord.SetGuid(18, ListaMaestroTempitem.IdCanalMovimiento);
                    sqlDataRecord.SetGuid(19, ListaMaestroTempitem.IdEsquemaMovimiento);
                    yield return sqlDataRecord;
                }
            }
        }
        public class TipoListaActualizarPlanBaseCollection : List<ListaActualizarPlanBase>, IEnumerable<SqlDataRecord>
        {
            IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
            {
                var sqlDataRecord = new SqlDataRecord(new SqlMetaData("IdInversion", SqlDbType.UniqueIdentifier),new SqlMetaData("CodCeCo", SqlDbType.Text),
                new SqlMetaData("CodProyecto", SqlDbType.Text), new SqlMetaData("NomProyecto", SqlDbType.Text), new SqlMetaData("TipoCapex", SqlDbType.Text), new SqlMetaData("TipoActivo", SqlDbType.Text),
                new SqlMetaData("NroOI", SqlDbType.Text), new SqlMetaData("DescOI", SqlDbType.Text), new SqlMetaData("MontoBase", SqlDbType.Text),
                new SqlMetaData("CodSociedad", SqlDbType.Text), new SqlMetaData("DescSociedad", SqlDbType.Text), new SqlMetaData("CodSector", SqlDbType.Text),
                new SqlMetaData("DescSector", SqlDbType.Text), new SqlMetaData("CodMacroservicio", SqlDbType.Text),
                new SqlMetaData("DescMacroservicio", SqlDbType.Text));

                foreach (ListaActualizarPlanBase ListaMaestroTempitem in this)
                {
                    sqlDataRecord.SetGuid(0, ListaMaestroTempitem.IdInversion);
                    sqlDataRecord.SetString(1, ListaMaestroTempitem.CodCeCo);
                    sqlDataRecord.SetString(2, ListaMaestroTempitem.CodProyecto);
                    sqlDataRecord.SetString(3, ListaMaestroTempitem.NomProyecto);
                    sqlDataRecord.SetString(4, ListaMaestroTempitem.TipoCapex);
                    sqlDataRecord.SetString(5, ListaMaestroTempitem.TipoActivo);
                    sqlDataRecord.SetString(6, ListaMaestroTempitem.NroOI);
                    sqlDataRecord.SetString(7, ListaMaestroTempitem.DescOI);
                    sqlDataRecord.SetString(8, ListaMaestroTempitem.MontoBase);
                    sqlDataRecord.SetString(9, ListaMaestroTempitem.CodSociedad);
                    sqlDataRecord.SetString(10, ListaMaestroTempitem.DescSociedad);
                    sqlDataRecord.SetString(11, ListaMaestroTempitem.CodSector);
                    sqlDataRecord.SetString(12, ListaMaestroTempitem.DescSector);
                    sqlDataRecord.SetString(13, ListaMaestroTempitem.CodMacroservicio);
                    sqlDataRecord.SetString(14, ListaMaestroTempitem.DescMacroservicio);
                    yield return sqlDataRecord;
                }
            }
        }

        private void LlenarEntidadPlanBase(RPlanBaseBE item, IDataReader iDataReader)
        {
            if (!Convert.IsDBNull(iDataReader["Id"]))
            {
                item.Id =(Guid)(iDataReader["Id"]);
            }
            if (!Convert.IsDBNull(iDataReader["CodidoCeCo"]))
            {
                item.CodCeCo = Convert.ToString(iDataReader["CodidoCeCo"]);
            }
            if (!Convert.IsDBNull(iDataReader["DescripcionCeCo"]))
            {
                item.DescCeCo = Convert.ToString(iDataReader["DescripcionCeCo"]);
            }
            if (!Convert.IsDBNull(iDataReader["CodigoProyecto"]))
            {
                item.CodProyecto = Convert.ToString(iDataReader["CodigoProyecto"]);
            }
            if (!Convert.IsDBNull(iDataReader["NombreProyecto"]))
            {
                item.NomProyecto = Convert.ToString(iDataReader["NombreProyecto"]);
            }
            if (!Convert.IsDBNull(iDataReader["DescripcionTipoCapex"]))
            {
                item.TipoCapex = Convert.ToString(iDataReader["DescripcionTipoCapex"]);
            }
            if (!Convert.IsDBNull(iDataReader["DescripcionTipoActivo"]))
            {
                item.TipoActivo = Convert.ToString(iDataReader["DescripcionTipoActivo"]);
            }
            if (!Convert.IsDBNull(iDataReader["CodigoOI"]))
            {
                item.NroOI = Convert.ToString(iDataReader["CodigoOI"]);
            }
            if (!Convert.IsDBNull(iDataReader["DescripcionOI"]))
            {
                item.DescOI = Convert.ToString(iDataReader["DescripcionOI"]);
            }
            if (!Convert.IsDBNull(iDataReader["MontoContable"]))
            {
                item.MontoBase = Convert.ToString(iDataReader["MontoContable"]);
            }
            
        }
        private void LlenarEntidadPlanBaseTemp(RPlanBaseBE item, IDataReader iDataReader)
        {
            if (!Convert.IsDBNull(iDataReader["CodidoCeCo"]))
            {
                item.CodCeCo = Convert.ToString(iDataReader["CodidoCeCo"]);
            }
            if (!Convert.IsDBNull(iDataReader["DescripcionCECO"]))
            {
                item.DescCeCo = Convert.ToString(iDataReader["DescripcionCECO"]);
            }
            if (!Convert.IsDBNull(iDataReader["CodigoProyecto"]))
            {
                item.CodProyecto = Convert.ToString(iDataReader["CodigoProyecto"]);
            }
            if (!Convert.IsDBNull(iDataReader["NombreProyecto"]))
            {
                item.NomProyecto = Convert.ToString(iDataReader["NombreProyecto"]);
            }
            if (!Convert.IsDBNull(iDataReader["DescripcionTipoCapex"]))
            {
                item.TipoCapex = Convert.ToString(iDataReader["DescripcionTipoCapex"]);
            }
            if (!Convert.IsDBNull(iDataReader["DescripcionTipoActivo"]))
            {
                item.TipoActivo = Convert.ToString(iDataReader["DescripcionTipoActivo"]);
            }
            if (!Convert.IsDBNull(iDataReader["CodigoOI"]))
            {
                item.NroOI = Convert.ToString(iDataReader["CodigoOI"]);
            }
            if (!Convert.IsDBNull(iDataReader["DescripcionOI"]))
            {
                item.DescOI = Convert.ToString(iDataReader["DescripcionOI"]);
            }
            if (!Convert.IsDBNull(iDataReader["MontoContable"]))
            {
                item.MontoBase = Convert.ToString(iDataReader["MontoContable"]);
            }
            if (!Convert.IsDBNull(iDataReader["CodigoSociedad"]))
            {
                item.CodigoSociedad = Convert.ToString(iDataReader["CodigoSociedad"]);
            }
            if (!Convert.IsDBNull(iDataReader["DescripcionSociedad"]))
            {
                item.DescripcionSociedad = Convert.ToString(iDataReader["DescripcionSociedad"]);
            }
            if (!Convert.IsDBNull(iDataReader["CodigoSector"]))
            {
                item.CodigoSector = Convert.ToString(iDataReader["CodigoSector"]);
            }
            if (!Convert.IsDBNull(iDataReader["DescripcionSector"]))
            {
                item.DescripcionSector = Convert.ToString(iDataReader["DescripcionSector"]);
            }
            if (!Convert.IsDBNull(iDataReader["CodigoMacroservicio"]))
            {
                item.CodigoMacroServicio = Convert.ToString(iDataReader["CodigoMacroservicio"]);
            }
            if (!Convert.IsDBNull(iDataReader["DescripcionMacroservicio"]))
            {
                item.DescripcionMacroServicio = Convert.ToString(iDataReader["DescripcionMacroservicio"]);
            }
        }

        private void LlenarEntidadPlanBaseBD(RPlanBaseBE item, IDataReader iDataReader)
        {
            if (!Convert.IsDBNull(iDataReader["Id"]))
            {
                item.Id = (Guid)(iDataReader["Id"]);
            }
            if (!Convert.IsDBNull(iDataReader["CodidoCeCo"]))
            {
                item.CodCeCo = Convert.ToString(iDataReader["CodidoCeCo"]);
            }
            if (!Convert.IsDBNull(iDataReader["DescripcionCECO"]))
            {
                item.DescCeCo = Convert.ToString(iDataReader["DescripcionCECO"]);
            }
            if (!Convert.IsDBNull(iDataReader["CodigoProyecto"]))
            {
                item.CodProyecto = Convert.ToString(iDataReader["CodigoProyecto"]);
            }
            if (!Convert.IsDBNull(iDataReader["NombreProyecto"]))
            {
                item.NomProyecto = Convert.ToString(iDataReader["NombreProyecto"]);
            }
            if (!Convert.IsDBNull(iDataReader["DescripcionTipoCapex"]))
            {
                item.TipoCapex = Convert.ToString(iDataReader["DescripcionTipoCapex"]);
            }
            if (!Convert.IsDBNull(iDataReader["DescripcionTipoActivo"]))
            {
                item.TipoActivo = Convert.ToString(iDataReader["DescripcionTipoActivo"]);
            }
            if (!Convert.IsDBNull(iDataReader["CodigoOI"]))
            {
                item.NroOI = Convert.ToString(iDataReader["CodigoOI"]);
            }
            if (!Convert.IsDBNull(iDataReader["DescripcionOI"]))
            {
                item.DescOI = Convert.ToString(iDataReader["DescripcionOI"]);
            }
            if (!Convert.IsDBNull(iDataReader["MontoContable"]))
            {
                item.MontoBase = Convert.ToString(iDataReader["MontoContable"]);
            }
            if (!Convert.IsDBNull(iDataReader["CodigoSociedad"]))
            {
                item.CodigoSociedad = Convert.ToString(iDataReader["CodigoSociedad"]);
            }
            if (!Convert.IsDBNull(iDataReader["DescripcionSociedad"]))
            {
                item.DescripcionSociedad = Convert.ToString(iDataReader["DescripcionSociedad"]);
            }
            if (!Convert.IsDBNull(iDataReader["CodigoSector"]))
            {
                item.CodigoSector = Convert.ToString(iDataReader["CodigoSector"]);
            }
            if (!Convert.IsDBNull(iDataReader["DescripcionSector"]))
            {
                item.DescripcionSector = Convert.ToString(iDataReader["DescripcionSector"]);
            }
            if (!Convert.IsDBNull(iDataReader["CodigoMacroservicio"]))
            {
                item.CodigoMacroServicio = Convert.ToString(iDataReader["CodigoMacroservicio"]);
            }
            if (!Convert.IsDBNull(iDataReader["DescripcionMacroservicio"]))
            {
                item.DescripcionMacroServicio = Convert.ToString(iDataReader["DescripcionMacroservicio"]);
            }
        }

        public bool ActualizarPlanBaseEliminados()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            bool Resultado = false;
            try
            {
                con.ConnectionString = connectionString;
                con.Open();

                string nombreProcedure = "ActualizarPlanBaseEliminados";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;               

                if (cmd.ExecuteNonQuery() > 0)
                    Resultado = true;                

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return Resultado;
        }

        public bool ActualizarVersion()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            bool Resultado = false;
            try
            {
                con.ConnectionString = connectionString;
                con.Open();

                string nombreProcedure = "APIActualizarVersionPlanBase";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (cmd.ExecuteNonQuery() > 0)
                    Resultado = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return Resultado;
 
        }
    }
}
