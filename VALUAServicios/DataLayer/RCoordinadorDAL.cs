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
    public class RCoordinadorDAL
    {
        public List<RCoordinadorBE> ObtenerCoordinadores()
        {
            ScriptorChannel canalCoordinadores = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Coordinador));
            //Obtener CeCos de la ultima version de la sociedad especificada
            List<ScriptorContent> listaCoordinadores = canalCoordinadores.QueryContents("#Id", Guid.NewGuid(), "<>").QueryContents("Version", ObtenerUltimaVersionMaestro(Canales.Coordinador), "=").ToList();
            List<RCoordinadorBE> oListaCoordinadores = new List<RCoordinadorBE>();
            RCoordinadorBE oCoordinador;
            foreach (ScriptorContent item in listaCoordinadores)
            {
                oCoordinador = new RCoordinadorBE();
                ScriptorContent contenidoCeCo = ((ScriptorDropdownListValue)item.Parts.IdCeCo).Content;
                oCoordinador.CodCeCo = contenidoCeCo.Parts.CodCECO;
                oCoordinador.DescCeCo = contenidoCeCo.Parts.DescCECO;
                oCoordinador.CuentaRed = item.Parts.CuentaRed;
                oListaCoordinadores.Add(oCoordinador);
            }
            return oListaCoordinadores;

        }
        public List<RCoordinadorBE> ObtenerCoordinadoresBD()
        {
            List<RCoordinadorBE> oListaCoordinadores = new List<RCoordinadorBE>();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerListaCoordinadores";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        RCoordinadorBE CoordinadorBE = new RCoordinadorBE();
                        LlenarEntidadCoordinador(CoordinadorBE, dataReader);
                        oListaCoordinadores.Add(CoordinadorBE);
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
            return oListaCoordinadores;

        }
        public List<RCoordinadorBE> ObtenerCoordinadoresTemp()
        {
            ScriptorChannel canalCoordinadoresTemp = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.CoordinadorTemp));
            //Obtener CeCos de la ultima version de la sociedad especificada
            List<ScriptorContent> listaCoordinadoresTemp = canalCoordinadoresTemp.QueryContents("#Id", Guid.NewGuid(), "<>").QueryContents("Version", ObtenerUltimaVersionMaestro(Canales.CoordinadorTemp), "=").ToList();
            List<RCoordinadorBE> oListaCoordinadoresTemp = new List<RCoordinadorBE>();
            RCoordinadorBE oCoordinadorTemp;
            foreach (ScriptorContent item in listaCoordinadoresTemp)
            {
                oCoordinadorTemp = new RCoordinadorBE();
                //ScriptorContent contenidoCeCo = ((ScriptorDropdownListValue)item.Parts.IdCeCo).Content;
                oCoordinadorTemp.CodCeCo = item.Parts.CodCeCo;
                oCoordinadorTemp.DescCeCo = item.Parts.DescCECO;
                oCoordinadorTemp.IdCeCo = item.Parts.IdCeCo;
                oCoordinadorTemp.CuentaRed = item.Parts.Coordinador;
                oCoordinadorTemp.Nombre = item.Parts.Nombre;
                oListaCoordinadoresTemp.Add(oCoordinadorTemp);
            }
            return oListaCoordinadoresTemp;
        }
        public List<RCoordinadorBE> ObtenerCoordinadoresTempBD()
        {
            List<RCoordinadorBE> oListaCoordinadores = new List<RCoordinadorBE>();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerListaCoordinadoresTemp";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        RCoordinadorBE CoordinadorBE = new RCoordinadorBE();
                        LlenarEntidadCoordinadorTemp(CoordinadorBE, dataReader);
                        oListaCoordinadores.Add(CoordinadorBE);
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
            return oListaCoordinadores;

        }

        public bool EsCoordinador(string CuentaRed)
        {
            bool EsCoordinador = false;
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ValidarCoordinador";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;                
                SqlParameter par1 = new SqlParameter("@CuentaRed", CuentaRed);
                cmd.Parameters.Add(par1);
                
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        EsCoordinador = Convert.ToInt32(dataReader["EsCoordinador"]) > 0 ? true : false;
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
            return EsCoordinador;
 
        }

        public RCoordinadorBE ObtenerCoordinadorPorCuentaPorIdCeCo(Guid IdCeco, string CuentaRed)
        {
            RCoordinadorBE CoordinadorBE = new RCoordinadorBE();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerCoordinadorPorCuentaPorIdCeCo";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@IdCeCo", IdCeco);
                SqlParameter par2 = new SqlParameter("@CuentaRed", CuentaRed);
                cmd.Parameters.Add(par1);
                cmd.Parameters.Add(par2);
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        CoordinadorBE.Id = (Guid)(dataReader["Id"]);
                        CoordinadorBE.CodCeCo = Convert.ToString(dataReader["Codigo"]);
                        CoordinadorBE.DescCeCo = Convert.ToString(dataReader["Descripcion"]);
                        CoordinadorBE.CuentaRed = Convert.ToString(dataReader["Coordinador"]);
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
            return CoordinadorBE;
        }


        public RCoordinadorBE ObtenerCoordinadorPorCuentaPorRedUltimaVersion(string CuentaRed)
        {
            RCoordinadorBE CoordinadorBE = new RCoordinadorBE();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerCoordinadorPorCuentaPorRedUltimaVersion";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@CuentaRed", CuentaRed);
                cmd.Parameters.Add(par1);
                
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        CoordinadorBE.Id = (Guid)(dataReader["Id"]);
                        CoordinadorBE.CodCeCo = Convert.ToString(dataReader["Codigo"]);
                        CoordinadorBE.DescCeCo = Convert.ToString(dataReader["Descripcion"]);
                        CoordinadorBE.CuentaRed = Convert.ToString(dataReader["Coordinador"]);
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
            return CoordinadorBE;
        }

        public RCoordinadorBE ObtenerCoordinadorPorCuentaPorCodigoCeCo(Guid IdCeco, string CuentaRed)
        {
            RCoordinadorBE CoordinadorBE = new RCoordinadorBE();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerCoordinadorPorCuentaPorCodigoCeCo";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@IdCeCo", IdCeco);
                SqlParameter par2 = new SqlParameter("@CuentaRed", CuentaRed);
                cmd.Parameters.Add(par1);
                cmd.Parameters.Add(par2);
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        CoordinadorBE.Id = (Guid)(dataReader["Id"]);
                        CoordinadorBE.IdCeCo = Convert.ToString(dataReader["IdCeCo"]);
                        CoordinadorBE.CodCeCo = Convert.ToString(dataReader["Codigo"]);
                        CoordinadorBE.DescCeCo = Convert.ToString(dataReader["Descripcion"]);
                        CoordinadorBE.CuentaRed = Convert.ToString(dataReader["Coordinador"]);
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
            return CoordinadorBE;
        }
        public RCoordinadorBE ValidatExisteCeCoUltimaVersion(Guid IdCeco)
        {
            RCoordinadorBE CoordinadorBE = new RCoordinadorBE();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ValidarCecoUlitmaVersion";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@IdCeCo", IdCeco);
                cmd.Parameters.Add(par1);
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        CoordinadorBE.Id = (Guid)(dataReader["Id"]);
                        CoordinadorBE.CodCeCo = Convert.ToString(dataReader["Codigo"]);
                        CoordinadorBE.DescCeCo = Convert.ToString(dataReader["Descripcion"]);
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
            return CoordinadorBE;
        }
        //public bool GrabarCoordinadorTemp(List<Dictionary<string, string>> lstDatos)
        //{
        //    bool resultado = true;
        //    var datos = new TipoListaCoordinadorTempCollection();
        //    string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
        //    foreach (Dictionary<string, string> item in lstDatos)
        //    {
        //        //datos.Add(new ListaCoordinadorTemp(item["CodCeCo"],item["DescCeCo"],item["Coordinador"],new Guid(CannotUnloadAppDomainException)
        //        datos.Add(new ListaCoordinadorTemp(item["CodCeCo"], item["DescCeCo"], item["Coordinador"], item["IdCeCo"], item["Nombre"],
        //                    new Guid(Canales.IdEsquemaCoordinadorTemp),
        //                    new Guid(Canales.IdCreatoCoordinadorTemp),
        //                    ConfigurationManager.AppSettings["EstadoFlujo"].ToString(), new Guid(Canales.IdCanalCoordinadorTemp)));
        //    }
        //    SqlConnection con = new SqlConnection();
        //    try
        //    {
        //        con.ConnectionString = connectionString;
        //        con.Open();
        //        string nombreProcedure = "InsertarRegistrosCoordinadoresTemp";
        //        SqlCommand cmd = new SqlCommand(nombreProcedure, con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        SqlParameter par1 = new SqlParameter("@ListaCoordinadoresTemp", SqlDbType.Structured);//, SqlDbType.Structured,);
        //        par1.TypeName = "dbo.ListaCoordinadoresTemp";
        //        par1.Value = datos;
        //        cmd.Parameters.Add(par1);
        //        using (IDataReader dataReader = cmd.ExecuteReader())
        //        {
        //            resultado = true;
        //            while (dataReader.Read())
        //            {

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resultado = false;
        //        throw ex;
        //    }

        //    finally
        //    {
        //        con.Close();
        //    }
        //    return resultado;
        //}

        public int ObtenerMaxVersionCoordinadorTemp()
        {
            int resultado = 0;
            
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;            

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();

                string nombreProcedure = "ObtenerMaxVersionCoordinadorTemp";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        resultado = Convert.ToInt32(dataReader["VersionCoordinador"]);
                    }
                }


            }
            catch (Exception ex)
            {
                resultado = 0;
                throw ex;
            }

            finally
            {
                con.Close();
            }
            return resultado;

        }


        //public string GrabarCoordinadorTemp(string CodCeCo,string DescCeCo, string Coordinador, string IdCeCo, string Nombre, Guid IdEsquema, Guid IdCreator, string EstadoFlujo, Guid IdCanal, DateTime Fecha,int VersionCoordinador)
        public bool GrabarCoordinadorTemp(DataTable Coordinador)
        {
            bool resultado = false;

            SqlConnection con = new SqlConnection();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            string nombreTable = "Coordinador1";

            try
            {

                con.ConnectionString = connectionString;
                con.Open();
                //string nombreProcedure = "InsertarCoordinadorTemp";
                //SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                //cmd.CommandType = CommandType.StoredProcedure;

                //SqlParameter par1 = new SqlParameter("@CodCeCo", CodCeCo);//, SqlDbType.Structured,);
                //cmd.Parameters.Add(par1);
                //par1 = new SqlParameter("@DescCeCo", DescCeCo);//, SqlDbType.Structured,);
                //cmd.Parameters.Add(par1);
                //par1 = new SqlParameter("@Coordinador", Coordinador);
                //cmd.Parameters.Add(par1);
                //par1 = new SqlParameter("@IdCeCo", IdCeCo);
                //cmd.Parameters.Add(par1);
                //par1 = new SqlParameter("@Nombre", Nombre);
                //cmd.Parameters.Add(par1);
                //par1 = new SqlParameter("@IdEsquema", IdEsquema);
                //cmd.Parameters.Add(par1);
                //par1 = new SqlParameter("@IdCreator", IdCreator);
                //cmd.Parameters.Add(par1);
                //par1 = new SqlParameter("@EstadoFlujo", EstadoFlujo);
                //cmd.Parameters.Add(par1);
                //par1 = new SqlParameter("@IdCanal", IdCanal);
                //cmd.Parameters.Add(par1);
                //par1 = new SqlParameter("@Fecha", Fecha);
                //cmd.Parameters.Add(par1);
                //par1 = new SqlParameter("@Version", VersionCoordinador);
                //cmd.Parameters.Add(par1);

                //using (IDataReader dataReader = cmd.ExecuteReader())
                //{
                //    while (dataReader.Read())
                //    {
                //        resultado = dataReader["Id"].ToString();
                //    }
                //}


                using (SqlBulkCopy bulkcopy = new SqlBulkCopy(con))
                {
                    try
                    {
                        bulkcopy.DestinationTableName = nombreTable;
                        bulkcopy.BatchSize = 1000;

                        bulkcopy.WriteToServer(Coordinador);
                        bulkcopy.Close();
                        resultado = true;
                    }
                    catch (Exception e)
                    {
                        ManejadorLogSimpleBL.WriteLog(Environment.NewLine + e.Message);
                        ManejadorLogSimpleBL.WriteLog(Environment.NewLine + e.InnerException);
                        ManejadorLogSimpleBL.WriteLog(Environment.NewLine + e.Source);
                        ManejadorLogSimpleBL.WriteLog(Environment.NewLine + e.StackTrace);
                    }
                }


            }
            catch (Exception ex)
            {
                resultado = false;
                 ManejadorLogSimpleBL.WriteLog(ex.InnerException.ToString());
                ManejadorLogSimpleBL.WriteLog(ex.Source);
                ManejadorLogSimpleBL.WriteLog(ex.StackTrace);
                throw ex;
            }

            finally
            {
                con.Close();
            }
            return resultado;
        }
        
        public bool GrabarCoordinadorContentTemp(Guid Id,Guid IdEsquema, Guid IdCreator, string EstadoFlujo, Guid IdCanal, DateTime Fecha)
        //public bool GrabarCoordinadorContentTemp(DataTable contentCoordinador)
        {
            bool resultado = false;
            
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            //string nombreTable = "channel_content";
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();

                string nombreProcedure1 = "InsertarCoordinadorContent";
                SqlCommand cmd1 = new SqlCommand(nombreProcedure1, con);
                cmd1.CommandType = CommandType.StoredProcedure;

                SqlParameter par2 = new SqlParameter("@IdEsquema", IdEsquema);
                cmd1.Parameters.Add(par2);
                par2 = new SqlParameter("@Id", Id);
                cmd1.Parameters.Add(par2);
                par2 = new SqlParameter("@IdCreator", IdCreator);
                cmd1.Parameters.Add(par2);
                par2 = new SqlParameter("@EstadoFlujo", EstadoFlujo);
                cmd1.Parameters.Add(par2);
                par2 = new SqlParameter("@IdCanal", IdCanal);
                cmd1.Parameters.Add(par2);
                par2 = new SqlParameter("@Fecha", Fecha);
                cmd1.Parameters.Add(par2);

                if (cmd1.ExecuteNonQuery() > 0)
                    resultado = true;       
                //using (SqlBulkCopy bulkcopy = new SqlBulkCopy(con))
                //{
                //    try
                //    {
                //        bulkcopy.DestinationTableName = nombreTable;
                //        bulkcopy.BatchSize = 1000;

                //        bulkcopy.WriteToServer(contentCoordinador);
                //        bulkcopy.Close();
                //    }
                //    catch (Exception e)
                //    {
                //        ManejadorLogSimpleBL.WriteLog(Environment.NewLine + e.InnerException);
                //        ManejadorLogSimpleBL.WriteLog(Environment.NewLine + e.Source);
                //        ManejadorLogSimpleBL.WriteLog(Environment.NewLine + e.StackTrace);
                //    }
                //}

                
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
        

        public bool GrabarCoordinador(List<RCoordinadorBE> lstDatos)
        {
            bool resultado = true;
            var datos = new TipoListaCoordinadorTempCollection();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            foreach (RCoordinadorBE item in lstDatos)
            {
                //datos.Add(new ListaCoordinadorTemp(item["CodCeCo"],item["DescCeCo"],item["Coordinador"],new Guid(CannotUnloadAppDomainException)
                datos.Add(new ListaCoordinadorTemp(item.CodCeCo,item.DescCeCo,item.CuentaRed, item.IdCeCo,item.Nombre,
                            new Guid(Canales.IdEsquemaCoordinador),
                            new Guid(Canales.IdCreatorCoordinador), ConfigurationManager.AppSettings["EstadoFlujo"].ToString()
                            , new Guid(Canales.IdCanalCoordinador)));
            }
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "InsertarRegistrosCoordinadores";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandTimeout = 600;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@ListaCoordinadoresTemp", SqlDbType.Structured);//, SqlDbType.Structured,);
                par1.TypeName = "dbo.ListaCoordinadoresTemp";
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
        public class TipoListaCoordinadorTempCollection : List<ListaCoordinadorTemp>, IEnumerable<SqlDataRecord>
        {
            IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
            {

                var sqlDataRecord = new SqlDataRecord(  new SqlMetaData("CodCeCo", SqlDbType.Text),
                      new SqlMetaData("DescCeCo", SqlDbType.Text),  new SqlMetaData("IdCeCo", SqlDbType.Text),
                new SqlMetaData("Coordinador", SqlDbType.Text), new SqlMetaData("Nombre", SqlDbType.Text),
                new SqlMetaData("IdEsquema", SqlDbType.UniqueIdentifier), new SqlMetaData("IdCreator", SqlDbType.UniqueIdentifier),
                new SqlMetaData("EstadoFlujo", SqlDbType.Text), new SqlMetaData("IdCanal", SqlDbType.UniqueIdentifier));

                foreach (ListaCoordinadorTemp ListaMaestroTempitem in this)
                {
                    sqlDataRecord.SetString(0, ListaMaestroTempitem.CodCeCo);
                    sqlDataRecord.SetString(1, ListaMaestroTempitem.DescCeCo);
                    sqlDataRecord.SetString(2, ListaMaestroTempitem.IdCeCo);
                    sqlDataRecord.SetString(3, ListaMaestroTempitem.Coordinador);
                    sqlDataRecord.SetString(4, ListaMaestroTempitem.Nombre);
                    sqlDataRecord.SetGuid(5, ListaMaestroTempitem.IdEsquema);
                    sqlDataRecord.SetGuid(6, ListaMaestroTempitem.IdCreator);
                    sqlDataRecord.SetString(7, ListaMaestroTempitem.EstadoFlujo);
                    sqlDataRecord.SetGuid(8, ListaMaestroTempitem.IdCanal);

                    yield return sqlDataRecord;
                }
            }
        }
        public int ObtenerUltimaVersionMaestro(string idCanal)
        {
            ScriptorChannel canalPlanBase = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(idCanal));

            //Obtener ultima version del canal de maestros
           // var test = canalPlanBase.QueryContents("#Id", Guid.NewGuid(), "<>").ToList();
           // var test2 = test.OrderByDescending(x => x.Parts.Version).ToList();
           // var tes3 = test2.Select(x => x.Parts.Version).ToList();

            int maxVersion = 1;
            var versionMax = canalPlanBase.QueryContents("#Id", Guid.NewGuid(), "<>").OrderBy("$$.Version desc").Take(1).FirstOrDefault().Parts.Version;
                //.OrderByDescending(x => x.Parts.Version).Select(x => x.Parts.Version).ToList().FirstOrDefault();
            if (versionMax != null)
            {
                maxVersion = versionMax;
            }
            return maxVersion;
        }

        private void LlenarEntidadCoordinador(RCoordinadorBE item, IDataReader iDataReader)
        {
            if (!Convert.IsDBNull(iDataReader["Codigo"]))
            {
                item.CodCeCo = Convert.ToString(iDataReader["Codigo"]);
            }
            if (!Convert.IsDBNull(iDataReader["Descripcion"]))
            {
                item.DescCeCo = Convert.ToString(iDataReader["Descripcion"]);
            }
            if (!Convert.IsDBNull(iDataReader["Coordinador"]))
            {
                item.CuentaRed = Convert.ToString(iDataReader["Coordinador"]);
            }
        }

        private void LlenarEntidadCoordinadorTemp(RCoordinadorBE item, IDataReader iDataReader)
        {
            if (!Convert.IsDBNull(iDataReader["Codigo"]))
            {
                item.CodCeCo = Convert.ToString(iDataReader["Codigo"]);
            }
            if (!Convert.IsDBNull(iDataReader["Descripcion"]))
            {
                item.DescCeCo = Convert.ToString(iDataReader["Descripcion"]);
            }
            if (!Convert.IsDBNull(iDataReader["Coordinador"]))
            {
                item.CuentaRed = Convert.ToString(iDataReader["Coordinador"]);
            }
            if (!Convert.IsDBNull(iDataReader["Nombre"]))
            {
                item.Nombre = Convert.ToString(iDataReader["Nombre"]);
            }
            if (!Convert.IsDBNull(iDataReader["IdCeco"]))
            {
                item.IdCeCo = Convert.ToString(iDataReader["IdCeco"]);
            }
        }
    }
}
