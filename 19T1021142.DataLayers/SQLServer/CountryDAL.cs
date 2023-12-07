using _19T1021142.DataLayer.SQLServer;
using _19T1021142.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace _19T1021142.DataLayers.SQLServer
{
    public class CountryDAL : _BaseDAL , ICountryDAL
    {
        public CountryDAL(string connectionString) : base(connectionString)
        {
        }

        public IList<Country> list()
        {
            List<Country> data = new List<Country>();
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "select * from Countries";
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {

                    data.Add(new Country()
                    {
                        CountryName = Convert.ToString(reader["CountryName"])
                    }) ;
                }
                reader.Close();
                cn.Close();
            }
                return data;
        }
    }
}
