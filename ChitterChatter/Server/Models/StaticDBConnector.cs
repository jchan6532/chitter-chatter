using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

using Constants.Models;
using Constants.Controllers;

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
            connectionString = $"server={server}; port={port}; userid={userName}; password={password}; database={database};";
            StaticDBConnector.connection = new MySqlConnection();
            StaticDBConnector.connection.ConnectionString = connectionString;

            try
            {
                StaticDBConnector.connection.Open();
                StaticDBConnector.command = StaticDBConnector.connection.CreateCommand();
            }
            catch (Exception e)
            {
                throw e;
            }
            return connectionString;
        }

        public static Int64 GetCount(string table, string column, string whereClause)
        {
            string query = $"SELECT COUNT({column}) FROM {table} WHERE {whereClause};";
            if (string.IsNullOrEmpty(whereClause) == true)
            {
                query = $"SELECT COUNT({column}) FROM {table};";
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
            Int64 count = StaticDBConnector.table.Rows[0].Field<Int64>($"count({column})");
            return count;
        }

        public static object GetFieldFromEntry(string table, string column, string whereClause)
        {
            string query = $"SELECT {column} FROM {table} WHERE {whereClause};";

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
            Int64 count = StaticDBConnector.GetCount(UserConstants.TABLENAME, UserConstants.USERNAME_COL, string.Format("{0}={1}", UserConstants.USERNAME_COL, enteredUserName));
            if (count != 1)
            {
                return ServerResponseCodes.USER_NOTRECOGNIZED;
            }
            string storedDBPassword = string.Empty;
            try
            {
                storedDBPassword = StaticDBConnector.GetFieldFromEntry(UserConstants.TABLENAME, UserConstants.PASSWORD_COL, string.Format("{0}={1}", UserConstants.USERNAME_COL, enteredUserName)).ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if (storedDBPassword != enteredPassword)
            {
                return ServerResponseCodes.INCORRECT_PASSWORD;
            }

            return ServerResponseCodes.LOGIN_SUCCESS;
        }

        public static void Create(string tableName, string[] columns, string[] values)
        {
            string columnQuery = string.Empty;
            foreach (string column in columns)
            {
                columnQuery = columnQuery + column;
                columnQuery = columnQuery + ",";
            }
            columnQuery = columnQuery.TrimEnd(',');

            string valueQuery = string.Empty;
            foreach (string value in values)
            {
                valueQuery = valueQuery + value;
                valueQuery = valueQuery + ",";
            }
            valueQuery = valueQuery.TrimEnd(',');

            string query = $"INSERT INTO {tableName} ({columnQuery}) VALUES ({valueQuery});";

            StaticDBConnector.command.CommandType = CommandType.Text;
            StaticDBConnector.command.CommandText = query;

            StaticDBConnector.command.ExecuteNonQuery();
            return;
        }

        public static void Read(string tableName, string whereClause)
        {

            string query = $"SELECT * FROM {tableName} WHERE {whereClause};";

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
        }

        public static void Update(string tableName, string whereClause, string updateClause)
        {
            string query = $"UPDATE {tableName} SET {updateClause} WHERE {whereClause};";

            StaticDBConnector.command.CommandType = CommandType.Text;
            StaticDBConnector.command.CommandText = query;

            StaticDBConnector.command.ExecuteNonQuery();
        }

        public static void Delete(string tableName, string whereClause)
        {
            string query = $"DELETE FROM {tableName} WHERE {whereClause};";

            StaticDBConnector.command.CommandType = CommandType.Text;
            StaticDBConnector.command.CommandText = query;

            StaticDBConnector.command.ExecuteNonQuery();
        }
    }
}
