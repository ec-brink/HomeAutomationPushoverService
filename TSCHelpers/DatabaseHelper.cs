using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TSCHelpers
{
    public static class DatabaseHelper
    {
        private static string connectionString;

        public static void SetupDatabase()
        {
            try
            {
                connectionString = HelperSettings.Settings.DatabaseConnectionString;

                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new Exception("Empty connection string");
            }
            catch (Exception ex)
            {
                LogHelper.LogError("Error setting up database.", ex);
                throw;
            }
        }

        public static void ExecuteStoredProcedureNonQuery(string storedProcedureName, SqlParameter[] sqlParameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        if (sqlParameters != null)
                        {
                            command.Parameters.AddRange(sqlParameters);
                        }

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError($"Error executing stored procedure nonquery {storedProcedureName}.", ex);
            }
        }

        public static DataSet ExecuteStoredProcedureQuery(string storedProcedureName, SqlParameter[] sqlParameters)
        {
            DataSet dataSet = new DataSet();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        if (sqlParameters != null)
                        {
                            command.Parameters.AddRange(sqlParameters);
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataSet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError($"Error executing stored procedure query {storedProcedureName}.", ex);
            }

            return dataSet;
        }

        public static void ExecuteParameterizedNonQuery(string query, SqlParameter[] sqlParameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;

                        if (sqlParameters != null)
                        {
                            command.Parameters.AddRange(sqlParameters);
                        }

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError($"Error executing parameterized nonquery {query}.", ex);
            }
        }

        public static DataSet ExecuteQueryWithResult(string query, SqlParameter[] sqlParameters)
        {
            DataSet dataSet = new DataSet();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;

                        if (sqlParameters != null)
                        {
                            command.Parameters.AddRange(sqlParameters);
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataSet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError($"Error executing parameterized query {query}.", ex);
            }

            return dataSet;
        }
    }
}
