using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace OOPDeneme.DbManager
{
    internal class DbManager : IDbServis
    {
        private readonly string baglantiCumlesi;
        public DbManager()
        {
            var yapilandir = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = yapilandir.Build();

    
            baglantiCumlesi = config.GetConnectionString("DefaultConnection");
        }


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
