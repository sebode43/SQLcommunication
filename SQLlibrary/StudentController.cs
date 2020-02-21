using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SQLlibrary {
    public class StudentController { //calls database

        public static BcConnection bcConnection { get; set; }

        public static Student LoadStudentInstance(SqlDataReader reader) {
            var student = new Student();
            student.ID = Convert.ToInt32(reader["ID"]);
            student.FirstName = reader["FirstName"].ToString();
            student.LastName = reader["LastName"].ToString();
            student.SAT = Convert.ToInt32(reader["SAT"]);
            student.GPA = Convert.ToDouble(reader["GPA"]);
            student.MajorID = Convert.IsDBNull(reader["MajorID"]) ? (int?)null : Convert.ToInt32(reader["MajorID"]);
            return student;
        }

        public static List<Student> GetAllStudents() {
            var sql = "SELECT * from Student s Left Join Major m on m.ID = s.MajorID";
            var command = new SqlCommand(sql, bcConnection.Connection);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                reader.Close();
                reader = null;
                Console.WriteLine("No rows shown");
                return new List<Student>();
            }
            var students = new List<Student>();
            while (reader.Read()) {
                var student = LoadStudentInstance(reader);
                /*var student = new Student();
                student.ID = Convert.ToInt32(reader["ID"]);
                student.FirstName = reader["FirstName"].ToString();
                student.LastName = reader["LastName"].ToString();
                student.SAT = Convert.ToInt32(reader["SAT"]);
                student.GPA = Convert.ToDouble(reader["GPA"]);
                student.MajorID = Convert.IsDBNull(reader["MajorID"]) ? (int?)null : Convert.ToInt32(reader["MajorID"]);*/
                if (Convert.IsDBNull(reader["Description"])) {
                    student.Major = null;
                } else {
                    var major = new Major {
                        Description = reader["Description"].ToString(),
                        MinSAT = Convert.ToInt32(reader["MinSAT"])
                    };
                    student.Major = major;
                }
                students.Add(student);
            }
            reader.Close();
            reader = null;
            return students;
        }

        public static Student GetStudentByPK(int id) {
            var sql = $"Select * from Student Where ID = @ID";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@ID", id);
            var reader = command.ExecuteReader(); //has exceptions that you would need to catch
            if (!reader.HasRows) {
                reader.Close();
                return null;
            }
            reader.Read();
            var student = LoadStudentInstance(reader);
            reader.Close();
            reader = null;
            return student;
        }

        public static bool InsertStudent (Student student) {
           /* var majorid = "";
            if (student.MajorID == null) {
                majorid = "NULL";
            } 
            else {
               majorid = student.MajorID.ToString();
            }
            */
                var sql = $"INSERT into Student(ID, Firstname, Lastname, SAT, GPA, MajorID)" +
                    $"Values (@ID, @FirstName, @LastName, @SAT, @GPA, @MajorID);";
                var command = new SqlCommand(sql, bcConnection.Connection);
                command.Parameters.AddWithValue("@ID", student.ID);
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@SAT", student.SAT);
                command.Parameters.AddWithValue("@GPA", student.GPA);
                command.Parameters.AddWithValue("@MajorID", student.MajorID ?? Convert.DBNull);
            var recsAffected = command.ExecuteNonQuery();
                if (recsAffected != 1) {
                    throw new Exception("Insert Failed");
                }
                return true;
            }

        public static bool UpdateStudent(Student student) {
            var sql = "UPDATE Student Set" +
            " FirstName = @FirstName, " + 
            " LastName = @LastName, " + 
            " SAT = @SAT, " + 
            " GPA = @GPA, " + 
            " MajorID = @MajorID " +
            " Where ID = @ID; "; //not interpolation because we are going ot use a parameterized query
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@ID", student.ID);
            command.Parameters.AddWithValue("@FirstName", student.FirstName);
            command.Parameters.AddWithValue("@LastName", student.LastName);
            command.Parameters.AddWithValue("@SAT", student.SAT);
            command.Parameters.AddWithValue("@GPA", student.GPA);
            command.Parameters.AddWithValue("@MajorID", student.MajorID ?? Convert.DBNull);
            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1) {
                throw new Exception("Update Failed");
            }
            return true;
        }

        public static bool DeleteStudent(Student student) {
            var sql = "DELETE from Student" +
                " Where ID = @ID ";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@ID", student.ID);
            var recsAffected = command.ExecuteNonQuery();
            if(recsAffected != 1) {
                throw new Exception("Delete Failed");
            }
            return true;
        }

        public static bool DeleteStudent(int id) {
            var student = GetStudentByPK(id);
            if(student == null) {
                return false;
            }
            DeleteStudent(student);
            return true;
        }

    }
}
