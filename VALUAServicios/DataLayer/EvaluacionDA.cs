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
    public class EvaluacionDA
    {
        private static EvaluacionDA _EvaluacionDA = null;

        private EvaluacionDA() { }

        public static EvaluacionDA Instanse 
	    {
		    get {
                if (_EvaluacionDA != null) return _EvaluacionDA;
                _EvaluacionDA = new EvaluacionDA();
                return _EvaluacionDA;
		    }
	    }

        public int MatrizKHTGetValorByVariable(string Variable)
        {
            int resultado = 0;

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "MatrizKHTGetByVariable";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter Sp1 = new SqlParameter("@variable", Variable);
            cmd.Parameters.Add(Sp1);

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    resultado = Convert.ToInt32(dataReader["sch_valor"]);
                }
            }

            return resultado;
        }
        public int CompetenciaTGetIdByDesc(string descripcion)
        {
            int Respuesta = -1;

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "ValoresEvaluacionGetIdByDesc";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp1 = new SqlParameter("@descripcion", descripcion);
            cmd.Parameters.Add(sp1);

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    Respuesta = Convert.ToInt32(dataReader["IdValoresEvaluacion"].ToString());
                }
            }
            return Respuesta;
        }
        public int CompetenciaGGetIdByDesc(string descripcion)
        {
            int Respuesta = -1;

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "ValoresEvaluacionGetIdByDesc";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp1 = new SqlParameter("@descripcion", descripcion);
            cmd.Parameters.Add(sp1);

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    Respuesta = Convert.ToInt32(dataReader["IdValoresEvaluacion"].ToString());
                }
            }
            return Respuesta;
        }
        public int CompetenciaRHGetIdByDesc(string descripcion)
        {
            int Respuesta = -1;

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "ValoresEvaluacionGetIdByDesc";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp1 = new SqlParameter("@descripcion", descripcion);
            cmd.Parameters.Add(sp1);

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    Respuesta = Convert.ToInt32(dataReader["IdValoresEvaluacion"].ToString());
                }
            }
            return Respuesta;
        }
        public int SolucionAGetIdByDesc(string descripcion)
        {
            int Respuesta = -1;

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "ValoresEvaluacionGetIdByDesc";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp1 = new SqlParameter("@descripcion", descripcion);
            cmd.Parameters.Add(sp1);

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    Respuesta = Convert.ToInt32(dataReader["IdValoresEvaluacion"].ToString());
                }
            }
            return Respuesta;
        }
        public int SolucionDGetIdByDesc(string descripcion)
        {
            int Respuesta = -1;

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "ValoresEvaluacionGetIdByDesc";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp1 = new SqlParameter("@descripcion", descripcion);
            cmd.Parameters.Add(sp1);

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    Respuesta = Convert.ToInt32(dataReader["IdValoresEvaluacion"].ToString());
                }
            }
            return Respuesta;
        }
        public int ResponsabilidadAGetIdByDesc(string descripcion)
        {
            int Respuesta = -1;

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "ValoresEvaluacionGetIdByDesc";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp1 = new SqlParameter("@descripcion", descripcion);
            cmd.Parameters.Add(sp1);

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    Respuesta = Convert.ToInt32(dataReader["IdValoresEvaluacion"].ToString());
                }
            }
            return Respuesta;
        }
        public int ResponsabilidadIGetIdByDesc(string descripcion)
        {
            int Respuesta = -1;

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "ValoresEvaluacionGetIdByDesc";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp1 = new SqlParameter("@descripcion", descripcion);
            cmd.Parameters.Add(sp1);

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    Respuesta = Convert.ToInt32(dataReader["IdValoresEvaluacion"].ToString());
                }
            }
            return Respuesta;
        }
        public int ResponsabilidadMGetIdByDesc(string descripcion)
        {
            int Respuesta = -1;

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "ValoresEvaluacionGetIdByDesc";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp1 = new SqlParameter("@descripcion", descripcion);
            cmd.Parameters.Add(sp1);

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    Respuesta = Convert.ToInt32(dataReader["IdValoresEvaluacion"].ToString());
                }
            }
            return Respuesta;
        }
        public MatrizEvaluacionBE MatricesEvaluacionGetByTablaVariableFilaColumna(DBHelper pDBHelper,string Tipo, string Variable,string Fila,string Columna)
        {
            MatrizEvaluacionBE MatrizEvaluacionBE = new MatrizEvaluacionBE();

            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@Tipo", string.IsNullOrEmpty(Tipo) ? (object)DBNull.Value : Tipo),
                    new DBHelper.Parameters("@Variable", string.IsNullOrEmpty(Variable) ? "" : Variable),
                    new DBHelper.Parameters("@Fila", string.IsNullOrEmpty(Fila) ? "" : Fila),
                    new DBHelper.Parameters("@Columna", string.IsNullOrEmpty(Columna) ? "" : Columna),
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dataReader = pDBHelper.ExecuteReader(pDBHelper.concatOwner("MatricesEvaluacionGetByTablaVariableFilaColumna"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    if (dataReader.Read())
                    {

                        MatrizEvaluacionBE.Valor = dataReader["Valor"].ToString();
                        MatrizEvaluacionBE.Variable = dataReader["Variable"].ToString();
                        MatrizEvaluacionBE.Tabla = dataReader["Tipo"].ToString();
                        MatrizEvaluacionBE.Sumar = dataReader["Sumar"].ToString();
                        MatrizEvaluacionBE.Signo = dataReader["Signo"].ToString();
                        MatrizEvaluacionBE.Fila = dataReader["Fila"].ToString();
                        MatrizEvaluacionBE.Columna = dataReader["Columna"].ToString();

                    }
                }
                return MatrizEvaluacionBE;
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
        public CompetenciaTBE CompetenciaTGetByIdCompT(int IdCompT)
        {
            CompetenciaTBE CompetenciaTBE = new CompetenciaTBE();

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "ValoresEvaluacionGetById";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp1 = new SqlParameter("@IdValoresEvaluacion", IdCompT);
            cmd.Parameters.Add(sp1);

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    CompetenciaTBE.Id = Convert.ToInt32(dataReader["IdValoresEvaluacion"].ToString());
                    CompetenciaTBE.Valor = dataReader["Valor"].ToString();
                    CompetenciaTBE.Signo = dataReader["Signo"].ToString();
                }
            }
            return CompetenciaTBE;
        }
        public CompetenciaGBE CompetenciaGGetByIdCompG(int IdCompG)
        {
            CompetenciaGBE CompetenciaGBE = new CompetenciaGBE();

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "ValoresEvaluacionGetById";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp1 = new SqlParameter("@IdValoresEvaluacion", IdCompG);
            cmd.Parameters.Add(sp1);

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    CompetenciaGBE.Id = Convert.ToInt32(dataReader["IdValoresEvaluacion"].ToString());
                    CompetenciaGBE.Valor = dataReader["Valor"].ToString();
                    CompetenciaGBE.Signo = dataReader["Signo"].ToString();
                }
            }
            return CompetenciaGBE;
        }
        public CompetenciaRHBE CompetenciaRHGetByIdCompRH(int IdCompRH)
        {
            CompetenciaRHBE CompetenciaRHBE = new CompetenciaRHBE();

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "ValoresEvaluacionGetById";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp1 = new SqlParameter("@IdValoresEvaluacion", IdCompRH);
            cmd.Parameters.Add(sp1);

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    CompetenciaRHBE.Id = Convert.ToInt32(dataReader["IdValoresEvaluacion"].ToString());
                    CompetenciaRHBE.Valor = dataReader["Valor"].ToString();
                    CompetenciaRHBE.Signo = dataReader["Signo"].ToString();
                }
            }
            return CompetenciaRHBE;
        }
        public SolucionABE SolucionAGetByIdSoluA(int IdSolA)
        {
            SolucionABE SolucionABE = new SolucionABE();

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "ValoresEvaluacionGetById";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp1 = new SqlParameter("@IdValoresEvaluacion", IdSolA);
            cmd.Parameters.Add(sp1);

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    SolucionABE.Id = Convert.ToInt32(dataReader["IdValoresEvaluacion"].ToString());
                    SolucionABE.Valor = dataReader["Valor"].ToString();
                    SolucionABE.Signo = dataReader["Signo"].ToString();
                }
            }
            return SolucionABE;
        }
        public SolucionDBE SolucionDGetByIdSoluD(int IdSolD)
        {
            SolucionDBE SolucionDBE = new SolucionDBE();

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "ValoresEvaluacionGetById";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp1 = new SqlParameter("@IdValoresEvaluacion", IdSolD);
            cmd.Parameters.Add(sp1);

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    SolucionDBE.Id = Convert.ToInt32(dataReader["IdValoresEvaluacion"].ToString());
                    SolucionDBE.Valor = dataReader["Valor"].ToString();
                    SolucionDBE.Signo = dataReader["Signo"].ToString();
                }
            }
            return SolucionDBE;
        }
        public ResponsabilidadABE ResponsabilidadAGetByIdRespA(int IdRespA)
        {
            ResponsabilidadABE ResponsabilidadABE = new ResponsabilidadABE();

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "ValoresEvaluacionGetById";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp1 = new SqlParameter("@IdValoresEvaluacion", IdRespA);
            cmd.Parameters.Add(sp1);

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    ResponsabilidadABE.Id = Convert.ToInt32(dataReader["IdValoresEvaluacion"].ToString());
                    ResponsabilidadABE.Valor = dataReader["Valor"].ToString();
                    ResponsabilidadABE.Signo = dataReader["Signo"].ToString();
                }
            }
            return ResponsabilidadABE;
        }
        public ResponsabilidadMBE ResponsabilidadMGetByIdRespM(int IdRespM)
        {
            ResponsabilidadMBE ResponsabilidadMBE = new ResponsabilidadMBE();

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "ValoresEvaluacionGetById";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp1 = new SqlParameter("@IdValoresEvaluacion", IdRespM);
            cmd.Parameters.Add(sp1);

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    ResponsabilidadMBE.Id = Convert.ToInt32(dataReader["IdValoresEvaluacion"].ToString());
                    ResponsabilidadMBE.Valor = dataReader["Valor"].ToString();
                    ResponsabilidadMBE.Signo = dataReader["Signo"].ToString();
                }
            }
            return ResponsabilidadMBE;
        }
        public ResponsabilidadIBE ResponsabilidadIGetByIdRespI(int IdRespI)
        {
            ResponsabilidadIBE ResponsabilidadIBE = new ResponsabilidadIBE();

            string connectionString = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.ConnectionString = connectionString;
            con.Open();
            string nombreProcedure = "ValoresEvaluacionGetById";
            SqlCommand cmd = new SqlCommand(nombreProcedure, con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter sp1 = new SqlParameter("@IdValoresEvaluacion", IdRespI);
            cmd.Parameters.Add(sp1);

            using (IDataReader dataReader = cmd.ExecuteReader())
            {
                if (dataReader.Read())
                {
                    ResponsabilidadIBE.Id = Convert.ToInt32(dataReader["IdValoresEvaluacion"].ToString());
                    ResponsabilidadIBE.Valor = dataReader["Valor"].ToString();
                    ResponsabilidadIBE.Signo = dataReader["Signo"].ToString();
                }
            }
            return ResponsabilidadIBE;
        }
        public GradeStructureBE GradeStructureGetByTotal(DBHelper pDBHelper, string Total)
        {
            GradeStructureBE GradeStructureBE = new GradeStructureBE();

            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@Total", Convert.ToInt32(Total)==Constantes.INT_NULO ? (object)DBNull.Value : Convert.ToInt32(Total)),
                   
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dataReader = pDBHelper.ExecuteReader(pDBHelper.concatOwner("GradeStructureGetByTotal"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    if (dataReader.Read())
                    {

                        GradeStructureBE.IdGradeStructure = Convert.ToInt32(dataReader["IdGradeStructure"]);
                        GradeStructureBE.Min = dataReader["Min"].ToString();
                        GradeStructureBE.Mid = dataReader["Mid"].ToString();
                        GradeStructureBE.Max = dataReader["Max"].ToString();
                        GradeStructureBE.Gs = dataReader["Gs"].ToString();
                    }
                }
                return GradeStructureBE;
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
        public ValoresEvaluacionBE ValoresEvaluacionGetIdByDesc(DBHelper pDBHelper, string Descripcion,string Tipo)
        {
            ValoresEvaluacionBE ValoresEvaluacionBE = new ValoresEvaluacionBE();

            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("descripcion", string.IsNullOrEmpty(Descripcion) ? (object)DBNull.Value :Descripcion),
                    new DBHelper.Parameters("Tipo", string.IsNullOrEmpty(Tipo) ? (object)DBNull.Value : Tipo),
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dataReader = pDBHelper.ExecuteReader(pDBHelper.concatOwner("ValoresEvaluacionGetIdByDesc"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    if (dataReader.Read())
                    {
                        ValoresEvaluacionBE.Id = Convert.ToInt32(dataReader["IdValoresEvaluacion"]);
                        ValoresEvaluacionBE.Valor = dataReader["Valor"].ToString();
                        ValoresEvaluacionBE.Tipo = dataReader["Tipo"].ToString();
                    }
                }
                return ValoresEvaluacionBE;
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
        public List<ValoresEvaluacionBE> ValoresEvaluacionGetAllByTipo(DBHelper pDBHelper, string Tipo)
        {
            List<ValoresEvaluacionBE> lst = null;
            ValoresEvaluacionBE obj = null;
            DBHelper.Parameters[] colParameters = null;
            lst = new List<ValoresEvaluacionBE>();
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                     new DBHelper.Parameters("Tipo", string.IsNullOrEmpty(Tipo) ? (object)DBNull.Value : Tipo),
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("ValoresEvaluacionGetAllByTipo"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new ValoresEvaluacionBE();

                        obj.Id = Convert.ToInt32(dr["IdValoresEvaluacion"].ToString());
                        obj.Valor = dr["Valor"].ToString();

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
