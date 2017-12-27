using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.SqlClient;
using System.Data;
using Viatecla.Factory.Scriptor;
using Viatecla.Factory.Web;
using ScriptorModel = Viatecla.Factory.Scriptor.ModularSite.Models;
using System.Data.Common;
using Common;
using System.Configuration;

namespace DataLayer
{
    public class RSociedadDAL
    {
        public List<RSociedadBE> ObtenerSociedades()
        {
            ScriptorChannel canalSociedad = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Sociedad));


            List<ScriptorContent> Sociedades = canalSociedad.QueryContents("#Id", Guid.NewGuid(), "<>").ToList();

            List<RSociedadBE> oListaSociedad = new List<RSociedadBE>();

            RSociedadBE oSociedad;

            foreach (ScriptorContent item in Sociedades)
            {
                oSociedad = new RSociedadBE();

                oSociedad.Id = item.Id;
                oSociedad.Codigo = item.Parts.Codigo;
                oSociedad.Descripcion = item.Parts.Descripcion;
                oListaSociedad.Add(oSociedad);
            }

            return oListaSociedad;

        }

        public List<RSociedadBE> ObtenerSociedadesActivas()
        {
            //ObtenerSociedadesActivas
            List<RSociedadBE> oListaSociedad = new List<RSociedadBE>();
            RSociedadBE oSociedad;
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerSociedadesActivas";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        oSociedad = new RSociedadBE();
                        oSociedad.Id = new Guid(dataReader["Id"].ToString());
                        oSociedad.Codigo = dataReader["Codigo"].ToString();
                        oSociedad.Descripcion = dataReader["Codigo"].ToString() + " " + dataReader["Descripcion"].ToString();
                        oSociedad.IdMoneda = dataReader["IdMoneda"].ToString();
                        oSociedad.DescripcionMoneda = dataReader["DescripcionMoneda"].ToString();

                        oListaSociedad.Add(oSociedad);                       

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
            return oListaSociedad;
            
        }
        /*
        public bool EliminarSociedad(int piIdSociedad)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_DEL_SOCIEDAD"))
            {

                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdSociedad);

                try
                {
                    res = objDB.ExecuteNonQuery(objCMD);


                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return res > 0;
        }
        public bool InsertarSociedad(RSociedadBE poSociedadBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_INS_SOCIEDAD"))
            {

                objDB.AddInParameter(objCMD, "@Descripcion", DbType.String, poSociedadBE.Descripcion);
                objDB.AddInParameter(objCMD, "@Codigo", DbType.String, poSociedadBE.Codigo);
                objDB.AddInParameter(objCMD, "@EstaActivo", DbType.Boolean, poSociedadBE.EstaActivo);
                objDB.AddInParameter(objCMD, "@IdPais", DbType.Boolean, poSociedadBE.IdPais);

                try
                {
                    res = objDB.ExecuteNonQuery(objCMD);


                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return res > 0;
        }

        public bool ActualizarSociedad(RSociedadBE poSociedadBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_SOCIEDAD"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, poSociedadBE.Id);
                objDB.AddInParameter(objCMD, "@Descripcion", DbType.String, poSociedadBE.Descripcion);
                objDB.AddInParameter(objCMD, "@Codigo", DbType.String, poSociedadBE.Codigo);
                objDB.AddInParameter(objCMD, "@IdPais", DbType.String, poSociedadBE.IdPais);

                try
                {
                    res = objDB.ExecuteNonQuery(objCMD);


                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return res > 0;
        }

       */

        /*
        public List<RSociedadBE> ObtenerSociedades()
        {
            List<RSociedadBE> oListaSociedades = new List<RSociedadBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERSOCIEDADES"))
            {

                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RSociedadBE oSociedadBE = new RSociedadBE();
                            oSociedadBE.Id = (int)oDataReader["Id"];
                            oSociedadBE.Descripcion = (string)oDataReader["Descripcion"];
                            oSociedadBE.Codigo = (string)oDataReader["Codigo"];
                            oSociedadBE.EstaActivo = (bool)oDataReader["EstaActivo"];
                            oListaSociedades.Add(oSociedadBE);
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oListaSociedades;
        }
        */
        
        /*public List<RSociedadBE> ObtenerSociedadesCombo()
        {
            List<RSociedadBE> oListaSociedades = new List<RSociedadBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERSOCIEDADES"))
            {
                try
                {

                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RSociedadBE oSociedad = new RSociedadBE();
                            oSociedad.Id = (int)oDataReader["Id"];
                            oSociedad.Descripcion = (string)oDataReader["Descripcion"];
                            oSociedad.EstaActivo = (bool)oDataReader["EstaActivo"];
                            oListaSociedades.Add(oSociedad);
                        }
                    }
             
                }
                catch (Exception ex)
                {
                    //{
                    //    EventLog objLog = new EventLog();
                    //    objLog.LogError(ex);

                    throw ex;
                }
            }

            return oListaSociedades;
        }
*/

        /*public RSociedadBE ObtenerSociedadPorId(int piIdSociedad)
        {
            RSociedadBE oSociedadBE = new RSociedadBE();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERSOCIEDADPORID"))
            {

                try
                {
                    objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdSociedad);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        if (oDataReader.Read())
                        {
                            oSociedadBE.Id = (int)oDataReader["Id"];
                            oSociedadBE.Descripcion = (string)oDataReader["Descripcion"];
                            oSociedadBE.Codigo = (string)oDataReader["Codigo"];
                            oSociedadBE.IdPais = oDataReader["IdPais"] == DBNull.Value ? 0 : (int)oDataReader["IdPais"];
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oSociedadBE;
        }
         * */
    }
}
