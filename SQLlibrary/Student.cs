using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SQLlibrary {
    public class Student {

        private BcConnection bcConnection;
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string  LastName { get; set; }
        public int SAT { get; set; }
        public double GPA { get; set; }
        public int MajorID { get; set; }

        public List<Student> GetAllStudents() {
            var sql = "SELECT * from Student";
            var command = new SqlCommand(sql, bcConnection.Connection);
            var reader = command.ExecuteReader();
            if(!reader.HasRows) {
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
                student.GPA = Convert.ToInt32(reader["GPA"]);
               // student.MajorID = Convert.ToInt32(reader["MajorID"]);
                students.Add(student);
            }
            return students;
        }

        public Student(BcConnection connection) {
            bcConnection = connection;

        }

        public Student() {

        }

    }
}
