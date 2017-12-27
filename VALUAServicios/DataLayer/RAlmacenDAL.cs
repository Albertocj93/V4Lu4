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
    public class RAlmacenDAL
    {

        public bool EliminarAlmacen(int piIdAlmacen)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_DEL_ALMACEN"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdAlmacen);
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
        public bool InsertarAlmacen(RAlmacenBE poAlmacenBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_INS_ALMACEN"))
            {
                objDB.AddInParameter(objCMD, "@IdLocal", DbType.String, poAlmacenBE.IdLocal);
                objDB.AddInParameter(objCMD, "@Descripcion", DbType.String, poAlmacenBE.Descripcion);
                objDB.AddInParameter(objCMD, "@EstaActivo", DbType.Boolean, poAlmacenBE.EstaActivo);

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

        public bool ActualizarAlmacen(RAlmacenBE poAlmacenBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_ALMACEN"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, poAlmacenBE.Id);
                objDB.AddInParameter(objCMD, "@IdLocal", DbType.Int32, poAlmacenBE.IdLocal);
                objDB.AddInParameter(objCMD, "@Descripcion", DbType.String, poAlmacenBE.Descripcion);
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
        public List<RAlmacenBE> ObtenerAlmacenes(int IdSociedad, int IdZona, int IdSucursal,int IdLocal)
        {
            List<RAlmacenBE> oListaAlmacenes = new List<RAlmacenBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERALMACENCOMPLETO"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@idSociedad", DbType.Int32, IdSociedad);
                    objDB.AddInParameter(objCMD, "@idZona", DbType.Int32, IdZona);
                    objDB.AddInParameter(objCMD, "@idSucursal", DbType.Int32, IdSucursal);
                    objDB.AddInParameter(objCMD, "@idLocal", DbType.Int32, IdLocal);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RAlmacenBE oAlmacenBE = new RAlmacenBE();
                            oAlmacenBE.Id = (int)oDataReader["Id"];
                            oAlmacenBE.Descripcion = (string)oDataReader["Descripcion"];
                            oAlmacenBE.IdSociedad = (int)oDataReader["IdSociedad"];
                            oAlmacenBE.DescripcionSociedad = (string)oDataReader["Sociedad"];
                            oAlmacenBE.IdZona = (int)oDataReader["IdZona"];
                            oAlmacenBE.DescripcionZona = (string)oDataReader["Zona"];
                            oAlmacenBE.IdSucursal = (int)oDataReader["IdSucursal"];
                            oAlmacenBE.DescripcionSucursal = (string)oDataReader["Sucursal"];
                            oAlmacenBE.DescripcionLocal = (string)oDataReader["NombreLocal"];
                            oAlmacenBE.EstaActivo = (bool)oDataReader["EstaActivo"];
                            oListaAlmacenes.Add(oAlmacenBE);
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oListaAlmacenes;
        }

        public RAlmacenBE ObtenerAlmacenPorId(int piIdAlmacen)
        {
            RAlmacenBE oAlmacenBE = new RAlmacenBE();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERALMACENPORID"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdAlmacen);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        if (oDataReader.Read())
                        {
                            oAlmacenBE.Id = (int)oDataReader["Id"];
                            oAlmacenBE.IdLocal = (int)oDataReader["IdLocal"];
                            oAlmacenBE.IdSociedad = (int)oDataReader["IdSociedad"];
                            oAlmacenBE.IdSucursal = (int)oDataReader["IdSucursal"];
                            oAlmacenBE.IdZona = (int)oDataReader["IdZona"];
                            oAlmacenBE.Descripcion = (string)oDataReader["Descripcion"];
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oAlmacenBE;
        }
    }
}
