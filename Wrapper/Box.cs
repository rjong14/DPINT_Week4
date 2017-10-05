using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain {
    public class Box {
        public Box () {
            X=-1;
            Y=-1;
            Value=-1;
        }

        public short X { get; set; }

        public short Y { get; set; }

        public short Value { get; set; }

        public bool IsEditable { get; set; }
    }
}
