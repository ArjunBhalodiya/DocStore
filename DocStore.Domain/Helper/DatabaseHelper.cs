﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace DocStore.Domain.Helper
{
    public class DatabaseHelper
    {
        private readonly ILogger<DatabaseHelper> logger;
        private readonly string connectionString;

        public DatabaseHelper(string connectionString, ILogger<DatabaseHelper> logger)
        {
            this.connectionString = connectionString;
            this.logger = logger;
        }

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
                    logger.LogError(ex.Message, ex.StackTrace);
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
                    logger.LogError(ex.Message, ex.StackTrace);
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
