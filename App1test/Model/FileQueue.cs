using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1test.Model
{
    class FileQueue
    {
        private static List<File> files = new List<File>();
        public static void MakeFile()
        {
            for (int i = 0; i<100; i++)
            {
                File file = new File();
                file.filename = "fuck" + i;
                file.filetype = i % 5;
                if(file.filetype == 0)
                {
                    file.filepicture = "Assets/folder.png";
                }
                else
                {
                    file.filepicture = "Assets/file_blank.png";
                    file.filesize = i;
                }
                files.Add(file);
            }
        }

        public static List<File> GetFiles()
        {
            return files;
        }
    }
}
