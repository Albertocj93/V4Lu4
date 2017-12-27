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
    public class AdjuntoDA
    {
        private static AdjuntoDA _AdjuntoDA = null;

        private AdjuntoDA() { }

        public static AdjuntoDA Instanse 
	    {
		    get {
                if (_AdjuntoDA != null) return _AdjuntoDA;
                _AdjuntoDA = new AdjuntoDA();
                return _AdjuntoDA;
		    }
	    }
        public void Insert(AdjuntoBE AdjuntoBE)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "AdjuntosInsert";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter Sp1 = new SqlParameter("@Filename", AdjuntoBE.NombreAdjunto);
            cmd.Parameters.Add(Sp1);
            SqlParameter Sp2 = new SqlParameter("@Adjunto", AdjuntoBE.AdjuntoFisico);
            cmd.Parameters.Add(Sp2);
            SqlParameter Sp3 = new SqlParameter("@Filesize", AdjuntoBE.AdjuntoFileSize);
            cmd.Parameters.Add(Sp3);
            SqlParameter Sp4 = new SqlParameter("@Type", AdjuntoBE.AdjuntoFileType);
            cmd.Parameters.Add(Sp4);
            SqlParameter Sp5 = new SqlParameter("@IdCarga", AdjuntoBE.IdCarga);
            cmd.Parameters.Add(Sp5);
            SqlParameter Sp6 = new SqlParameter("@IdAdjunto", AdjuntoBE.IdAdjunto);
            cmd.Parameters.Add(Sp6);

            cmd.ExecuteNonQuery();
        }
        public void DeleteByIdCargaFilename(AdjuntoBE AdjuntoBE)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "AdjuntoDeleteByIdCargaFilename";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter Sp1 = new SqlParameter("@IdCarga", AdjuntoBE.IdCarga);
            cmd.Parameters.Add(Sp1);
            SqlParameter Sp2 = new SqlParameter("@FileName", AdjuntoBE.NombreAdjunto);
            cmd.Parameters.Add(Sp2);

            cmd.ExecuteNonQuery();
        }
        public List<AdjuntoBE> GetByIdCarga(string IdCarga)
        {
            List<AdjuntoBE> oLista = new List<AdjuntoBE>();

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "AdjuntosGetByIdCarga";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sp1 = new SqlParameter("@IdCarga", IdCarga);
            cmd.Parameters.Add(sp1);

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    AdjuntoBE oAdjuntoBE = new AdjuntoBE();

                    oAdjuntoBE.NombreAdjunto = dataReader["Adjunto_filename"].ToString();
                    oAdjuntoBE.AdjuntoFisico = (byte[])dataReader["Adjunto"];
                    oAdjuntoBE.AdjuntoFileSize = Convert.ToInt32(dataReader["Adjunto_filesize"]);
                    oAdjuntoBE.AdjuntoFileType = dataReader["Adjunto_Type"].ToString();
                    oAdjuntoBE.IdCarga = dataReader["IdCarga"].ToString();
                    oAdjuntoBE.IdAdjunto = Convert.ToInt32(dataReader["IdAdjunto"].ToString());

                    oLista.Add(oAdjuntoBE);
                }
            }

            return oLista;
        }
        public AdjuntoBE GetByIdAdjunto(string IdAdjunto)
        {
            AdjuntoBE oAdjuntoBE = new AdjuntoBE();

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "AdjuntosGetByIdAdjunto";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sp1 = new SqlParameter("@IdAdjunto", IdAdjunto);
            cmd.Parameters.Add(sp1);

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    
                    oAdjuntoBE.NombreAdjunto = dataReader["Adjunto_filename"].ToString();
                    oAdjuntoBE.AdjuntoFisico = (byte[])dataReader["Adjunto"];
                    oAdjuntoBE.AdjuntoFileSize = Convert.ToInt32(dataReader["Adjunto_filesize"]);
                    oAdjuntoBE.AdjuntoFileType = dataReader["Adjunto_Type"].ToString();
                    oAdjuntoBE.IdCarga = dataReader["IdCarga"].ToString();
                    oAdjuntoBE.IdAdjunto = Convert.ToInt32(dataReader["IdAdjunto"].ToString());

                }
            }

            return oAdjuntoBE;
        }
        public bool AdjuntoTemporalInsert(DBHelper pDBHelper, AdjuntoBE AdjuntoBE)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@Nombre", string.IsNullOrEmpty(AdjuntoBE.NombreAdjunto) ? (object)DBNull.Value : AdjuntoBE.NombreAdjunto),
                new DBHelper.Parameters("@Fisico", AdjuntoBE.AdjuntoFisico),
                new DBHelper.Parameters("@Tamaño", string.IsNullOrEmpty(AdjuntoBE.AdjuntoFileSize.ToString()) ? (object)DBNull.Value : AdjuntoBE.AdjuntoFileSize.ToString()),
                new DBHelper.Parameters("@Tipo", string.IsNullOrEmpty(AdjuntoBE.AdjuntoFileType) ? (object)DBNull.Value : AdjuntoBE.AdjuntoFileType),
                new DBHelper.Parameters("@IdCarga", string.IsNullOrEmpty(AdjuntoBE.IdCarga) ? (object)DBNull.Value : AdjuntoBE.IdCarga),
                new DBHelper.Parameters("@UsuarioCreacion", string.IsNullOrEmpty(AdjuntoBE.UsuarioCreacion) ? (object)DBNull.Value : AdjuntoBE.UsuarioCreacion)
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("AdjuntoTemporalInsert"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

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
        public bool AdjuntoTemporalDeleteByIdCargaFilename(DBHelper pDBHelper, AdjuntoBE AdjuntoBE)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@Nombre", string.IsNullOrEmpty(AdjuntoBE.NombreAdjunto) ? (object)DBNull.Value : AdjuntoBE.NombreAdjunto),
                new DBHelper.Parameters("@IdCarga", string.IsNullOrEmpty(AdjuntoBE.IdCarga) ? (object)DBNull.Value : AdjuntoBE.IdCarga),
                new DBHelper.Parameters("@UsuarioModificacion", string.IsNullOrEmpty(AdjuntoBE.UsuarioModificacion) ? (object)DBNull.Value : AdjuntoBE.UsuarioModificacion)
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("AdjuntoTemporalDeleteByIdCargaFilename"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

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
        public bool AdjuntoTemporalDeleteByIdCarga(DBHelper pDBHelper, AdjuntoBE AdjuntoBE)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@IdCarga", string.IsNullOrEmpty(AdjuntoBE.IdCarga) ? (object)DBNull.Value : AdjuntoBE.IdCarga),
                new DBHelper.Parameters("@UsuarioModificacion", string.IsNullOrEmpty(AdjuntoBE.UsuarioModificacion) ? (object)DBNull.Value : AdjuntoBE.UsuarioModificacion)
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("AdjuntoTemporalDeleteByIdCarga"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

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
        public List<AdjuntoBE> AdjuntoTemporalGetByIdCarga(DBHelper pDBHelper,string IdCarga)
        {
            List<AdjuntoBE> lst = new List<AdjuntoBE>();
            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdCarga", string.IsNullOrEmpty(IdCarga) ? (object)DBNull.Value : IdCarga),
                };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("AdjuntoTemporalGetByIdCarga"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        AdjuntoBE AdjuntoBE = new BusinessEntities.AdjuntoBE();

                        AdjuntoBE.IdAdjunto = Convert.ToInt32(dr["IdAdjuntoTemporal"]);

                        lst.Add(AdjuntoBE);
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
        public AdjuntoBE GetByIdAdjunto(DBHelper pDBHelper,int IdAdjunto)
        {
            AdjuntoBE oAdjuntoBE = new AdjuntoBE();

            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdAdjunto", IdAdjunto==Constantes.INT_NULO ? (object)DBNull.Value : IdAdjunto),
                   
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dataReader = pDBHelper.ExecuteReader(pDBHelper.concatOwner("AdjuntoGetById"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    if (dataReader.Read())
                    {

                        oAdjuntoBE.NombreAdjunto = dataReader["Nombre"].ToString();
                        oAdjuntoBE.AdjuntoFisico = (byte[])dataReader["Fisico"];
                        oAdjuntoBE.AdjuntoFileSize = Convert.ToInt32(dataReader["Tamano"]);
                        oAdjuntoBE.AdjuntoFileType = dataReader["Tipo"].ToString();
                        oAdjuntoBE.IdAdjunto = Convert.ToInt32(dataReader["IdAdjunto"].ToString());
                        oAdjuntoBE.UsuarioCreacion = dataReader["UsuarioCreacion"].ToString();
                        oAdjuntoBE.FechaCreacion = Convert.ToDateTime(dataReader["FechaCreacion"]);
                        oAdjuntoBE.UsuarioModificacion = dataReader["UsuarioModificacion"].ToString();
                        oAdjuntoBE.FechaModificacion = Convert.ToDateTime(dataReader["FechaModificacion"]);
                    }
                }
                return oAdjuntoBE;
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
        public AdjuntoBE GetLastByUser(DBHelper pDBHelper, string CuentaRed)
        {
            AdjuntoBE oAdjuntoBE = new AdjuntoBE();

            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@CuentaRed", string.IsNullOrEmpty(CuentaRed) ? (object)DBNull.Value : CuentaRed),
                   
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dataReader = pDBHelper.ExecuteReader(pDBHelper.concatOwner("AdjuntoGetLastByUser"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    if (dataReader.Read())
                    {
                        oAdjuntoBE.IdAdjunto = Convert.ToInt32(dataReader["IdAdjunto"].ToString());
                        oAdjuntoBE.NombreAdjunto = dataReader["Nombre"].ToString();
                        oAdjuntoBE.AdjuntoFileSize = Convert.ToInt32(dataReader["Tamano"]);
                        oAdjuntoBE.AdjuntoFileType = dataReader["Tipo"].ToString();
                        oAdjuntoBE.UsuarioCreacion = dataReader["UsuarioCreacion"].ToString();
                        oAdjuntoBE.FechaCreacion = Convert.ToDateTime(dataReader["FechaCreacion"]);
                        oAdjuntoBE.UsuarioModificacion = dataReader["UsuarioModificacion"].ToString();
                        oAdjuntoBE.FechaModificacion = Convert.ToDateTime(dataReader["FechaModificacion"]);
                    }
                }
                return oAdjuntoBE;
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