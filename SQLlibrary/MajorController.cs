using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace SQLlibrary {
    public class MajorController {

        public static BcConnection bcConnection { get; set; } //contains the sql connection

        private static Major LoadMajorInstance(SqlDataReader reader) { //returning a Major variable, but are passing in the reader
            var major = new Major();
            major.ID = Convert.ToInt32(reader["ID"]);
            major.Description = reader["Description"].ToString();
            major.MinSAT = Convert.ToInt32(reader["MinSAT"]);
            return major;
        }

        public static List<Major> GetAllMajors() {
            var sql = "SELECT * from Major;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                reader.Close();
                reader = null;
                Console.WriteLine("No rows found");
                return new List<Major>();
            }
            var majors = new List<Major>();
            while (reader.Read()) {
                var major = LoadMajorInstance(reader); //calling the method with all the variables
                majors.Add(major);
            }
            reader.Close();
            reader = null;
            return majors;
        }

        public static Major GetMajorByPK(int id) {
            var sql = $"Select * from Major Where ID = @ID; ";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@ID", id);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                reader.Close();
                reader = null;
                return null;
            }
            reader.Read();
            var major = LoadMajorInstance(reader);

            reader.Close();
            reader = null;
            return major;
        }

        public static bool InsertMajor (Major major) {
            var sql = $"INSERT into Major(ID, Description, MinSAT)" +
                    $"Values (@ID, @Description, @MinSAT);";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@ID", major.ID);
            command.Parameters.AddWithValue("@Description", major.Description);
            command.Parameters.AddWithValue("@MinSAT", major.MinSAT);
            var rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected != 1) {
                throw new Exception("Insert Major Failed");
            }
            return true;

        }

        public static bool UpdateMajor (Major major) {
            var sql = $"UPDATE Major set" +
                " Description = @Description, " +
                " MinSAT = @MinSAT" +
                " Where ID = @ID; ";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Description", major.Description);
            command.Parameters.AddWithValue("@MinSat", major.MinSAT);
            command.Parameters.AddWithValue("@ID", major.ID);
            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1) {
                throw new Exception("Update Major failed");
            }
            return true;
        }

        public static bool DeleteMajor (Major major) {
            var sql = "DELETE from Major" +
                " Where ID = @ID; ";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@ID", major.ID);
            var rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected != 1) {
                throw new Exception("Delte Major Failed");
            }
            return true;
        }

    }
}
