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
    public class SubAreaDA
    {
        public List<SubAreaBE> GetAll()
        {
            List<SubAreaBE> oLista = new List<SubAreaBE>();
            
            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "SubAreaGetAll";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    SubAreaBE oSubArea = new SubAreaBE();

                    oSubArea.Id = Convert.ToInt32(dataReader["IdSubArea"].ToString());
                    oSubArea.Descripcion = dataReader["Descripcion"].ToString();

                    oLista.Add(oSubArea);
                }
            }

            return oLista;
        }

        public List<SubAreaBE> GetByIdArea(int IdArea)
        {
            List<SubAreaBE> oLista = new List<SubAreaBE>();

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "SubAreaGetByIdArea";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter Sp1 = new SqlParameter("@IdArea",IdArea);
            cmd.Parameters.Add(Sp1);

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    SubAreaBE oSubArea = new SubAreaBE();

                    oSubArea.Id = Convert.ToInt32(dataReader["IdSubArea"].ToString());
                    oSubArea.Descripcion = dataReader["Descripcion"].ToString();

                    oLista.Add(oSubArea);
                }
            }

            return oLista;
        }
        public int GetIdByDescSArAreDepEmp(DBHelper pDBHelper,string SubArea, string DescArea, string DescEmpresa, string DescDepartamento)
        {
            int Respuesta = -1;

            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@DescSubArea", string.IsNullOrEmpty(DescArea)  ? (object)DBNull.Value : SubArea),
                new DBHelper.Parameters("@DescArea", string.IsNullOrEmpty(DescArea)  ? (object)DBNull.Value : DescArea),
                new DBHelper.Parameters("@DescEmpresa", string.IsNullOrEmpty(DescEmpresa)  ? (object)DBNull.Value : DescEmpresa),
                new DBHelper.Parameters("@DescDepartamento", string.IsNullOrEmpty(DescDepartamento)  ? (object)DBNull.Value : DescDepartamento),
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("SubAreaGetIdByDescSArAreDepEmp"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {

                    // Leyendo reader 
                    if (dr.Read())
                    {
                        Respuesta = Convert.ToInt32(dr["IdSubArea"].ToString());
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
        public SubAreaBE GetByIdSubArea(string IdSubArea)
        {
            SubAreaBE oSubArea = new SubAreaBE();

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "SubAreaGetByIdSubArea";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter Sp1 = new SqlParameter("@IdSubArea", IdSubArea);
            cmd.Parameters.Add(Sp1);

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    oSubArea.Id = Convert.ToInt32(dataReader["IdSubArea"].ToString());
                    oSubArea.Descripcion = dataReader["Descripcion"].ToString();
                }
            }

            return oSubArea;
        }



        private static SubAreaDA _SubAreaDA = null;

        private SubAreaDA() { }

        public static SubAreaDA Instanse 
	    {
		    get {
                if (_SubAreaDA != null) return _SubAreaDA;
                _SubAreaDA = new SubAreaDA();
                return _SubAreaDA;
		    }
	    }

         public bool Insert(DBHelper pDBHelper, SubAreaBE pSubArea)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@IDAREA", pSubArea.IdArea == Constantes.INT_NULO ? (object)DBNull.Value : pSubArea.IdArea),
                new DBHelper.Parameters("@DESCRIPCION", string.IsNullOrEmpty(pSubArea.Descripcion) ? (object)DBNull.Value : pSubArea.Descripcion),
                new DBHelper.Parameters("@SIGLA", string.IsNullOrEmpty(pSubArea.Sigla) ? (object)DBNull.Value : pSubArea.Sigla),
                new DBHelper.Parameters("@USUARIOCREACION", string.IsNullOrEmpty(pSubArea.UsuarioCreacion) ? (object)DBNull.Value : pSubArea.UsuarioCreacion)
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("SubAreaInsert"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

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

         public bool Update(DBHelper pDBHelper, SubAreaBE pSubArea)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@ID", pSubArea.Id == Constantes.INT_NULO ? (object)DBNull.Value : pSubArea.Id),
                    new DBHelper.Parameters("@IDAREA", pSubArea.IdArea == Constantes.INT_NULO ? (object)DBNull.Value : pSubArea.IdArea),
                    new DBHelper.Parameters("@DESCRIPCION", string.IsNullOrEmpty(pSubArea.Descripcion) ? (object)DBNull.Value : pSubArea.Descripcion),
                    new DBHelper.Parameters("@SIGLA", string.IsNullOrEmpty(pSubArea.Sigla) ? (object)DBNull.Value : pSubArea.Sigla),
                    new DBHelper.Parameters("@USUARIOMODIFICACION", string.IsNullOrEmpty(pSubArea.UsuarioModificacion) ? (object)DBNull.Value : pSubArea.UsuarioModificacion)
                };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("SubAreaUpdate"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

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

         public bool Delete(DBHelper pDBHelper, SubAreaBE pSubArea)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@ID", pSubArea.Id == Constantes.INT_NULO ? (object)DBNull.Value : pSubArea.Id),
                 new DBHelper.Parameters("@USUARIOMODIFICACION", string.IsNullOrEmpty(pSubArea.UsuarioModificacion) ? (object)DBNull.Value : pSubArea.UsuarioModificacion)
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("SubAreaDelete"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

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

         public List<SubAreaBE> GetAll(DBHelper pDBHelper)
        {
            List<SubAreaBE> lst = null;
            SubAreaBE obj = null;
            DBHelper.Parameters[] colParameters = null;
            lst = new List<SubAreaBE>();
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("SubAreaGetAll"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new SubAreaBE();

                        obj.Id = int.Parse(dr["IdSubArea"].ToString());
                        obj.Descripcion = dr["Descripcion"].ToString();
                        obj.Sigla = dr["Sigla"].ToString();
                        obj.IdArea = int.Parse(dr["IdArea"].ToString());
                        obj.Area = dr["Area"].ToString();
                        obj.IdDepartamento = int.Parse(dr["IdDepartamento"].ToString());
                        obj.Departamento = dr["Departamento"].ToString();
                        obj.IdEmpresa = int.Parse(dr["IdEmpresa"].ToString());
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





    }
}
