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
    public class RRolDAL
    {

        public List<RRolBE> ObtenerRolPorAnio(string Anio)
        {
            List<RRolBE> oLista = new List<RRolBE>();
            RValoresAprobacionDAL valor = new RValoresAprobacionDAL();
            List<RValoresAprobacionBE> olistaValores = valor.ObtenerValoresAprobacionPorAnio(Anio);
            List<RRolBE> oListaAux = ObtenerRoles();
            RRolBE rol;
            foreach(RValoresAprobacionBE item in olistaValores)
            {
                oListaAux.RemoveAll(x=>x.Id.ToString()==item.IdRol);
                
            }

            return oListaAux;
        }

        public List<RRolBE> ObtenerRoles()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            List<RRolBE> oLista = new List<RRolBE>();
            RRolBE oRol;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerRol";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        oRol = new RRolBE();
                        oRol.Id = new Guid(dataReader["IdRol"].ToString());
                        oRol.Descripcion = dataReader["Rol"].ToString();

                        oLista.Add(oRol);
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

        public RRolBE ObtenerRolPorId(string IdRol)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

             RRolBE oRol=null;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerRolPorId";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@Id", IdRol);
                cmd.Parameters.Add(par1);

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        oRol = new RRolBE();
                        oRol.Id = new Guid(dataReader["IdRol"].ToString());
                        oRol.Descripcion = dataReader["Rol"].ToString();

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
            return oRol;
        }
        /*
        public bool EliminarAprobador(int IdAprobador)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_DEL_ADMINISTRADOR"))
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
        public bool InsertarAprobador(RAdministradoresBE poAprobadorBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_INS_ADMINISTRADOR"))
            {
                objDB.AddInParameter(objCMD, "@Usuario", DbType.String, poAprobadorBE.Usuario);
                objDB.AddInParameter(objCMD, "@Email", DbType.String, poAprobadorBE.Email);
                objDB.AddInParameter(objCMD, "@Nombre", DbType.String, poAprobadorBE.Nombre);
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
    
        public List<RAdministradoresBE> ObtenerAdministradores()
        {
            List<RAdministradoresBE> listAdministradores = new List<RAdministradoresBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_ADMINISTRADORES"))
            {
                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RAdministradoresBE oAprobador = new RAdministradoresBE();
                            oAprobador.Id = (int)oDataReader["Id"];
                            oAprobador.Usuario = (string)oDataReader["Usuario"];
                            oAprobador.Email = (string)oDataReader["Email"];
                            oAprobador.Nombre = Convert.ToString(oDataReader["Nombre"]);
                            listAdministradores.Add(oAprobador);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return listAdministradores;
        }

        public List<RAdministradoresBE> ObtenerAdministradoresPorUsuario(string usuario)
        {
            List<RAdministradoresBE> listAdministradores = new List<RAdministradoresBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_ADMINISTRADORPORUSUARIO"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@Usuario", DbType.String, usuario);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RAdministradoresBE oAprobador = new RAdministradoresBE();
                            oAprobador.Id = (int)oDataReader["Id"];
                            oAprobador.Usuario = (string)oDataReader["Usuario"];
                            oAprobador.Email = (string)oDataReader["Email"];
                            oAprobador.Nombre = Convert.ToString(oDataReader["Nombre"]);
                            listAdministradores.Add(oAprobador);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return listAdministradores;
        }

        */
    }
}
