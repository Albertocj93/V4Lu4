using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace DataLayer
{
    public class TrazabilidadDAL
    {

    //    public List<TrazabilidadBE> ObtenerTrazabilidadPorNumeroSolicitud(string psNumSolicitud)
    //    {
    //        List<TrazabilidadBE> oListaTrazabilidad = new List<TrazabilidadBE>();
    //        Database objDB = Util.CrearBaseDatos();
    //        using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_GET_OBTENERTRAZABILIDADPORNUMSOLICITUD"))
    //        {
    //            objDB.AddInParameter(objCMD, "@NumSolicitud", DbType.String, psNumSolicitud);


    //            try
    //            {
    //                using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
    //                {
    //                    while (oDataReader.Read())
    //                    {
    //                        TrazabilidadBE oTrazabilidad = new TrazabilidadBE();
    //                        oTrazabilidad.Id = (Int64)oDataReader["Id"];
    //                        oTrazabilidad.NumSolicitud = (string)oDataReader["NumSolicitud"];
    //                        oTrazabilidad.Aprobador = (string)oDataReader["Aprobador"];
    //                        oTrazabilidad.AprobadorNombreCompleto = (string)oDataReader["AprobadorNombreCompleto"];
    //                        oTrazabilidad.Rol = (string)oDataReader["Rol"];
    //                        oTrazabilidad.ReasignadoA = (string)oDataReader["ReasignadoA"];
    //                        oTrazabilidad.ReasignadoANombreCompleto = (string)oDataReader["ReasignadoANombreCompleto"];
    //                        oTrazabilidad.IdEstado = (int)oDataReader["IdEstado"];
    //                        oTrazabilidad.DescEstado = (string)oDataReader["EstadoSolicitud"];
    //                        oTrazabilidad.FechaEjecucion = (DateTime?)(oDataReader["FechaEjecucion"] == DBNull.Value ? null : oDataReader["FechaEjecucion"]);
    //                        oTrazabilidad.IdentificadorUnico = oDataReader["IdentificadorUnico"] == DBNull.Value ? Guid.Empty : (Guid)(oDataReader["IdentificadorUnico"]);
    //                        oTrazabilidad.Comentario = oDataReader["Comentario"] as string;
    //                        oTrazabilidad.FechaEnvio = (DateTime?)(oDataReader["FechaEnvio"] == DBNull.Value ? null : oDataReader["FechaEnvio"]);
    //                        oTrazabilidad.Version = (int?)(oDataReader["Version"] == DBNull.Value ? null : oDataReader["Version"]);
    //                        oListaTrazabilidad.Add(oTrazabilidad);
    //                    }
    //                }
    //            }
    //            catch
    //            {

    //                throw;
    //            }
    //        }

    //        return oListaTrazabilidad;
    //    }

    //    public List<TrazabilidadBE> ObtenerTrazabilidadSSLPorNumeroSolicitud(string psNumSolicitud)
    //    {
    //        List<TrazabilidadBE> oListaTrazabilidad = new List<TrazabilidadBE>();
    //        Database objDB = Util.CrearBaseDatos();
    //        using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_GET_OBTENERTRAZABILIDADSSL_POR_NUMSOLICITUD"))
    //        {
    //            objDB.AddInParameter(objCMD, "@NumSolicitud", DbType.String, psNumSolicitud);


    //            try
    //            {
    //                using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
    //                {
    //                    while (oDataReader.Read())
    //                    {
    //                        TrazabilidadBE oTrazabilidad = new TrazabilidadBE();
    //                        oTrazabilidad.Id = (Int64)oDataReader["Id"];
    //                        oTrazabilidad.NumSolicitud = (string)oDataReader["NumSolicitud"];
    //                        oTrazabilidad.Aprobador = (string)oDataReader["Aprobador"];
    //                        oTrazabilidad.AprobadorNombreCompleto = (string)oDataReader["AprobadorNombreCompleto"];
    //                        oTrazabilidad.Rol = (string)oDataReader["Rol"];
    //                        oTrazabilidad.ReasignadoA = (string)oDataReader["ReasignadoA"];
    //                        oTrazabilidad.ReasignadoANombreCompleto = (string)oDataReader["ReasignadoANombreCompleto"];
    //                        oTrazabilidad.IdEstado = (int)oDataReader["IdEstado"];
    //                        oTrazabilidad.DescEstado = (string)oDataReader["EstadoSolicitud"];
    //                        oTrazabilidad.FechaEjecucion = (DateTime?)(oDataReader["FechaEjecucion"] == DBNull.Value ? null : oDataReader["FechaEjecucion"]);
    //                        oTrazabilidad.IdentificadorUnico = oDataReader["IdentificadorUnico"] == DBNull.Value ? Guid.Empty : (Guid)(oDataReader["IdentificadorUnico"]);
    //                        oTrazabilidad.Comentario = oDataReader["Comentario"] as string;
    //                        oTrazabilidad.FechaEnvio = (DateTime?)(oDataReader["FechaEnvio"] == DBNull.Value ? null : oDataReader["FechaEnvio"]);

    //                        oListaTrazabilidad.Add(oTrazabilidad);
    //                    }
    //                }
    //            }
    //            catch
    //            {

    //                throw;
    //            }
    //        }

    //        return oListaTrazabilidad;
    //    }

    //    public List<TrazabilidadBE> ObtenerComentariosTrazabilidadPorNumeroSolicitud(string psNumSolicitud)
    //    {
    //        List<TrazabilidadBE> oListaTrazabilidad = new List<TrazabilidadBE>();
    //        Database objDB = Util.CrearBaseDatos();
    //        using (DbCommand objCMD = objDB.GetStoredProcCommand("PA_GET_OBTENERCOMENTARIOTRAZABILIDADPORNUMSOLICITUD"))
    //        {
    //            objDB.AddInParameter(objCMD, "@P_NUM_SOLICITUD", DbType.String, psNumSolicitud);

    //            try
    //            {
    //                using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
    //                {
    //                    while (oDataReader.Read())
    //                    {
    //                        TrazabilidadBE oTrazabilidad = new TrazabilidadBE();
    //                        oTrazabilidad.Id = (Int64)oDataReader["Id"];
    //                        oTrazabilidad.Aprobador = (string)oDataReader["Aprobador"];
    //                        oTrazabilidad.AprobadorNombreCompleto = (string)oDataReader["AprobadorNombreCompleto"];
    //                        oTrazabilidad.Rol = (string)oDataReader["Rol"];
    //                        oTrazabilidad.FechaEjecucion = (DateTime?)(oDataReader["FechaEjecucion"] == DBNull.Value ? null : oDataReader["FechaEjecucion"]);
    //                        oTrazabilidad.Comentario = oDataReader["Comentario"] as string;
    //                        oListaTrazabilidad.Add(oTrazabilidad);

    //                    }
    //                }
    //            }
    //            catch
    //            {
    //                throw;
    //            }
    //        }

    //        return oListaTrazabilidad;
    //    }

    //    public List<TrazabilidadBE> ObtenerComentariosTrazabilidadSSLPorNumeroSolicitud(string psNumSolicitud)
    //    {
    //        List<TrazabilidadBE> oListaTrazabilidad = new List<TrazabilidadBE>();
    //        Database objDB = Util.CrearBaseDatos();
    //        using (DbCommand objCMD = objDB.GetStoredProcCommand("PA_GET_OBTENER_COMENTARIO_TRAZABILIDADSSL_POR_NUMSOLICITUD"))
    //        {
    //            objDB.AddInParameter(objCMD, "@P_NUM_SOLICITUD", DbType.String, psNumSolicitud);

    //            try
    //            {
    //                using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
    //                {
    //                    while (oDataReader.Read())
    //                    {
    //                        TrazabilidadBE oTrazabilidad = new TrazabilidadBE();
    //                        oTrazabilidad.Id = (Int64)oDataReader["Id"];
    //                        oTrazabilidad.Aprobador = (string)oDataReader["Aprobador"];
    //                        oTrazabilidad.AprobadorNombreCompleto = (string)oDataReader["AprobadorNombreCompleto"];
    //                        oTrazabilidad.Rol = (string)oDataReader["Rol"];
    //                        oTrazabilidad.FechaEjecucion = (DateTime?)(oDataReader["FechaEjecucion"] == DBNull.Value ? null : oDataReader["FechaEjecucion"]);
    //                        oTrazabilidad.Comentario = oDataReader["Comentario"] as string;
    //                        oListaTrazabilidad.Add(oTrazabilidad);

    //                    }
    //                }
    //            }
    //            catch
    //            {
    //                throw;
    //            }
    //        }

    //        return oListaTrazabilidad;
    //    }

    //    public List<TrazabilidadBE> ObtenerAprobadoresPreviosPorNumeroSolicitud(string psNumSolicitud)
    //    {
    //        List<TrazabilidadBE> oListaTrazabilidad = new List<TrazabilidadBE>();
    //        Database objDB = Util.CrearBaseDatos();
    //        using (DbCommand objCMD = objDB.GetStoredProcCommand("PA_GET_OBTENERAPROBADORESPREVIOSPORNUMSOLICITUD"))
    //        {
    //            objDB.AddInParameter(objCMD, "@P_NUM_SOLICITUD", DbType.String, psNumSolicitud);

    //            try
    //            {
    //                using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
    //                {
    //                    while (oDataReader.Read())
    //                    {
    //                        TrazabilidadBE oTrazabilidad = new TrazabilidadBE();
    //                        oTrazabilidad.Id = (Int64)oDataReader["Id"];
    //                        oTrazabilidad.Aprobador = (string)oDataReader["Aprobador"];
    //                        oTrazabilidad.AprobadorNombreCompleto = (string)oDataReader["AprobadorNombreCompleto"];
    //                        oTrazabilidad.Rol = (string)oDataReader["Rol"];
    //                        oTrazabilidad.FechaEjecucion = (DateTime?)(oDataReader["FechaEjecucion"] == DBNull.Value ? null : oDataReader["FechaEjecucion"]);
    //                        oTrazabilidad.Comentario = oDataReader["Comentario"] as string;
    //                        oListaTrazabilidad.Add(oTrazabilidad);

    //                    }
    //                }
    //            }
    //            catch
    //            {
    //                throw;
    //            }
    //        }

    //        return oListaTrazabilidad;
    //    }

    //    public List<TrazabilidadBE> ObtenerAprobadoresPreviosPorNumeroSolicitudSSL(string psNumSolicitud)
    //    {
    //        List<TrazabilidadBE> oListaTrazabilidad = new List<TrazabilidadBE>();
    //        Database objDB = Util.CrearBaseDatos();
    //        using (DbCommand objCMD = objDB.GetStoredProcCommand("PA_GET_OBTENERAPROBADORESPREVIOSPORNUMSOLICITUD_SSL"))
    //        {
    //            objDB.AddInParameter(objCMD, "@P_NUM_SOLICITUD", DbType.String, psNumSolicitud);

    //            try
    //            {
    //                using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
    //                {
    //                    while (oDataReader.Read())
    //                    {
    //                        TrazabilidadBE oTrazabilidad = new TrazabilidadBE();
    //                        oTrazabilidad.Id = (Int64)oDataReader["Id"];
    //                        oTrazabilidad.Aprobador = (string)oDataReader["Aprobador"];
    //                        oTrazabilidad.AprobadorNombreCompleto = (string)oDataReader["AprobadorNombreCompleto"];
    //                        oTrazabilidad.Rol = (string)oDataReader["Rol"];
    //                        oTrazabilidad.FechaEjecucion = (DateTime?)(oDataReader["FechaEjecucion"] == DBNull.Value ? null : oDataReader["FechaEjecucion"]);
    //                        oTrazabilidad.Comentario = oDataReader["Comentario"] as string;
    //                        oListaTrazabilidad.Add(oTrazabilidad);

    //                    }
    //                }
    //            }
    //            catch
    //            {
    //                throw;
    //            }
    //        }

    //        return oListaTrazabilidad;
    //    }


    //    public TrazabilidadBE ObtenerCabeceraTrazabilidadPorIdSolicitud(Int64 piIdSolicitud, string psTipoSolicitud)
    //    {
    //        TrazabilidadBE oTrazabilidad = null;
    //        Database objDB = Util.CrearBaseDatos();
    //        using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_GET_OBTENERTRAZABILIDADCABPORIDSOLICITUD"))
    //        {
    //            objDB.AddInParameter(objCMD, "@IdSolicitud", DbType.Int64, piIdSolicitud);
    //            objDB.AddInParameter(objCMD, "@TipoSolicitudInversion", DbType.String, psTipoSolicitud);

    //            try
    //            {
    //                using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
    //                {
    //                    if (oDataReader.Read())
    //                    {
    //                        oTrazabilidad = new TrazabilidadBE();
    //                        oTrazabilidad.NumSolicitud = (string)oDataReader["NumSolicitud"];
    //                        oTrazabilidad.AprobadorNombreCompleto = (string)oDataReader["UsuarioRegistro"];
    //                        oTrazabilidad.FechaEjecucion = (DateTime?)oDataReader["FechaRegistro"];

    //                    }
    //                }
    //            }
    //            catch
    //            {

    //                throw;
    //            }
    //        }

    //        return oTrazabilidad;
    //    }

    //    public bool ReasignarTrazabilidad(Int64 piIdTrazabilidad, string psReasignadoA, string psReasignadoANombreCompleto)
    //    {
    //        Database objDB = Util.CrearBaseDatos();
    //        int res = 0;
    //        using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_UPD_REASIGNARTRAZABILIDAD"))
    //        {
    //            objDB.AddInParameter(objCMD, "@IdTrazabilidad", DbType.Int64, piIdTrazabilidad);
    //            objDB.AddInParameter(objCMD, "@ReasignadoA", DbType.String, psReasignadoA);
    //            objDB.AddInParameter(objCMD, "@ReasignadoANombreCompleto", DbType.String, psReasignadoANombreCompleto);
    //            try
    //            {
    //                res = objDB.ExecuteNonQuery(objCMD);

    //            }
    //            catch (Exception ex)
    //            {

    //                throw ex;
    //            }
    //        }

    //        return res > 0;
    //    }

    //    public bool ReasignarTrazabilidadSSL(Int64 piIdTrazabilidad, string psReasignadoA, string psReasignadoANombreCompleto)
    //    {
    //        Database objDB = Util.CrearBaseDatos();
    //        int res = 0;
    //        using (DbCommand objCMD = objDB.GetStoredProcCommand("PA_UPD_REASIGNAR_TRAZABILIDADSSL"))
    //        {
    //            objDB.AddInParameter(objCMD, "@IdTrazabilidad", DbType.Int64, piIdTrazabilidad);
    //            objDB.AddInParameter(objCMD, "@ReasignadoA", DbType.String, psReasignadoA);
    //            objDB.AddInParameter(objCMD, "@ReasignadoANombreCompleto", DbType.String, psReasignadoANombreCompleto);
    //            try
    //            {
    //                res = objDB.ExecuteNonQuery(objCMD);

    //            }
    //            catch (Exception ex)
    //            {

    //                throw ex;
    //            }
    //        }

    //        return res > 0;
    //    }

    //    public bool ActualizarTrazabilidad(TrazabilidadBE oTrazabilidad)
    //    {
    //        int res = 0;
    //        //try
    //        //{
    //        Database objDB = Util.CrearBaseDatos();
    //        using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_UPD_TRAZABILIDAD"))
    //        {
    //            objDB.AddInParameter(objCMD, "@NroSolicitud", DbType.String, oTrazabilidad.NumSolicitud);
    //            objDB.AddInParameter(objCMD, "@IdEstado", DbType.String, oTrazabilidad.IdEstado);
    //            objDB.AddInParameter(objCMD, "@IdentificadorUnico", DbType.Guid, oTrazabilidad.IdentificadorUnico);
    //            objDB.AddInParameter(objCMD, "@Aprobador", DbType.String, oTrazabilidad.Aprobador);
    //            objDB.AddInParameter(objCMD, "@Comentario", DbType.String, oTrazabilidad.Comentario);
    //            try
    //            {
    //                res = objDB.ExecuteNonQuery(objCMD);

    //            }
    //            catch (Exception ex)
    //            {

    //                throw ex;
    //            }
    //        }
    //        //}
    //        //catch (Exception)
    //        //{
    //        //}
    //        return res > 0;
    //    }

    //    public bool ActualizarTrazabilidadSSL(TrazabilidadBE oTrazabilidad)
    //    {
    //        int res = 0;
    //        Database objDB = Util.CrearBaseDatos();
    //        using (DbCommand objCMD = objDB.GetStoredProcCommand("PA_UPD_TRAZABILIDAD_SSL"))
    //        {
    //            objDB.AddInParameter(objCMD, "@NroSolicitud", DbType.String, oTrazabilidad.NumSolicitud);
    //            objDB.AddInParameter(objCMD, "@IdEstado", DbType.String, oTrazabilidad.IdEstado);
    //            objDB.AddInParameter(objCMD, "@IdentificadorUnico", DbType.Guid, oTrazabilidad.IdentificadorUnico);
    //            objDB.AddInParameter(objCMD, "@Aprobador", DbType.String, oTrazabilidad.Aprobador);
    //            objDB.AddInParameter(objCMD, "@Comentario", DbType.String, oTrazabilidad.Comentario);
    //            try
    //            {
    //                res = objDB.ExecuteNonQuery(objCMD);

    //            }
    //            catch (Exception ex)
    //            {

    //                throw ex;
    //            }
    //        }
    //        return res > 0;
    //    }

    //    public bool ActualizarTrazabilidadErrorGUID(TrazabilidadBE oTrazabilidad)
    //    {
    //        int res = 0;
    //        //try
    //        //{
    //        Database objDB = Util.CrearBaseDatos();
    //        using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_UPD_TRAZABILIDAD_ERROR_GUID"))
    //        {
    //            objDB.AddInParameter(objCMD, "@NroSolicitud", DbType.String, oTrazabilidad.NumSolicitud);
    //            objDB.AddInParameter(objCMD, "@IdEstado", DbType.String, oTrazabilidad.IdEstado);
    //            objDB.AddInParameter(objCMD, "@IdentificadorUnico", DbType.Guid, oTrazabilidad.IdentificadorUnico);
    //            objDB.AddInParameter(objCMD, "@Aprobador", DbType.String, oTrazabilidad.Aprobador);
    //            objDB.AddInParameter(objCMD, "@Comentario", DbType.String, oTrazabilidad.Comentario);
    //            try
    //            {
    //                res = objDB.ExecuteNonQuery(objCMD);

    //            }
    //            catch (Exception ex)
    //            {

    //                throw ex;
    //            }
    //        }
    //        //}
    //        //catch (Exception)
    //        //{
    //        //}
    //        return res > 0;
    //    }

    //    public bool ConsultarEstadoErrorTrazabilidad(string pNumSolicitud, string pAprobador)
    //    {
    //        bool Estado = true;
    //        Database OD = Util.CrearBaseDatos();
    //        using (IDataReader oreader = OD.ExecuteReader("PA_GET_OBTENERESTADOERRORTRAZABILIDAD", pNumSolicitud, pAprobador))
    //        {
    //            if (oreader.Read())
    //            {
    //                Estado = Convert.ToBoolean(oreader[0]);

    //            }
    //        }
    //        return Estado;
    //    }

    //    public void ActualizarFechaEnvioTrazabilidad(string pSolicitud, string usuario)
    //    {
    //        Database objDB = Util.CrearBaseDatos();
    //        int res = 0;
    //        using (DbCommand objCMD = objDB.GetStoredProcCommand("PA_UPD_TRAZABILIDADFECHAENVIO"))
    //        {
    //            objDB.AddInParameter(objCMD, "@pNumSolicitud", DbType.String, pSolicitud);
    //            objDB.AddInParameter(objCMD, "@pUsuario", DbType.String, usuario);
    //            try
    //            {
    //                res = objDB.ExecuteNonQuery(objCMD);

    //            }
    //            catch (Exception ex)
    //            {

    //                throw ex;
    //            }
    //        }
    //    }

    //    public void ActualizarFechaEnvioTrazabilidadSSL(string pSolicitud, string usuario)
    //    {
    //        Database objDB = Util.CrearBaseDatos();
    //        int res = 0;
    //        using (DbCommand objCMD = objDB.GetStoredProcCommand("PA_UPD_TRAZABILIDADSSL_FECHAENVIO"))
    //        {
    //            objDB.AddInParameter(objCMD, "@pNumSolicitud", DbType.String, pSolicitud);
    //            objDB.AddInParameter(objCMD, "@pUsuario", DbType.String, usuario);
    //            try
    //            {
    //                res = objDB.ExecuteNonQuery(objCMD);

    //            }
    //            catch (Exception ex)
    //            {

    //                throw ex;
    //            }
    //        }
    //    }

    //    public void EliminarTrazabilidadPorError(string pSolicitud)
    //    {
    //        Database objDB = Util.CrearBaseDatos();
    //        int res = 0;
    //        using (DbCommand objCMD = objDB.GetStoredProcCommand("PA_DEL_ELIMINARTRAZABILIDADPORSOLICITUD"))
    //        {
    //            objDB.AddInParameter(objCMD, "@pNumSolicitud", DbType.String, pSolicitud);
    //            try
    //            {
    //                res = objDB.ExecuteNonQuery(objCMD);
    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //            }
    //        }
    //    }

    //    public void EliminarTrazabilidadSSLPorError(string pSolicitud)
    //    {
    //        Database objDB = Util.CrearBaseDatos();
    //        int res = 0;
    //        using (DbCommand objCMD = objDB.GetStoredProcCommand("PA_DEL_ELIMINARTRAZABILIDADSSL_POR_SOLICITUD"))
    //        {
    //            objDB.AddInParameter(objCMD, "@pNumSolicitud", DbType.String, pSolicitud);
    //            try
    //            {
    //                res = objDB.ExecuteNonQuery(objCMD);
    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //            }
    //        }
    //    }

    //    public bool CancelarTrazabilidad(TrazabilidadBE oTrazabilidad)
    //    {
    //        Database objDB = Util.CrearBaseDatos();
    //        int res = 0;

    //        try
    //        {
    //            res = objDB.ExecuteNonQuery("USP_UPD_CANCELTRAZABILIDAD", oTrazabilidad.NumSolicitud, oTrazabilidad.IdEstado, oTrazabilidad.Comentario);
    //        }
    //        catch (Exception ex)
    //        {

    //            throw ex;
    //        }
    //        return res > 0;
    //    }

    //    public bool CancelarTrazabilidadSSL(TrazabilidadBE oTrazabilidad)
    //    {
    //        Database objDB = Util.CrearBaseDatos();
    //        int res = 0;

    //        try
    //        {
    //            res = objDB.ExecuteNonQuery("PA_UPD_CANCELTRAZABILIDAD_SSL", oTrazabilidad.NumSolicitud, oTrazabilidad.IdEstado, oTrazabilidad.Comentario);
    //        }
    //        catch (Exception ex)
    //        {

    //            throw ex;
    //        }
    //        return res > 0;
    //    }

    //    public bool CancelarTrazabilidadFlujo(TrazabilidadBE oTrazabilidad)
    //    {
    //        Database objDB = Util.CrearBaseDatos();
    //        int res = 0;

    //        //using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_UPD_CANCELTRAZABILIDAD"))
    //        //{
    //        //objDB.AddInParameter(objCMD, "@NroSolicitud", DbType.String, oTrazabilidad.NumSolicitud);
    //        //objDB.AddInParameter(objCMD, "@IdEstado", DbType.String, oTrazabilidad.IdEstado);
    //        //objDB.AddInParameter(objCMD, "@Comentario", DbType.String, oTrazabilidad.Comentario);
    //        try
    //        {
    //            res = objDB.ExecuteNonQuery("USP_UPD_CANCELTRAZABILIDADFLUJO", oTrazabilidad.NumSolicitud, oTrazabilidad.IdEstado, oTrazabilidad.Comentario);
    //            //res = objDB.ExecuteNonQuery(objCMD);

    //        }
    //        catch (Exception ex)
    //        {

    //            throw ex;
    //        }

    //        //}

    //        return res > 0;
    //    }

    //    public int ObtenereEstadoSolicitud(string NroSolicitud)
    //    {
    //        int Estado = 0;
    //        //Database OD = DatabaseFactory.CreateDatabase("CnxAlicorp");
    //        Database OD = Util.CrearBaseDatos();
    //        using (IDataReader oreader = OD.ExecuteReader("USP_GET_ESTADOSOLICITUD", NroSolicitud))
    //        {
    //            if (oreader.Read())
    //            {
    //                Estado = Convert.ToInt32(oreader[0]);

    //            }
    //        }
    //        return Estado;
    //    }

    //    public int ObtenereEstadoSolicitudSSL(string NroSolicitud)
    //    {
    //        int Estado = 0;
    //        Database OD = Util.CrearBaseDatos();
    //        using (IDataReader oreader = OD.ExecuteReader("PA_GET_ESTADOSOLICITUD_SSL", NroSolicitud))
    //        {
    //            if (oreader.Read())
    //            {
    //                Estado = Convert.ToInt32(oreader[0]);

    //            }
    //        }
    //        return Estado;
    //    }

    //    private int ObtenereUltimaVersion(string NroSolicitud)
    //    {
    //        int Estado = 0;
    //        Database OD = Util.CrearBaseDatos();
    //        using (IDataReader oreader = OD.ExecuteReader("PA_GETULTIMAVERSIONTRAZABILIDAD", NroSolicitud))
    //        {
    //            if (oreader.Read())
    //            {
    //                Estado = Convert.ToInt32(oreader[0]);

    //            }
    //        }
    //        return Estado;
    //    }

    //    public bool InsertarTrazabilidad(List<TrazabilidadBE> ListTrazabilidadBE, string pNumSolicitud)
    //    {
    //        int res = 0;
    //        int versionNumSolicitud = ObtenereUltimaVersion(pNumSolicitud);

    //        ListTrazabilidadBE.ForEach(delegate(TrazabilidadBE poTrazabilidadBE)
    //        {
    //            Database objDB = Util.CrearBaseDatos();
    //            using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_INS_TRAZABILIDAD"))
    //            {
    //                objDB.AddInParameter(objCMD, "@NumSolicitud", DbType.String, poTrazabilidadBE.NumSolicitud);
    //                objDB.AddInParameter(objCMD, "@Aprobador", DbType.String, poTrazabilidadBE.Aprobador);
    //                objDB.AddInParameter(objCMD, "@Rol", DbType.String, poTrazabilidadBE.Rol);
    //                objDB.AddInParameter(objCMD, "@ReasignadoA", DbType.String, poTrazabilidadBE.ReasignadoA);
    //                objDB.AddInParameter(objCMD, "@IdEstado", DbType.Int32, poTrazabilidadBE.IdEstado);
    //                objDB.AddInParameter(objCMD, "@ReasignadoANombreCompleto", DbType.String, poTrazabilidadBE.ReasignadoANombreCompleto);
    //                objDB.AddInParameter(objCMD, "@AprobadorNombreCompleto", DbType.String, poTrazabilidadBE.AprobadorNombreCompleto);
    //                objDB.AddInParameter(objCMD, "@version", DbType.Int32, versionNumSolicitud);

    //                try
    //                {
    //                    res = objDB.ExecuteNonQuery(objCMD);
    //                }
    //                catch
    //                {
    //                    throw;
    //                }
    //            }
    //        });

    //        return res > 0;
    //    }

    //    public string ObtenereComentarioSolicitud(string NroSolicitud)
    //    {
    //        string Comentario = string.Empty;
    //        //Database OD = DatabaseFactory.CreateDatabase("CnxAlicorp");
    //        Database OD = Util.CrearBaseDatos();
    //        using (IDataReader oreader = OD.ExecuteReader("USP_GET_ULTIMATRAZABILIDAD", NroSolicitud))
    //        {
    //            if (oreader.Read())
    //            {
    //                Comentario = Convert.ToString(oreader[0]);

    //            }
    //        }
    //        return Comentario;
    //    }

    //    /// <summary>
    //    /// Autor: Alexis Paredes Carbonel
    //    /// Fecha Creación: 12/03/2014
    //    /// Asunto: Obtener la trazabilidad de la solicitud cuando el aprobador tiene el rol de VP Solicitante
    //    /// </summary>
    //    /// <param name="pNumSolicitud"></param>
    //    /// <returns></returns>
    //    public bool ObtenerTrazabilidadPorVPSolicitante(string pNumSolicitud)
    //    {
    //        int Cantidad = 0;
    //        Database OD = Util.CrearBaseDatos();
    //        using (IDataReader oreader = OD.ExecuteReader("PA_GET_TRAZABILIDAD_NUMSOLICITUD_BY_VP_SOLICITANTE", pNumSolicitud))
    //        {
    //            if (oreader.Read())
    //            {
    //                Cantidad = int.Parse(oreader[0].ToString());
    //            }
    //        }
    //        return Cantidad > 0;
    //    }

    //    private int ObtenereUltimaVersionSSL(string NroSolicitud)
    //    {
    //        int Estado = 0;
    //        Database OD = Util.CrearBaseDatos();
    //        using (IDataReader oreader = OD.ExecuteReader("PA_GET_ULTIMAVERSIONTRAZABILIDAD_SSL", NroSolicitud))
    //        {
    //            if (oreader.Read())
    //            {
    //                Estado = Convert.ToInt32(oreader[0]);

    //            }
    //        }
    //        return Estado;
    //    }

    //    public bool InsertarTrazabilidadSSL(List<TrazabilidadBE> ListTrazabilidadBE, string pNumSolicitud)
    //    {
    //        int res = 0;
    //        int versionNumSolicitud = ObtenereUltimaVersionSSL(pNumSolicitud);

    //        ListTrazabilidadBE.ForEach(delegate(TrazabilidadBE poTrazabilidadBE)
    //        {
    //            Database objDB = Util.CrearBaseDatos();
    //            using (DbCommand objCMD = objDB.GetStoredProcCommand("PA_INS_TRAZABILIDAD_SSL"))
    //            {
    //                objDB.AddInParameter(objCMD, "@NumSolicitud", DbType.String, poTrazabilidadBE.NumSolicitud);
    //                objDB.AddInParameter(objCMD, "@Aprobador", DbType.String, poTrazabilidadBE.Aprobador);
    //                objDB.AddInParameter(objCMD, "@Rol", DbType.String, poTrazabilidadBE.Rol);
    //                objDB.AddInParameter(objCMD, "@ReasignadoA", DbType.String, poTrazabilidadBE.ReasignadoA);
    //                objDB.AddInParameter(objCMD, "@IdEstado", DbType.Int32, poTrazabilidadBE.IdEstado);
    //                objDB.AddInParameter(objCMD, "@ReasignadoANombreCompleto", DbType.String, poTrazabilidadBE.ReasignadoANombreCompleto);
    //                objDB.AddInParameter(objCMD, "@AprobadorNombreCompleto", DbType.String, poTrazabilidadBE.AprobadorNombreCompleto);
    //                objDB.AddInParameter(objCMD, "@version", DbType.Int32, versionNumSolicitud);

    //                try
    //                {
    //                    res = objDB.ExecuteNonQuery(objCMD);
    //                }
    //                catch
    //                {
    //                    throw;
    //                }
    //            }
    //        });

    //        return res > 0;
    //    }

    //    public TrazabilidadBE ObtenerCabeceraTrazabilidadSSLPorIdSolicitud(Int64 piIdSolicitud)
    //    {
    //        TrazabilidadBE oTrazabilidad = null;
    //        Database objDB = Util.CrearBaseDatos();
    //        using (DbCommand objCMD = objDB.GetStoredProcCommand("PA_GET_OBTENERTRAZABILIDADSSLCAB_POR_IDSOLICITUD"))
    //        {
    //            objDB.AddInParameter(objCMD, "@IdSolicitud", DbType.Int64, piIdSolicitud);
    //            try
    //            {
    //                using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
    //                {
    //                    if (oDataReader.Read())
    //                    {
    //                        oTrazabilidad = new TrazabilidadBE();
    //                        oTrazabilidad.NumSolicitud = (string)oDataReader["NumSolicitud"];
    //                        oTrazabilidad.AprobadorNombreCompleto = (string)oDataReader["UsuarioRegistro"];
    //                        oTrazabilidad.FechaEjecucion = (DateTime?)oDataReader["FechaRegistro"];

    //                    }
    //                }
    //            }
    //            catch
    //            {

    //                throw;
    //            }
    //        }

    //        return oTrazabilidad;
    //    }

    //    public List<TrazabilidadBE> ObtenerUltimoComentarioPorSolicitud(string psNumSolicitud)
    //    {
    //        List<TrazabilidadBE> oListaTrazabilidad = new List<TrazabilidadBE>();
    //        Database objDB = Util.CrearBaseDatos();
    //        using (DbCommand objCMD = objDB.GetStoredProcCommand("PA_GET_ULTIMO_COMENTARIO_POR_SOLICITUD"))
    //        {
    //            objDB.AddInParameter(objCMD, "@pNumSolicitud", DbType.String, psNumSolicitud);

    //            using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
    //            {
    //                while (oDataReader.Read())
    //                {
    //                    TrazabilidadBE oTrazabilidad = new TrazabilidadBE();
    //                    oTrazabilidad.Id = (Int64)oDataReader["Id"];
    //                    oTrazabilidad.NumSolicitud = (string)oDataReader["NumSolicitud"];
    //                    oListaTrazabilidad.Add(oTrazabilidad);
    //                }
    //            }
    //        }

    //        return oListaTrazabilidad;
    //    }
             
    //    public List<TrazabilidadBE> ObtenerTrazabilidadByNum(string psNumSolicitud)
    //    {
    //        List<TrazabilidadBE> oListaTrazabilidad = new List<TrazabilidadBE>();
    //        Database objDB = Util.CrearBaseDatos();
    //        using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_GET_OBTENERTRAZABILIDAD"))
    //        {
    //            objDB.AddInParameter(objCMD, "@NumSolicitud", DbType.String, psNumSolicitud);


    //            try
    //            {
    //                using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
    //                {
    //                    while (oDataReader.Read())
    //                    {
    //                        TrazabilidadBE oTrazabilidad = new TrazabilidadBE();
    //                        oTrazabilidad.Id = (Int64)oDataReader["Id"];
    //                        oTrazabilidad.NumSolicitud = (string)oDataReader["NumSolicitud"];
    //                        oTrazabilidad.Aprobador = (string)oDataReader["Aprobador"];
    //                        oTrazabilidad.AprobadorNombreCompleto = (string)oDataReader["AprobadorNombreCompleto"];
    //                        oTrazabilidad.Rol = (string)oDataReader["Rol"];
    //                        oTrazabilidad.ReasignadoA = (string)oDataReader["ReasignadoA"];
    //                        oTrazabilidad.ReasignadoANombreCompleto = (string)oDataReader["ReasignadoANombreCompleto"];
    //                        oTrazabilidad.IdEstado = (int)oDataReader["IdEstado"];
    //                        oTrazabilidad.DescEstado = (string)oDataReader["EstadoSolicitud"];
    //                        oTrazabilidad.FechaEjecucion = (DateTime?)(oDataReader["FechaEjecucion"] == DBNull.Value ? null : oDataReader["FechaEjecucion"]);
    //                        oTrazabilidad.IdentificadorUnico = oDataReader["IdentificadorUnico"] == DBNull.Value ? Guid.Empty : (Guid)(oDataReader["IdentificadorUnico"]);
    //                        oTrazabilidad.Comentario = oDataReader["Comentario"] as string;
    //                        oTrazabilidad.FechaEnvio = (DateTime?)(oDataReader["FechaEnvio"] == DBNull.Value ? null : oDataReader["FechaEnvio"]);
    //                        oListaTrazabilidad.Add(oTrazabilidad);
    //                    }
    //                }
    //            }
    //            catch
    //            {

    //                throw;
    //            }
    //        }

    //        return oListaTrazabilidad;
    //    }
   }

}
