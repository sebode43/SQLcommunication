using System;
using SQLlibrary;

namespace SQLcommunication {
    class Program {
        static void Main(string[] args) {

            var sqllib = new BcConnection();
            sqllib.Connect(@"localhost\sqlexpress", "EdDb", "trusted_connection = true");
            var student = new Student(sqllib);
            var students = student.GetAllStudents();
            sqllib.Disconnect();

        }
    }
}
