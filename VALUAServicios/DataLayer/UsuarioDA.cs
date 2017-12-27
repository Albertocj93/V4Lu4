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
    public class UsuarioDA
    {
        private static UsuarioDA _UsuarioDA = null;

        private UsuarioDA() { }

        public static UsuarioDA Instanse 
	    {
		    get {
                if (_UsuarioDA != null) return _UsuarioDA;
                _UsuarioDA = new UsuarioDA();
                return _UsuarioDA;
		    }
	    }

        public UsuarioBE GetByAccount(DBHelper pDBHelper, string cuenta)
        {
            UsuarioBE Respuesta = new UsuarioBE();

            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    
                new DBHelper.Parameters("@Cuenta", string.IsNullOrEmpty(cuenta) ? (object)DBNull.Value : cuenta)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("UsuarioGetByCuenta"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    if (dr.Read())
                    {
                        Respuesta.Id = Convert.ToInt32(dr["IdUsuario"].ToString());
                        Respuesta.NombreCompleto = dr["NombreCompleto"].ToString();
                        Respuesta.CuentaUsuario = dr["CuentaRed"].ToString();
                        Respuesta.IdEmpresa = Convert.ToInt32(dr["IdEmpresa"].ToString());
                        Respuesta.IdVista = Convert.ToInt32(dr["IdVista"].ToString());
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
        public UsuarioBE GetVistaByAccount(DBHelper pDBHelper, string cuenta)
        {
            UsuarioBE Respuesta = new UsuarioBE();

            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    
                new DBHelper.Parameters("@Cuenta", string.IsNullOrEmpty(cuenta) ? (object)DBNull.Value : cuenta)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("GetVistaByAccount"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    if (dr.Read())
                    {
                        Respuesta.Id = Convert.ToInt32(dr["IdUsuario"].ToString());
                        Respuesta.NombreCompleto = dr["NombreCompleto"].ToString();
                        Respuesta.CuentaUsuario = dr["CuentaRed"].ToString();
                        Respuesta.IdVista = Convert.ToInt32(dr["IdVista"].ToString());
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
        public UsuarioBE GetUserRansaByCuentaRed(DBHelper pDBHelper, string cuenta)
        {
            UsuarioBE Respuesta = new UsuarioBE();

            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    
                new DBHelper.Parameters("@CuentaRed", string.IsNullOrEmpty(cuenta) ? (object)DBNull.Value : cuenta)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("GetUserByCuentaRed"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    if (dr.Read())
                    {
                        Respuesta.NombreCompleto = dr["nombresCompletos"].ToString();
                        Respuesta.CuentaUsuario = dr["cuentaRed"].ToString();
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
        public UsuarioBE GetPermisosByPuesto(DBHelper pDBHelper, string cuenta,int IdPuesto)
        {
            UsuarioBE Respuesta = new UsuarioBE();

            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdPuesto", IdPuesto==Constantes.INT_NULO ? (object)DBNull.Value : IdPuesto), 
                    new DBHelper.Parameters("@CuentaRed", string.IsNullOrEmpty(cuenta) ? (object)DBNull.Value : cuenta)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("UsuarioGetPermisosByPuesto"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    if (dr.Read())
                    {
                        Respuesta.Id = Convert.ToInt32(dr["IdUsuario"].ToString());
                        Respuesta.CuentaUsuario = dr["CuentaRed"].ToString();
                        Respuesta.NombreCompleto = dr["NombreCompleto"].ToString();
                        Respuesta.IdPerfil = Convert.ToInt32(dr["IdPerfil"].ToString());
                        Respuesta.Perfil = dr["Perfil"].ToString();
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
        public UsuarioBE UsuarioGetPerfil(DBHelper pDBHelper, string cuenta)
        {
            UsuarioBE Respuesta = new UsuarioBE();

            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@CuentaRed", string.IsNullOrEmpty(cuenta) ? (object)DBNull.Value : cuenta)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("UsuarioGetPerfil"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    if (dr.Read())
                    {
                        Respuesta.Id = Convert.ToInt32(dr["IdUsuario"].ToString());
                        Respuesta.IdPerfil = Convert.ToInt32(dr["IdPerfil"].ToString());
                        Respuesta.Perfil = dr["Perfil"].ToString();
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
        public List<UsuarioBE> GetAll(DBHelper pDBHelper)
        {
            List<UsuarioBE> lst = null;
            UsuarioBE obj = null;
            DBHelper.Parameters[] colParameters = null;
            lst = new List<UsuarioBE>();
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("UsuarioGetAll"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new UsuarioBE();

                        obj.Id = int.Parse(dr["IdUsuario"].ToString());
                        obj.CuentaUsuario = dr["CuentaRed"].ToString();
                        obj.NombreCompleto = dr["NombreCompleto"].ToString();
                        obj.Activo = Convert.ToInt32(Convert.ToBoolean(dr["Activo"].ToString()));
                        obj.IdEmpresa = int.Parse(dr["idEmpresa"].ToString());
                        obj.Email = dr["Email"].ToString();
                        obj.Confidencial = int.Parse(dr["Confidencial"].ToString());

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
        public List<UsuarioBE> GetPermisosByIdUsuario(DBHelper pDBHelper, UsuarioBE oUsuario)
        {
            List<UsuarioBE> lst = null;
            UsuarioBE obj = null;
            DBHelper.Parameters[] colParameters = null;
            lst = new List<UsuarioBE>();
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@IdUsuario", oUsuario.Id == Constantes.INT_NULO ? (object)DBNull.Value : oUsuario.Id)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("UsuarioGetPermisosByIdUsuario"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new UsuarioBE();

                        obj.Id = int.Parse(dr["IdUsuario"].ToString());
                        obj.IdEmpresa = int.Parse(dr["IdEmpresa"].ToString());
                        obj.IdPerfil = int.Parse(dr["IdPerfil"].ToString());

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
        
        public bool Insert(DBHelper pDBHelper, UsuarioBE oUsuario)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@CuentaRed", string.IsNullOrEmpty(oUsuario.CuentaUsuario) ? (object)DBNull.Value : oUsuario.CuentaUsuario),
                new DBHelper.Parameters("@NombreCompleto", string.IsNullOrEmpty(oUsuario.NombreCompleto) ? (object)DBNull.Value : oUsuario.NombreCompleto),
                new DBHelper.Parameters("@Activo", oUsuario.Activo == Constantes.INT_NULO ? false : Convert.ToBoolean(oUsuario.Activo)),
                new DBHelper.Parameters("@IdEmpresa", oUsuario.IdEmpresa == Constantes.INT_NULO ? (object)DBNull.Value : oUsuario.IdEmpresa),
                new DBHelper.Parameters("@Email", string.IsNullOrEmpty(oUsuario.Email) ? (object)DBNull.Value : oUsuario.Email),
                new DBHelper.Parameters("@Confidencial", oUsuario.Confidencial == Constantes.INT_NULO ? (object)DBNull.Value : oUsuario.Confidencial)
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("UsuarioInsert"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

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

        public bool Update(DBHelper pDBHelper, UsuarioBE oUsuario)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@IdUsuario", oUsuario.Id == Constantes.INT_NULO ? (object)DBNull.Value : oUsuario.Id),
                new DBHelper.Parameters("@CuentaRed", string.IsNullOrEmpty(oUsuario.CuentaUsuario) ? (object)DBNull.Value : oUsuario.CuentaUsuario),
                new DBHelper.Parameters("@NombreCompleto", string.IsNullOrEmpty(oUsuario.NombreCompleto) ? (object)DBNull.Value : oUsuario.NombreCompleto),
                new DBHelper.Parameters("@Activo", oUsuario.Activo == Constantes.INT_NULO ? false : Convert.ToBoolean(oUsuario.Activo)),
                new DBHelper.Parameters("@IdEmpresa", oUsuario.IdEmpresa == Constantes.INT_NULO ? (object)DBNull.Value : oUsuario.IdEmpresa),
                new DBHelper.Parameters("@Email", string.IsNullOrEmpty(oUsuario.Email) ? (object)DBNull.Value : oUsuario.Email),
                new DBHelper.Parameters("@Confidencial", oUsuario.Confidencial == Constantes.INT_NULO ? (object)DBNull.Value : oUsuario.Confidencial)
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("UsuarioUpdate"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

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
        public bool AsignarAdministrador(DBHelper pDBHelper, UsuarioBE oUsuario)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@IdUsuario", oUsuario.Id == Constantes.INT_NULO ? (object)DBNull.Value : oUsuario.Id)
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("UsuarioAsignarAdministrador"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

                return true;
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

        public bool EliminarTodosPerfiles(DBHelper pDBHelper, UsuarioBE oUsuario)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@IdUsuario", oUsuario.Id == Constantes.INT_NULO ? (object)DBNull.Value : oUsuario.Id)
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("UsuarioEliminarTodosPerfiles"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

                return true;
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
        public bool UsuarioPerfilEmpresaInsert(DBHelper pDBHelper, UsuarioBE oUsuario)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@IdUsuario", oUsuario.Id == Constantes.INT_NULO ? (object)DBNull.Value : oUsuario.Id),
                new DBHelper.Parameters("@IdEmpresa", oUsuario.IdEmpresa == Constantes.INT_NULO ? (object)DBNull.Value : oUsuario.IdEmpresa),
                new DBHelper.Parameters("@IdPerfil", oUsuario.IdPerfil == Constantes.INT_NULO ? (object)DBNull.Value : oUsuario.IdPerfil)
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("UsuarioPerfilEmpresaInsert"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

                return true;
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
        public bool UsuarioPerfilEmpresaDelete(DBHelper pDBHelper, UsuarioBE oUsuario)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@IdUsuario", oUsuario.Id == Constantes.INT_NULO ? (object)DBNull.Value : oUsuario.Id),
                new DBHelper.Parameters("@IdEmpresa", oUsuario.IdEmpresa == Constantes.INT_NULO ? (object)DBNull.Value : oUsuario.IdEmpresa),
                new DBHelper.Parameters("@IdPerfil", oUsuario.IdPerfil == Constantes.INT_NULO ? (object)DBNull.Value : oUsuario.IdPerfil)
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("UsuarioPerfilEmpresaDelete"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

                return true;
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
