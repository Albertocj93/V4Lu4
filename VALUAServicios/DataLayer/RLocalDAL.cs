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
    public class RLocalDAL
    {

        public bool EliminarLocal(int piIdLocal)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_DEL_LOCAL"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdLocal);
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
        public bool InsertarLocal(RLocalBE poLocalBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_INS_LOCAL"))
            {
                objDB.AddInParameter(objCMD, "@IdSucursal", DbType.String, poLocalBE.IdSucursal);
                objDB.AddInParameter(objCMD, "@Descripcion", DbType.String, poLocalBE.Descripcion);
                objDB.AddInParameter(objCMD, "@EstaActivo", DbType.Boolean, poLocalBE.EstaActivo);

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

        public bool ActualizarLocal(RLocalBE poLocalBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_LOCAL"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, poLocalBE.Id);
                objDB.AddInParameter(objCMD, "@IdSucursal", DbType.Int32, poLocalBE.IdSucursal);
                objDB.AddInParameter(objCMD, "@Descripcion", DbType.String, poLocalBE.Descripcion);
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
        public List<RLocalBE> ObtenerLocales(int IdSociedad, int IdZona, int IdSucursal)
        {
            List<RLocalBE> oListaLocales = new List<RLocalBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERLOCALCOMPLETO"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@idSociedad", DbType.Int32, IdSociedad);
                    objDB.AddInParameter(objCMD, "@idZona", DbType.Int32, IdZona);
                    objDB.AddInParameter(objCMD, "@idSucursal", DbType.Int32, IdSucursal);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RLocalBE oLocalBE = new RLocalBE();
                            oLocalBE.Id = (int)oDataReader["Id"];
                            oLocalBE.Descripcion = (string)oDataReader["Descripcion"];
                            oLocalBE.IdSociedad = (int)oDataReader["IdSociedad"];
                            oLocalBE.DescripcionSociedad = (string)oDataReader["Sociedad"];
                            oLocalBE.IdZona = (int)oDataReader["IdZona"];
                            oLocalBE.DescripcionZona = (string)oDataReader["Zona"];
                            oLocalBE.IdSucursal = (int)oDataReader["IdSucursal"];
                            oLocalBE.DescripcionSucursal = (string)oDataReader["Sucursal"];
                            oLocalBE.EstaActivo = (bool)oDataReader["EstaActivo"];
                            oListaLocales.Add(oLocalBE);
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oListaLocales;
        }

        public RLocalBE ObtenerLocalPorId(int piIdLocal)
        {
            RLocalBE oLocalBE = new RLocalBE();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERLOCALPORID"))
            {

                try
                {
                    objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdLocal);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        if (oDataReader.Read())
                        {

                            oLocalBE.Id = (int)oDataReader["Id"];
                            oLocalBE.IdSucursal = (int)oDataReader["IdSucursal"];
                            oLocalBE.IdZona = (int)oDataReader["IdZona"];
                            oLocalBE.IdSociedad = (int)oDataReader["IdSociedad"];
                            oLocalBE.Descripcion = (string)oDataReader["Descripcion"];

                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oLocalBE;
        }
    }
}
