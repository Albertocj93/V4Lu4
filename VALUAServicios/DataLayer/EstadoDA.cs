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
    public class EstadoDA
    {
        private static EstadoDA _EstadoDA = null;

        private EstadoDA() { }

        public static EstadoDA Instanse 
	    {
		    get {
                if (_EstadoDA != null) return _EstadoDA;
                _EstadoDA = new EstadoDA();
                return _EstadoDA;
		    }
	    }
        public List<EstadoBE> GetAll(DBHelper pDBHelper)
        {
            List<EstadoBE> oLista = new List<EstadoBE>();

            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dataReader = pDBHelper.ExecuteReader(pDBHelper.concatOwner("EstadosGetAll"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dataReader.Read())
                    {
                        EstadoBE oEstado = new EstadoBE();

                        oEstado.Id = Convert.ToInt32(dataReader["IdEstado"]);
                        oEstado.Descripcion = dataReader["Descripcion"].ToString();

                        oLista.Add(oEstado);
                    }
                }
                return oLista;
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

        public List<EstadoBE> GetByIdEstadoInicial(DBHelper pDBHelper,int IdEstadoInicial)
        {
            List<EstadoBE> oLista = new List<EstadoBE>();
            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdEstado", IdEstadoInicial == Constantes.INT_NULO ? (object)DBNull.Value : IdEstadoInicial),
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dataReader = pDBHelper.ExecuteReader(pDBHelper.concatOwner("EstadosGetByIdEstadoInicial"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dataReader.Read())
                    {
                        EstadoBE oEstado = new EstadoBE();

                        oEstado.Id = Convert.ToInt32(dataReader["IdEstado"]);
                        oEstado.Descripcion = dataReader["Descripcion"].ToString();

                        oLista.Add(oEstado);
                    }
                }
                return oLista;
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

        public List<EstadoBE> GetSiguienteByIdPuesto(DBHelper pDBHelper,int IdPuesto)
        {

            List<EstadoBE> oLista = new List<EstadoBE>();
            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdPuesto", IdPuesto == Constantes.INT_NULO ? (object)DBNull.Value : IdPuesto),
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dataReader = pDBHelper.ExecuteReader(pDBHelper.concatOwner("EstadosGetSiguienteByIdPuesto"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dataReader.Read())
                    {
                        EstadoBE oEstado = new EstadoBE();

                        oEstado.Id = Convert.ToInt32(dataReader["IdEstado"]);
                        oEstado.Descripcion = dataReader["descripcion"].ToString();

                        oLista.Add(oEstado);
                    }
                }
                return oLista;
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
        
        public int GetIdByDesc(DBHelper pDBHelper,string descripcion)
        {
            int Respuesta=-1 ;
            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@descripcion", string.IsNullOrEmpty(descripcion) ? (object)DBNull.Value : descripcion)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dataReader = pDBHelper.ExecuteReader(pDBHelper.concatOwner("EstadosGetIdByDesc"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    if (dataReader.Read())
                    {
                        EstadoBE oEstado = new EstadoBE();

                        Respuesta = Convert.ToInt32(dataReader["IdEstado"]);
                        
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

        public EstadoBE GetByIdEstado(DBHelper pDBHelper,int IdEstado)
        {
            EstadoBE EstadoBE = new EstadoBE();
            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdEstado", IdEstado == Constantes.INT_NULO ? (object)DBNull.Value : IdEstado),
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dataReader = pDBHelper.ExecuteReader(pDBHelper.concatOwner("EstadosGetByIdEstado"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    if (dataReader.Read())
                    {
                        EstadoBE.Id = Convert.ToInt32(dataReader["IdEstado"]);
                        EstadoBE.Descripcion = dataReader["Descripcion"].ToString();

                    }
                }
                return EstadoBE;
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
