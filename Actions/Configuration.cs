using Npgsql;
using System;
using System.Data;
using System.Windows;

namespace Coins_Database.Actions
{
    public class Configuration
    {
        const string DATABASE_NAME = "postgres";
        public static NpgsqlConnection conn;
        static NpgsqlCommand Command;
        public string conn_param;

        public static string LoadSettings(string login, string password)
        {
            return $"Server = {XML.ReadXML(XML.CheckOrCreateXML())[0]}; User Id = {login}; " +
                $"Database = {DATABASE_NAME}; Port = {XML.ReadXML(XML.CheckOrCreateXML())[1]}; " +
                $"Password = {password}";
        }

        public static DataSet SDataSet(NpgsqlCommand command, NpgsqlConnection connection)
        {
            command.Connection = connection;
            NpgsqlDataAdapter iAdapter = new NpgsqlDataAdapter(command);
            DataSet iDataSet = new DataSet();
            iAdapter.Fill(iDataSet, "LIST");
            return iDataSet;
        }

        public static bool Connect(string login, string password)
        {
            bool result;
            Configuration config = new Configuration();
            config.conn_param = LoadSettings(login, password);
            try
            {
                conn = new NpgsqlConnection(config.conn_param);
                Command = conn.CreateCommand();
                conn.Open();
                result = true;
                Session.Login = login;

                if (!CheckSAAccess())
                {
                    if (!CheckTeacherAccess())
                    {
                        result = false;
                        MessageBox.Show("Вход недоступен");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Вход недоступен");
                result = false;
            }
            return result;
        }

        private static bool CheckTeacherAccess()
        {
            bool result = false;
            try
            {
                Command.CommandText = "SET ROLE Teacher;";
                Command.ExecuteNonQuery();
                Session.Access = Session.ACCESS.Teacher;
                result = true;
            }
            catch (PostgresException)
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return result;
        }

        private static bool CheckSAAccess()
        {
            bool result = false;
            try
            {
                Command.CommandText = "SET ROLE Superadmin;";
                Command.ExecuteNonQuery();
                Session.Access = Session.ACCESS.Superadmin;
                result = true;
            }
            catch (PostgresException)
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return result;
        }

        public static void Disconnect()
        {
            try
            {
                conn.Close();
            }
            catch (Exception exeption)
            {
                MessageBox.Show(exeption.Message);
            }
        }
    }
}
