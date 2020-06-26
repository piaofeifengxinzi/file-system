using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1test.Model;
namespace App1test.Model
{
    class File
    {
        public string filename { set; get;}
        //0为文件夹，1为文件
        public int filetype { set; get; }
        public int filesize { set; get; }
        public string filepicture { set; get; }
    }
}
