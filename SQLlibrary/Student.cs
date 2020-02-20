using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SQLlibrary {
    public class Student {
        
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string  LastName { get; set; }
        public int SAT { get; set; }
        public double GPA { get; set; }
        public int? MajorID { get; set; }

        public override string ToString() {
            return $"{ID}. {FirstName} {LastName} | {SAT} | {GPA}";
        }

        public Student() {

        }

    }
}
