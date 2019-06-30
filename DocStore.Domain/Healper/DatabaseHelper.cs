using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DocStore.Domain.Healper
{
    public class DatabaseHelper
    {
        private readonly string connectionString = "Data Source=.\\;Initial Catalog=DocStoreDb;Integrated Security=True";

        public int ExecuteQuery(string commandText, List<SqlParameter> sqlParameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddRange(sqlParameters.ToArray());

                        connection.Open();

                        return command.ExecuteNonQuery();
                    }
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
        }

        public DataTable ExecuteSelectQuery(string commandText, List<SqlParameter> sqlParameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddRange(sqlParameters.ToArray());

                        connection.Open();

                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command))
                        {
                            DataTable table = new DataTable();
                            sqlDataAdapter.Fill(table);
                            return table;
                        }
                    }
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
        }
    }
}
