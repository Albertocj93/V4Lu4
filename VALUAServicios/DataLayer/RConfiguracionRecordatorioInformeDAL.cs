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
    public class RConfiguracionRecordatorioInformeDAL
    {
        public bool InsertarConfiguracionRecordatorioInforme(RConfiguracionRecordatorioInformeBE pConfiguracionRecordatorioInformeBE)
        {

            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("PA_INS_ConfiguracionRecordatorioInforme"))
            {

                objDB.AddInParameter(objCMD, "@Estado", DbType.String, pConfiguracionRecordatorioInformeBE.Estado);
                objDB.AddInParameter(objCMD, "@Dias", DbType.Int32, pConfiguracionRecordatorioInformeBE.Dias);
                objDB.AddInParameter(objCMD, "@Frecuencia", DbType.Int32, pConfiguracionRecordatorioInformeBE.Frecuencia);
                objDB.AddInParameter(objCMD, "@Mensaje", DbType.String, pConfiguracionRecordatorioInformeBE.Mensaje);

                try
                {
                    res = objDB.ExecuteNonQuery(objCMD);
                }
                catch
                {
                    throw;
                }
            }

            return res > 0;
        }

        public bool ActualizarConfiguracionRecordatorioInforme(RConfiguracionRecordatorioInformeBE pConfiguracionRecordatorioInformeBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("PA_UPD_ConfiguracionRecordatorioInforme"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, pConfiguracionRecordatorioInformeBE.Id);
                objDB.AddInParameter(objCMD, "@Dias", DbType.Int32, pConfiguracionRecordatorioInformeBE.Dias);
                objDB.AddInParameter(objCMD, "@Frecuencia", DbType.Int32, pConfiguracionRecordatorioInformeBE.Frecuencia);
                objDB.AddInParameter(objCMD, "@Mensaje", DbType.String, pConfiguracionRecordatorioInformeBE.Mensaje);
                objDB.AddInParameter(objCMD, "@FlagDesactivar", DbType.Boolean, pConfiguracionRecordatorioInformeBE.Activo);

                try
                {
                    res = objDB.ExecuteNonQuery(objCMD);
                }
                catch
                {
                    throw;
                }
            }

            return res > 0;
        }

        public bool EliminarConfiguracionRecordatorioInforme(int pId)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("PA_DEL_ConfiguracionRecordatorioInforme"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, pId);
                
                try
                {
                    res = objDB.ExecuteNonQuery(objCMD);
                }
                catch
                {
                    throw;
                }
            }

            return res > 0;
        }

        public List<RConfiguracionRecordatorioInformeBE> ObtenerConfiguracionRecordatorioInforme(string tipo)
        {
            List<RConfiguracionRecordatorioInformeBE> oListaMontosVp = new List<RConfiguracionRecordatorioInformeBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("PA_GET_LISTConfiguracionRecordatorioInforme"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@Tipo", DbType.String, tipo);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RConfiguracionRecordatorioInformeBE oConfiguracionRecordatorioInformeBE = new RConfiguracionRecordatorioInformeBE();
                            oConfiguracionRecordatorioInformeBE.Id = (int)oDataReader["Id"];
                            oConfiguracionRecordatorioInformeBE.Estado = (string)oDataReader["Estado"];
                            oConfiguracionRecordatorioInformeBE.Dias = (int)oDataReader["Dias"];
                            oConfiguracionRecordatorioInformeBE.Frecuencia = (int)oDataReader["Frecuencia"];
                            oConfiguracionRecordatorioInformeBE.Mensaje = (string)oDataReader["Mensaje"];
                            oConfiguracionRecordatorioInformeBE.Activo = (bool)oDataReader["FlagDesactivar"];
                            oListaMontosVp.Add(oConfiguracionRecordatorioInformeBE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return oListaMontosVp;
        }

        public RConfiguracionRecordatorioInformeBE ExisteoConfiguracionRecordatorioInforme(string pTipo)
        {
            RConfiguracionRecordatorioInformeBE oConfiguracionRecordatorioInformeBE = null;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("PA_GET_EXISTEConfiguracionRecordatorioInforme"))
            {
                objDB.AddInParameter(objCMD, "@Estado", DbType.String, pTipo);

                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        if (oDataReader.Read())
                        {
                            oConfiguracionRecordatorioInformeBE = new RConfiguracionRecordatorioInformeBE();
                            oConfiguracionRecordatorioInformeBE.Id = (int)oDataReader["Id"];
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return oConfiguracionRecordatorioInformeBE;
        }

        public RConfiguracionRecordatorioInformeBE ObtenerConfiguracionRecordatorioInforme(int pId)
        {
            RConfiguracionRecordatorioInformeBE oConfiguracionRecordatorioInformeBE = null;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("dbo.PA_GET_LISTConfiguracionRecordatorioInformePORID"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, pId);

                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        if (oDataReader.Read())
                        {
                            oConfiguracionRecordatorioInformeBE = new RConfiguracionRecordatorioInformeBE();
                            oConfiguracionRecordatorioInformeBE.Id = (int)oDataReader["Id"];
                            oConfiguracionRecordatorioInformeBE.Estado = (string)oDataReader["Estado"];
                            oConfiguracionRecordatorioInformeBE.Dias = (int)oDataReader["Dias"];
                            oConfiguracionRecordatorioInformeBE.Frecuencia = (int)oDataReader["Frecuencia"];
                            oConfiguracionRecordatorioInformeBE.Mensaje = (string)oDataReader["Mensaje"];
                            oConfiguracionRecordatorioInformeBE.Activo = (bool)oDataReader["FlagDesactivar"];
                            oConfiguracionRecordatorioInformeBE.IdServicio = (string)oDataReader["IdServicioRecordatorio"];
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return oConfiguracionRecordatorioInformeBE;
        }
    }

 
}
