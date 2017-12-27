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
using BusinessEntities;
using BusinessEntities.DTO;
using Common;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DataLayer
{
    public class RAprobadoresDAL
    {

        
        /*
        public bool EliminarAprobador(int IdAprobador)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_DEL_APROBADOR"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, IdAprobador);
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
        public bool InsertarAprobador(RAprobadoresBE poAprobadorBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_INS_APROBADOR"))
            {
                objDB.AddInParameter(objCMD, "@IdAreaCorporativa", DbType.String, poAprobadorBE.IdAreaCorporativa);
                objDB.AddInParameter(objCMD, "@Usuario", DbType.String, poAprobadorBE.Usuario);
                objDB.AddInParameter(objCMD, "@Nombre", DbType.String, poAprobadorBE.Nombre);
                objDB.AddInParameter(objCMD, "@Rol", DbType.String, poAprobadorBE.Rol);
                objDB.AddInParameter(objCMD, "@IdRol", DbType.String, poAprobadorBE.IdRol);
                objDB.AddInParameter(objCMD, "@EstaActivo", DbType.Boolean, poAprobadorBE.EstaActivo);

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
        public List<RAprobadoresBE> ObtenerTipos()
        {
            List<RAprobadoresBE> oListaTipos = new List<RAprobadoresBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERTIPOS"))
            {
                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RAprobadoresBE oAprobadorBE = new RAprobadoresBE();
                            oAprobadorBE.Id = (int)oDataReader["Id"];
                            oAprobadorBE.Descripcion = (string)oDataReader["Descripcion"];
                            oListaTipos.Add(oAprobadorBE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return oListaTipos;
        }
        public bool ActualizarZona(RZonaBE poZonaBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_ZONA"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, poZonaBE.Id);
                objDB.AddInParameter(objCMD, "@IdSociedad", DbType.Int32, poZonaBE.IdSociedad);
                objDB.AddInParameter(objCMD, "@Descripcion", DbType.String, poZonaBE.Descripcion);

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

        public List<RAprobadoresBE> ObtenerAprobadores(int IdAreaCorporativa,int Idrol)
        {
            List<RAprobadoresBE> listAprobadores = new List<RAprobadoresBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_APROBADORES"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@IdAreaCorporativa", DbType.Int32, IdAreaCorporativa);
                    objDB.AddInParameter(objCMD, "@IdRol", DbType.Int32, Idrol);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RAprobadoresBE oAprobador = new RAprobadoresBE();
                            oAprobador.Id = (int)oDataReader["Id"];
                            oAprobador.Usuario = (string)oDataReader["Usuario"];
                            oAprobador.Nombre = Convert.ToString(oDataReader["Nombre"]);
                            listAprobadores.Add(oAprobador);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return listAprobadores;
        }
        public List<RAprobadoresBE> ObtenerAprobadoresPorUsuario(string usuario)
        {
            List<RAprobadoresBE> listAprobadores = new List<RAprobadoresBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_APROBADORPORUSUARIO"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@Usuario", DbType.String, usuario);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RAprobadoresBE oAprobador = new RAprobadoresBE();
                            oAprobador.Id = (int)oDataReader["Id"];
                            oAprobador.Usuario = (string)oDataReader["Usuario"];
                            oAprobador.Nombre = Convert.ToString(oDataReader["Nombre"]);
                            oAprobador.Rol = Convert.ToString(oDataReader["Rol"]);
                            oAprobador.IdRol = (int)oDataReader["IdRol"];
                            oAprobador.IdAreaCorporativa = (int)oDataReader["IdAreaCorporativa"];
                            listAprobadores.Add(oAprobador);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return listAprobadores;
        }

        public List<RAprobadoresBE> ObtenerAprobadoresPorArea(int IdAreaCorporativa)
        {
            List<RAprobadoresBE> listAprobadores = new List<RAprobadoresBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_APROBADORESPORAREA"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@IdAreaCorporativa", DbType.Int32, IdAreaCorporativa);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RAprobadoresBE oAprobador = new RAprobadoresBE();
                            oAprobador.Id = (int)oDataReader["Id"];
                            oAprobador.Usuario = (string)oDataReader["Usuario"];
                            oAprobador.Nombre = Convert.ToString(oDataReader["Nombre"]);
                            oAprobador.Rol = Convert.ToString(oDataReader["Rol"]);
                            oAprobador.IdRol = (int)oDataReader["IdRol"];
                            listAprobadores.Add(oAprobador);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return listAprobadores;
        }
         * */
    }
       
}
