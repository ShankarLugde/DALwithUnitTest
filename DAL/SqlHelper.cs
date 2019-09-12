using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SqlHelper
    {
        private string ConnectionString { get; set; }
        public SqlHelper(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public void CloseConnection(SqlConnection connection)
        {
            connection.Close();
        }
        public SqlParameter CreateParameter(string name, object value, DbType dbType)
        {
            return CreateParameter(name, 0, value, dbType, ParameterDirection.Input);
        }
        public SqlParameter CreateParameter(string name, int size, object value, DbType dbType)
        {
            return CreateParameter(name, size, value, dbType, ParameterDirection.Input);
        }
        public SqlParameter CreateParameter(string name, int size, object value, DbType dbType, ParameterDirection direction)
        {
            return new SqlParameter
            {
                DbType = dbType,
                ParameterName = name,
                Size = size,
                Direction = direction,
                Value = value
            };
        }
        public DataTable GetDataTable(string SpName, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    var dataset = new DataSet();
                    var dataAdaper = new SqlDataAdapter(command);
                    dataAdaper.Fill(dataset);
                    return dataset.Tables[0];
                }
            }
        }
        public DataSet GetDataSet(string SpName, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    var dataset = new DataSet();
                    var dataAdaper = new SqlDataAdapter(command);
                    dataAdaper.Fill(dataset);
                    return dataset;
                }
            }
        }
        public IDataReader GetDataReader(string SpName, SqlParameter[] parameters, out SqlConnection connection)
        {
            IDataReader reader = null;
            connection = new SqlConnection(ConnectionString);
            connection.Open();
            var command = new SqlCommand(SpName, connection);
            command.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
            reader = command.ExecuteReader();

            return reader;
        }
        public void Delete(string SpName, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    command.ExecuteNonQuery();
                }
            }
        }
        public void Insert(string SpName, SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    command.ExecuteNonQuery();
                }
            }
        }
        public int Insert(string SpName, SqlParameter[] parameters, out int lastId)
        {
            lastId = 0;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    object newId = command.ExecuteScalar();
                    lastId = Convert.ToInt32(newId);
                }
            }
            return lastId;
        }
        public long Insert(string SpName, SqlParameter[] parameters, out long lastId)
        {
            lastId = 0;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    object newId = command.ExecuteScalar();
                    lastId = Convert.ToInt64(newId);
                }
            }
            return lastId;
        }
        public void InsertWithTransaction(string SpName, SqlParameter[] parameters)
        {
            SqlTransaction transactionScope = null;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                transactionScope = connection.BeginTransaction();
                using (var command = new SqlCommand(SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    try
                    {
                        command.ExecuteNonQuery();
                        transactionScope.Commit();
                    }
                    catch (Exception)
                    {
                        transactionScope.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        public void InsertWithTransaction(string SpName, IsolationLevel isolationLevel, SqlParameter[] parameters)
        {
            SqlTransaction transactionScope = null;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                transactionScope = connection.BeginTransaction(isolationLevel);
                using (var command = new SqlCommand(SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    try
                    {
                        command.ExecuteNonQuery();
                        transactionScope.Commit();
                    }
                    catch (Exception)
                    {
                        transactionScope.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        public void Update(string SpName, SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    command.ExecuteNonQuery();
                }
            }
        }
        public void UpdateWithTransaction(string SpName, SqlParameter[] parameters)
        {
            SqlTransaction transactionScope = null;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                transactionScope = connection.BeginTransaction();
                using (var command = new SqlCommand(SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    try
                    {
                        command.ExecuteNonQuery();
                        transactionScope.Commit();
                    }
                    catch (Exception)
                    {
                        transactionScope.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        public void UpdateWithTransaction(string SpName, IsolationLevel isolationLevel, SqlParameter[] parameters)
        {
            SqlTransaction transactionScope = null;
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                transactionScope = connection.BeginTransaction(isolationLevel);
                using (var command = new SqlCommand(SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    try
                    {
                        command.ExecuteNonQuery();
                        transactionScope.Commit();
                    }
                    catch (Exception)
                    {
                        transactionScope.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        public object GetScalarValue(string SpName, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    return command.ExecuteScalar();
                }
            }
        }
    }
}
