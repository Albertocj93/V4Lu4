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
    public class RGerenciaDivisionDAL
    {

        public bool EliminarGerenciaDivision(int piIdGerenciaDivision)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_DEL_GERENCIADIVISION"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdGerenciaDivision);
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
        public bool InsertarGerenciaDivision(RGerenciaDivisionBE poGerenciaDivisionBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_INS_GERENCIADIVISION"))
            {
                objDB.AddInParameter(objCMD, "@IdGerenciaCentral", DbType.String, poGerenciaDivisionBE.IdGerenciaCentral);
                objDB.AddInParameter(objCMD, "@Descripcion", DbType.String, poGerenciaDivisionBE.Descripcion);
                objDB.AddInParameter(objCMD, "@ResponsableGerenciaDivision", DbType.String, poGerenciaDivisionBE.ResponsableGerenciaDivision);
                objDB.AddInParameter(objCMD, "@EstaActivo", DbType.Boolean, poGerenciaDivisionBE.EstaActivo);

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

        public bool ActualizarGerenciaDivision(RGerenciaDivisionBE poGerenciaDivisionBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_GERENCIADIVISION"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, poGerenciaDivisionBE.Id);
                objDB.AddInParameter(objCMD, "@IdGerenciaCentral", DbType.Int32, poGerenciaDivisionBE.IdGerenciaCentral);
                objDB.AddInParameter(objCMD, "@Descripcion", DbType.String, poGerenciaDivisionBE.Descripcion);
                objDB.AddInParameter(objCMD, "@ResponsableGerenciaDivision", DbType.String, poGerenciaDivisionBE.ResponsableGerenciaDivision);

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
        public List<RGerenciaDivisionBE> ObtenerGerenciaDivisions(int IdGerenciaCentral)
        {
            List<RGerenciaDivisionBE> oListaGerenciaDivisions = new List<RGerenciaDivisionBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERGERENCIADIVISION"))
            {

                try
                {
                    objDB.AddInParameter(objCMD, "@IdGerenciaCentral", DbType.Int32, IdGerenciaCentral);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RGerenciaDivisionBE oGerenciaDivisionBE = new RGerenciaDivisionBE();
                            oGerenciaDivisionBE.Id = (int)oDataReader["Id"];
                            oGerenciaDivisionBE.Descripcion = (string)oDataReader["Descripcion"];
                            oGerenciaDivisionBE.DescripcionGerenciaCentral = (string)oDataReader["GerenciaCentral"];
                            oGerenciaDivisionBE.ResponsableGerenciaDivision = (string)oDataReader["ResponsableGerenciaDivision"];
                            oGerenciaDivisionBE.EstaActivo = (bool)oDataReader["EstaActivo"];
                            oListaGerenciaDivisions.Add(oGerenciaDivisionBE);
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oListaGerenciaDivisions;
        }
    

        public RGerenciaDivisionBE ObtenerGerenciaDivisionPorId(int piIdGerenciaDivision)
        {
            RGerenciaDivisionBE oGerenciaDivisionBE = new RGerenciaDivisionBE();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERGERENCIADIVISIONPORID"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdGerenciaDivision);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        if (oDataReader.Read())
                        {
                            oGerenciaDivisionBE.Id = (int)oDataReader["Id"];
                            oGerenciaDivisionBE.IdGerenciaCentral = (int)oDataReader["IdGerenciaCentral"];
                            oGerenciaDivisionBE.Descripcion = (string)oDataReader["Descripcion"];
                            oGerenciaDivisionBE.ResponsableGerenciaDivision = (string)oDataReader["ResponsableGerenciaDivision"];
                            oGerenciaDivisionBE.EstaActivo = (bool)oDataReader["EstaActivo"];
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oGerenciaDivisionBE;
        }
    }
}
