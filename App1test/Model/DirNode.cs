using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1test.Model
{
    class DirNode
    {
        public string name { get; set; }
        public int type { get; set; }
        public DirNode next { set; get; }
        public DirNode sub { set; get; }
        public DirNode father { set; get; }
        public int size { set; get; }
        public string content { set; get; }
    }
}
