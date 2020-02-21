using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace SQLlibrary {
    public class InstructorController {

        public static BcConnection bcConnection { get; set; }

        private static Instructor LoadInstrustorInstance(SqlDataReader reader) {
            var instructor = new Instructor();
            instructor.ID = Convert.ToInt32(reader["ID"]);
            instructor.Firstname = reader["Firstname"].ToString();
            instructor.Lastname = reader["Lastname"].ToString();
            instructor.YearsExperience = Convert.ToInt32(reader["YearsExperience"]);
            instructor.IsTenured = Convert.ToBoolean(reader["IsTenured"]);
            return instructor;
        }

        public static List<Instructor> GetAllInstructors() {
            var sql = "Select * from Instructor;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                reader.Close();
                reader = null;
                Console.WriteLine("No rows found for Instructor");
                return new List<Instructor>();
            }
            var instructors = new List<Instructor>();
            while (reader.Read()){
                var instructor = LoadInstrustorInstance(reader);
                instructors.Add(instructor);
            }
            reader.Close();
            reader = null;
            return instructors;
        }

        public static Instructor GetInstructorByPK(int id) {
            var sql = "Select * from Instructor Where ID = @ID;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@ID", id);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                reader.Close();
                reader = null;
                return null;
            }
            reader.Read();
            var instructor = LoadInstrustorInstance(reader);
            reader.Close();
            reader = null;
            return instructor;
        }

        public static bool InsertInstructor(Instructor instructor) {
            var sql = $"INSERT into Instructor (ID, Firstname, Lastname, YearsExperience, IsTenured)" +
                " Values (@ID, @Firstname, @Lastname, @YearsExperience, @IsTenured);";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@ID", instructor.ID);
            command.Parameters.AddWithValue("@Firstname", instructor.Firstname);
            command.Parameters.AddWithValue("@Lastname", instructor.Lastname);
            command.Parameters.AddWithValue("@YearsExperience", instructor.YearsExperience);
            command.Parameters.AddWithValue("@IsTenured", instructor.IsTenured);
            var rowsAffected = command.ExecuteNonQuery();
            if(rowsAffected != 1) {
                throw new Exception("Insert Instructor failed");
            }
            return true;
        }

        public static bool UpdateInstructor(Instructor instructor) {
            var sql = "UPDATE Instructor set" +
                " Firstname = @Firstname, " +
                " Lastname = @Lastname, " +
                " YearsExperience = @YearsExperience, " +
                " IsTenured = @IsTenured " +
                " Where ID = @ID; ";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@ID", instructor.ID);
            command.Parameters.AddWithValue("@Firstname", instructor.Firstname);
            command.Parameters.AddWithValue("@Lastname", instructor.Lastname);
            command.Parameters.AddWithValue("@YearsExperience", instructor.YearsExperience);
            command.Parameters.AddWithValue("@IsTenured", instructor.IsTenured);
            var rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected != 1) {
                throw new Exception("Update Instructor failed");
            }
            return true;
        }

        public static bool DeleteInstructor(Instructor instructor) {
            var sql = "DELETE from Instructor " +
                " Where ID = @ID; ";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@ID", instructor.ID);
            var rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected != 1){
                throw new Exception("Delete Instructor failed");
            }
            return true;
        }

    }
}
