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
    public class VisitaDA
    {
        private static VisitaDA _VisitaDA = null;

        private VisitaDA() { }

        public static VisitaDA Instanse 
	    {
		    get {
                if (_VisitaDA != null) return _VisitaDA;
                _VisitaDA = new VisitaDA();
                return _VisitaDA;
		    }
	    }


        public bool Insert(DBHelper pDBHelper, VisitaBE pVisita)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@USUARIOCREACION", string.IsNullOrEmpty(pVisita.UsuarioCreacion) ? (object)DBNull.Value : pVisita.UsuarioCreacion),
                new DBHelper.Parameters("@IDEMPRESA", pVisita.IdEmpresa == Constantes.INT_NULO ? (object)DBNull.Value : pVisita.IdEmpresa)
                };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("VisitaInsert"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

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
        public List<ReporteChildBE> ReporteUsoGetEmpresa(DBHelper pDBHelper)
        {
            List<ReporteChildBE> lst = null;
            ReporteChildBE obj = null;

            DBHelper.Parameters[] colParameters = null;
            lst = new List<ReporteChildBE>();

            try
            {
                colParameters = new DBHelper.Parameters[] {
                };
                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("ReporteUsoGetEmpresa"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new ReporteChildBE();

                        obj.task = dr["Descripcion"].ToString();
                        
                        StringBuilder sb = new StringBuilder();
                        sb.Append(dr["horas"].ToString().PadLeft(2,'0'));
                        sb.Append(":");
                        sb.Append(dr["minutos"].ToString().PadLeft(2,'0'));

                        obj.duration = sb.ToString();
                        obj.idEmpresa = int.Parse(dr["idEmpresa"].ToString());

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
                //colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }
        }
        public List<ReporteChildBE> ReporteUsoGetAnho(DBHelper pDBHelper, ReporteChildBE pReporteChild)
        {
            List<ReporteChildBE> oLista = new List<ReporteChildBE>();
            ReporteChildBE obj = null;
            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdEmpresa", pReporteChild.idEmpresa == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.idEmpresa),
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("ReporteUsoGetAnho"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new ReporteChildBE();

                        StringBuilder sb = new StringBuilder();
                        sb.Append(dr["horas"].ToString().PadLeft(2, '0'));
                        sb.Append(":");
                        sb.Append(dr["minutos"].ToString().PadLeft(2, '0'));

                        obj.duration = sb.ToString();
                        obj.idEmpresa = int.Parse(dr["idEmpresa"].ToString());
                        obj.anho = int.Parse(dr["anho"].ToString());

                        oLista.Add(obj);
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
        public List<ReporteChildBE> ReporteUsoGetSemestre(DBHelper pDBHelper, ReporteChildBE pReporteChild)
        {
            List<ReporteChildBE> oLista = new List<ReporteChildBE>();
            ReporteChildBE obj = null;
            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdEmpresa", pReporteChild.idEmpresa == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.idEmpresa),
                    new DBHelper.Parameters("@Anho", pReporteChild.anho == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.anho)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("ReporteUsoGetSemestre"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new ReporteChildBE();

                        StringBuilder sb = new StringBuilder();
                        sb.Append(dr["horas"].ToString().PadLeft(2, '0'));
                        sb.Append(":");
                        sb.Append(dr["minutos"].ToString().PadLeft(2, '0'));

                        obj.duration = sb.ToString();
                        obj.idEmpresa = int.Parse(dr["idEmpresa"].ToString());
                        obj.anho = int.Parse(dr["anho"].ToString());
                        obj.semestre = int.Parse(dr["semestre"].ToString());

                        oLista.Add(obj);
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
        public List<ReporteChildBE> ReporteUsoGetTrimestre(DBHelper pDBHelper, ReporteChildBE pReporteChild)
        {
            List<ReporteChildBE> oLista = new List<ReporteChildBE>();
            ReporteChildBE obj = null;
            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdEmpresa", pReporteChild.idEmpresa == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.idEmpresa),
                    new DBHelper.Parameters("@Anho", pReporteChild.anho == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.anho),
                    new DBHelper.Parameters("@semestre", pReporteChild.semestre == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.semestre)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("ReporteUsoGetTrimestre"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new ReporteChildBE();

                        StringBuilder sb = new StringBuilder();
                        sb.Append(dr["horas"].ToString().PadLeft(2, '0'));
                        sb.Append(":");
                        sb.Append(dr["minutos"].ToString().PadLeft(2, '0'));

                        obj.duration = sb.ToString();
                        obj.idEmpresa = int.Parse(dr["idEmpresa"].ToString());
                        obj.anho = int.Parse(dr["anho"].ToString());
                        obj.semestre = int.Parse(dr["semestre"].ToString());
                        obj.trimestre = int.Parse(dr["trimestre"].ToString());

                        oLista.Add(obj);
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
        public List<ReporteChildBE> ReporteUsoGetMes(DBHelper pDBHelper, ReporteChildBE pReporteChild)
        {
            List<ReporteChildBE> oLista = new List<ReporteChildBE>();
            ReporteChildBE obj = null;
            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdEmpresa", pReporteChild.idEmpresa == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.idEmpresa),
                    new DBHelper.Parameters("@Anho", pReporteChild.anho == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.anho),
                    new DBHelper.Parameters("@semestre", pReporteChild.semestre == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.semestre),
                    new DBHelper.Parameters("@trimestre", pReporteChild.trimestre == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.trimestre)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("ReporteUsoGetMes"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new ReporteChildBE();

                        StringBuilder sb = new StringBuilder();
                        sb.Append(dr["horas"].ToString().PadLeft(2, '0'));
                        sb.Append(":");
                        sb.Append(dr["minutos"].ToString().PadLeft(2, '0'));

                        obj.duration = sb.ToString();
                        obj.idEmpresa = int.Parse(dr["idEmpresa"].ToString());
                        obj.anho = int.Parse(dr["anho"].ToString());
                        obj.semestre = int.Parse(dr["semestre"].ToString());
                        obj.trimestre = int.Parse(dr["trimestre"].ToString());
                        obj.mes = int.Parse(dr["mes"].ToString());

                        oLista.Add(obj);
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
        public List<ReporteChildBE> ReporteUsoGetDia(DBHelper pDBHelper, ReporteChildBE pReporteChild)
        {
            List<ReporteChildBE> oLista = new List<ReporteChildBE>();
            ReporteChildBE obj = null;
            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdEmpresa", pReporteChild.idEmpresa == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.idEmpresa),
                    new DBHelper.Parameters("@Anho", pReporteChild.anho == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.anho),
                    new DBHelper.Parameters("@semestre", pReporteChild.semestre == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.semestre),
                    new DBHelper.Parameters("@trimestre", pReporteChild.trimestre == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.trimestre),
                    new DBHelper.Parameters("@mes", pReporteChild.mes == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.mes)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("ReporteUsoGetDia"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new ReporteChildBE();

                        StringBuilder sb = new StringBuilder();
                        sb.Append(dr["horas"].ToString().PadLeft(2, '0'));
                        sb.Append(":");
                        sb.Append(dr["minutos"].ToString().PadLeft(2, '0'));

                        obj.duration = sb.ToString();
                        obj.idEmpresa = int.Parse(dr["idEmpresa"].ToString());
                        obj.anho = int.Parse(dr["anho"].ToString());
                        obj.semestre = int.Parse(dr["semestre"].ToString());
                        obj.trimestre = int.Parse(dr["trimestre"].ToString());
                        obj.mes = int.Parse(dr["mes"].ToString());
                        obj.dia = int.Parse(dr["dia"].ToString());

                        oLista.Add(obj);
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
        public List<ReporteChildBE> ReporteUsoUsuGetUsuarios(DBHelper pDBHelper, int IdEmpresa)
        {
            List<ReporteChildBE> oLista = new List<ReporteChildBE>();
            ReporteChildBE obj = null;
            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdEmpresa", IdEmpresa == Constantes.INT_NULO ? (object)DBNull.Value : IdEmpresa)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("ReporteUsoUsuGetUsuarios"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new ReporteChildBE();

                        StringBuilder sb = new StringBuilder();
                        sb.Append(dr["horas"].ToString().PadLeft(2, '0'));
                        sb.Append(":");
                        sb.Append(dr["minutos"].ToString().PadLeft(2, '0'));

                        obj.duration = sb.ToString();
                        obj.idEmpresa = int.Parse(dr["idEmpresa"].ToString());
                        obj.idUsuario = int.Parse(dr["idUsuario"].ToString());
                        obj.NombreUsuario = dr["NombreCompleto"].ToString();

                        oLista.Add(obj);
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
        public List<ReporteChildBE> ReporteUsoUsuGetAnho(DBHelper pDBHelper,ReporteChildBE pReporteChild)
        {
            List<ReporteChildBE> oLista = new List<ReporteChildBE>();
            ReporteChildBE obj = null;
            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdEmpresa", pReporteChild.idEmpresa == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.idEmpresa),
                    new DBHelper.Parameters("@IdUsuario", pReporteChild.idUsuario == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.idUsuario)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("ReporteUsoUsuGetAnho"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new ReporteChildBE();

                        StringBuilder sb = new StringBuilder();
                        sb.Append(dr["horas"].ToString().PadLeft(2, '0'));
                        sb.Append(":");
                        sb.Append(dr["minutos"].ToString().PadLeft(2, '0'));

                        obj.duration = sb.ToString();
                        obj.idEmpresa = int.Parse(dr["idEmpresa"].ToString());
                        obj.idUsuario = int.Parse(dr["idUsuario"].ToString());
                        obj.NombreUsuario = dr["NombreCompleto"].ToString();
                        obj.anho = int.Parse(dr["anho"].ToString());

                        oLista.Add(obj);
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
        public List<ReporteChildBE> ReporteUsoUsuGetSemestre(DBHelper pDBHelper, ReporteChildBE pReporteChild)
        {
            List<ReporteChildBE> oLista = new List<ReporteChildBE>();
            ReporteChildBE obj = null;
            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdEmpresa", pReporteChild.idEmpresa == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.idEmpresa),
                    new DBHelper.Parameters("@IdUsuario", pReporteChild.idUsuario == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.idUsuario),
                    new DBHelper.Parameters("@Anho", pReporteChild.anho == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.anho)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("ReporteUsoUsuGetSemestre"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new ReporteChildBE();

                        StringBuilder sb = new StringBuilder();
                        sb.Append(dr["horas"].ToString().PadLeft(2, '0'));
                        sb.Append(":");
                        sb.Append(dr["minutos"].ToString().PadLeft(2, '0'));

                        obj.duration = sb.ToString();
                        obj.idEmpresa = int.Parse(dr["idEmpresa"].ToString());
                        obj.idUsuario = int.Parse(dr["idUsuario"].ToString());
                        obj.NombreUsuario = dr["NombreCompleto"].ToString();
                        obj.anho = int.Parse(dr["anho"].ToString());
                        obj.semestre = int.Parse(dr["Semestre"].ToString());
                        oLista.Add(obj);
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

        public List<ReporteChildBE> ReporteUsoUsuGetTrimestre(DBHelper pDBHelper, ReporteChildBE pReporteChild)
        {
            List<ReporteChildBE> oLista = new List<ReporteChildBE>();
            ReporteChildBE obj = null;
            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdEmpresa", pReporteChild.idEmpresa == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.idEmpresa),
                    new DBHelper.Parameters("@IdUsuario", pReporteChild.idUsuario == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.idUsuario),
                    new DBHelper.Parameters("@Anho", pReporteChild.anho == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.anho),
                    new DBHelper.Parameters("@Semestre", pReporteChild.semestre == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.semestre)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("ReporteUsoUsuGetTrimestre"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new ReporteChildBE();

                        StringBuilder sb = new StringBuilder();
                        sb.Append(dr["horas"].ToString().PadLeft(2, '0'));
                        sb.Append(":");
                        sb.Append(dr["minutos"].ToString().PadLeft(2, '0'));

                        obj.duration = sb.ToString();
                        obj.idEmpresa = int.Parse(dr["idEmpresa"].ToString());
                        obj.idUsuario = int.Parse(dr["idUsuario"].ToString());
                        obj.NombreUsuario = dr["NombreCompleto"].ToString();
                        obj.anho = int.Parse(dr["anho"].ToString());
                        obj.semestre = int.Parse(dr["Semestre"].ToString());
                        obj.trimestre = int.Parse(dr["Trimestre"].ToString());
                        oLista.Add(obj);
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
        public List<ReporteChildBE> ReporteUsoUsuGetMes(DBHelper pDBHelper, ReporteChildBE pReporteChild)
        {
            List<ReporteChildBE> oLista = new List<ReporteChildBE>();
            ReporteChildBE obj = null;
            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdEmpresa", pReporteChild.idEmpresa == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.idEmpresa),
                    new DBHelper.Parameters("@IdUsuario", pReporteChild.idUsuario == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.idUsuario),
                    new DBHelper.Parameters("@Anho", pReporteChild.anho == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.anho),
                    new DBHelper.Parameters("@Semestre", pReporteChild.semestre == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.semestre),
                    new DBHelper.Parameters("@Trimestre", pReporteChild.trimestre == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.trimestre)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("ReporteUsoUsuGetMes"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new ReporteChildBE();

                        StringBuilder sb = new StringBuilder();
                        sb.Append(dr["horas"].ToString().PadLeft(2, '0'));
                        sb.Append(":");
                        sb.Append(dr["minutos"].ToString().PadLeft(2, '0'));

                        obj.duration = sb.ToString();
                        obj.idEmpresa = int.Parse(dr["idEmpresa"].ToString());
                        obj.idUsuario = int.Parse(dr["idUsuario"].ToString());
                        obj.NombreUsuario = dr["NombreCompleto"].ToString();
                        obj.anho = int.Parse(dr["anho"].ToString());
                        obj.semestre = int.Parse(dr["Semestre"].ToString());
                        obj.trimestre = int.Parse(dr["Trimestre"].ToString());
                        obj.mes = int.Parse(dr["Mes"].ToString());
                        oLista.Add(obj);
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
        public List<ReporteChildBE> ReporteUsoUsuGetDia(DBHelper pDBHelper, ReporteChildBE pReporteChild)
        {
            List<ReporteChildBE> oLista = new List<ReporteChildBE>();
            ReporteChildBE obj = null;
            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdEmpresa", pReporteChild.idEmpresa == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.idEmpresa),
                    new DBHelper.Parameters("@IdUsuario", pReporteChild.idUsuario == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.idUsuario),
                    new DBHelper.Parameters("@Anho", pReporteChild.anho == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.anho),
                    new DBHelper.Parameters("@Semestre", pReporteChild.semestre == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.semestre),
                    new DBHelper.Parameters("@Trimestre", pReporteChild.trimestre == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.trimestre),
                    new DBHelper.Parameters("@Mes", pReporteChild.mes == Constantes.INT_NULO ? (object)DBNull.Value : pReporteChild.mes)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("ReporteUsoUsuGetDia"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new ReporteChildBE();

                        StringBuilder sb = new StringBuilder();
                        sb.Append(dr["horas"].ToString().PadLeft(2, '0'));
                        sb.Append(":");
                        sb.Append(dr["minutos"].ToString().PadLeft(2, '0'));

                        obj.duration = sb.ToString();
                        obj.idEmpresa = int.Parse(dr["idEmpresa"].ToString());
                        obj.idUsuario = int.Parse(dr["idUsuario"].ToString());
                        obj.NombreUsuario = dr["NombreCompleto"].ToString();
                        obj.anho = int.Parse(dr["anho"].ToString());
                        obj.semestre = int.Parse(dr["Semestre"].ToString());
                        obj.trimestre = int.Parse(dr["Trimestre"].ToString());
                        obj.mes = int.Parse(dr["Mes"].ToString());
                        obj.dia = int.Parse(dr["Dia"].ToString());
                        oLista.Add(obj);
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
    }
}
