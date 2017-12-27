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
    public class RCentroCostoDAL
    {
        public List<RCentroCostoBE> ObtenerCentrosCosto()
        {
            
            ScriptorChannel canalMaestroCeCos = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCo));

            //Obtener CeCos de la ultima version de la sociedad especificada
            List<ScriptorContent> listaCeCosMaestro = canalMaestroCeCos.QueryContents("#Id", Guid.NewGuid(), "<>").QueryContents("Version", ObtenerUltimaVersionMaestro(Canales.MaestroCeCo), "=").ToList();
            List<RCentroCostoBE> oListaCentroCosto = new List<RCentroCostoBE>();
            RCentroCostoBE oCentroCosto;
            foreach(ScriptorContent item in listaCeCosMaestro)
            {
                oCentroCosto = new RCentroCostoBE();
                oCentroCosto.Id = item.Id;
                oCentroCosto.Codigo = item.Parts.CodCECO;
                oCentroCosto.Descripcion = item.Parts.DescCECO;

                ScriptorContent contenidoMacroservicio = ((ScriptorDropdownListValue)item.Parts.IdMacroservicio).Content;
                oCentroCosto.CodigoMacroServicio = contenidoMacroservicio.Parts.Codigo;
                oCentroCosto.DescripcionMacroServicio = contenidoMacroservicio.Parts.Descripcion;

                ScriptorContent contenidoSector = ((ScriptorDropdownListValue)item.Parts.IdSector).Content;
                oCentroCosto.CodigoSector = contenidoSector.Parts.Codigo;
                oCentroCosto.DescripcionSector = contenidoSector.Parts.Descripcion;

                ScriptorContent contenidoSociedad = ((ScriptorDropdownListValue)item.Parts.IdSociedad).Content;
                oCentroCosto.CodigoSociedad = contenidoSociedad.Parts.Codigo;
                oCentroCosto.DescripcionSociedad = contenidoSociedad.Parts.Descripcion;

                oCentroCosto.GerenteLinea = item.Parts.GerenteLinea;
                oCentroCosto.GerenteCentral = item.Parts.GerenteCentral;
                oListaCentroCosto.Add(oCentroCosto);
            }
            return oListaCentroCosto;
        }

        public List<RCentroCostoBE> ObtenerCentrosCostoBD()
        {
            List<RCentroCostoBE> oListaCentroCosto = new List<RCentroCostoBE>();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerListaCentroCosto";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 600;
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        RCentroCostoBE CentroCostoBE = new RCentroCostoBE();
                        LlenarEntidadCeCoTemp(CentroCostoBE, dataReader);
                        oListaCentroCosto.Add(CentroCostoBE);
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
            return oListaCentroCosto;
        }

        public List<RCentroCostoBE> ObtenerCentrosCostoTemp()
        {

            ScriptorChannel canalMaestroCeCosTemp = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCoTemp));

            //Obtener CeCos de la ultima version de la sociedad especificada
            List<ScriptorContent> listaCeCosMaestroTemp = canalMaestroCeCosTemp.QueryContents("#Id", Guid.NewGuid(), "<>")
                                                                                .QueryContents("Version", ObtenerUltimaVersionMaestro(Canales.MaestroCeCoTemp), "=").ToList();

            List<RCentroCostoBE> oListaCentroCosto = new List<RCentroCostoBE>();
            RCentroCostoBE oCentroCosto;
            foreach (ScriptorContent item in listaCeCosMaestroTemp)
            {
                oCentroCosto = new RCentroCostoBE();

                oCentroCosto.Id = item.Id;
                oCentroCosto.Codigo = item.Parts.CodCeCo;
                oCentroCosto.Descripcion = item.Parts.DescCeCo;
                oCentroCosto.CodigoMacroServicio = item.Parts.CodMacroServicio;
                oCentroCosto.DescripcionMacroServicio = item.Parts.DescMacroservicio;
                oCentroCosto.CodigoSector = item.Parts.CodSector;
                oCentroCosto.DescripcionSector = item.Parts.DescSector;
                oCentroCosto.CodigoSociedad = item.Parts.CodSociedad;
                oCentroCosto.DescripcionSociedad = item.Parts.DescSociedad;
                oCentroCosto.GerenteLinea = item.Parts.UserGerenteLinea;
                oCentroCosto.GerenteCentral = item.Parts.UserGerenteCentral;
                if (!String.IsNullOrEmpty(item.Parts.IdSociedad))
                {
                    oCentroCosto.IdSociedad = new Guid(item.Parts.IdSociedad);
                }
                if (!String.IsNullOrEmpty(item.Parts.IdSector))
                {
                    oCentroCosto.IdSector = new Guid(item.Parts.IdSector);
                } if (!String.IsNullOrEmpty(item.Parts.IdMacroServicio))
                {
                    oCentroCosto.IdMacroServicio = new Guid(item.Parts.IdMacroServicio);
                }
                oListaCentroCosto.Add(oCentroCosto);
            }
            return oListaCentroCosto;
        }

        public List<RCentroCostoBE> ObtenerCentrosCostoTempBD()
        {
            List<RCentroCostoBE> oListaCentroCosto = new List<RCentroCostoBE>();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerListaCentroCostoTemp";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        RCentroCostoBE CentroCostoBE = new RCentroCostoBE();
                        LlenarEntidadCeCoTemp(CentroCostoBE, dataReader);
                        oListaCentroCosto.Add(CentroCostoBE);
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
            return oListaCentroCosto;
        }
        public List<RCentroCostoBE> ObtenerCentrosCostoPorSociedadCoordinador(string idSociedad, string loginNameCoordinador)
        {

            /*
            ScriptorChannel canalCoordinador = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Coordinador));            
            ScriptorChannel canalMaestroCeCos = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCo));

            //Obtener lista de CeCos que le pertenecen al coordinador
            List<ScriptorContent> listaCeCosAux = canalCoordinador.QueryContents("CuentaRed", loginNameCoordinador, "=").ToList();

            //Obtener CeCos de la ultima version de la sociedad especificada
            List<ScriptorContent> listaCeCosMaestro = canalMaestroCeCos.QueryContents("IdSociedad", idSociedad, "=")
                                                                        .QueryContents("Version", ObtenerUltimaVersionMaestro(Canales.MaestroCeCo), "=").ToList();
                //.QueryContents("DescCECO", "%"+ valueCOD_DESC+ "%", "LIKE")
                                                                       // .QueryContents("CodCECO", "%" + valueCOD_DESC + "%", "LIKE")
                                                               
            
            //Verificar que se encuentren en las 2 listas
            List<ScriptorContent> listaFinal = listaCeCosAux.Where(x =>
                listaCeCosMaestro.Contains(((ScriptorDropdownListValue)x.Parts.IdCeCo).Content)).Select(x => (x.Parts.IdCeCo as ScriptorDropdownListValue).Content).ToList();

            List<RCentroCostoBE> oListaCentroCosto = new List<RCentroCostoBE>();
            RCentroCostoBE oCentroCosto;

            foreach (ScriptorContent item in listaFinal)
            {
                oCentroCosto = new RCentroCostoBE();
                oCentroCosto.Codigo = item.Parts.CodCECO;
                oCentroCosto.Descripcion = item.Parts.CodCECO + " " + item.Parts.DescCECO;
                ScriptorContent itemMacroservicio = ((ScriptorDropdownListValue)item.Parts.IdMacroservicio).Content;
                oCentroCosto.IdMacroServicio = itemMacroservicio.Id;
                oCentroCosto.CodigoMacroServicio = itemMacroservicio.Parts.Codigo;
                oCentroCosto.DescripcionMacroServicio = itemMacroservicio.Parts.Descripcion;
                ScriptorContent itemSector = ((ScriptorDropdownListValue)item.Parts.IdSector).Content;

                oCentroCosto.IdSector = itemSector.Id;
                oCentroCosto.DescripcionSector = itemSector.Parts.Descripcion;
                oCentroCosto.CodigoSector = itemSector.Parts.Codigo;

                oCentroCosto.Id = item.Id;

                oListaCentroCosto.Add(oCentroCosto);
            }
            */
            List<RCentroCostoBE> oListaCentroCosto = new List<RCentroCostoBE>();
            RCentroCostoBE oCentroCosto;
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
           
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerListaCentroCostoPorSociedadCoordinador";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@IdSociedad", SqlDbType.NVarChar);//, SqlDbType.Structured,);                
                par1.Value = idSociedad;
                cmd.Parameters.Add(par1);

                if (loginNameCoordinador == null)
                    par1 = new SqlParameter("@Coordinador", DBNull.Value);
                else
                    par1 = new SqlParameter("@Coordinador", loginNameCoordinador);
                cmd.Parameters.Add(par1);

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        
                        oCentroCosto = new RCentroCostoBE();

                        oCentroCosto.Codigo = dataReader["Codigo"].ToString();
                        oCentroCosto.Descripcion = dataReader["Codigo"].ToString() + " " + dataReader["Descripcion"].ToString();

                        oCentroCosto.IdMacroServicio = new Guid(dataReader["IdMacroServicio"].ToString());
                        oCentroCosto.CodigoMacroServicio = dataReader["CodMacroServicio"].ToString();
                        oCentroCosto.DescripcionMacroServicio = dataReader["DescMacroServicio"].ToString();

                        oCentroCosto.IdSector = new Guid(dataReader["IdSector"].ToString());
                        oCentroCosto.DescripcionSector = dataReader["DescSector"].ToString();
                        oCentroCosto.CodigoSector = dataReader["CodSector"].ToString();

                        oCentroCosto.Id = new Guid(dataReader["Id"].ToString());

                        oListaCentroCosto.Add(oCentroCosto);
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
            

            return oListaCentroCosto;

        }

        public List<RCentroCostoBE> ObtenerCentrosCostoPorSociedadAdministracion(string idSociedad, string loginNameCoordinador)
        {
            List<RCentroCostoBE> oListaCentroCosto = new List<RCentroCostoBE>();
            RCentroCostoBE oCentroCosto;
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerListaCentroCostoPorSociedadAdministracion";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@IdSociedad", SqlDbType.NVarChar);//, SqlDbType.Structured,);                
                par1.Value = idSociedad;
                cmd.Parameters.Add(par1);

                if (loginNameCoordinador == null)
                    par1 = new SqlParameter("@Coordinador", DBNull.Value);
                else
                    par1 = new SqlParameter("@Coordinador", loginNameCoordinador);
                cmd.Parameters.Add(par1);

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        oCentroCosto = new RCentroCostoBE();

                        oCentroCosto.Codigo = dataReader["Codigo"].ToString();
                        oCentroCosto.Descripcion = dataReader["Codigo"].ToString() + " " + dataReader["Descripcion"].ToString();

                        oCentroCosto.IdMacroServicio = new Guid(dataReader["IdMacroServicio"].ToString());
                        oCentroCosto.CodigoMacroServicio = dataReader["CodMacroServicio"].ToString();
                        oCentroCosto.DescripcionMacroServicio = dataReader["DescMacroServicio"].ToString();

                        oCentroCosto.IdSector = new Guid(dataReader["IdSector"].ToString());
                        oCentroCosto.DescripcionSector = dataReader["DescSector"].ToString();
                        oCentroCosto.CodigoSector = dataReader["CodSector"].ToString();

                        oCentroCosto.Id = new Guid(dataReader["Id"].ToString());

                        oListaCentroCosto.Add(oCentroCosto);
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


            return oListaCentroCosto;    
        }

        public List<RCentroCostoBE> ObtenerCentrosCostoPorSociedadQuery(string idSociedad, string loginNameCoordinador, string query)
        {

            List<RCentroCostoBE> oListaCentroCosto = new List<RCentroCostoBE>();
            RCentroCostoBE oCentroCosto;
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();

                string nombreProcedure = "APIObtenerListaCentroCostoPorSociedadQuery";

                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@IdSociedad", SqlDbType.NVarChar);//, SqlDbType.Structured,);                
                par1.Value = idSociedad;
                cmd.Parameters.Add(par1);

                if (loginNameCoordinador == null)
                    par1 = new SqlParameter("@Coordinador", DBNull.Value);
                else
                    par1 = new SqlParameter("@Coordinador", loginNameCoordinador);
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@query", query);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        oCentroCosto = new RCentroCostoBE();

                        oCentroCosto.Codigo = dataReader["Codigo"].ToString();
                        oCentroCosto.Descripcion = dataReader["Codigo"].ToString() + " " + dataReader["Descripcion"].ToString();

                        oCentroCosto.IdMacroServicio = new Guid(dataReader["IdMacroServicio"].ToString());
                        oCentroCosto.CodigoMacroServicio = dataReader["CodMacroServicio"].ToString();
                        oCentroCosto.DescripcionMacroServicio = dataReader["DescMacroServicio"].ToString();

                        oCentroCosto.IdSector = new Guid(dataReader["IdSector"].ToString());
                        oCentroCosto.DescripcionSector = dataReader["DescSector"].ToString();
                        oCentroCosto.CodigoSector = dataReader["CodSector"].ToString();

                        oCentroCosto.Id = new Guid(dataReader["Id"].ToString());

                        oListaCentroCosto.Add(oCentroCosto);
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


            return oListaCentroCosto;

        }

        public string ValidarCentroCostoActual(string CodigoInversion)
        {
            
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            string res = "";

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "APIValidarCeCoActual";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@CodigoInversion", CodigoInversion);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                         res = dataReader["Respuesta"].ToString();
                        
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

        public int APIValidarCeCoSociedad(string idCeCo, string idSociedad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            int res = 0;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "APIValidarCeCoSociedad";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdCeCo", idCeCo);
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@idSociedad", idSociedad);
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

        public bool  existeCentroCosto(string codigo)
        {
            bool resultado = false;
            ScriptorChannel canalMaestroCeCos = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCo));
            List<ScriptorContent> listaCeCosMaestro = canalMaestroCeCos.QueryContents("#Id", Guid.NewGuid(), "<>")
                                                                       .QueryContents("Version", ObtenerUltimaVersionMaestro(Canales.MaestroCeCo), "=")
                                                                       .QueryContents("CodCECO",codigo, "=").ToList();
            if (listaCeCosMaestro.Count > 0)
            {
                resultado = true;
            }
            return resultado;
        }
        public Guid obtenerIdCentroCostro(string codigo)
        {
            Guid resultado = new Guid();
            ScriptorChannel canalMaestroCeCos = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCo));
            List<ScriptorContent> listaCeCosMaestro = canalMaestroCeCos.QueryContents("#Id", Guid.NewGuid(), "<>")
                                                                       .QueryContents("Version", ObtenerUltimaVersionMaestro(Canales.MaestroCeCo), "=")
                                                                       .QueryContents("CodCECO", codigo, "=").ToList();
            if (listaCeCosMaestro.Count > 0)
            {
                resultado =listaCeCosMaestro[0].Id;
            }
            return resultado;

        }
        public RCentroCostoBE obtenerCentroCostoPorCodigo(string codigo)
        {
            RCentroCostoBE oCentroCosto = new RCentroCostoBE();
            ScriptorChannel canalMaestroCeCos = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCo));
            List<ScriptorContent> listaCeCosMaestro = canalMaestroCeCos.QueryContents("#Id", Guid.NewGuid(), "<>")
                                                                       .QueryContents("Version", ObtenerUltimaVersionMaestro(Canales.MaestroCeCo), "=")
                                                                       .QueryContents("CodCECO", codigo, "=").ToList();
            if (listaCeCosMaestro.Count > 0)
            {

                ScriptorContent item = listaCeCosMaestro[0];
                oCentroCosto.Id = item.Id;
                oCentroCosto.Codigo = item.Parts.CodCECO;
                oCentroCosto.Descripcion = item.Parts.DescCECO;
                //oCentroCosto.CodigoMacroServicio = ((ScriptorDropdownListValue)item.Parts.IdMacroservicio).Content.Parts.Codigo;
                //oCentroCosto.DescripcionMacroServicio = ((ScriptorDropdownListValue)item.Parts.IdMacroservicio).Content.Parts.Descripcion;
                //oCentroCosto.CodigoSector = ((ScriptorDropdownListValue)item.Parts.IdSector).Content.Parts.Codigo;
                //oCentroCosto.DescripcionSector = ((ScriptorDropdownListValue)item.Parts.IdSector).Content.Parts.Descripcion;
                //oCentroCosto.CodigoSociedad = ((ScriptorDropdownListValue)item.Parts.IdSociedad).Content.Parts.Codigo;
                //oCentroCosto.DescripcionSociedad = ((ScriptorDropdownListValue)item.Parts.IdSociedad).Content.Parts.Descripcion;
                //oCentroCosto.GerenteLinea = item.Parts.GerenteLinea;
                //oCentroCosto.GerenteCentral = item.Parts.GerenteCentral;
                
            }
            return oCentroCosto;

        }

        public RCentroCostoBE obtenerCentroCostoPorId(string IdCeCo)
        {
            

            RCentroCostoBE oCentroCosto = new RCentroCostoBE();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            string res = "";

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "APIObtenerListaCentroCostoPorId";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdCeCo", IdCeCo);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        LlenarEntidadCeCoTemp(oCentroCosto, dataReader);
                        
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

            return oCentroCosto;

        }
        public RCentroCostoBE obtenerCentroCostoPorCodigoBD(string codigo)
        {
            RCentroCostoBE CentroCostoBE = new RCentroCostoBE();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerListaCentroCostoPorCodigo";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@Codigo", codigo);
                cmd.Parameters.Add(par1);
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        LlenarEntidadCeCoTemp(CentroCostoBE, dataReader);
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
            return CentroCostoBE;
        }

        public int ObtenerUltimaVersionMaestro(string idCanal)
        {
            ScriptorChannel canalMaestroCeCos = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(idCanal));

            //Obtener ultima version del canal de maestros
            int maxVersion = 1;
            //var versionMax = canalMaestroCeCos.QueryContents("#Id", Guid.NewGuid(), "<>").OrderByDescending(x => x.Parts.Version).Select(x => x.Parts.Version).ToList().FirstOrDefault();
            var versionMax = canalMaestroCeCos.QueryContents("#Id", Guid.NewGuid(), "<>").OrderBy("$$.Version desc").Take(1).FirstOrDefault().Parts.Version;
            if (versionMax != null)
            {
                maxVersion = versionMax;
            }
            return maxVersion;
        }
        public RCentroCostoBE ObtenerMaestroCentroCostoPorCodigo(string codigo)
        {
            RCentroCostoBE oCentroCosto = new RCentroCostoBE();
            ScriptorChannel canalMaestroCeCos = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCo));
            List<ScriptorContent> listaCeCosMaestro = canalMaestroCeCos.QueryContents("#Id", Guid.NewGuid(), "<>")
                                                                       .QueryContents("Version", ObtenerUltimaVersionMaestro(Canales.MaestroCeCo), "=")
                                                                       .QueryContents("CodCECO", codigo, "=").ToList();
            if (listaCeCosMaestro.Count > 0)
            {

                ScriptorContent item = listaCeCosMaestro[0];
                oCentroCosto.Id = item.Id;
                oCentroCosto.Codigo = item.Parts.CodCECO;
                oCentroCosto.Descripcion = item.Parts.DescCECO;
                oCentroCosto.CodigoMacroServicio = ((ScriptorDropdownListValue)item.Parts.IdMacroservicio).Content.Parts.Codigo;
                oCentroCosto.DescripcionMacroServicio = ((ScriptorDropdownListValue)item.Parts.IdMacroservicio).Content.Parts.Descripcion;
                oCentroCosto.CodigoSector = ((ScriptorDropdownListValue)item.Parts.IdSector).Content.Parts.Codigo;
                oCentroCosto.DescripcionSector = ((ScriptorDropdownListValue)item.Parts.IdSector).Content.Parts.Descripcion;
                oCentroCosto.CodigoSociedad = ((ScriptorDropdownListValue)item.Parts.IdSociedad).Content.Parts.Codigo;
                oCentroCosto.DescripcionSociedad = ((ScriptorDropdownListValue)item.Parts.IdSociedad).Content.Parts.Descripcion;
                oCentroCosto.GerenteLinea = item.Parts.GerenteLinea;
                oCentroCosto.GerenteCentral = item.Parts.GerenteCentral;

            }
            return oCentroCosto;

        }
        public List<RSociedadBE> ObtenerDescripcionSociedades(List<string> codigosSociedad)
        {
            List<RSociedadBE> listaSociedades = new List<RSociedadBE>();
            var dataNumeros = new TipoListaCodigosCollection();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            foreach (string codigo in codigosSociedad)
            {
                dataNumeros.Add(new ListaCodigos(codigo));
            }
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerListaDescripcionSociedad";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@ListaCodigosSociedad", SqlDbType.Structured);//, SqlDbType.Structured,);
                par1.TypeName = "dbo.ListaCodigos";
                par1.Value = dataNumeros;
                cmd.Parameters.Add(par1);
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        RSociedadBE SociedadBE = new RSociedadBE();
                        LlenarEntidadSociedad(SociedadBE, dataReader);
                        listaSociedades.Add(SociedadBE);
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
            return listaSociedades;
        }
        public List<RSectorBE> ObtenerDescripcionSectores(List<string> codigosSector)
        {
            List<RSectorBE> listaSectores = new List<RSectorBE>();
            var dataNumeros = new TipoListaCodigosCollection();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            foreach (string codigo in codigosSector)
            {
                dataNumeros.Add(new ListaCodigos(codigo));
            }
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerListaDescripcionSector";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@ListaCodigosSector", SqlDbType.Structured);//, SqlDbType.Structured,);
                par1.TypeName = "dbo.ListaCodigos";
                par1.Value = dataNumeros;
                cmd.Parameters.Add(par1);
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        RSectorBE RSectorBE = new RSectorBE();
                        LlenarEntidadSector(RSectorBE, dataReader);
                        listaSectores.Add(RSectorBE);
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
            return listaSectores;
        }
        public List<RMacroservicioBE> ObtenerDescripcionMacroservicio(List<string> codigosMacroservicio)
        {
            List<RMacroservicioBE> listaMacroservicio = new List<RMacroservicioBE>();
            var dataNumeros = new TipoListaCodigosCollection();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            foreach (string codigo in codigosMacroservicio)
            {
                dataNumeros.Add(new ListaCodigos(codigo));
            }
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerListaDescripcionMacroservicio";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@ListaCodigosMacroservicio", SqlDbType.Structured);//, SqlDbType.Structured,);
                par1.TypeName = "dbo.ListaCodigos";
                par1.Value = dataNumeros;
                cmd.Parameters.Add(par1);
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        RMacroservicioBE RMacroservicioBE = new RMacroservicioBE();
                        LlenarEntidadMacroservicio(RMacroservicioBE, dataReader);
                        listaMacroservicio.Add(RMacroservicioBE);
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
            return listaMacroservicio;
        }

        public List<RCentroCostoBE> ObtenerCeCoAdministracion(string IdSociedad, string IdMacroservicio, string IdSector)
        {
            List<RCentroCostoBE> oLista = new List<RCentroCostoBE>();
            RCentroCostoBE oCentroCosto;

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerCeCoAdministracion";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdSociedad", IdSociedad);
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@IdMacroservicio", IdMacroservicio);
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@IdSector", IdSector);
                cmd.Parameters.Add(par1);

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        oCentroCosto = new RCentroCostoBE();
                        //oCentroCosto.Id = new Guid(dataReader["Id"].ToString());
                        oCentroCosto.Codigo = dataReader["Codigo"].ToString();
                        oCentroCosto.Descripcion = dataReader["Descripcion"].ToString();
                        oLista.Add(oCentroCosto);
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

        public List<RSectorBE> ObtenerSectorPorSociedad(string IdSociedad)
        {
            List<RSectorBE> oLista = new List<RSectorBE>();
            RSectorBE oSector;

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerSectorPorSociedad";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdSociedad", IdSociedad);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        oSector = new RSectorBE();
                        oSector.Id = new Guid(dataReader["Id"].ToString());
                        oSector.Codigo = dataReader["Codigo"].ToString();
                        oSector.Descripcion = dataReader["Descripcion"].ToString();
                        oLista.Add(oSector);

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

        public List<RMacroservicioBE> ObtenerMacroservicioPorSociedad(string IdSociedad)
        {
            List<RMacroservicioBE> oLista = new List<RMacroservicioBE>();
            RMacroservicioBE oMacroservicio;

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerMacroservicioPorSociedad";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdSociedad", IdSociedad);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        oMacroservicio = new RMacroservicioBE();
                        oMacroservicio.Id = new Guid(dataReader["Id"].ToString());
                        oMacroservicio.Codigo = dataReader["Codigo"].ToString();
                        oMacroservicio.Descripcion = dataReader["Descripcion"].ToString();
                        oLista.Add(oMacroservicio);

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

        public bool GrabarMaestroTemp(List<Dictionary<string,string>> lstDatos)
        {
            bool resultado = true;
            var datos = new TipoListaMaestroTempCollection();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            foreach (Dictionary<string, string> item in lstDatos)
            {
              
                datos.Add(new ListaMaestroTemp(item["CodSociedad"], item["CodMacroServicio"], item["CodSector"], item["CodCeCo"],
                    item["DescCeCo"], item["UserGerenteLinea"], item["UserGerenteCentral"], item["DescripcionSociedad"], item["DescripcionSector"],
                    item["DescripcionMacroservicio"],new Guid(Canales.IdEsquemaMaestroTemp),new Guid(Canales.IdCreatorMaestroTemp),
                    ConfigurationManager.AppSettings["EstadoFlujo"].ToString()
                    ,new Guid(Canales.IdCanalMaestroTemp),item["IdSociedad"],item["IdMacroservicio"],item["IdSector"]));
            }
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "InsertarRegistrosMaestroTemp";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandTimeout = 600;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@ListaMaestroTemp", SqlDbType.Structured);//, SqlDbType.Structured,);
                par1.TypeName = "dbo.ListaMaestroTemp";
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
                ManejadorLogSimpleBL.WriteLog(ex.Message);
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
        public bool GrabarMaestro(List<RCentroCostoBE> lstDatos)
        {
            bool resultado = true;
            var datos = new TipoListaMaestroCollection();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            foreach (RCentroCostoBE item in lstDatos)
            {
                //datos.Add(new ListaMaestro(item.IdSociedad,i));
                datos.Add(new ListaMaestro(item.IdSociedad,item.IdMacroServicio,item.IdSector,item.Codigo,item.Descripcion,item.GerenteLinea,
                item.GerenteCentral,new Guid(Canales.IdEsquemaMaestro),new Guid(Canales.IdCreatorMaestro),
                ConfigurationManager.AppSettings["EstadoFlujo"].ToString(), new Guid(Canales.IdCanalMaestro)));
            }
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "InsertarRegistrosMaestro";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandTimeout = 600;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@ListaMaestro", SqlDbType.Structured);//, SqlDbType.Structured,);
                par1.TypeName = "dbo.ListaMaestro";
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
        public class TipoListaMaestroTempCollection : List<ListaMaestroTemp>, IEnumerable<SqlDataRecord>
        {
            IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
            {
              
                var sqlDataRecord = new SqlDataRecord(new SqlMetaData("CodSociedad", SqlDbType.Text),
                new SqlMetaData("CodMacroServicio", SqlDbType.Text), new SqlMetaData("CodSector", SqlDbType.Text), new SqlMetaData("CodCeCo", SqlDbType.Text),
                new SqlMetaData("DescCeCo", SqlDbType.Text), new SqlMetaData("UserGerenteLinea", SqlDbType.Text), new SqlMetaData("UserGerenteCentral", SqlDbType.Text),
                new SqlMetaData("DescSociedad", SqlDbType.Text), new SqlMetaData("DescSector", SqlDbType.Text), new SqlMetaData("DescMacroservicio", SqlDbType.Text),
                new SqlMetaData("IdEsquema", SqlDbType.UniqueIdentifier), new SqlMetaData("IdCreator", SqlDbType.UniqueIdentifier),
                new SqlMetaData("EstadoFlujo", SqlDbType.Text), new SqlMetaData("IdCanal", SqlDbType.UniqueIdentifier),
                new SqlMetaData("IdSociedad", SqlDbType.Text), new SqlMetaData("IdMacroservicio", SqlDbType.Text),
                new SqlMetaData("IdSector", SqlDbType.Text));
 
                foreach (ListaMaestroTemp ListaMaestroTempitem in this)
                {
                  //  sqlDataRecord.SetGuid(0, ListaMaestroTempitem.IDGenerado);
                    sqlDataRecord.SetString(0, ListaMaestroTempitem.CodCeCo);                    
                    sqlDataRecord.SetString(1, ListaMaestroTempitem.CodMacroservicio);
                    sqlDataRecord.SetString(2, ListaMaestroTempitem.CodSector);
                    sqlDataRecord.SetString(3, ListaMaestroTempitem.CodSociedad);
                    sqlDataRecord.SetString(4, ListaMaestroTempitem.DescCeCo);
                    sqlDataRecord.SetString(5, ListaMaestroTempitem.DescMacroservicio);
                    sqlDataRecord.SetString(6, ListaMaestroTempitem.DescSector);
                    sqlDataRecord.SetString(7, ListaMaestroTempitem.DescSociedad);
                    sqlDataRecord.SetString(8, ListaMaestroTempitem.UserGerenteCentral);
                    sqlDataRecord.SetString(9, ListaMaestroTempitem.UserGerenteLinea);
                    sqlDataRecord.SetGuid(10, ListaMaestroTempitem.IdEsquema);
                    sqlDataRecord.SetGuid(11, ListaMaestroTempitem.IdCreator);
                    sqlDataRecord.SetString(12, ListaMaestroTempitem.EstadoFlujo);
                    sqlDataRecord.SetGuid(13, ListaMaestroTempitem.IdCanal);
                    sqlDataRecord.SetString(14, ListaMaestroTempitem.IdSociedad);
                    sqlDataRecord.SetString(15, ListaMaestroTempitem.IdMacroservicio);
                    sqlDataRecord.SetString(16, ListaMaestroTempitem.IdSector);
                    yield return sqlDataRecord;
                }
            }
        }
        public class TipoListaMaestroCollection : List<ListaMaestro>, IEnumerable<SqlDataRecord>
        {
            IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
            {

                var sqlDataRecord = new SqlDataRecord( 
                    new SqlMetaData("CodCeCo", SqlDbType.Text),
                    new SqlMetaData("CodMacroServicio", SqlDbType.UniqueIdentifier),
                    new SqlMetaData("CodSector", SqlDbType.UniqueIdentifier), 
                    new SqlMetaData("CodSociedad", SqlDbType.UniqueIdentifier),
                    new SqlMetaData("DescCeCo", SqlDbType.Text),
                    new SqlMetaData("UserGerenteCentral", SqlDbType.Text),
                    new SqlMetaData("UserGerenteLinea", SqlDbType.Text),
                    new SqlMetaData("IdEsquema", SqlDbType.UniqueIdentifier), 
                    new SqlMetaData("IdCreator", SqlDbType.UniqueIdentifier),
                    new SqlMetaData("EstadoFlujo", SqlDbType.Text), 
                    new SqlMetaData("IdCanal", SqlDbType.UniqueIdentifier));

                foreach (ListaMaestro ListaMaestroTempitem in this)
                {
                    
                    sqlDataRecord.SetString(0, ListaMaestroTempitem.CodCeCo);
                    sqlDataRecord.SetGuid(1, ListaMaestroTempitem.CodMacroservicio);
                    sqlDataRecord.SetGuid(2, ListaMaestroTempitem.CodSector);
                    sqlDataRecord.SetGuid(3, ListaMaestroTempitem.CodSociedad);
                    sqlDataRecord.SetString(4, ListaMaestroTempitem.DescCeCo);
                    sqlDataRecord.SetString(5, ListaMaestroTempitem.UserGerenteCentral);
                    sqlDataRecord.SetString(6, ListaMaestroTempitem.UserGerenteLinea);
                    sqlDataRecord.SetGuid(7, ListaMaestroTempitem.IdEsquema);
                    sqlDataRecord.SetGuid(8, ListaMaestroTempitem.IdCreator);
                    sqlDataRecord.SetString(9, ListaMaestroTempitem.EstadoFlujo);
                    sqlDataRecord.SetGuid(10, ListaMaestroTempitem.IdCanal);
                    yield return sqlDataRecord;
                }
            }
        }
        
        private void LlenarEntidadSociedad(RSociedadBE item, IDataReader iDataReader)
        {
            if (!Convert.IsDBNull(iDataReader["Codigo"]))
            {
                item.Codigo = Convert.ToString(iDataReader["Codigo"]);
            }
            if (!Convert.IsDBNull(iDataReader["Descripcion"]))
            {
                item.Descripcion = Convert.ToString(iDataReader["Descripcion"]);
            }
            if (!Convert.IsDBNull(iDataReader["Id"]))
            {
                item.Id = (Guid)iDataReader["Id"];
            }
        }
        private void LlenarEntidadSector(RSectorBE item, IDataReader iDataReader)
        {
            if (!Convert.IsDBNull(iDataReader["Codigo"]))
            {
                item.Codigo = Convert.ToString(iDataReader["Codigo"]);
            }
            if (!Convert.IsDBNull(iDataReader["Descripcion"]))
            {
                item.Descripcion = Convert.ToString(iDataReader["Descripcion"]);
            }
            if (!Convert.IsDBNull(iDataReader["Id"]))
            {
                item.Id = (Guid)iDataReader["Id"];
            }
        }
        private void LlenarEntidadMacroservicio(RMacroservicioBE item, IDataReader iDataReader)
        {
            if (!Convert.IsDBNull(iDataReader["Codigo"]))
            {
                item.Codigo = Convert.ToString(iDataReader["Codigo"]);
            }
            if (!Convert.IsDBNull(iDataReader["Descripcion"]))
            {
                item.Descripcion = Convert.ToString(iDataReader["Descripcion"]);
            }
            if (!Convert.IsDBNull(iDataReader["Id"]))
            {
                item.Id = (Guid)iDataReader["Id"];
            }
        }

        private void LlenarEntidadCeCoTemp(RCentroCostoBE item, IDataReader iDataReader)
        {
            if (!Convert.IsDBNull(iDataReader["CodigoSociedad"]))
            {
                item.CodigoSociedad = Convert.ToString(iDataReader["CodigoSociedad"]);
            }
            if (!Convert.IsDBNull(iDataReader["CodigoMacroServicio"]))
            {
                item.CodigoMacroServicio = Convert.ToString(iDataReader["CodigoMacroServicio"]);
            }
            if (!Convert.IsDBNull(iDataReader["CodigoSector"]))
            {
                item.CodigoSector = Convert.ToString(iDataReader["CodigoSector"]);
            }
            if (!Convert.IsDBNull(iDataReader["Codigo"]))
            {
                item.Codigo = Convert.ToString(iDataReader["Codigo"]);
            }
            if (!Convert.IsDBNull(iDataReader["Descripcion"]))
            {
                item.Descripcion = Convert.ToString(iDataReader["Descripcion"]);
            }
            if (!Convert.IsDBNull(iDataReader["GerenteLinea"]))
            {
                item.GerenteLinea = Convert.ToString(iDataReader["GerenteLinea"]);
            }
            if (!Convert.IsDBNull(iDataReader["GerenteCentral"]))
            {
                item.GerenteCentral = Convert.ToString(iDataReader["GerenteCentral"]);
            }
            if (!Convert.IsDBNull(iDataReader["Id"]))
            {
                item.Id = (Guid)(iDataReader["Id"]);
            }    
            if (!Convert.IsDBNull(iDataReader["DescripcionMacroServicio"]))
            {
                item.DescripcionMacroServicio = Convert.ToString(iDataReader["DescripcionMacroServicio"]);
            }
         
            if (!Convert.IsDBNull(iDataReader["DescripcionSector"]))
            {
                item.DescripcionSector = Convert.ToString(iDataReader["DescripcionSector"]);
            }
            
            if (!Convert.IsDBNull(iDataReader["DescripcionSociedad"]))
            {
                item.DescripcionSociedad = Convert.ToString(iDataReader["DescripcionSociedad"]);
            }
            if (!Convert.IsDBNull(iDataReader["IdSociedad"]))
            {
                if (!String.IsNullOrEmpty(iDataReader["IdSociedad"].ToString()))
                {
                    item.IdSociedad = new Guid(iDataReader["IdSociedad"].ToString());
                }
            }
            if (!Convert.IsDBNull(iDataReader["IdSector"]))
            {
                if (!String.IsNullOrEmpty(iDataReader["IdSector"].ToString()))
                {
                    item.IdSector = new Guid(iDataReader["IdSector"].ToString());
                }
            }
            if (!Convert.IsDBNull(iDataReader["IdMacroServicio"]))
            {
                if (!String.IsNullOrEmpty(iDataReader["IdMacroServicio"].ToString()))
                {
                    item.IdMacroServicio = new Guid(iDataReader["IdMacroServicio"].ToString());
                }
            }
        }

        

        
    }
}
