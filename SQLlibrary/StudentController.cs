using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SQLlibrary {
    public class StudentController { //calls database

        public static BcConnection bcConnection { get; set; }

        public static List<Student> GetAllStudents() {
            var sql = "SELECT * from Student";
            var command = new SqlCommand(sql, bcConnection.Connection);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                Console.WriteLine("No rows shown");
                return new List<Student>();
            }
            var students = new List<Student>();
            while (reader.Read()) {
                var student = new Student();
                student.ID = Convert.ToInt32(reader["ID"]);
                student.FirstName = reader["FirstName"].ToString();
                student.LastName = reader["LastName"].ToString();
                student.SAT = Convert.ToInt32(reader["SAT"]);
                student.GPA = Convert.ToDouble(reader["GPA"]);
                //student.MajorID = Convert.ToInt32(reader["MajorID"]);
                students.Add(student);
            }
            reader.Close();
            reader = null;
            return students;
        }

        public static Student GetStudentByPK(int id) {
            var sql = $"Select * from Student Where ID = {id}";
            var command = new SqlCommand(sql, bcConnection.Connection);
            var reader = command.ExecuteReader(); //has exceptions that you would need to catch
            if (!reader.HasRows) {
                return null;
            }
            reader.Read();
            var student = new Student();
            student.ID = Convert.ToInt32(reader["ID"]);
            student.FirstName = reader["FirstName"].ToString();
            student.LastName = reader["LastName"].ToString();
            student.SAT = Convert.ToInt32(reader["SAT"]);
            student.GPA = Convert.ToDouble(reader["GPA"]);

            reader.Close();
            reader = null;
            return student;
        }

        public static bool InsertStudent (Student student) {
            var majorid = "";
            if (student.MajorID == null) {
                majorid = "NULL";
            } 
            else {
               majorid = student.MajorID.ToString();
            }
                var sql = $"INSERT into Student(ID, Firstname, Lastname, SAT, GPA, MajorID)" +
                    $"Values ({student.ID},'{student.FirstName}', '{student.LastName}', {student.SAT}, {student.GPA}, {majorid});";
                var command = new SqlCommand(sql, bcConnection.Connection);
                var recsAffected = command.ExecuteNonQuery();
                if (recsAffected != 1) {
                    throw new Exception("Insert Failed");
                }
                return true;
            }

    }
}
