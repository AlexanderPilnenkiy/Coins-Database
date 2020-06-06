using Npgsql;
using System;
using System.Data;
using System.Windows;

namespace Coins_Database.Actions
{
    public class Configuration
    {
        public const string DATABASE_NAME = "postgres";
        public static NpgsqlConnection Connection;
        static NpgsqlCommand Command;
        public string ConnectionParameters;

        public static string LoadSettings(string Login, string Password)
        {
            return $"Server = {XML.ReadXML(XML.CheckOrCreateXML())[0]}; User Id = {Login}; " +
                $"Database = {DATABASE_NAME}; Port = {XML.ReadXML(XML.CheckOrCreateXML())[1]}; " +
                $"Password = {Password}";
        }

        public static DataSet SDataSet(NpgsqlCommand Command, NpgsqlConnection Connection)
        {
            Command.Connection = Connection;
            NpgsqlDataAdapter IAdapter = new NpgsqlDataAdapter(Command);
            DataSet IDataSet = new DataSet();
            IAdapter.Fill(IDataSet, "LIST");
            return IDataSet;
        }

        public static bool Connect(string Login, string Password)
        {
            bool result;
            Configuration Config = new Configuration();
            Config.ConnectionParameters = LoadSettings(Login, Password);
            try
            {
                Connection = new NpgsqlConnection(Config.ConnectionParameters);
                Command = Connection.CreateCommand();
                Connection.Open();
                result = true;
                Session.Login = Login;

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
                Connection.Close();
            }
            catch (Exception exeption)
            {
                MessageBox.Show(exeption.Message);
            }
        }
    }
}
