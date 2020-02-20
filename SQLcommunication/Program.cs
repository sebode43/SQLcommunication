using System;
using SQLlibrary;

namespace SQLcommunication {
    class Program {
        static void Main(string[] args) {

            var sqllib = new BcConnection(); 
            sqllib.Connect(@"localhost\sqlexpress", "EdDb", "trusted_connection = true");
            StudentController.bcConnection = sqllib;
            ////////////////////////////////////////////////// all code must go in-between the Connect and Disconnect

            var newStudent = new Student {
                ID = 380,
                FirstName = "Kelly",
                LastName = "Hudson",
                SAT = 1200,
                GPA = 3.75,
                MajorID = null
            };
            var success = StudentController.InsertStudent(newStudent);

            var student = StudentController.GetStudentByPK(100);
            if (student == null) {
                Console.WriteLine("Student not found");
            } 
            else {
                Console.WriteLine(student);
            }
            
            var students = StudentController.GetAllStudents();
            foreach (var allstudent in students) {
                Console.WriteLine(allstudent);
            }
            ////////////////////////////////////////////////// all code must go in-between the Connect and Disconnect
            sqllib.Disconnect();

        }
    }
}
