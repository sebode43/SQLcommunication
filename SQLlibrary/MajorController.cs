using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace SQLlibrary {
    public class MajorController {

        public static BcConnection bcConnection { get; set; } //contains the sql connection

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
                var major = new Major();
                major.ID = Convert.ToInt32(reader["ID"]);
                major.Description = reader["Description"].ToString();
                major.MinSat = Convert.ToInt32(reader["MinSat"]);
                majors.Add(major);
            }
            reader.Close();
            reader = null;
            return majors;
        }

    }
}
