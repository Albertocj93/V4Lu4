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
    public class RCriticidadDAL
    {

        public RCriticidadBE ObtenerCriticidad(string valor)
        {
            RCriticidadBE oSociedadBE = new RCriticidadBE();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("UPSR_GET_GRADOCRITICIDAD"))
            {

                try
                {
                    objDB.AddInParameter(objCMD, "@GradoCriticidad", DbType.String, valor);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        if (oDataReader.Read())
                        {
                            oSociedadBE.Id = (int)oDataReader["Id"];
                            oSociedadBE.Descripcion = (string)oDataReader["Descripcion"];
                            oSociedadBE.Color = (string)oDataReader["Color"];

                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oSociedadBE;
        }
    }
}
