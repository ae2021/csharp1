﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace mysql_1
{

    class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DBConnect()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            
            server = "localhost";
            database = "connectcsharptomysql";
            uid = "uschi_glas";
            password = "19nooDles_78";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                
                switch (ex.Number)
                {
                    case 0:
                        Console.Write("Kann keine Verbindung zum Datenbank Server erstellen");
                        break;

                    case 1045:
                        Console.Write("Benutzername und oder passwort ungültig");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }


        //Insert statement
        public void Insert()
        {
            string query = "INSERT INTO tableinfo (name, age) VALUES('Uschi Glas', '33')";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Update statement
        public void Update()
        {
            string query = "UPDATE tableinfo SET name='Uschi', age='22' WHERE name='Uschi Glas'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Delete statement
        public void Delete()
        {
            string query = "DELETE FROM tableinfo WHERE name='Uschi Glas'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Select statement
        public List<string>[] Select()
        {
            string query = "SELECT * FROM tableinfo";

            //Create a list to store the result
            List<string>[] list = new List<string>[3];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["id"] + "");
                    list[1].Add(dataReader["name"] + "");
                    list[2].Add(dataReader["age"] + "");
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

    class Program
    {
        static void Main(string[] args)
        {
            DBConnect db = new DBConnect();
         

            db.Select();
            Console.WriteLine(String.Join("\n",db ));
            Console.WriteLine(db.Select());
            string cs = "server=localhost;database=connectcsharptomysql;uid=uschi_glas;pwd=19nooDles_78;";

            string sql = "SELECT id, name FROM tableinfo";
   using (var conn = new MySqlConnection(cs))
            {
                conn.Open();
                using (var cmd = new MySqlCommand(sql, conn))
                {
                   
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Id: {0} Name: {1}",
                                             reader["id"], reader["name"]);
                        }
                    }
                }
            }


            Console.ReadKey();
        }
    }
}

