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
    public class DepartamentoDA
    {

        public List<DepartamentoBE> GetByIdEmpresa(int IdEmpresa)
        {
            List<DepartamentoBE> oLista = new List<DepartamentoBE>();

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "DepartamentoGetByIdEmpresa";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter par1 = new SqlParameter("@IdEmpresa", IdEmpresa);
            cmd.Parameters.Add(par1);

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    DepartamentoBE oDepartamento = new DepartamentoBE();

                    oDepartamento.Id = Convert.ToInt32(dataReader["IdDepartamento"].ToString());
                    oDepartamento.Descripcion = dataReader["descripcion"].ToString();
                    oDepartamento.Sigla = dataReader["sigla"].ToString();

                    oLista.Add(oDepartamento);
                }
            }

            return oLista;
        }

        public int GetIdByDescDepEmp(DBHelper pDBHelper, string DescEmpresa, string DescDepartamento)
        {
            int Respuesta = -1;

            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    
                new DBHelper.Parameters("@DescEmpresa", string.IsNullOrEmpty(DescEmpresa)  ? (object)DBNull.Value : DescEmpresa),
                new DBHelper.Parameters("@DescDepartamento", string.IsNullOrEmpty(DescDepartamento)  ? (object)DBNull.Value : DescDepartamento),
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("DepartamentoGetIdByDescDepEmp"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {

                    // Leyendo reader 
                    if (dr.Read())
                    {
                        Respuesta = Convert.ToInt32(dr["IdDepartamento"].ToString());
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
        public DepartamentoBE GetByIdDepartamento(DBHelper pDBHelper, int IdDepartamento)
        {
            DepartamentoBE oDepartamento = new DepartamentoBE();

            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    
                new DBHelper.Parameters("@IdDepartamento", IdDepartamento == Constantes.INT_NULO  ? (object)DBNull.Value : IdDepartamento)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("DepartamentoGetByIdDep"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {

                    // Leyendo reader 
                    if (dr.Read())
                    {
                        oDepartamento.Id = Convert.ToInt32(dr["IdDepartamento"].ToString());
                        oDepartamento.Descripcion = dr["Descripcion"].ToString();
                        oDepartamento.Sigla = dr["Sigla"].ToString();
                    }
                }
                return oDepartamento;
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



        private static DepartamentoDA _DepartamentoDA = null;

         private DepartamentoDA() { }

         public static DepartamentoDA Instanse 
	    {
		    get {
                if (_DepartamentoDA != null) return _DepartamentoDA;
                _DepartamentoDA = new DepartamentoDA();
                return _DepartamentoDA;
		    }
	    }

         public bool Insert(DBHelper pDBHelper, DepartamentoBE pDepartamento)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@IDEMPRESA", pDepartamento.IdEmpresa == Constantes.INT_NULO ? (object)DBNull.Value : pDepartamento.IdEmpresa),
                new DBHelper.Parameters("@DESCRIPCION", string.IsNullOrEmpty(pDepartamento.Descripcion) ? (object)DBNull.Value : pDepartamento.Descripcion),
                new DBHelper.Parameters("@SIGLA", string.IsNullOrEmpty(pDepartamento.Sigla) ? (object)DBNull.Value : pDepartamento.Sigla),
                new DBHelper.Parameters("@USUARIOCREACION", string.IsNullOrEmpty(pDepartamento.UsuarioCreacion) ? (object)DBNull.Value : pDepartamento.UsuarioCreacion)
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("DepartamentoInsert"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

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

         public bool Update(DBHelper pDBHelper, DepartamentoBE pDepartamento)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@ID", pDepartamento.Id == Constantes.INT_NULO ? (object)DBNull.Value : pDepartamento.Id),
                new DBHelper.Parameters("@IDEMPRESA", pDepartamento.IdEmpresa == Constantes.INT_NULO ? (object)DBNull.Value : pDepartamento.IdEmpresa),
                new DBHelper.Parameters("@DESCRIPCION", string.IsNullOrEmpty(pDepartamento.Descripcion) ? (object)DBNull.Value : pDepartamento.Descripcion),
                new DBHelper.Parameters("@SIGLA", string.IsNullOrEmpty(pDepartamento.Sigla) ? (object)DBNull.Value : pDepartamento.Sigla),
                new DBHelper.Parameters("@USUARIOMODIFICACION", string.IsNullOrEmpty(pDepartamento.UsuarioModificacion) ? (object)DBNull.Value : pDepartamento.UsuarioModificacion)
                };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("DepartamentoUpdate"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

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

         public bool Delete(DBHelper pDBHelper, DepartamentoBE pDepartamento)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                 new DBHelper.Parameters("@ID", pDepartamento.Id == Constantes.INT_NULO ? (object)DBNull.Value : pDepartamento.Id),
                 new DBHelper.Parameters("@USUARIOMODIFICACION", string.IsNullOrEmpty(pDepartamento.UsuarioModificacion) ? (object)DBNull.Value : pDepartamento.UsuarioModificacion)
                };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("DepartamentoDelete"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

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

        public List<DepartamentoBE> GetAll(DBHelper pDBHelper)
        {
            List<DepartamentoBE> lst = null;
            DepartamentoBE obj = null;
            DBHelper.Parameters[] colParameters = null;
            lst = new List<DepartamentoBE>();
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("DepartamentoGetAll"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new DepartamentoBE();

                        obj.Id = int.Parse(dr["IdDepartamento"].ToString());
                        obj.IdEmpresa = int.Parse(dr["IdEmpresa"].ToString());
                        obj.Descripcion = dr["Descripcion"].ToString();
                        obj.Sigla = dr["Sigla"].ToString();
                        obj.UsuarioModificacion = dr["UsuarioModificacionConfidencial"].ToString() == "1" ?
                                                        ConfigurationManager.AppSettings["UsuarioAdministracion"].ToString() :
                                                        dr["UsuarioModificacion"].ToString();
                        obj.UsuarioCreacion = dr["UsuarioCreacionConfidencial"].ToString() == "1" ?
                                                        ConfigurationManager.AppSettings["UsuarioAdministracion"].ToString() :
                                                        dr["UsuarioCreacion"].ToString();

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

        public List<DepartamentoBE> GetByIdEmpresa(DBHelper pDBHelper,DepartamentoBE pDepartamento)
        {

            List<DepartamentoBE> lst = null;
            DepartamentoBE obj = null;
            DBHelper.Parameters[] colParameters = null;
            lst = new List<DepartamentoBE>();
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IDEMPRESA", pDepartamento.IdEmpresa == Constantes.INT_NULO ? (object)DBNull.Value : pDepartamento.IdEmpresa)
                };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("DepartamentoGetByIdEmpresa"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new DepartamentoBE();

                        obj.Id = int.Parse(dr["IdDepartamento"].ToString());
                        obj.Descripcion = dr["Descripcion"].ToString();
                        obj.Sigla = dr["Sigla"].ToString();

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

    }
}
