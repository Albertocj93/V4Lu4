using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Viatecla.Factory.Scriptor;
using Viatecla.Factory.Web;
using ScriptorModel = Viatecla.Factory.Scriptor.ModularSite.Models;
using Common;
using System.Configuration;

namespace DataLayer
{
    public class RTareaUsuarioDAL
    {

        public bool InsertarTareaUsuario(RTareUsuarioBE TareaUsuarioBE)
        {
            bool resultado = false;
            ScriptorChannel canalTareaUsuario = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TareaUsuario));
            ScriptorContent contenido = canalTareaUsuario.NewContent();

            contenido.Parts.IdInstancia = TareaUsuarioBE.IdInstancia;
            contenido.Parts.IdTareaUsuario = TareaUsuarioBE.IdTareaUsuario;
            contenido.Parts.CuentaUsuario = TareaUsuarioBE.CuentaUsuario;
            contenido.Parts.EstadoTarea = TareaUsuarioBE.EstadoTarea;
            contenido.Parts.IdRol = TareaUsuarioBE.IdRol;

            resultado = contenido.Save();

            return resultado;

        }

        public bool ActualizarTareaUsuarioSolicitudInversion(int idTareaUsuario, string Estado)
        {
            bool resultado = false;
            ScriptorChannel canalTareaUsuario = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TareaUsuario));

            ScriptorContent contenido = canalTareaUsuario.QueryContents("IdTareaUsuario", idTareaUsuario, "=").ToList().FirstOrDefault();
            contenido.Parts.EstadoTarea = Estado;
            resultado = contenido.Save();
            
            return resultado;

        }

        public bool ActualizarTareaUsuarioReasignar(int IdTareaUsuario, string CuentaUsuario)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            bool Resultado = false;
            try
            {
                con.ConnectionString = connectionString;
                con.Open();

                string nombreProcedure = "ActualizarTareaReasignar";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdTareaUsuario", IdTareaUsuario);
                cmd.Parameters.Add(par1);

                SqlParameter par2 = new SqlParameter("@CuentaUsuario", CuentaUsuario);
                cmd.Parameters.Add(par2);

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


        public List<int> ObtenerTareaUsuarioSolicitudInversion(int idInstancia, string estadoTarea, string cuenta)
        {
            //ObtenerTareaUsuarioSolicitudInversion

            List<int> oLista;
            
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerTareaUsuarioSolicitudInversion";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdInstancia", idInstancia);
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@estadoTarea", estadoTarea);
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@cuenta", cuenta);
                cmd.Parameters.Add(par1);

                int idTarea = 0;
                oLista = new List<int>();

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        idTarea = Convert.ToInt32(dataReader["TareaUsuario"]);
                        oLista.Add(idTarea);

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

        public List<int> ObtenerTareaUsuarioSolicitudInversionAdmin(int idInstancia, string estadoTarea)
        {
            //ObtenerTareaUsuarioSolicitudInversion

            List<int> oLista;

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerTareaUsuarioSolicitudInversionAdmin";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdInstancia", idInstancia);
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@estadoTarea", estadoTarea);
                cmd.Parameters.Add(par1);

                int idTarea = 0;
                oLista = new List<int>();

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        idTarea = Convert.ToInt32(dataReader["TareaUsuario"]);
                        oLista.Add(idTarea);

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

        //****************************************//
        public bool InsertarTareaUsuaarioInforme(RTareUsuarioBE TareaUsuarioBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_INS_TAREAUSUARIOINFORME"))
            {
                objDB.AddInParameter(objCMD, "@IdInstancia", DbType.Int32, TareaUsuarioBE.IdInstancia);
                objDB.AddInParameter(objCMD, "@IdTareaUsuario", DbType.Int32, TareaUsuarioBE.IdTareaUsuario);
                objDB.AddInParameter(objCMD, "@CuentaUsuario", DbType.String, TareaUsuarioBE.CuentaUsuario);
                objDB.AddInParameter(objCMD, "@EstadoTarea", DbType.String, TareaUsuarioBE.EstadoTarea);
                objDB.AddInParameter(objCMD, "@IdRol", DbType.String, TareaUsuarioBE.IdRol);
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
        public bool InsertarTareaUsuaarioEvento(RTareUsuarioBE TareaUsuarioBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_INS_TAREAUSUARIOEVENTO"))
            {
                objDB.AddInParameter(objCMD, "@IdInstancia", DbType.Int32, TareaUsuarioBE.IdInstancia);
                objDB.AddInParameter(objCMD, "@IdTareaUsuario", DbType.Int32, TareaUsuarioBE.IdTareaUsuario);
                objDB.AddInParameter(objCMD, "@CuentaUsuario", DbType.String, TareaUsuarioBE.CuentaUsuario);
                objDB.AddInParameter(objCMD, "@EstadoTarea", DbType.String, TareaUsuarioBE.EstadoTarea);
                objDB.AddInParameter(objCMD, "@IdEstado", DbType.String, TareaUsuarioBE.IdRol);
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
        public List<int> ObtenerTareaUsuario(int idInstancia, string estadoTarea, string cuenta)
        {
            List<int> idTareasUsuario = new List<int>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_TAREAUSUARIO"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@IdInstancia", DbType.Int32, idInstancia);
                    objDB.AddInParameter(objCMD, "@EstadoTarea", DbType.String, estadoTarea);
                    objDB.AddInParameter(objCMD, "@CuentaTareaUsuario", DbType.String, cuenta);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            int idTareaUsuario;
                            idTareaUsuario = (int)oDataReader["IdTareaUsuario"];
                            idTareasUsuario.Add(idTareaUsuario);
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return idTareasUsuario;
        }
        public List<int> ObtenerTareaUsuarioEvento(int idInstancia, string estadoTarea, string cuenta)
        {
            List<int> idTareasUsuario = new List<int>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_TAREAUSUARIOEVENTO"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@IdInstancia", DbType.Int32, idInstancia);
                    objDB.AddInParameter(objCMD, "@EstadoTarea", DbType.String, estadoTarea);
                    objDB.AddInParameter(objCMD, "@CuentaTareaUsuario", DbType.String, cuenta);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            int idTareaUsuario;
                            idTareaUsuario = (int)oDataReader["IdTareaUsuario"];
                            idTareasUsuario.Add(idTareaUsuario);
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return idTareasUsuario;
        }
        public string ObtenerTareaUsuarioPorId(int IdTareaUsuario)
        {
            string cuentaUsuario = "";
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_TAREAUSUARIO_PORID"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@IdTareaUsuario", DbType.Int32, IdTareaUsuario);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            cuentaUsuario = (string)oDataReader["CuentaUsuario"];
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return cuentaUsuario;
        }

        public List<RTareUsuarioBE> ObtenerTareaUsuarioPorInstancia(int idInstancia)
        {
            List<RTareUsuarioBE> TareasUsuario = new List<RTareUsuarioBE>();

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerTareaUsuarioPorInstancia";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdInstancia", idInstancia);
                cmd.Parameters.Add(par1);
                
                int idTarea = 0;
                

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        RTareUsuarioBE TareaUsuario = new RTareUsuarioBE();
                        TareaUsuario.Id = new Guid(dataReader["Id"].ToString());
                        TareaUsuario.CuentaUsuario = (string)dataReader["CuentaUsuario"];
                        TareaUsuario.EstadoTarea = (string)dataReader["EstadoTarea"];
                        TareaUsuario.IdTareaUsuario = (int)dataReader["IdTareaUsuario"];
                        TareaUsuario.IdRol = (int)dataReader["IdRol"];
                        
                        TareasUsuario.Add(TareaUsuario);

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



            return TareasUsuario;
        }
        public bool ActualizarTareaUsuario(int idTareaUsuario, string estado)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_TAREAUSUARIO"))
            {
                objDB.AddInParameter(objCMD, "@IdTareaUsuario", DbType.Int32, idTareaUsuario);
                objDB.AddInParameter(objCMD, "@EstadoTarea", DbType.String, estado);
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
        public bool ActualizarTareaUsuarioEvento(int idTareaUsuario, string estado)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_TAREAUSUARIOEVENTO"))
            {
                objDB.AddInParameter(objCMD, "@IdTareaUsuario", DbType.Int32, idTareaUsuario);
                objDB.AddInParameter(objCMD, "@EstadoTarea", DbType.String, estado);
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
        public bool ActualizarTareaUsuarioReasignacion(int idTareaUsuario, string cuenta)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_TAREA_USUARIO_REASIGNACION"))
            {
                objDB.AddInParameter(objCMD, "@IdTareaUsuario", DbType.Int32, idTareaUsuario);
                objDB.AddInParameter(objCMD, "@CuentaUsuario", DbType.String, cuenta);
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
        public bool ActualizarTareaUsuarioReasignacionEvento(int idTareaUsuario, string cuenta)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_TAREA_USUARIO_REASIGNACION_EVENTO"))
            {
                objDB.AddInParameter(objCMD, "@IdTareaUsuario", DbType.Int32, idTareaUsuario);
                objDB.AddInParameter(objCMD, "@CuentaUsuario", DbType.String, cuenta);
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

        public bool ValidarAprobar(string IdSolicitud, string CuentaRed)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            bool respuesta = false;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ValidarAprobar";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdSolicitud", IdSolicitud);
                cmd.Parameters.Add(par1);


                par1 = new SqlParameter("@CuentaRed", CuentaRed);
                cmd.Parameters.Add(par1);

                int cuenta = 0;
                

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        cuenta = Convert.ToInt32(dataReader["Aprueba"]);
                        if (cuenta > 0)
                            respuesta = true;

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

            return respuesta;
 
        }

    }
}
