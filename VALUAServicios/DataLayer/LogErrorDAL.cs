using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using System.Data.Common;
using System.Data;

namespace DataLayer
{
    public class LogErrorDAL
    {

        //public bool InsertarLogError(LogErrorBE poLogErrorBE)
        //{
        //    int resultado = 0;
        //    Database objDB = Util.CrearBaseDatos();
        //    using (DbCommand objCMD = objDB.GetStoredProcCommand("USP_INS_LOGERROR"))
        //    {

        //        objDB.AddInParameter(objCMD, "@Error", DbType.String, poLogErrorBE.Error);
        //        objDB.AddInParameter(objCMD, "@Fecha", DbType.DateTime, poLogErrorBE.FechaHora);


        //        try
        //        {
        //            resultado = objDB.ExecuteNonQuery(objCMD);


        //        }
        //        catch (Exception ex)
        //        {
        //            //{
        //            //    EventLog objLog = new EventLog();
        //            //    objLog.LogError(ex);

        //            throw ex;
        //        }
        //    }

        //    return resultado > 0;
        //}

    }
}
