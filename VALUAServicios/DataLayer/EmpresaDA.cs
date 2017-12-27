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
    public class EmpresaDA
    {

        

         private static EmpresaDA _EmpresaDA = null;

         private EmpresaDA() { }


        public static EmpresaDA Instanse 
	    {
		    get {
                if (_EmpresaDA != null) return _EmpresaDA;
                _EmpresaDA = new EmpresaDA();
                return _EmpresaDA;
		    }
	    }

        public bool Insert(DBHelper pDBHelper, EmpresaBE pEmpresa)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@DESCRIPCION", string.IsNullOrEmpty(pEmpresa.Descripcion) ? (object)DBNull.Value : pEmpresa.Descripcion),
                new DBHelper.Parameters("@SIGLA", string.IsNullOrEmpty(pEmpresa.Sigla) ? (object)DBNull.Value : pEmpresa.Sigla),
                new DBHelper.Parameters("@USUARIOCREACION", string.IsNullOrEmpty(pEmpresa.UsuarioCreacion) ? (object)DBNull.Value : pEmpresa.UsuarioCreacion)
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("EmpresaInsert"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

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
        public bool Update(DBHelper pDBHelper, EmpresaBE pEmpresa)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@ID", pEmpresa.Id == Constantes.INT_NULO ? (object)DBNull.Value : pEmpresa.Id),
                new DBHelper.Parameters("@DESCRIPCION", string.IsNullOrEmpty(pEmpresa.Descripcion) ? (object)DBNull.Value : pEmpresa.Descripcion),
                new DBHelper.Parameters("@SIGLA", string.IsNullOrEmpty(pEmpresa.Sigla) ? (object)DBNull.Value : pEmpresa.Sigla),
                new DBHelper.Parameters("@USUARIOMODIFICACION", string.IsNullOrEmpty(pEmpresa.UsuarioModificacion) ? (object)DBNull.Value : pEmpresa.UsuarioModificacion)
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("EmpresaUpdate"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

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
        public bool Delete(DBHelper pDBHelper, EmpresaBE pEmpresa)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@ID", pEmpresa.Id == Constantes.INT_NULO ? (object)DBNull.Value : pEmpresa.Id),
                 new DBHelper.Parameters("@USUARIOMODIFICACION", string.IsNullOrEmpty(pEmpresa.UsuarioModificacion) ? (object)DBNull.Value : pEmpresa.UsuarioModificacion)
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("EmpresaDelete"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

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
        public List<EmpresaBE> GetAll(DBHelper pDBHelper)
        {
            List<EmpresaBE> lst = null;
            EmpresaBE obj = null;
            DBHelper.Parameters[] colParameters = null;
            lst = new List<EmpresaBE>();
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("EmpresaGetAll"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new EmpresaBE();

                        obj.Id = int.Parse(dr["IdEmpresa"].ToString());
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
                            obj.FechaCreacion = null;
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

        public List<EmpresaBE> GetByUser(string usuario)
        {
            List<EmpresaBE> oLista = new List<EmpresaBE>();

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "EmpresaGetByUser";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp1 = new SqlParameter("@Usuario", usuario);
            cmd.Parameters.Add(sp1);

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    EmpresaBE oEmpresa = new EmpresaBE();

                    oEmpresa.Id = int.Parse(dataReader["IdEmpresa"].ToString());
                    oEmpresa.Descripcion = dataReader["descripcion"].ToString();
                    oEmpresa.Sigla = dataReader["Sigla"].ToString();
                    oEmpresa.IdVista = int.Parse(dataReader["IdVista"].ToString());

                    oLista.Add(oEmpresa);
                }
            }
            return oLista;
        }

        public int GetIdByDesc(DBHelper pDBHelper,string descripcion)
        {
            int Respuesta = -1;

            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    
                new DBHelper.Parameters("@descripcion", string.IsNullOrEmpty(descripcion)  ? (object)DBNull.Value : descripcion)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("EmpresaGetIdByDesc"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    if (dr.Read())
                    {
                        Respuesta = Convert.ToInt32(dr["IdEmpresa"].ToString());
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

        public EmpresaBE GetByIdEmpresa(DBHelper pDBHelper, int IdEmpresa)
        {
            EmpresaBE EmpresaBE = new EmpresaBE();

            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    
                new DBHelper.Parameters("@IdEmpresa", IdEmpresa == Constantes.INT_NULO  ? (object)DBNull.Value : IdEmpresa)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("EmpresaGetByIdEmpresa"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    if (dr.Read())
                    {
                        EmpresaBE.Id = int.Parse(dr["IdEmpresa"].ToString());
                        EmpresaBE.Descripcion = dr["descripcion"].ToString();
                        EmpresaBE.Sigla = dr["Sigla"].ToString();
                        EmpresaBE.IdVista = int.Parse(dr["IdVista"].ToString());
                    }

                }
                return EmpresaBE;
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
