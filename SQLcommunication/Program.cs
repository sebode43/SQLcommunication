using System;
using SQLlibrary;

namespace SQLcommunication {
    class Program {
        static void Main(string[] args) {

            var sqllib = new BcConnection(); 
            sqllib.Connect(@"localhost\sqlexpress", "EdDb", "trusted_connection = true");
            StudentController.bcConnection = sqllib;
            MajorController.bcConnection = sqllib;
            ////////////////////////////////////////////////// all code must go in-between the Connect and Disconnect

            /*var newMajor = new Major {
                ID = 8,
                Description = "Engineering",
                MinSAT = 1200,
            };
            var insert = MajorController.InsertMajor(newMajor);*/

            var majors = MajorController.GetAllMajors();
            foreach (var major in majors) {
                Console.WriteLine(major);
            }

            var mpk = MajorController.GetMajorByPK(1);
            Console.WriteLine(mpk);

            //mpk.Description = "Computer Engineering";
            //var update = MajorController.UpdateMajor(mpk);

            //mpk.ID = 8;
            //var delete = MajorController.DeleteMajor(mpk);



            /* var newStudent = new Student {
                ID = 310,
                FirstName = "Gwen",
                LastName = "Clarkson",
                SAT = 1100,
                GPA = 3.65,
                MajorID = null
           };
            var success = StudentController.InsertStudent(newStudent); */



            var student = StudentController.GetStudentByPK(100);
            if (student == null) {
                Console.WriteLine("Student not found");
            } 
            else {
                Console.WriteLine(student);
            }

            //student.FirstName = "Blake";
            //student.LastName = "Legion";
            //var success = StudentController.UpdateStudent(student);

            //student.ID = 300;
            //var success = StudentController.DeleteStudent(student);
            //success = StudentController.DeleteStudent(380); //another way to delete
            
            var students = StudentController.GetAllStudents();
            foreach (var allstudent in students) {
                Console.WriteLine(allstudent);
            }
            ////////////////////////////////////////////////// all code must go in-between the Connect and Disconnect
            sqllib.Disconnect();

        }
    }
}
