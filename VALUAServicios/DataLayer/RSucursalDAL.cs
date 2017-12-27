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
    public class RSucursalDAL
    {

        public bool EliminarSucursal(int piIdSucursal)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_DEL_SUCURSAL"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdSucursal);
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
        public bool InsertarSucursal(RSucursalBE poSucursalBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_INS_SUCURSAL"))
            {
                objDB.AddInParameter(objCMD, "@IdZona", DbType.String, poSucursalBE.IdZona);
                objDB.AddInParameter(objCMD, "@Descripcion", DbType.String, poSucursalBE.Descripcion);
                objDB.AddInParameter(objCMD, "@EstaActivo", DbType.Boolean, poSucursalBE.EstaActivo);

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

        public bool ActualizarSucursal(RSucursalBE poSucursalBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_SUCURSAL"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, poSucursalBE.Id);
                objDB.AddInParameter(objCMD, "@IdZona", DbType.Int32, poSucursalBE.IdZona);
                objDB.AddInParameter(objCMD, "@Descripcion", DbType.String, poSucursalBE.Descripcion);

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
        public List<RSucursalBE> ObtenerSucursales(int IdSociedad, int IdZona)
        {
            List<RSucursalBE> oListaSucursales = new List<RSucursalBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERSUCURSALCOMPLETO"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@idSociedad", DbType.Int32, IdSociedad);
                    objDB.AddInParameter(objCMD, "@idZona", DbType.Int32, IdZona);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RSucursalBE oSucursalaBE = new RSucursalBE();
                            oSucursalaBE.Id = (int)oDataReader["Id"];
                            oSucursalaBE.Descripcion = (string)oDataReader["Descripcion"];
                            oSucursalaBE.IdSociedad = (int)oDataReader["IdSociedad"];
                            oSucursalaBE.DescripcionSociedad = (string)oDataReader["Sociedad"];
                            oSucursalaBE.DescripcionZona = (string)oDataReader["Zona"];
                            oSucursalaBE.EstaActivo = (bool)oDataReader["EstaActivo"];
                            oListaSucursales.Add(oSucursalaBE);
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oListaSucursales;
        }

        public RSucursalBE ObtenerSucursalPorId(int piIdSucursal)
        {
            RSucursalBE oSucursalBE = new RSucursalBE();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERSUCURSALPORID"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdSucursal);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        if (oDataReader.Read())
                        {
                            oSucursalBE.Id = (int)oDataReader["Id"];
                            oSucursalBE.IdZona = (int)oDataReader["IdZona"];
                            oSucursalBE.IdSociedad = (int)oDataReader["IdSociedad"];
                            oSucursalBE.Descripcion = (string)oDataReader["Descripcion"];

                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oSucursalBE;
        }
    }
}
