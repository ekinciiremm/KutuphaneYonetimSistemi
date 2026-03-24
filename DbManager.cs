using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace OOPDeneme.DbManager
{
    internal class DbManager : IDbServis
    {
        private string baglantiCumlesi = "Server=.\\SQLEXPRESS; Database=kutuphaneYonetimi; " +
            "User Id=stajyer; Password=12345; TrustServerCertificate=True;";

        public void ExecuteCommand(string query, List<SqlParameter> parameters)
        {
            using (SqlConnection connection = new SqlConnection(baglantiCumlesi))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null) command.Parameters.AddRange(parameters.ToArray());
                    command.ExecuteNonQuery();
                }
            }
        }

        public SqlDataReader ExecuteReader(string query)
        {
            SqlConnection connection = new SqlConnection(baglantiCumlesi);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }
    }
}
