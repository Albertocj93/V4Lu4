using System;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;

/* 
 * Creado por           : Jorge Girao
 * Fecha creación       : 2012-10-10
 * Modificado por       :
 * Fecha modificación   :
*/

namespace Utility
{
    public class DBHelper : IDisposable
    {
        #region STRUCTURE BASED

        /// <summary>
        ///Description	    :	This function is used to Create Parameters for the Command For Execution
        ///Author			:	Shyam SS
        ///Date				:	28 June 2007
        ///Input			:	2-Dimensional Parameter Array
        ///OutPut			:	NA
        ///Comments			:	
        /// </summary>
        public void CreateDBParameters(Parameters[] colParameters)
        {
            for (int i = 0; i < colParameters.Length; i++)
            {
                Parameters oParam = (Parameters)colParameters[i];

                this.AddParameter(oParam.ParamName, oParam.ParamValue, oParam.ParamDirection, oParam.ParamSize);
            }
        }

        #endregion


        #region STRUCTURES

        /// <summary>
        ///Description	    :	This function is used to Execute the Command
        ///Author			:	Shyam SS 
        ///Date				:	28 June 2007
        ///Input			:	
        ///OutPut			:	
        ///Comments			:	
        /// </summary>
        public struct Parameters
        {
            public string ParamName;
            public object ParamValue;
            public ParameterDirection ParamDirection;
            public DbType? ParamDBType;
            public int ParamSize;


            //public int size;
            public Parameters(string Name, object Value, ParameterDirection Direction, object ParamDBType, int ParamSize)
            {

                this.ParamName = Name;
                this.ParamValue = Value;
                this.ParamDirection = Direction;
                this.ParamDBType = null;
                this.ParamSize = ParamSize;
                this.ParamDBType = (DbType)ParamDBType;
            }

            public Parameters(string Name, object Value, ParameterDirection Direction, object ParamDBType)
            {

                this.ParamName = Name;
                this.ParamValue = Value;
                this.ParamDirection = Direction;
                this.ParamDBType = null;
                this.ParamSize = 0;
                this.ParamDBType = (DbType)ParamDBType;
            }

            public Parameters(string Name, object Value, ParameterDirection Direction)
            {
                this.ParamName = Name;
                this.ParamValue = Value;
                this.ParamDirection = Direction;
                this.ParamDBType = null;
                this.ParamSize = 0;
            }

            public Parameters(string Name, object Value, ParameterDirection Direction, int Size)
            {
                this.ParamName = Name;
                this.ParamValue = Value;
                this.ParamDirection = Direction;
                this.ParamDBType = null;
                this.ParamSize = Size;
            }

            public Parameters(string Name, object Value)
            {
                this.ParamName = Name;
                this.ParamValue = Value;
                this.ParamDirection = ParameterDirection.Input;
                this.ParamDBType = null;
                this.ParamSize = 0;
            }
        }

        #endregion


        public const bool PARAMETROS_OUT = false;
        public const int MENSAJE_SIZE = 1000;
        public const int INDEX_PARAMETROS_OUT = 0;
        public const string MENSAJE_ERROR = "p_vch_mensaje";
        private string _ownerDB;

        private string strConnectionString;
        private DbConnection objConnection;
        private DbCommand objCommand;
        private DbProviderFactory objFactory = null;
        private bool boolHandleErrors;
        private string strLastError;
        private bool boolLogError;
        private string strLogFile;

        public DBHelper(string connectionstring, Providers provider)
        {
            CreateDBHelper(connectionstring, provider);
        }

        public DBHelper(string connectionstring)
            : this(connectionstring, Providers.SqlServer)
        {
        }

        private void CreateDBHelper(string connectionstring, Providers provider)
        {
            strConnectionString = connectionstring;
            switch (provider)
            {
                case Providers.SqlServer:
                    objFactory = SqlClientFactory.Instance;
                    break;
                case Providers.OleDb:
                    objFactory = OleDbFactory.Instance;
                    break;
                //case Providers.Oracle:
                //    objFactory = OracleClientFactory.Instance;
                //    break;
                case Providers.ODBC:
                    objFactory = OdbcFactory.Instance;
                    break;
                //case Providers.IBMDB2:
                //    objFactory = DB2Factory.Instance;
                //    break;
            }

            objConnection = objFactory.CreateConnection();
            objCommand = objFactory.CreateCommand();

            objConnection.ConnectionString = strConnectionString;
            objCommand.Connection = objConnection;
        }

        public bool HandleErrors
        {
            get
            {
                return boolHandleErrors;
            }
            set
            {
                boolHandleErrors = value;
            }
        }
        public string LastError
        {
            get
            {
                return strLastError;
            }
        }
        public bool LogErrors
        {
            get
            {
                return boolLogError;
            }
            set
            {
                boolLogError = value;
            }
        }
        public string LogFile
        {
            get
            {
                return strLogFile;
            }
            set
            {
                strLogFile = value;
            }
        }
        public int AddParameter(string name, object value)
        {
            DbParameter p = objFactory.CreateParameter();
            p.ParameterName = name;
            p.Value = value;
            return objCommand.Parameters.Add(p);
            //objFactory.
        }
        public int AddParameter(string name, object value, ParameterDirection Direccion, DbType tipo)
        {
            return AddParameter(name, value, Direccion, tipo, 0);
        }
        public int AddParameter(string name, object value, ParameterDirection Direccion, DbType tipo, int size)
        {
            DbParameter p = objFactory.CreateParameter();
            p.ParameterName = name;
            p.Value = value;
            p.Direction = Direccion;
            p.DbType = tipo;
            if (size > 0) p.Size = size;
            return objCommand.Parameters.Add(p);
        }

        public int AddParameter(string name, object value, ParameterDirection Direccion, int size)
        {
            DbParameter p = objFactory.CreateParameter();
            p.ParameterName = name;
            p.Value = value;
            p.Direction = Direccion;
            if (size > 0) p.Size = size;
            return objCommand.Parameters.Add(p);

        }
        public int AddParameter(string name, object value, ParameterDirection Direccion)
        {
            DbParameter p = objFactory.CreateParameter();
            p.ParameterName = name;
            p.Value = value;
            p.Direction = Direccion;
            return objCommand.Parameters.Add(p);
        }
        public int AddParameter(DbParameter parameter)
        {

            return objCommand.Parameters.Add(parameter);
        }
        public DbParameter GetParameter(string parameter)
        {

            return objCommand.Parameters[parameter];
        }
        public DbCommand Command
        {
            get
            {
                return objCommand;
            }
        }
        public void BeginTransaction()
        {
            if (objConnection.State == System.Data.ConnectionState.Closed)
            {
                objConnection.Open();
            }
            objCommand.Transaction = objConnection.BeginTransaction();
        }
        public void CommitTransaction()
        {
            objCommand.Transaction.Commit();
            //objConnection.Close(); //Comentado WE - 20110905
        }
        public void RollbackTransaction()
        {
            objCommand.Transaction.Rollback();
            //objConnection.Close();  //Comentado WE - 20110905
        }
        public int ExecuteNonQuery(string query)
        {
            return ExecuteNonQuery(query, CommandType.Text, ConnectionState.CloseOnExit, ParameterError.notInclude, CleanParameters.clean);
        }
        public int ExecuteNonQuery(string query, CommandType commandtype)
        {
            return ExecuteNonQuery(query, commandtype, ConnectionState.CloseOnExit, ParameterError.notInclude, CleanParameters.clean);
        }
        public int ExecuteNonQuery(string query, ConnectionState connectionstate)
        {
            return ExecuteNonQuery(query, CommandType.Text, connectionstate, ParameterError.notInclude, CleanParameters.clean);
        }
        public int ExecuteNonQuery(string query, CommandType commandtype, ConnectionState connectionstate)
        {
            return ExecuteNonQuery(query, commandtype, connectionstate, ParameterError.notInclude, CleanParameters.clean);


        }
        public int ExecuteNonQuery(string query, CommandType commandtype, ConnectionState connectionstate, ParameterError parameterError, CleanParameters cleanParameters)
        {
            objCommand.CommandText = query;
            objCommand.CommandType = commandtype;
            if (parameterError == ParameterError.include) addParameterError();

            int i = -1;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                i = objCommand.ExecuteNonQuery();

                if (parameterError == ParameterError.include)
                {
                    string error = objCommand.Parameters[DBHelper.MENSAJE_ERROR].Value.ToString();
                    if (error != string.Empty) throw new ValidatorException(error);

                }
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
            }
            finally
            {
                if (cleanParameters == CleanParameters.clean) objCommand.Parameters.Clear();

                if (connectionstate == ConnectionState.CloseOnExit)
                {
                    //objConnection.Close();
                }
            }

            return i;
        }
        public object ExecuteScalar(string query)
        {
            return ExecuteScalar(query, CommandType.Text, ConnectionState.CloseOnExit, ParameterError.notInclude, CleanParameters.clean);
        }
        public object ExecuteScalar(string query, CommandType commandtype)
        {
            return ExecuteScalar(query, commandtype, ConnectionState.CloseOnExit, ParameterError.notInclude, CleanParameters.clean);
        }
        public object ExecuteScalar(string query, ConnectionState connectionstate)
        {
            return ExecuteScalar(query, CommandType.Text, connectionstate, ParameterError.notInclude, CleanParameters.clean);
        }
        public object ExecuteScalar(string query, CommandType commandtype, ConnectionState connectionstate)
        {

            return ExecuteScalar(query, commandtype, connectionstate, ParameterError.notInclude, CleanParameters.clean);
        }
        public object ExecuteScalar(string query, CommandType commandtype, ConnectionState connectionstate, ParameterError parameterError, CleanParameters cleanParameters)
        {
            objCommand.CommandText = query;
            objCommand.CommandType = commandtype;
            if (parameterError == ParameterError.include) addParameterError();

            object o = null;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                o = objCommand.ExecuteScalar();

                if (parameterError == ParameterError.include)
                {
                    string error = objCommand.Parameters[DBHelper.MENSAJE_ERROR].Value.ToString();
                    if (error != string.Empty) throw new ValidatorException(error);

                }
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
            }
            finally
            {
                if (cleanParameters == CleanParameters.clean) objCommand.Parameters.Clear();

                if (connectionstate == ConnectionState.CloseOnExit)
                {
                    objConnection.Close();
                }
            }

            return o;
        }
        public DbDataReader ExecuteReader(string query)
        {
            return ExecuteReader(query, CommandType.Text, ConnectionState.CloseOnExit);
        }
        public DbDataReader ExecuteReader(string query, CommandType commandtype)
        {
            return ExecuteReader(query, commandtype, ConnectionState.CloseOnExit);
        }
        public DbDataReader ExecuteReader(string query, ConnectionState connectionstate)
        {
            return ExecuteReader(query, CommandType.Text, connectionstate, ParameterError.notInclude, CleanParameters.clean);
        }
        public DbDataReader ExecuteReader(string query, CommandType commandtype, ConnectionState connectionstate)
        {
            return ExecuteReader(query, commandtype, connectionstate, ParameterError.notInclude, CleanParameters.preserve);


        }
        public DbDataReader ExecuteReader(string query, CommandType commandtype, ConnectionState connectionstate, ParameterError parameterError, CleanParameters cleanParameters)
        {
            objCommand.CommandText = query;
            objCommand.CommandType = commandtype;
            DbDataReader reader = null;
            try
            {
                if (parameterError == ParameterError.include) addParameterError();

                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                if (connectionstate == ConnectionState.CloseOnExit)
                {
                    reader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);
                }
                else
                {
                    reader = objCommand.ExecuteReader();
                }

                if (parameterError == ParameterError.include)
                {
                    string error = objCommand.Parameters[DBHelper.MENSAJE_ERROR].Value.ToString();
                    if (error != string.Empty) throw new ValidatorException(error);

                }

            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
            }
            finally
            {
                if (cleanParameters == CleanParameters.clean) objCommand.Parameters.Clear();

            }

            return reader;
        }
        public DataSet ExecuteDataSet(string query)
        {
            return ExecuteDataSet(query, CommandType.Text, ConnectionState.CloseOnExit, ParameterError.notInclude, CleanParameters.clean);
        }
        public DataSet ExecuteDataSet(string query, CommandType commandtype)
        {
            return ExecuteDataSet(query, commandtype, ConnectionState.CloseOnExit, ParameterError.notInclude, CleanParameters.clean);
        }
        public DataSet ExecuteDataSet(string query, ConnectionState connectionstate)
        {
            return ExecuteDataSet(query, CommandType.Text, connectionstate, ParameterError.notInclude, CleanParameters.clean);
        }
        public DataSet ExecuteDataSet(string query, CommandType commandtype, ConnectionState connectionstate)
        {
            return ExecuteDataSet(query, commandtype, connectionstate, ParameterError.notInclude, CleanParameters.clean);


        }
        public DataSet ExecuteDataSet(string query, CommandType commandtype, ConnectionState connectionstate, ParameterError parameterError, CleanParameters cleanParameters)
        {
            DbDataAdapter adapter = objFactory.CreateDataAdapter();

            if (parameterError == ParameterError.include) addParameterError();

            objCommand.CommandText = query;
            objCommand.CommandType = commandtype;
            adapter.SelectCommand = objCommand;
            DataSet ds = new DataSet();

            try
            {
                adapter.Fill(ds);

                if (parameterError == ParameterError.include)
                {
                    string error = objCommand.Parameters[DBHelper.MENSAJE_ERROR].Value.ToString();
                    if (error != string.Empty) throw new ValidatorException(error);

                }
            }
            catch (Exception ex)
            {
                HandleExceptions(ex);
            }
            finally
            {
                if (cleanParameters == CleanParameters.clean) objCommand.Parameters.Clear();
                if (connectionstate == ConnectionState.CloseOnExit)
                {
                    if (objConnection.State == System.Data.ConnectionState.Open)
                    {
                        objConnection.Close();
                    }
                }
            }
            return ds;
        }
        public void ClearParameter()
        {
            objCommand.Parameters.Clear();

        }
        private void HandleExceptions(Exception ex)
        {
            if (LogErrors)
            {
                WriteToLog(ex.Message);
            }
            if (HandleErrors)
            {
                strLastError = ex.Message;
            }
            else
            {
                throw ex;
            }
        }
        private void WriteToLog(string msg)
        {
            StreamWriter writer = File.AppendText(LogFile);
            writer.WriteLine(DateTime.Now.ToString() + " - " + msg);
            writer.Close();
        }
        public void Dispose()
        {
            if (objConnection != null)
            {  //Validacion agregada WE - 20110905
                objConnection.Close();
                objConnection.Dispose();
            }
            if (objCommand != null)
            { //Validacion agregada WE - 20110905
                objCommand.Dispose();
            }
        }
        private void addParameterError()
        {
            if (objCommand.Parameters == null) throw new Exception("No se ha definido Coleccion de parametros");
            if (objCommand.Parameters.Contains(DBHelper.MENSAJE_ERROR)) return;

            DbParameter p = objFactory.CreateParameter();
            p.ParameterName = DBHelper.MENSAJE_ERROR;
            p.Value = string.Empty;
            p.Size = DBHelper.MENSAJE_SIZE;
            p.DbType = DbType.String;
            p.Direction = ParameterDirection.InputOutput;

            objCommand.Parameters.Add(p);
        }

        public string concatOwner(string pObjetoDB)
        {
            //String propietario = ConfigurationManager.AppSettings["ownerDB"];
            String propietario = string.Empty;
            if (!string.IsNullOrEmpty(propietario))
                _ownerDB = propietario;
            return string.IsNullOrEmpty(_ownerDB) ? pObjetoDB : _ownerDB + "." + pObjetoDB;

        }
    }


    public enum Providers
    {
        SqlServer, OleDb, Oracle, ODBC, IBMDB2, ConfigDefined
    }

    public enum ConnectionState
    {
        KeepOpen, CloseOnExit
    }

    public enum ParameterError
    {
        include, notInclude
    }

    public enum CleanParameters
    {
        clean, preserve
    }


    [global::System.Serializable]
    public class ValidatorException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ValidatorException(string message) : base("DB Error " + message) { }
    }
}
