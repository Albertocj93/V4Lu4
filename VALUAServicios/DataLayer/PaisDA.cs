using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using System.Data;
using System.Data.Common;
using Common;
using System.Data.SqlClient;
using System.Configuration;
using Utility;

namespace DataLayer
{
    public class PaisDA
    {
        private static PaisDA _PaisDA = null;

        private PaisDA() { }

        public static PaisDA Instanse 
	    {
		    get {
                if (_PaisDA != null) return _PaisDA;
                _PaisDA = new PaisDA();
                return _PaisDA;
		    }
	    }

        public List<PaisBE> GetAll(DBHelper pDBHelper)
        {
            List<PaisBE> lst = null;
            PaisBE obj = null;
            DBHelper.Parameters[] colParameters = null;
            lst = new List<PaisBE>();
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("PaisGetAll"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new PaisBE();

                        obj.Id = int.Parse(dr["IdPais"].ToString());
                        obj.Descripcion = dr["Descripcion"].ToString();
                        obj.Sigla = dr["Sigla"].ToString();
                        obj.UsuarioModificacion = dr["UsuarioModificacionConfidencial"].ToString() == "1" ?
                                                        ConfigurationManager.AppSettings["UsuarioAdministracion"].ToString() :
                                                        dr["usuarioModificacion"].ToString();
                        obj.UsuarioCreacion = dr["UsuarioCreacionConfidencial"].ToString() == "1" ?
                                                        ConfigurationManager.AppSettings["UsuarioAdministracion"].ToString() :
                                                        dr["usuarioCreacion"].ToString();

                        if (string.IsNullOrEmpty(dr["FechaCreacion"].ToString()))
                        {
                            obj.FechaCreacion =  null ;
                        }
                        else
                        {
                            obj.FechaCreacion = DateTime.Parse(dr["FechaCreacion"].ToString());
                        }


                        if (string.IsNullOrEmpty(dr["FechaModificacion"].ToString()))
                        {
                            obj.FechaModificacion = null;
                        }
                        else
                        {
                            obj.FechaModificacion = DateTime.Parse(dr["FechaModificacion"].ToString());
                        }

                        lst.Add(obj);
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }
        }


        public List<PaisBE> GetAllAnt()
        {
            List<PaisBE> oLista = new List<PaisBE>();

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "PaisGetAll";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    PaisBE oPais = new PaisBE();

                    oPais.Id = Convert.ToInt32(dataReader["IdPais"]);
                    oPais.Descripcion = dataReader["Descripcion"].ToString();
                    oPais.Sigla = dataReader["Sigla"].ToString();
                    
                    oLista.Add(oPais);
                }
            }

            return oLista;
        }
        public int GetIdByDesc(DBHelper pDBHelper,string descripcion)
        {
            int Respuesta=-1;

            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    
                new DBHelper.Parameters("@descripcion", string.IsNullOrEmpty(descripcion) ? (object)DBNull.Value : descripcion)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("PaisGetIdByDesc"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    if (dr.Read())
                    {
                        Respuesta = Convert.ToInt32(dr["IdPais"].ToString());
                    }
                }
                return Respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }
        }
        public PaisBE GetByIdPais(DBHelper pDBHelper, int IdPais)
        {
            PaisBE PaisBE = new BusinessEntities.PaisBE();

            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    
                new DBHelper.Parameters("@IdPais", IdPais == Constantes.INT_NULO  ? (object)DBNull.Value : IdPais)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("PaisGetByIdPais"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    if (dr.Read())
                    {
                        PaisBE.Id = Convert.ToInt32(dr["IdPais"].ToString());
                        PaisBE.Descripcion = dr["Descripcion"].ToString();
                        PaisBE.Sigla = dr["Sigla"].ToString();
                    }
                }
                return PaisBE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }
        }



        public bool Insert(DBHelper pDBHelper, PaisBE pPais)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@DESCRIPCION", string.IsNullOrEmpty(pPais.Descripcion) ? (object)DBNull.Value : pPais.Descripcion),
                new DBHelper.Parameters("@SIGLA", string.IsNullOrEmpty(pPais.Sigla) ? (object)DBNull.Value : pPais.Sigla),
                new DBHelper.Parameters("@USUARIOCREACION", string.IsNullOrEmpty(pPais.UsuarioCreacion) ? (object)DBNull.Value : pPais.UsuarioCreacion)
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("PaisInsert"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

                return (lfilasAfectadas > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }

        }

        public bool Update(DBHelper pDBHelper, PaisBE pPais)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@ID", pPais.Id == Constantes.INT_NULO ? (object)DBNull.Value : pPais.Id),
                new DBHelper.Parameters("@DESCRIPCION", string.IsNullOrEmpty(pPais.Descripcion) ? (object)DBNull.Value : pPais.Descripcion),
                new DBHelper.Parameters("@SIGLA", string.IsNullOrEmpty(pPais.Sigla) ? (object)DBNull.Value : pPais.Sigla),
                new DBHelper.Parameters("@USUARIOMODIFICACION", string.IsNullOrEmpty(pPais.UsuarioModificacion) ? (object)DBNull.Value : pPais.UsuarioModificacion)
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("PaisUpdate"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

                return (lfilasAfectadas > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }
        }

        public bool Delete(DBHelper pDBHelper, PaisBE pPais)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@ID", pPais.Id == Constantes.INT_NULO ? (object)DBNull.Value : pPais.Id),
                 new DBHelper.Parameters("@USUARIOMODIFICACION", string.IsNullOrEmpty(pPais.UsuarioModificacion) ? (object)DBNull.Value : pPais.UsuarioModificacion)
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("PaisDelete"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

                return (lfilasAfectadas > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }
        }

    }
}
