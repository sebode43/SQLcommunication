using System;
using System.Data.SqlClient;

namespace SQLlibrary {
    public class BcConnection {

        public SqlConnection Connection { get; set; } 

        public void Connect(string server, string database, string auth) {
            var connStr = $"server = {server}; database = {database}; {auth}";
            Connection = new SqlConnection(connStr);
            Connection.Open(); //this is an example of one where you might want to throw a try catch lock since it can have 3 exceptions
            if(Connection.State != System.Data.ConnectionState.Open) {
                Console.WriteLine("Could not open the connection. Check connection string.");
                Connection = null;
                return;
            }
            Console.WriteLine("Connection opened");
        }

        public void Disconnect() {
            if(Connection == null) {
                return;
            }
            if(Connection.State == System.Data.ConnectionState.Open) {
                Connection.Close();
                Connection.Dispose();
                Connection = null;
            }
        }

    }
}
