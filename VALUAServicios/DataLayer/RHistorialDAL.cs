using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

namespace DataLayer
{
    public class RHistorialDAL
    {

        public bool InsertarHistorial(RHistorialBE poHistorialBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_INS_HISTORIAL"))
            {
                objDB.AddInParameter(objCMD, "@IdSolicitud", DbType.String, poHistorialBE.IdSolicitud);
                objDB.AddInParameter(objCMD, "@FechaModificacion", DbType.DateTime, poHistorialBE.FechaModificacion);
                objDB.AddInParameter(objCMD, "@Comentario", DbType.String, poHistorialBE.Comentario);
                objDB.AddInParameter(objCMD, "@Usuario", DbType.String, poHistorialBE.Usuario);
                objDB.AddInParameter(objCMD, "@NombreUsuario", DbType.String, poHistorialBE.NombreUsuario);
                objDB.AddInParameter(objCMD, "@EstadoInforme", DbType.String, poHistorialBE.EstadoInforme);
                objDB.AddInParameter(objCMD, "@Datos", DbType.String, poHistorialBE.Datos);
                objDB.AddInParameter(objCMD, "@Tipo", DbType.String, poHistorialBE.Tipo);
                objDB.AddInParameter(objCMD, "@EstaActivo", DbType.Boolean, poHistorialBE.EstaActivo);

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
        public List<RHistorialBE> ObtenerHistorialPorTipo(int IdSolicitud, string Tipo)
        {
            List<RHistorialBE> oListaHistorial = new List<RHistorialBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERHISTORIAL"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@IdSolicitud", DbType.Int32, IdSolicitud);
                    objDB.AddInParameter(objCMD, "@Tipo", DbType.String, Tipo);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RHistorialBE oHistorialBE = new RHistorialBE();
                            oHistorialBE.Id = (int)oDataReader["Id"];
                            oHistorialBE.IdSolicitud = (int)oDataReader["IdSolicitud"];
                            oHistorialBE.FechaModificacion = (DateTime)oDataReader["FechaModificacion"];
                            oHistorialBE.Comentario = (string)oDataReader["Comentario"];
                            oHistorialBE.NombreUsuario = (string)oDataReader["NombreUsuario"];
                            oHistorialBE.EstadoInforme = (string)oDataReader["EstadoInforme"];
                            oHistorialBE.Tipo = (string)oDataReader["Tipo"];
                            oListaHistorial.Add(oHistorialBE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListaHistorial;
        }
        public RHistorialBE ObtenerHistorialEvento(int Id)
        {
            RHistorialBE oHistorialBE = new RHistorialBE();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERHISTORIAL_EVENTO"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@Id", DbType.Int32, Id);
//                    objDB.AddInParameter(objCMD, "@FechaModificacion", DbType.String, fechaModificacion);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            oHistorialBE.Id = (int)oDataReader["Id"];
                            oHistorialBE.IdSolicitud = (int)oDataReader["IdEventoRiesgo"];
                            oHistorialBE.FechaModificacion = (DateTime)oDataReader["FechaModificacion"];
                            oHistorialBE.NombreUsuario = (string)oDataReader["NombreUsuario"];
                            oHistorialBE.DataJson = (string)oDataReader["DataJson"];
                            oHistorialBE.Usuario = (string)oDataReader["Usuario"];
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oHistorialBE;
        }
        public List<RHistorialBE> ObtenerHistorialesEvento(int IdSolicitud)
        {
            List<RHistorialBE> oListaHistorial = new List<RHistorialBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERHISTORIAL_EVENTO_POR_ID"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@IdEventoRiesgo", DbType.Int32, IdSolicitud);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RHistorialBE oHistorialBE = new RHistorialBE();
                            oHistorialBE.Id = (int)oDataReader["Id"];
                            oHistorialBE.IdSolicitud = (int)oDataReader["IdEventoRiesgo"];
                            oHistorialBE.FechaModificacion = (DateTime)oDataReader["FechaModificacion"];
                            oHistorialBE.NombreUsuario = (string)oDataReader["NombreUsuario"];
                            oHistorialBE.DataJson = (string)oDataReader["DataJson"];
                            oHistorialBE.Usuario = (string)oDataReader["Usuario"];
                            oListaHistorial.Add(oHistorialBE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListaHistorial;
        }
        public bool InsertarHistorialEvento(RHistorialBE poHistorialBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_INS_HISTORIALEVENTO"))
            {
                objDB.AddInParameter(objCMD, "@IdEventoRiesgo", DbType.Int32, poHistorialBE.IdSolicitud);
                //objDB.AddInParameter(objCMD, "@Versiones", DbType.Int32, poHistorialBE.Versiones);
                objDB.AddInParameter(objCMD, "@FechaModificacion", DbType.DateTime, poHistorialBE.FechaModificacion);
                objDB.AddInParameter(objCMD, "@Usuario", DbType.String, poHistorialBE.Usuario);
                objDB.AddInParameter(objCMD, "@NombreUsuario", DbType.String, poHistorialBE.NombreUsuario);
                objDB.AddInParameter(objCMD, "@DataJson", DbType.String, poHistorialBE.DataJson);
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
    }
}