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
    public class RTablasGeneralesDAL
    {

        public int ValidarMaestroInforme(int IdSociedad, int IdZona,int IdSucursal, int IdLocal, int IdAlmacen, int IdPais, int IdCiudad,int IdArea)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_VALIDAR_MAESTROs_INFORME"))
            {

                objDB.AddInParameter(objCMD, "@IdSociedad", DbType.Int32, IdSociedad);
                objDB.AddInParameter(objCMD, "@IdZona", DbType.Int32, IdZona);
                objDB.AddInParameter(objCMD, "@IdSucursal", DbType.Int32, IdSucursal);
                objDB.AddInParameter(objCMD, "@IdLocal", DbType.Int32, IdLocal);
                objDB.AddInParameter(objCMD, "@IdAlmacen", DbType.Int32, IdAlmacen);
                objDB.AddInParameter(objCMD, "@IdPais", DbType.Int32, IdPais);
                objDB.AddInParameter(objCMD, "@IdCiudad", DbType.Int32, IdCiudad);
                objDB.AddInParameter(objCMD, "@IdArea", DbType.Int32, IdArea);

                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            res = (int)oDataReader["Id"];
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return res;
        }
        public int ValidarMaestroEvento(int IdGerenciaCentral, int IdGerenciaDivision, int IdArea,int IdTipo, int IdRiesgo)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_VALIDAR_MAESTROS_EVENTO"))
            {

                objDB.AddInParameter(objCMD, "@IdGerenciaCentral", DbType.Int32, IdGerenciaCentral);
                objDB.AddInParameter(objCMD, "@IdGerenciaDivision", DbType.Int32, IdGerenciaDivision);
                objDB.AddInParameter(objCMD, "@IdArea", DbType.Int32, IdArea);
                objDB.AddInParameter(objCMD, "@IdTipo", DbType.Int32, IdTipo);
                objDB.AddInParameter(objCMD, "@IdRiesgo", DbType.Int32, IdRiesgo);
                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            res = (int)oDataReader["Id"];
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return res;
        }
        public bool EliminarTablasGenerales(int piIdTablasGenerales)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_DEL_TABLASGENERALES"))
            {

                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdTablasGenerales);

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
        public bool EliminarPais(int piIdTablasGenerales)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_DEL_PAIS"))
            {

                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdTablasGenerales);

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
        public bool EliminarTipoRiesgo(int piIdTablasGenerales)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_DEL_TIPORIESGO"))
            {

                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdTablasGenerales);

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
        public bool EliminarRiesgoEvento(int piIdTablasGenerales)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_DEL_RIESGOEVENTO"))
            {

                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdTablasGenerales);

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
        public bool EliminarCiudad(int piIdTablasGenerales)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_DEL_CIUDAD"))
            {

                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdTablasGenerales);

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
        public bool InsertarTablasGenerales(RTablasGeneralesBE poTablasGeneralesBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_INS_TABLASGENERALES"))
            {

                objDB.AddInParameter(objCMD, "@Descripcion", DbType.String, poTablasGeneralesBE.Descripcion);
                objDB.AddInParameter(objCMD, "@Tipo", DbType.String, poTablasGeneralesBE.Tipo);
                objDB.AddInParameter(objCMD, "@EstaActivo", DbType.Boolean, poTablasGeneralesBE.EstaActivo);

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
        public bool InsertarPais(RTablasGeneralesBE poTablasGeneralesBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_INS_PAIS"))
            {

                objDB.AddInParameter(objCMD, "@Descripcion", DbType.String, poTablasGeneralesBE.Descripcion);
                objDB.AddInParameter(objCMD, "@EstaActivo", DbType.Boolean, poTablasGeneralesBE.EstaActivo);
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
        public bool InsertarTipoRiesgo(RTablasGeneralesBE poTablasGeneralesBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_INS_TIPORIESGO"))
            {

                objDB.AddInParameter(objCMD, "@Descripcion", DbType.String, poTablasGeneralesBE.Descripcion);
                objDB.AddInParameter(objCMD, "@EstaActivo", DbType.Boolean, poTablasGeneralesBE.EstaActivo);
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
        public bool InsertarRiesgoEvento(RTablasGeneralesBE poTablasGeneralesBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_INS_RIESGOEVENTO"))
            {

                objDB.AddInParameter(objCMD, "@Descripcion", DbType.String, poTablasGeneralesBE.Descripcion);
                objDB.AddInParameter(objCMD, "@EstaActivo", DbType.Boolean, poTablasGeneralesBE.EstaActivo);
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
        public bool InsertarCiudad(RTablasGeneralesBE poTablasGeneralesBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_INS_CIUDAD"))
            {

                objDB.AddInParameter(objCMD, "@IdPais", DbType.Int32, poTablasGeneralesBE.IdPais);
                objDB.AddInParameter(objCMD, "@Ciudad", DbType.String, poTablasGeneralesBE.Ciudad);
                objDB.AddInParameter(objCMD, "@EstaActivo", DbType.Boolean, poTablasGeneralesBE.EstaActivo);

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
        public bool ActualizarTablasGenerales(RTablasGeneralesBE poTablasGeneralesBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_TABLASGENERALES"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, poTablasGeneralesBE.Id);
                objDB.AddInParameter(objCMD, "@Descripcion", DbType.String, poTablasGeneralesBE.Descripcion);
                objDB.AddInParameter(objCMD, "@Tipo", DbType.String, poTablasGeneralesBE.Tipo);

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

        public bool ActualizarPais(RTablasGeneralesBE poTablasGeneralesBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_PAIS"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, poTablasGeneralesBE.Id);
                objDB.AddInParameter(objCMD, "@Descripcion", DbType.String, poTablasGeneralesBE.Descripcion);

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
        public bool ActualizarTipoRiesgo(RTablasGeneralesBE poTablasGeneralesBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_TIPORIESGO"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, poTablasGeneralesBE.Id);
                objDB.AddInParameter(objCMD, "@Descripcion", DbType.String, poTablasGeneralesBE.Descripcion);

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
        public bool ActualizarRiesgoEvento(RTablasGeneralesBE poTablasGeneralesBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_RIESGOEVENTO"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, poTablasGeneralesBE.Id);
                objDB.AddInParameter(objCMD, "@Descripcion", DbType.String, poTablasGeneralesBE.Descripcion);

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
        public bool ActualizarCiudad(RTablasGeneralesBE poTablasGeneralesBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_CIUDAD"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, poTablasGeneralesBE.Id);
                objDB.AddInParameter(objCMD, "@IdPais", DbType.Int32, poTablasGeneralesBE.IdPais);
                objDB.AddInParameter(objCMD, "@Ciudad", DbType.String, poTablasGeneralesBE.Ciudad);

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
        public List<RTablasGeneralesBE> ObtenerTablasGeneraleses(string tipo)
        {
            List<RTablasGeneralesBE> oListaTablasGeneraleses = new List<RTablasGeneralesBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERTABLASGENERALES"))
            {
                objDB.AddInParameter(objCMD, "@Tipo", DbType.String, tipo);
                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RTablasGeneralesBE oTablasGeneralesBE = new RTablasGeneralesBE();
                            oTablasGeneralesBE.Id = (int)oDataReader["Id"];
                            oTablasGeneralesBE.Descripcion = (string)oDataReader["Descripcion"];
                            oTablasGeneralesBE.Tipo = (string)oDataReader["Tipo"];
                            oTablasGeneralesBE.EstaActivo = (bool)oDataReader["EstaActivo"];
                            oListaTablasGeneraleses.Add(oTablasGeneralesBE);
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oListaTablasGeneraleses;
        }
        public List<RTablasGeneralesBE> ObtenerCiudades(int IdPais)
        {
            List<RTablasGeneralesBE> oListaTablasGeneraleses = new List<RTablasGeneralesBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERCIUDADES"))
            {
                objDB.AddInParameter(objCMD, "@IdPais", DbType.Int32, IdPais);
                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RTablasGeneralesBE oTablasGeneralesBE = new RTablasGeneralesBE();
                            oTablasGeneralesBE.Id = (int)oDataReader["Id"];
                            oTablasGeneralesBE.Pais = (string)oDataReader["Pais"];
                            oTablasGeneralesBE.Descripcion = (string)oDataReader["Ciudad"];
                            oTablasGeneralesBE.EstaActivo = (bool)oDataReader["EstaActivo"];
                            oListaTablasGeneraleses.Add(oTablasGeneralesBE);
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oListaTablasGeneraleses;
        }
        public List<RTablasGeneralesBE> ObtenerTipoTablasGeneraleses()
        {
            int count = 0;
            List<RTablasGeneralesBE> oListaTablasGeneraleses = new List<RTablasGeneralesBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENER_TIPO_TABLAGENERALES"))
            {

                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RTablasGeneralesBE oTablasGeneralesBE = new RTablasGeneralesBE();
                            oTablasGeneralesBE.Id = count++ ;
                            oTablasGeneralesBE.Descripcion = (string)oDataReader["Descripcion"];
                            oListaTablasGeneraleses.Add(oTablasGeneralesBE);
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oListaTablasGeneraleses;
        }

        public List<RTablasGeneralesBE> ObtenerPaisTablasGeneraleses()
        {
            List<RTablasGeneralesBE> oListaTablasGeneraleses = new List<RTablasGeneralesBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERPAIS_TABLAGENERALES"))
            {
                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RTablasGeneralesBE oTablasGeneralesBE = new RTablasGeneralesBE();
                            oTablasGeneralesBE.Id = (int)oDataReader["Id"];
                            oTablasGeneralesBE.EstaActivo = (bool)oDataReader["EstaActivo"];
                            oTablasGeneralesBE.Descripcion = (string)oDataReader["Descripcion"];
                            oListaTablasGeneraleses.Add(oTablasGeneralesBE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListaTablasGeneraleses;
        }
        public List<RTablasGeneralesBE> ObtenerTipoRiesgoTablasGeneraleses()
        {
            List<RTablasGeneralesBE> oListaTablasGeneraleses = new List<RTablasGeneralesBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERTIPORIESGO_TABLAGENERALES"))
            {
                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RTablasGeneralesBE oTablasGeneralesBE = new RTablasGeneralesBE();
                            oTablasGeneralesBE.Id = (int)oDataReader["Id"];
                            oTablasGeneralesBE.EstaActivo = (bool)oDataReader["EstaActivo"];
                            oTablasGeneralesBE.Descripcion = (string)oDataReader["Descripcion"];
                            oListaTablasGeneraleses.Add(oTablasGeneralesBE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListaTablasGeneraleses;
        }

        public List<RTablasGeneralesBE> ObtenerRiesgoEventoTablasGeneraleses()
        {
            List<RTablasGeneralesBE> oListaTablasGeneraleses = new List<RTablasGeneralesBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERRIESGOEVENTO_TABLAGENERALES"))
            {
                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RTablasGeneralesBE oTablasGeneralesBE = new RTablasGeneralesBE();
                            oTablasGeneralesBE.Id = (int)oDataReader["Id"];
                            oTablasGeneralesBE.EstaActivo = (bool)oDataReader["EstaActivo"];
                            oTablasGeneralesBE.Descripcion = (string)oDataReader["Descripcion"];
                            oListaTablasGeneraleses.Add(oTablasGeneralesBE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListaTablasGeneraleses;
        }


        public List<RTablasGeneralesBE> ObtenerTipoInforme()
        {
            List<RTablasGeneralesBE> oListaTablasGeneraleses = new List<RTablasGeneralesBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERTIPOINFORME"))
            {
                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RTablasGeneralesBE oTablasGeneralesBE = new RTablasGeneralesBE();
                            oTablasGeneralesBE.Id = (int)oDataReader["Id"];
                            oTablasGeneralesBE.EstaActivo = (bool)oDataReader["EstaActivo"];
                            oTablasGeneralesBE.Descripcion = (string)oDataReader["Descripcion"];
                            oListaTablasGeneraleses.Add(oTablasGeneralesBE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListaTablasGeneraleses;
        }
        public List<RTablasGeneralesBE> ObtenerImpacto()
        {
            List<RTablasGeneralesBE> oListaTablasGeneraleses = new List<RTablasGeneralesBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERIMPACTO"))
            {
                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RTablasGeneralesBE oTablasGeneralesBE = new RTablasGeneralesBE();
                            oTablasGeneralesBE.Id = (int)oDataReader["Id"];
                            oTablasGeneralesBE.EstaActivo = (bool)oDataReader["EstaActivo"];
                            oTablasGeneralesBE.Descripcion = (string)oDataReader["Descripcion"];
                            oListaTablasGeneraleses.Add(oTablasGeneralesBE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListaTablasGeneraleses;
        }

        public List<RTablasGeneralesBE> ObtenerProbabilidad()
        {
            List<RTablasGeneralesBE> oListaTablasGeneraleses = new List<RTablasGeneralesBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERPROBABILIDAD"))
            {
                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RTablasGeneralesBE oTablasGeneralesBE = new RTablasGeneralesBE();
                            oTablasGeneralesBE.Id = (int)oDataReader["Id"];
                            oTablasGeneralesBE.EstaActivo = (bool)oDataReader["EstaActivo"];
                            oTablasGeneralesBE.Descripcion = (string)oDataReader["Descripcion"];
                            oListaTablasGeneraleses.Add(oTablasGeneralesBE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListaTablasGeneraleses;
        }
        public List<RTablasGeneralesBE> ObtenerTipoRiesgo()
        {
            List<RTablasGeneralesBE> oListaTablasGeneraleses = new List<RTablasGeneralesBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERTIPORIESGO"))
            {
                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RTablasGeneralesBE oTablasGeneralesBE = new RTablasGeneralesBE();
                            oTablasGeneralesBE.Id = (int)oDataReader["Id"];
                            oTablasGeneralesBE.EstaActivo = (bool)oDataReader["EstaActivo"];
                            oTablasGeneralesBE.Descripcion = (string)oDataReader["Descripcion"];
                            oListaTablasGeneraleses.Add(oTablasGeneralesBE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListaTablasGeneraleses;
        }

        public List<RTablasGeneralesBE> ObtenerRiesgoEventoDetectado()
        {
            List<RTablasGeneralesBE> oListaTablasGeneraleses = new List<RTablasGeneralesBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERRIESGOEVENTODETECTADO"))
            {
                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RTablasGeneralesBE oTablasGeneralesBE = new RTablasGeneralesBE();
                            oTablasGeneralesBE.Id = (int)oDataReader["Id"];
                            oTablasGeneralesBE.EstaActivo = (bool)oDataReader["EstaActivo"];
                            oTablasGeneralesBE.Descripcion = (string)oDataReader["Descripcion"];
                            oListaTablasGeneraleses.Add(oTablasGeneralesBE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListaTablasGeneraleses;
        }
        public RTablasGeneralesBE ObtenerTablasGeneralesPorId(int piIdTablasGenerales)
        {
            RTablasGeneralesBE oTablasGeneralesBE = new RTablasGeneralesBE();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERTABLASGENERALESPORID"))
            {

                try
                {
                    objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdTablasGenerales);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        if (oDataReader.Read())
                        {

                            oTablasGeneralesBE.Id = (int)oDataReader["Id"];
                            oTablasGeneralesBE.Descripcion = (string)oDataReader["Descripcion"];
                            oTablasGeneralesBE.Tipo = (string)oDataReader["Tipo"];

                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oTablasGeneralesBE;
        }

        public RTablasGeneralesBE ObtenerPaisPorId(int piIdTablasGenerales)
        {
            RTablasGeneralesBE oTablasGeneralesBE = new RTablasGeneralesBE();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERPAISPORID"))
            {

                try
                {
                    objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdTablasGenerales);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        if (oDataReader.Read())
                        {

                            oTablasGeneralesBE.Id = (int)oDataReader["Id"];
                            oTablasGeneralesBE.Descripcion = (string)oDataReader["Descripcion"];

                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oTablasGeneralesBE;
        }
        public RTablasGeneralesBE ObtenerTipoRiesgoPorId(int piIdTablasGenerales)
        {
            RTablasGeneralesBE oTablasGeneralesBE = new RTablasGeneralesBE();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERTIPORIESGOPORID"))
            {

                try
                {
                    objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdTablasGenerales);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        if (oDataReader.Read())
                        {

                            oTablasGeneralesBE.Id = (int)oDataReader["Id"];
                            oTablasGeneralesBE.Descripcion = (string)oDataReader["Descripcion"];

                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oTablasGeneralesBE;
        }
        public RTablasGeneralesBE ObtenerRiesgoEventoPorId(int piIdTablasGenerales)
        {
            RTablasGeneralesBE oTablasGeneralesBE = new RTablasGeneralesBE();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERRIESGOEVENTOPORID"))
            {

                try
                {
                    objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdTablasGenerales);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        if (oDataReader.Read())
                        {

                            oTablasGeneralesBE.Id = (int)oDataReader["Id"];
                            oTablasGeneralesBE.Descripcion = (string)oDataReader["Descripcion"];

                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oTablasGeneralesBE;
        }
        public RTablasGeneralesBE ObtenerCiudadPorId(int piIdTablasGenerales)
        {
            RTablasGeneralesBE oTablasGeneralesBE = new RTablasGeneralesBE();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERCIUDADPORID"))
            {

                try
                {
                    objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdTablasGenerales);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        if (oDataReader.Read())
                        {
                            oTablasGeneralesBE.Id = (int)oDataReader["Id"];
                            oTablasGeneralesBE.IdPais = (int)oDataReader["IdPais"];
                            oTablasGeneralesBE.Ciudad = (string)oDataReader["Ciudad"];
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oTablasGeneralesBE;
        }
    }
}
