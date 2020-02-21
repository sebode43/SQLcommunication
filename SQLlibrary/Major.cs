using System;
using System.Collections.Generic;
using System.Text;

namespace SQLlibrary {
    public class Major {

        public int ID { get; set; }
        public string Description { get; set; }
        public int MinSAT { get; set; }

        public override string ToString() {
            return $"{ID}. {Description} | {MinSAT}";
        }

    }
}
