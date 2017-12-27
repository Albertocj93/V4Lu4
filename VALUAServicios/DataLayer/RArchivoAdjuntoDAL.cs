using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataLayer
{
    public class RArchivoAdjuntoDAL
    {


        //public Guid InsertarArchivoAdjunto(RArchivoAdjuntoBE poArchivoAdjunto)
        //{
        //    Guid idArchivoAdjuntoGenerado = new Guid();
        //    Database objDB = Util.CrearBaseDatosPDF();
        //    using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_INS_ARCHIVOADJUNTO"))
        //    {
        //        objDB.AddInParameter(objCMD, "@NombreArchivo", DbType.String, poArchivoAdjunto.NombreArchivo);
        //        objDB.AddInParameter(objCMD, "@IdentificadorUnico", DbType.Guid, poArchivoAdjunto.IdUnico);
        //        objDB.AddInParameter(objCMD, "@ArchivoFisico", DbType.Binary, poArchivoAdjunto.ArchivoFisico);
        //        objDB.AddInParameter(objCMD, "@ContentType", DbType.String, poArchivoAdjunto.ContentType);                
        //        try
        //        {
        //            objDB.ExecuteNonQuery(objCMD);
        //            idArchivoAdjuntoGenerado = (Guid)objDB.GetParameterValue(objCMD, "@IdentificadorUnico");
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //    return idArchivoAdjuntoGenerado;
        //}

        public List<ArchivoAdjuntoBE> obtenerArchivoAdjuntoPorId_TipoSolicitud(string IdSolicitud)
        {

            List<ArchivoAdjuntoBE> lst = new List<ArchivoAdjuntoBE>();
            ArchivoAdjuntoBE obj = new ArchivoAdjuntoBE();

            string connectionString = ConfigurationManager.ConnectionStrings["BD_Demo"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "USP_GET_OBTENERARCHIVOADJUNTOPORIDSOLICITUD";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@IdSolicitud", new Guid(IdSolicitud));
                cmd.Parameters.Add(par1);
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        obj = new ArchivoAdjuntoBE();

                        obj.idDoc = int.Parse(dataReader["Id"].ToString());
                        obj.titulo = dataReader["NombreArchivo"].ToString();
                        obj.IdUnico = (Guid)(dataReader["IdentificadorUnico"]);
                        obj.IdSolicitud = (Guid)(dataReader["IdSolicitud"]);
                        obj.TamanioArchivo = (Decimal)(dataReader["TamanioArchivo"]);
                        obj.urlItem = "/extapps/webservices/APIRANSA/Handler.ashx?id=" + obj.idDoc;
                        obj.nuevo = "";

                        lst.Add(obj);

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

            return lst;
        }


      
        
    }
}
