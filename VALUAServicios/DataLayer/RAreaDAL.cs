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
    public class RAreaDAL
    {

        public bool EliminarArea(int piIdArea)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_DEL_AREA"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdArea);
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
        public bool InsertarArea(RAreaBE poAreaBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_INS_AREA"))
            {
                objDB.AddInParameter(objCMD, "@IdGerenciaDivision", DbType.String, poAreaBE.IdGerenciaDivision);
                objDB.AddInParameter(objCMD, "@Descripcion", DbType.String, poAreaBE.Descripcion);
                objDB.AddInParameter(objCMD, "@ResponsableArea", DbType.String, poAreaBE.ResponsableArea);
                objDB.AddInParameter(objCMD, "@JefeSupervisor", DbType.String, poAreaBE.JefeSupervisor);
                objDB.AddInParameter(objCMD, "@EstaActivo", DbType.Boolean, poAreaBE.EstaActivo);

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
        public bool InsertarResponsable(RResponsableObservacionBE ResponsableObservacionBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_INS_RESPONSABLE_OBSERVACION"))
            {
                objDB.AddInParameter(objCMD, "@IdArea", DbType.String, ResponsableObservacionBE.IdArea);
                objDB.AddInParameter(objCMD, "@Cuenta", DbType.String, ResponsableObservacionBE.Cuenta);
                objDB.AddInParameter(objCMD, "@Nombre", DbType.String, ResponsableObservacionBE.Nombre);
                objDB.AddInParameter(objCMD, "@EstaActivo", DbType.Boolean, ResponsableObservacionBE.EstaActivo);

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

        public bool ActualizarArea(RAreaBE poAreaBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_AREA"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, poAreaBE.Id);
                objDB.AddInParameter(objCMD, "@IdGerenciaDivision", DbType.Int32, poAreaBE.IdGerenciaDivision);
                objDB.AddInParameter(objCMD, "@Descripcion", DbType.String, poAreaBE.Descripcion);
                objDB.AddInParameter(objCMD, "@ResponsableArea", DbType.String, poAreaBE.ResponsableArea);
                objDB.AddInParameter(objCMD, "@JefeSupervisor", DbType.String, poAreaBE.JefeSupervisor);

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

        public List<RResponsableObservacionBE> ObtenerResponsablesObservacion(int IdArea)
        {
            List<RResponsableObservacionBE> listaResponsables = new List<RResponsableObservacionBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_RESPONSABLE_OBSERVACION"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@IdArea", DbType.Int32, IdArea);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RResponsableObservacionBE responsable = new RResponsableObservacionBE();
                            
                            responsable.Id = (int)oDataReader["Id"];
                            responsable.Cuenta = (string)oDataReader["Cuenta"];
                            responsable.Nombre = (string)oDataReader["Nombre"];
                            listaResponsables.Add(responsable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return listaResponsables;
        }
        public RResponsableObservacionBE ObtenerResponsableObservaciónPorId(int IdRo)
        {
            RResponsableObservacionBE ResponsableObservacionBE = new RResponsableObservacionBE();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENER_RO_PORID"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@Id", DbType.Int32, IdRo);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        if (oDataReader.Read())
                        {
                            ResponsableObservacionBE.Id = (int)oDataReader["Id"];
                            ResponsableObservacionBE.Cuenta = (string)oDataReader["Cuenta"];
                            ResponsableObservacionBE.Nombre = (string)oDataReader["Nombre"];
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return ResponsableObservacionBE;
        }
        public bool EliminarResponsableObservacion(int IdResponsableObservacion)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_DEL_RESPONSABLE_OBSERVACION"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, IdResponsableObservacion);
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
        public bool EliminarResponsableObservacionPorArea(int IdArea)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_DEL_RESPONSABLE_OBSERVACION_POR_AREA"))
            {
                objDB.AddInParameter(objCMD, "@IdArea", DbType.Int32, IdArea);
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
        public List<RAreaBE> ObtenerAreas(int IdGerenciaCentral, int IdGerenciaDivision)
        {
            List<RAreaBE> oListaAreaes = new List<RAreaBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERAREACOMPLETO"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@idGerenciaCentral", DbType.Int32, IdGerenciaCentral);
                    objDB.AddInParameter(objCMD, "@idGerenciaDivision", DbType.Int32, IdGerenciaDivision);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RAreaBE oAreaaBE = new RAreaBE();
                            oAreaaBE.Id = (int)oDataReader["Id"];
                            oAreaaBE.Descripcion = (string)oDataReader["Descripcion"];
                            oAreaaBE.ResponsableArea = (string)oDataReader["ResponsableArea"];
                            oAreaaBE.JefeSupervisor = (string)oDataReader["JefeSupervisor"];
                            oAreaaBE.IdGerenciaCentral = (int)oDataReader["IdGerenciaCentral"];
                            oAreaaBE.DescripcionGerenciaCentral = (string)oDataReader["GerenciaCentral"];
                            oAreaaBE.DescripcionGerenciaDivision = (string)oDataReader["GerenciaDivision"];
                            oAreaaBE.EstaActivo = (bool)oDataReader["EstaActivo"];
                            oListaAreaes.Add(oAreaaBE);
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oListaAreaes;
        }

        public RAreaBE ObtenerAreaPorId(int piIdArea)
        {
            RAreaBE oAreaBE = new RAreaBE();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERAREAPORID"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdArea);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        if (oDataReader.Read())
                        {
                            oAreaBE.Id = (int)oDataReader["Id"];
                            oAreaBE.IdGerenciaDivision = (int)oDataReader["IdGerenciaDivision"];
                            oAreaBE.IdGerenciaCentral = (int)oDataReader["IdGerenciaCentral"];
                            oAreaBE.Descripcion = (string)oDataReader["Descripcion"];
                            oAreaBE.ResponsableArea = (string)oDataReader["ResponsableArea"];
                            oAreaBE.JefeSupervisor = (string)oDataReader["JefeSupervisor"];
                            oAreaBE.EstaActivo = (bool)oDataReader["EstaActivo"];
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oAreaBE;
        }
    }
}
