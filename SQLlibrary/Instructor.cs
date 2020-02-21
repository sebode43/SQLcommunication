using System;
using System.Collections.Generic;
using System.Text;

namespace SQLlibrary {
    public class Instructor {
        public int ID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int YearsExperience { get; set; }
        public bool IsTenured { get; set; }

        public override string ToString() {
            return $"{ID}. {Firstname} {Lastname} | {YearsExperience} | {IsTenured}";
        }


    }
}
