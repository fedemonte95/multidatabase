﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace nsMultiDBService
{
    class ServerConnect
    {
        private SqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public ServerConnect(string pServer, string pDatabase, string pUid, string pPassword)
        {
            connection = new SqlConnection();
            server = pServer;
            database = pDatabase;
            uid = pUid;
            password = pPassword;
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            string connectionString;
            connectionString = "Server=" + server + ";Database=" + 
            database + ";User Id=" + uid + ";Password=" + password + ";";

            connection.ConnectionString = connectionString;
        }

        //Open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }

        //Close connection to database
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }

        public void NonQuery(string query)
        {
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                SqlCommand cmd = new SqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Select statement
        public ArrayList Select(string tableName)
        {
            string query = "SELECT * " + "FROM " + tableName;
            //Create a list to store the result
            ArrayList list = new ArrayList();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                SqlCommand cmd = new SqlCommand(query, connection);
                //Create a data reader and Execute the command
                SqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    //list[0].Add(dataReader["id"] + "");
                    object[] values = new object[dataReader.FieldCount];
                    dataReader.GetValues(values);
                    list.Add(values);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        //Select statement
        public ArrayList Select(string tableName, string conditional)
        {
            string query = "SELECT * " + "FROM " + tableName + " WHERE " + conditional + ";";
            //Create a list to store the result
            ArrayList list = new ArrayList();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                SqlCommand cmd = new SqlCommand(query, connection);
                //Create a data reader and Execute the command
                SqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    //list[0].Add(dataReader["id"] + "");
                    object[] values = new object[dataReader.FieldCount];
                    dataReader.GetValues(values);
                    list.Add(values);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

    }
}