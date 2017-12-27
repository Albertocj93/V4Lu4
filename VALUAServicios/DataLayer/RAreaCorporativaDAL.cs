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
    public class RAreaCorporativaDAL
    {

        public bool EliminarAreaCorporativa(int piIdAreaCorporativa)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_DEL_AREACORPORATIVA"))
            {

                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdAreaCorporativa);

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
        public bool InsertarAreaCorporativa(RAreaCorporativaBE poAreaCorporativaBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_INS_AREACORPORATIVA"))
            {

                objDB.AddInParameter(objCMD, "@Descripcion", DbType.String, poAreaCorporativaBE.Descripcion);
                objDB.AddInParameter(objCMD, "@EstaActivo", DbType.Boolean, poAreaCorporativaBE.EstaActivo);
                objDB.AddInParameter(objCMD, "@Codigo", DbType.String, poAreaCorporativaBE.Codigo);

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

        public bool ActualizarAreaCorporativa(RAreaCorporativaBE poAreaCorporativaBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_AREACORPORATIVA"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, poAreaCorporativaBE.Id);
                objDB.AddInParameter(objCMD, "@Descripcion", DbType.String, poAreaCorporativaBE.Descripcion);
                objDB.AddInParameter(objCMD, "@Codigo", DbType.String, poAreaCorporativaBE.Codigo);

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
        public List<RAreaCorporativaBE> ObtenerAreaCorporativa()
        {
            List<RAreaCorporativaBE> oListaAreaCorporativaes = new List<RAreaCorporativaBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERAREACORPORATIVA"))
            {

                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RAreaCorporativaBE oAreaCorporativaBE = new RAreaCorporativaBE();
                            oAreaCorporativaBE.Id = (int)oDataReader["Id"];
                            oAreaCorporativaBE.Codigo = (string)oDataReader["Codigo"];
                            oAreaCorporativaBE.Descripcion = (string)oDataReader["Descripcion"];
                            oAreaCorporativaBE.EstaActivo = (bool)oDataReader["EstaActivo"];
                            oListaAreaCorporativaes.Add(oAreaCorporativaBE);
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oListaAreaCorporativaes;
        }

        public RAreaCorporativaBE ObtenerAreaCorporativaPorId(int piIdAreaCorporativa)
        {
            RAreaCorporativaBE oAreaCorporativaBE = new RAreaCorporativaBE();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERAREACORPORATIVAPORID"))
            {

                try
                {
                    objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdAreaCorporativa);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        if (oDataReader.Read())
                        {
                            oAreaCorporativaBE.Id = (int)oDataReader["Id"];
                            oAreaCorporativaBE.Descripcion = (string)oDataReader["Descripcion"];
                            oAreaCorporativaBE.Codigo = (string)oDataReader["Codigo"];
                            oAreaCorporativaBE.EstaActivo = (bool)oDataReader["EstaActivo"];

                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oAreaCorporativaBE;
        }
    }
}
