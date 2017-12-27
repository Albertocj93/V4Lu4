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
    public class RGerenciaCentralDAL
    {

        public bool EliminarGerenciaCentral(int piIdGerenciaCentral)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_DEL_GERENCIACENTRAL"))
            {

                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdGerenciaCentral);

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
        public bool InsertarGerenciaCentral(RGerenciaCentralBE poGerenciaCentralBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_INS_GERENCIACENTRAL"))
            {

                objDB.AddInParameter(objCMD, "@Descripcion", DbType.String, poGerenciaCentralBE.Descripcion);
                objDB.AddInParameter(objCMD, "@ResponsableGerenciaCentral", DbType.String, poGerenciaCentralBE.ResponsableGerenciaCentral);
                objDB.AddInParameter(objCMD, "@EstaActivo", DbType.Boolean, poGerenciaCentralBE.EstaActivo);
                objDB.AddInParameter(objCMD, "@IdColor", DbType.Int32, poGerenciaCentralBE.IdColor);

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

        public bool ActualizarGerenciaCentral(RGerenciaCentralBE poGerenciaCentralBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_GERENCIACENTRAL"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, poGerenciaCentralBE.Id);
                objDB.AddInParameter(objCMD, "@Descripcion", DbType.String, poGerenciaCentralBE.Descripcion);
                objDB.AddInParameter(objCMD, "@ResponsableGerenciaCentral", DbType.String, poGerenciaCentralBE.ResponsableGerenciaCentral);
                objDB.AddInParameter(objCMD, "@IdColor", DbType.String, poGerenciaCentralBE.IdColor);

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
        public List<RGerenciaCentralBE> ObtenerGerenciaCentrales()
        {
            List<RGerenciaCentralBE> oListaGerenciaCentrales = new List<RGerenciaCentralBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERGERENCIACENTRAL"))
            {

                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RGerenciaCentralBE oGerenciaCentralBE = new RGerenciaCentralBE();
                            oGerenciaCentralBE.Id = (int)oDataReader["Id"];
                            oGerenciaCentralBE.Descripcion = (string)oDataReader["Descripcion"];
                            oGerenciaCentralBE.ResponsableGerenciaCentral = (string)oDataReader["ResponsableGerenciaCentral"];
                            oGerenciaCentralBE.EstaActivo = (bool)oDataReader["EstaActivo"];
                            oGerenciaCentralBE.ValorColor = (string)oDataReader["Valor"];
                            oListaGerenciaCentrales.Add(oGerenciaCentralBE);
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            return oListaGerenciaCentrales;
        }
        public List<RColorBE> ObtenerColores()
        {
            List<RColorBE> Colores = new List<RColorBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERCOLORES"))
            {

                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RColorBE oColorBE = new RColorBE();
                            oColorBE.Id = (int)oDataReader["Id"];
                            oColorBE.Descripcion = (string)oDataReader["Valor"];
                            oColorBE.Codigo = (string)oDataReader["Valor"];
                            Colores.Add(oColorBE);
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            return Colores;
        }
     
     
        public RGerenciaCentralBE ObtenerGerenciaCentralPorId(int piIdGerenciaCentral)
        {
            RGerenciaCentralBE oGerenciaCentralBE = new RGerenciaCentralBE();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERGERENCIACENTRALPORID"))
            {

                try
                {
                    objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdGerenciaCentral);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        if (oDataReader.Read())
                        {

                            oGerenciaCentralBE.Id = (int)oDataReader["Id"];
                            oGerenciaCentralBE.IdColor = (int)oDataReader["IdColor"];
                            oGerenciaCentralBE.Descripcion = (string)oDataReader["Descripcion"];
                            oGerenciaCentralBE.ResponsableGerenciaCentral = (string)oDataReader["ResponsableGerenciaCentral"];
                            oGerenciaCentralBE.EstaActivo = (bool)oDataReader["EstaActivo"];
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oGerenciaCentralBE;
        }

    }
}
