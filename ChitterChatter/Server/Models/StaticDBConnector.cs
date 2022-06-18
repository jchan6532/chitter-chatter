using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace Server.Models
{
    public static class StaticDBConnector
    {
        public static MySqlConnection connection;
        public static MySqlCommand command;

        private static DataTable table = new DataTable();
        private static MySqlDataAdapter adapter;

        public static string GetConnectionString()
        {
            string connectionString = string.Empty;
            string userName = ConfigurationManager.AppSettings.Get("UserName");
            string password = ConfigurationManager.AppSettings.Get("Password");
            string database = ConfigurationManager.AppSettings.Get("DataBase");
            string server = ConfigurationManager.AppSettings.Get("Server");
            string port = ConfigurationManager.AppSettings.Get("DBPort");
            connectionString = string.Format("server={0}; port={1}; userid={2}; password={3}; database={4};", server, port, userName, password, database);
            StaticDBConnector.connection.ConnectionString = connectionString;

            try
            {
                StaticDBConnector.connection.Open();
                StaticDBConnector.command = StaticDBConnector.connection.CreateCommand();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return connectionString;
        }

        public static Int64 GetCount(string table, string column, string whereClause)
        {
            string query = string.Format("SELECT COUNT({0}) FROM {1} WHERE {2};", column, table, whereClause);
            if (string.IsNullOrEmpty(whereClause) == true)
            {
                query = string.Format("SELECT COUNT({0}) FROM {1};", column, table);
            }
            StaticDBConnector.command.CommandType = CommandType.Text;
            StaticDBConnector.command.CommandText = query;

            StaticDBConnector.command.ExecuteNonQuery();
            StaticDBConnector.adapter = new MySqlDataAdapter(StaticDBConnector.command);
            try
            {
                StaticDBConnector.table.Clear();
                StaticDBConnector.adapter.Fill(StaticDBConnector.table);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Int64 count = StaticDBConnector.table.Rows[0].Field<Int64>(string.Format("count({0})", column));
            return count;
        }

        public static object GetFieldFromEntry(string table, string column, string whereClause)
        {
            string query = string.Format("SELECT {0} FROM {1} WHERE {2};", column, table, whereClause);

            StaticDBConnector.command.CommandType = CommandType.Text;
            StaticDBConnector.command.CommandText = query;

            StaticDBConnector.command.ExecuteNonQuery();
            StaticDBConnector.adapter = new MySqlDataAdapter(StaticDBConnector.command);
            try
            {
                StaticDBConnector.table.Clear();
                StaticDBConnector.adapter.Fill(StaticDBConnector.table);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            object field = StaticDBConnector.table.Rows[0].Field<object>(column);
            return field;
        }

        public static int CheckLoginCredentials(string enteredUserName, string enteredPassword)
        {
            Int64 count = StaticDBConnector.GetCount()
        }
    }
}
