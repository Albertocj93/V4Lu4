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
    public class AreaDA
    {
        public List<AreaBE> GetAll()
        {
            List<AreaBE> oLista = new List<AreaBE>();

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "AreaGetAll";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    AreaBE oArea = new AreaBE();

                    oArea.Id = Convert.ToInt32(dataReader["IdArea"].ToString());
                   

                    oLista.Add(oArea);
                }
            }

            return oLista;
        }

        public List<AreaBE> GetByIdDepartamento(int IdDepartamento)
        {
            List<AreaBE> oLista = new List<AreaBE>();

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "AreaGetByIdDepartamento";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;
            
            SqlParameter Sp1 = new SqlParameter("@IdDepartamento",IdDepartamento);
            cmd.Parameters.Add(Sp1);

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    AreaBE oArea = new AreaBE();

                    oArea.Id = Convert.ToInt32(dataReader["IdArea"].ToString());
                    oArea.Descripcion = dataReader["Descripcion"].ToString();

                    oLista.Add(oArea);
                }
            }

            return oLista;
        }
        public int GetIdByDescAreDepEmp(DBHelper pDBHelper,string DescArea, string DescEmpresa, string DescDepartamento)
        {
            int Respuesta = -1;

            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    
                new DBHelper.Parameters("@DescArea", string.IsNullOrEmpty(DescArea)  ? (object)DBNull.Value : DescArea),
                new DBHelper.Parameters("@DescEmpresa", string.IsNullOrEmpty(DescEmpresa)  ? (object)DBNull.Value : DescEmpresa),
                new DBHelper.Parameters("@DescDepartamento", string.IsNullOrEmpty(DescDepartamento)  ? (object)DBNull.Value : DescDepartamento),
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("AreaGetIdByDescAreDepEmp"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {

                    // Leyendo reader 
                    if (dr.Read())
                    {
                        Respuesta = Convert.ToInt32(dr["IdArea"].ToString());
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
        public AreaBE GetByIdArea(int IdArea)
        {
            AreaBE oArea = new AreaBE();

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "AreaGetByIdArea";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter Sp1 = new SqlParameter("@IdArea", IdArea);
            cmd.Parameters.Add(Sp1);

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    oArea.Id = Convert.ToInt32(dataReader["IdArea"].ToString());
                   
                }
            }

            return oArea;
        }




        private static AreaDA _AreaDA = null;

        private AreaDA() { }

        public static AreaDA Instanse 
	    {
		    get {
                if (_AreaDA != null) return _AreaDA;
                _AreaDA = new AreaDA();
                return _AreaDA;
		    }
	    }

         public bool Insert(DBHelper pDBHelper, AreaBE pArea)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@IDDEPARTAMENTO", pArea.IdDepartamento == Constantes.INT_NULO ? (object)DBNull.Value : pArea.IdDepartamento),
                new DBHelper.Parameters("@DESCRIPCION", string.IsNullOrEmpty(pArea.Descripcion) ? (object)DBNull.Value : pArea.Descripcion),
                new DBHelper.Parameters("@SIGLA", string.IsNullOrEmpty(pArea.Sigla) ? (object)DBNull.Value : pArea.Sigla),
                new DBHelper.Parameters("@USUARIOCREACION", string.IsNullOrEmpty(pArea.UsuarioCreacion) ? (object)DBNull.Value : pArea.UsuarioCreacion)
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("AreaInsert"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

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

         public bool Update(DBHelper pDBHelper, AreaBE pArea)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@ID", pArea.Id == Constantes.INT_NULO ? (object)DBNull.Value : pArea.Id),
                    new DBHelper.Parameters("@IDDEPARTAMENTO", pArea.IdDepartamento == Constantes.INT_NULO ? (object)DBNull.Value : pArea.IdDepartamento),
                    new DBHelper.Parameters("@DESCRIPCION", string.IsNullOrEmpty(pArea.Descripcion) ? (object)DBNull.Value : pArea.Descripcion),
                    new DBHelper.Parameters("@SIGLA", string.IsNullOrEmpty(pArea.Sigla) ? (object)DBNull.Value : pArea.Sigla),
                    new DBHelper.Parameters("@USUARIOMODIFICACION", string.IsNullOrEmpty(pArea.UsuarioModificacion) ? (object)DBNull.Value : pArea.UsuarioModificacion)
                };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("AreaUpdate"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

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

         public bool Delete(DBHelper pDBHelper, AreaBE pArea)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@ID", pArea.Id == Constantes.INT_NULO ? (object)DBNull.Value : pArea.Id),
                 new DBHelper.Parameters("@USUARIOMODIFICACION", string.IsNullOrEmpty(pArea.UsuarioModificacion) ? (object)DBNull.Value : pArea.UsuarioModificacion)
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("AreaDelete"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

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

        public List<AreaBE> GetAll(DBHelper pDBHelper)
        {
            List<AreaBE> lst = null;
            AreaBE obj = null;
            DBHelper.Parameters[] colParameters = null;
            lst = new List<AreaBE>();
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("AreaGetAll"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new AreaBE();

                        obj.Id = int.Parse(dr["IdArea"].ToString());
                        obj.IdDepartamento = int.Parse(dr["IdDepartamento"].ToString());
                        obj.IdEmpresa = int.Parse(dr["IdEmpresa"].ToString());
                        obj.Descripcion = dr["Descripcion"].ToString();
                        obj.Departamento = dr["Departamento"].ToString();
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

    }
}
