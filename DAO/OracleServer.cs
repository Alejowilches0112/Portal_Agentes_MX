using System.Data;
using System;
//using Oracle.DataAccess;
//using Oracle.DataAccess.Client;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace DAO
{
    class OracleServer
    {
        private OracleConnection connection;
        public OracleCommand command;
        private OracleDataReader reader;
       

        //! CONSTRUCTORES
        public OracleServer(string TNS, string User, string Password)
        {
            var stringOra = string.Format("Data Source={0};User ID={1};Password={2}", TNS, User, Password);

            connection = new OracleConnection(stringOra);
            command = new OracleCommand();
        }
        public OracleServer(string StringConnection)
        {
            connection = new OracleConnection(StringConnection);
            command = new OracleCommand();
            command.CommandTimeout = 0;
        }

        public void GetClobOracle(string CommandText)
        {
            try
            {
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //! METODOS DE COMMAND
        public OracleDataReader ExecuteCommand(string CommandText, bool ResetSizeIfImageReturnAsByte = false)
        {
            OracleDataReader rdr;
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                command.CommandType = CommandType.Text;
                command.CommandText = CommandText;
                command.Connection = connection;
                command.InitialLONGFetchSize = -1;

                if (ResetSizeIfImageReturnAsByte) command.InitialLONGFetchSize = -1;

                reader = command.ExecuteReader();
                rdr = reader;
                return rdr;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //! METODOS DE PROCEDURE
        public IDataReader ExecuteProcedure(string ProcedureName, bool ResetSizeIfImageReturnAsByte = false)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = ProcedureName;
                command.Connection = connection;
                if (ResetSizeIfImageReturnAsByte) command.InitialLONGFetchSize = -1;

                reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                connection.Close();
            }


            return reader;
        }
        public void ExecuteProcedureNonQuery(string ProcedureName)
        {
            var con = connection.State;

            try
            {
                if (con == ConnectionState.Closed)
                    connection.Open();


                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = ProcedureName;
                command.Connection = connection;
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();

            }

        }

        public string ExecuteProcedureNonQueryClob(string ProcedureName, string ClobParameter)
        {
            var con = connection.State;

            try
            {
                if (con == ConnectionState.Closed)
                    connection.Open();


                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = ProcedureName;
                command.Connection = connection;
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
                return ((Oracle.ManagedDataAccess.Types.OracleClob)command.Parameters[ClobParameter].Value).Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();

            }

        }

        //! METODOS PARA PARAMETERS
        public void AddParameter<OracleDbType>(string Name, object Value, OracleDbType Type)
        {
            var parameter = new OracleParameter(Name, Type)
            {
                Value = Value
            };
            command.Parameters.Add(parameter);
        }
        public void AddParameter(string Name, object Value)
        {
            var parameter = new OracleParameter(Name, Value);
            command.Parameters.Add(parameter);
        }
        public void AddParameter<OracleParameter>(OracleParameter Parameter)
        {
            command.Parameters.Add(Parameter);
        }
        public object GetParameter(string Name)
        {
            var val = command.Parameters[Name].Value;
            return val;
        }

        public void ClearParameters()
        {
            command.Parameters.Clear();
        }

        //! METODOS DE CONEXION
        // se implementa la interfaz IDisposable para liberar recursos de las conexiones con DataReader y DataConnection
        //? Se debe tener en cuenta el uso de éste metodo en los DAOs 
        public void Dispose()
        {
            if (reader != null && !reader.IsClosed) reader.Dispose();
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
                connection.Dispose();
            }
        }

    }
}
