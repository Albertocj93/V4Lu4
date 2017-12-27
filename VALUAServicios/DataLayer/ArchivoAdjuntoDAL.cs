using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataLayer
{
    public class ArchivoAdjuntoDAL
    {
        //public List<ArchivoAdjuntoBE> ObtenerArchivoAdjuntosPorIdSolicitudTipoSolicitud(Int64 piIdSolicitud,string psTipoSolicitud)
        //{
        //    List<ArchivoAdjuntoBE> oListaArchivosAdjuntos = new List<ArchivoAdjuntoBE>();
        //    Database objDB = Util.CrearBaseDatos();
        //    using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_GET_OBTENERARCHIVOADJUNTOPORIDSOLICITUD"))
        //    {
        //        objDB.AddInParameter(objCMD, "@IdSolicitud", DbType.Int64, piIdSolicitud);
        //        objDB.AddInParameter(objCMD, "@TipoSolicitud", DbType.String, psTipoSolicitud);
        //        try
        //        {
        //            using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
        //            {
        //                while (oDataReader.Read())
        //                {
        //                    ArchivoAdjuntoBE oArchivoAdjunto = new ArchivoAdjuntoBE();
        //                    oArchivoAdjunto.Id = (int)oDataReader["Id"];
        //                    oArchivoAdjunto.NombreArchivo = (string)oDataReader["NombreArchivo"];
        //                    oArchivoAdjunto.IdUnico = (Guid)oDataReader["IdentificadorUnico"];
        //                    oArchivoAdjunto.IdSolicitud = (Int64)oDataReader["IdSolicitud"];
        //                    oArchivoAdjunto.TamanioArchivo = (decimal)oDataReader["TamanioArchivo"];
        //                    oListaArchivosAdjuntos.Add(oArchivoAdjunto);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //            throw ex;
        //        }
        //    }

        //    return oListaArchivosAdjuntos;
        //}
        //public List<ArchivoAdjuntoBE> ObtenerArchivoAdjuntosPorTipo(string psTipoSolicitud)
        //{
        //    List<ArchivoAdjuntoBE> oListaArchivosAdjuntos = new List<ArchivoAdjuntoBE>();
        //    Database objDB = Util.CrearBaseDatos();
        //    using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_GET_OBTENERARCHIVOADJUNTOPORTIPO"))
        //    {
        //        objDB.AddInParameter(objCMD, "@TipoSolicitud", DbType.String, psTipoSolicitud);
        //        try
        //        {
        //            using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
        //            {
        //                while (oDataReader.Read())
        //                {
        //                    ArchivoAdjuntoBE oArchivoAdjunto = new ArchivoAdjuntoBE();
        //                    oArchivoAdjunto.Id = (int)oDataReader["Id"];
        //                    oArchivoAdjunto.NombreArchivo = (string)oDataReader["NombreArchivo"];
        //                    oArchivoAdjunto.IdUnico = (Guid)oDataReader["IdentificadorUnico"];
        //                 //  oArchivoAdjunto.IdSolicitud = (Int64)oDataReader["IdSolicitud"];
        //                    oArchivoAdjunto.TamanioArchivo = (decimal)oDataReader["TamanioArchivo"];
        //                    oListaArchivosAdjuntos.Add(oArchivoAdjunto);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //            throw ex;
        //        }
        //    }

        //    return oListaArchivosAdjuntos;
        //}

        //public ArchivoAdjuntoBE ObtenerArchivoAdjuntoPorId(int piIdArchivoAdjunto)
        //{
        //    ArchivoAdjuntoBE oArchivoAdjunto = null;
        //    Database objDB = Util.CrearBaseDatos();
        //    using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_GET_OBTENERARCHIVOADJUNTOPORID"))
        //    {
        //        objDB.AddInParameter(objCMD, "@Id", DbType.Int64, piIdArchivoAdjunto);

        //        try
        //        {
        //            using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
        //            {
        //                if (oDataReader.Read())
        //                {
        //                    oArchivoAdjunto = new ArchivoAdjuntoBE();
        //                    oArchivoAdjunto.Id = (int)oDataReader["Id"];
        //                    oArchivoAdjunto.NombreArchivo = (string)oDataReader["NombreArchivo"];
        //                    oArchivoAdjunto.IdUnico = (Guid)oDataReader["IdentificadorUnico"];
        //                    oArchivoAdjunto.IdSolicitud = (Int64)(oDataReader["IdSolicitud"] == DBNull.Value ? 0 : (Int64)oDataReader["IdSolicitud"]);
        //                    oArchivoAdjunto.ArchivoFisico = (byte[])oDataReader["ArchivoFisico"];
        //                    oArchivoAdjunto.TamanioArchivo = (decimal)oDataReader["TamanioArchivo"];
        //                    oArchivoAdjunto.ContentType = (string)oDataReader["ContentType"];
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            //{
        //            //    EventLog objLog = new EventLog();
        //            //    objLog.LogError(ex);

        //            throw ex;
        //        }
        //    }

        //    return oArchivoAdjunto;
        //}
        
        //public int InsertarArchivoAdjunto(ArchivoAdjuntoBE poArchivoAdjunto)
        //{
        //    int idArchivoAdjuntoGenerado = 0;
        //    Database objDB = Util.CrearBaseDatos();
        //    using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_INS_ARCHIVOADJUNTO"))
        //    {
        //        objDB.AddInParameter(objCMD, "@TipoSolicitud", DbType.String, poArchivoAdjunto.TipoSolicitud);
        //        objDB.AddInParameter(objCMD, "@NombreArchivo", DbType.String, poArchivoAdjunto.NombreArchivo);
        //        objDB.AddInParameter(objCMD, "@IdentificadorUnico", DbType.Guid, poArchivoAdjunto.IdUnico);
        //        objDB.AddInParameter(objCMD, "@ArchivoFisico", DbType.Binary, poArchivoAdjunto.ArchivoFisico);
        //        objDB.AddInParameter(objCMD, "@TamanioArchivo", DbType.Decimal, poArchivoAdjunto.TamanioArchivo);
        //        objDB.AddInParameter(objCMD, "@ContentType", DbType.String, poArchivoAdjunto.ContentType);                
        //        objDB.AddOutParameter(objCMD, "@Id", DbType.Int64, 0);
        //        try
        //        {
        //            objDB.ExecuteNonQuery(objCMD);
        //            idArchivoAdjuntoGenerado = Convert.ToInt32(objDB.GetParameterValue(objCMD, "@Id"));

        //        }
        //        catch (Exception ex)
        //        {

        //            throw ex;
        //        }
        //    }

        //    return idArchivoAdjuntoGenerado;
        //}

        //public bool ActualizarCodigoSolicitudArchivoAdjunto(int piIdArchivoAdjunto, Int64 piIdSolicitud)
        //{
        //    int i = 0;
        //    Database objDB = Util.CrearBaseDatos();
        //    using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_UPD_ARCHIVOADJUNTO"))
        //    {

        //        objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdArchivoAdjunto);
        //        objDB.AddInParameter(objCMD, "@IdSolicitud", DbType.Int64, piIdSolicitud);
        //        try
        //        {
        //            //i = objDB.ExecuteNonQuery(objCMD);
        //            objDB.ExecuteNonQuery(objCMD);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }

        //    return i > 0;
        //}

        //public bool ActualizarCodigoSolicitudArchivoAdjuntoSSL(int piIdArchivoAdjunto, Int64 piIdSolicitud)
        //{
        //    int i = 0;
        //    Database objDB = Util.CrearBaseDatos();
        //    using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_UPD_ARCHIVOADJUNTOSSL"))
        //    {
        //        objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdArchivoAdjunto);
        //        objDB.AddInParameter(objCMD, "@IdSolicitud", DbType.Int64, piIdSolicitud);

        //        try
        //        {
        //            i = objDB.ExecuteNonQuery(objCMD);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }

        //    return i > 0;
        //}

        //public bool EliminarArchivoAdjunto(int piIdArchivoAdjunto)
        //{
        //    int i = 0;
        //    Database objDB = Util.CrearBaseDatos();
        //    using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_DEL_ARCHIVOADJUNTO"))
        //    {

        //        objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdArchivoAdjunto);

        //        try
        //        {
        //            i = objDB.ExecuteNonQuery(objCMD);

        //        }
        //        catch (Exception ex)
        //        {

        //            throw ex;
        //        }
        //    }

        //    return i > 0;
        //}

        //public bool EliminarArchivoAdjuntoSSL(int piIdArchivoAdjunto)
        //{
        //    int i = 0;
        //    Database objDB = Util.CrearBaseDatos();
        //    using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_DEL_ARCHIVOADJUNTOSSL"))
        //    {

        //        objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdArchivoAdjunto);

        //        try
        //        {
        //            i = objDB.ExecuteNonQuery(objCMD);

        //        }
        //        catch (Exception ex)
        //        {

        //            throw ex;
        //        }
        //    }

        //    return i > 0;
        //}

        //public List<ArchivoAdjuntoBE> ObtenerArchivoAdjuntosSSLPorIdSolicitudTipoSolicitud(Int64 piIdSolicitud)
        //{
        //    List<ArchivoAdjuntoBE> oListaArchivosAdjuntos = new List<ArchivoAdjuntoBE>();
        //    Database objDB = Util.CrearBaseDatos();
        //    using (DbCommand objCMD = objDB.GetStoredProcCommand("PA_GET_OBTENERARCHIVOADJUNTOSSLPORIDSOLICITUD"))
        //    {
        //        objDB.AddInParameter(objCMD, "@IdSolicitud", DbType.Int64, piIdSolicitud);
        //        try
        //        {
        //            using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
        //            {
        //                while (oDataReader.Read())
        //                {
        //                    ArchivoAdjuntoBE oArchivoAdjunto = new ArchivoAdjuntoBE();
        //                    oArchivoAdjunto.Id = (int)oDataReader["Id"];
        //                    oArchivoAdjunto.NombreArchivo = (string)oDataReader["NombreArchivo"];
        //                    oArchivoAdjunto.IdUnico = (Guid)oDataReader["IdentificadorUnico"];
        //                    oArchivoAdjunto.IdSolicitud = (Int64)oDataReader["IdSolicitud"];
        //                    oArchivoAdjunto.TamanioArchivo = (decimal)oDataReader["TamanioArchivo"];
        //                    oListaArchivosAdjuntos.Add(oArchivoAdjunto);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }

        //    return oListaArchivosAdjuntos;
        //}

        //public int InsertarArchivoAdjuntSSL(ArchivoAdjuntoBE poArchivoAdjunto)
        //{
        //    int idArchivoAdjuntoGenerado = 0;
        //    Database objDB = Util.CrearBaseDatos();
        //    using (DbCommand objCMD = objDB.GetStoredProcCommand("PA_INS_ARCHIVOADJUNTO_SSL"))
        //    {
        //        objDB.AddInParameter(objCMD, "@TipoSolicitud", DbType.String, poArchivoAdjunto.TipoSolicitud);
        //        objDB.AddInParameter(objCMD, "@NombreArchivo", DbType.String, poArchivoAdjunto.NombreArchivo);
        //        objDB.AddInParameter(objCMD, "@IdentificadorUnico", DbType.Guid, poArchivoAdjunto.IdUnico);
        //        objDB.AddInParameter(objCMD, "@ArchivoFisico", DbType.Binary, poArchivoAdjunto.ArchivoFisico);
        //        objDB.AddInParameter(objCMD, "@TamanioArchivo", DbType.Decimal, poArchivoAdjunto.TamanioArchivo);
        //        objDB.AddInParameter(objCMD, "@ContentType", DbType.String, poArchivoAdjunto.ContentType);

        //        objDB.AddOutParameter(objCMD, "@Id", DbType.Int64, 0);

        //        try
        //        {
        //            objDB.ExecuteNonQuery(objCMD);
        //            idArchivoAdjuntoGenerado = Convert.ToInt32(objDB.GetParameterValue(objCMD, "@Id"));
        //        }
        //        catch (Exception ex)
        //        {

        //            throw ex;
        //        }
        //    }

        //    return idArchivoAdjuntoGenerado;
        //}

        //public ArchivoAdjuntoBE ObtenerArchivoAdjuntoSSLPorId(int piIdArchivoAdjunto)
        //{
        //    ArchivoAdjuntoBE oArchivoAdjunto = null;
        //    Database objDB = Util.CrearBaseDatos();
        //    using (DbCommand objCMD = objDB.GetStoredProcCommand("PA_GET_OBTENERARCHIVOADJUNTOSSLPORID"))
        //    {
        //        objDB.AddInParameter(objCMD, "@Id", DbType.Int64, piIdArchivoAdjunto);

        //        try
        //        {
        //            using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
        //            {
        //                if (oDataReader.Read())
        //                {
        //                    oArchivoAdjunto = new ArchivoAdjuntoBE();
        //                    oArchivoAdjunto.Id = (int)oDataReader["Id"];
        //                    oArchivoAdjunto.NombreArchivo = (string)oDataReader["NombreArchivo"];
        //                    oArchivoAdjunto.IdUnico = (Guid)oDataReader["IdentificadorUnico"];
        //                    oArchivoAdjunto.IdSolicitud = (Int64)(oDataReader["IdSolicitud"] == DBNull.Value ? 0 : (Int64)oDataReader["IdSolicitud"]);
        //                    oArchivoAdjunto.ArchivoFisico = (byte[])oDataReader["ArchivoFisico"];
        //                    oArchivoAdjunto.TamanioArchivo = (decimal)oDataReader["TamanioArchivo"];
        //                    oArchivoAdjunto.ContentType = (string)oDataReader["ContentType"];
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            //{
        //            //    EventLog objLog = new EventLog();
        //            //    objLog.LogError(ex);

        //            throw ex;
        //        }
        //    }

        //    return oArchivoAdjunto;
        //}

        
    }
}
