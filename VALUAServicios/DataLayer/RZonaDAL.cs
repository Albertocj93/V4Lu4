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
    public class RZonaDAL
    {

        public bool EliminarZona(int piIdZona)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_DEL_ZONA"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdZona);
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
        public bool InsertarZona(RZonaBE poZonaBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_INS_ZONA"))
            {
                objDB.AddInParameter(objCMD, "@IdSociedad", DbType.String, poZonaBE.IdSociedad);
                objDB.AddInParameter(objCMD, "@Descripcion", DbType.String, poZonaBE.Descripcion);
                objDB.AddInParameter(objCMD, "@EstaActivo", DbType.Boolean, poZonaBE.EstaActivo);

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
        public List<RZonaBE> ObtenerZonas(int IdSociedad)
        {
            List<RZonaBE> oListaZonas = new List<RZonaBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERZONAS"))
            {

                try
                {
                    objDB.AddInParameter(objCMD, "@IdSociedad", DbType.Int32, IdSociedad);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RZonaBE oZonaBE = new RZonaBE();
                            oZonaBE.Id = (int)oDataReader["Id"];
                            oZonaBE.Descripcion = (string)oDataReader["Descripcion"];
                            oZonaBE.DescripcionSociedad= (string)oDataReader["Sociedad"];
                            oZonaBE.EstaActivo = (bool)oDataReader["EstaActivo"];
                            oListaZonas.Add(oZonaBE);
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oListaZonas;
        }
        public List<RZonaBE> ObtenerZonasCombo(int IdSociedad)
        {
            List<RZonaBE> oListaZonas = new List<RZonaBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERZONAS"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@IdSociedad", DbType.Int32, IdSociedad);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RZonaBE oZona = new RZonaBE();
                            oZona.Id = (int)oDataReader["Id"];
                            oZona.Descripcion = (string)oDataReader["Descripcion"];
                            oZona.EstaActivo = (bool)oDataReader["EstaActivo"];
                            oListaZonas.Add(oZona);
                        }
                    }
                    /*
                     * vintMessageID =
                        Convert.ToInt32(objDB.GetParameterValue(objCMD, "@intErrID"));
                        vstrMessage = objDB.GetParameterValue(objCMD, "@strMessage").ToString();
                        vobjUser.UserID = vintMessageID > 0 ? vintMessageID : 0;
                     */
                }
                catch (Exception ex)
                {
                    //{
                    //    EventLog objLog = new EventLog();
                    //    objLog.LogError(ex);

                    throw ex;
                }
            }
            return oListaZonas;
        }

        public RZonaBE ObtenerZonaPorId(int piIdZona)
        {
            RZonaBE oZonaBE = new RZonaBE();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERZONAPORID"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdZona);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        if (oDataReader.Read())
                        {
                            oZonaBE.Id = (int)oDataReader["Id"];
                            oZonaBE.IdSociedad = (int)oDataReader["IdSociedad"];
                            oZonaBE.Descripcion = (string)oDataReader["Descripcion"];
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oZonaBE;
        }
    }
}
