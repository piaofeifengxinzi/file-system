using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1test.Model
{
    class DirNodeManager
    {
        private DirNode workDir;
        private DirNode root;
        private string path;
        private string output;
        private List<DirNode> dirnodes = new List<DirNode>();
        private ObservableCollection <File> files = new ObservableCollection<Model.File>();
        
        public void initial()
        {
            root = new DirNode();
            root.name = "root";
            root.type = 0;
            root.size = 0;
            workDir = root;
            path = "root";
        }

        public DirNode init()
        {
            var p = new DirNode();
            root.name = "";
            root.type = 0;
            root.next = null;
            root.sub = null;
            root.father = null;
            root.size = 0;
            return p;
        }

        public void BackDir()
        {
            workDir = workDir.father;
        }

        public void CD(string dirName)
        {
            //这里还有改进的空间，现在只能进入子节点，不能返回父节点
            var flag = 0;
            var p = workDir.sub;
            var f = workDir.father;
            if (p == null)
            {
                // cout<<"错误,\""<<dirName<<"\"子目录不存在"<<endl; 
                //window.alert("错误,\"" + dirName + "\"子目录不存在");
            }
            else
            {
                while (p != null)
                {
                    if (p.type == 0)
                    {
                        if (p.name == dirName)
                        {
                            flag = 1;
                            break;
                        }
                    }
                    p = p.next;
                }
                if (flag == 1)
                {
                    workDir = p;
                    path = path + "\\";
                    // strcat(path,"\\ "); 
                    path = path + p.name;
                    // strcat(path,p->name); 
                    //window.alert("工作目录已进入\"" + dirName + "\"");
                    //document.getElementById("fcfs-time").innerHTML = "当前路径：" + path;
                    // cout<<"工作目录已进入\""<<dirName<<"\""<<endl; 
                }
                else
                {
                    //window.alert("错误,\"" + dirName + "\"子目录不存在")
                   // cout<<"错误,\""<<dirName<<"\"子目录不存在"<<endl; 
            }
            }
        }

        public void CreateFile(string fileName, int fileSize, int type)
        {
            int flag;
            var q = new DirNode();
            q.name = fileName;
            q.sub = null;
            q.type = type;
            q.next = null;
            q.father = workDir;
            q.size = fileSize;
            var p = workDir.sub;
            if (p == null)
            {
                workDir.sub = q;
            }
            else
            {
                flag = 0;
                while (p != null)
                {
                    if (p.type == type)
                    {
                        if (p.name == fileName)
                        {
                            flag = 1;
                    }
                    }
                    p = p.next;
                }
                if (flag == 0)
                {
                    p = workDir.sub;
                    while (p.next!=null)
                    {
                        p = p.next;
                    }
                    p.next = q;
            }
            }
        }

        public void DeleteFile(string fileName, int type)
        {
            int flag = 0;
            var q = new DirNode();

            var p = workDir.sub;
            if (p == null)
            {
            }
            else
            {
                while (p != null)
                {
                    if (p.type == type)
                    {
                        if (p.name == fileName)
                        {
                            flag = 1;
                            break;
                        }
                    }
                    p = p.next;
                }
                if (flag == 1)
                {
                    if (p == workDir.sub)
                    {
                        workDir.sub = p.next;
                    }
                    else
                    {
                        q = workDir.sub;
                        while (q.next != p)
                        {
                            q = q.next;
                        }
                        q.next = p.next;
                    }
                }
                else
                {
            }
            }
        }

        public void MakeDir(string dirName)
        {
            int flag;
            var q = new DirNode();
            q.name = dirName;
            q.sub = null;
            q.type = 0;
            q.next = null;
            q.father = workDir;
            q.size = 0;
            var p = workDir.sub;
            if (p == null)
            {
                workDir.sub = q;
                //window.alert("\"" + dirName + "\"子目录创建成功");
                // cout<<"\""<<dirName<<"\"子目录创建成功"<<endl; 
            }
            else
            {
                flag = 0;
                while (p != null)
                {
                    if (p.type == 0)
                    {
                        if (p.name == dirName)
                        {
                            flag = 1;
                            //window.alert("错误,\"" + dirName + "\"子目录已存在");
                            // cout<<"错误,\""<<dirName<<"\"子目录已存在"<<endl; 
                        }
                    }
                    p = p.next;
                }
                if (flag == 0)
                {
                    p = workDir.sub;
                    while (p.next != null)
                    {
                        p = p.next;
                    }
                    p.next = q;
                    //window.alert("\"" + dirName + "\"子目录创建成功");
                    // cout<<"\""<<dirName<<"\"子目录创建成功"<<endl; 
                }
            }
        }

        public void RemoveDir(string dirName)
        {
            int flag = 0;
            var p = workDir.sub;
            var q = new DirNode();
            if (p == null)
            {
                //window.alert("错误,\"" + dirName + "\"子目录不存在");
                // cout<<"错误,\""<<dirName<<"\"子目录不存在"<<endl; 
            }
            else
            {
                while (p != null)
                {
                    if (p.type == 0)
                    {
                        if (p.name == dirName)
                        {
                            flag = 1;
                            break;
                        }
                    }
                    p = p.next;
                }
                if (flag == 1)
                {
                    if (p == workDir.sub)
                    {
                        workDir.sub = p.next;
                    }
                    else
                    {
                        q = workDir.sub;
                        while (q.next != p)
                        {
                            q = q.next;
                        }
                        q.next = p.next;
                        // delete p; 
                    }
                    //window.alert("\"" + dirName + "\"子目录已删除");
                    // cout<<"\""<<dirName<<"\"子目录已删除"<<endl; 
                }
                else
                {
                    //window.alert("错误,\"" + dirName + "\"子目录不存在");
                    // cout<<"错误,\""<<dirName<<"\"子目录不存在"<<endl; 
                }
            }
        }

        private void dir(DirNode p)
        {
            while (p != null)
            {
                if (p.type == 0)
                {
                    //这里要在网页上输出
                    // cout.setf(2); 
                    output += p.name + "___________" + "目录" + "<br>";
                    // cout<<setw(14)<<p.name<<setw(12)<<"<DIR>"<<endl; 
                }
                else
                {
                    // cout.setf(2); 
                    output += p.name + "____________" + "文件" + "____________" + p.size + "<br>";
                    // cout<<setw(14)<<p.name<<setw(12)<<"<FILE>"<<setw(10)<<p.size<<endl; 
                }
                dirnodes.Add(p);
                p = p.next;
                
            }
        }

        private void dirs(DirNode p, string str)
        {
            string newstr;
            DirNode q;
            //这里在网页上输出
            // cout<<str<<"下子目录及文件:"<<endl;
            output += str + "下子目录及文件:" + "<br>";
            output += "文件名" + "____________" + "类型" + "____________" + "大小" + "<br>";
            //这里调用了上面的显示函数
            dir(p);

            q = p;
            if (q.sub != null)
            {
                // strcpy(newstr,""); 
                newstr = "";
                // strcat(newstr,str); 
                newstr = newstr + str;
                // strcat(newstr,"\\");
                newstr = newstr + "\\";
                // strcat(newstr,q->name);
                newstr = newstr + q.name;
                //这里进行了递归 
                dirs(q.sub, newstr);
            }
            /*
            q = p;
            while (q.next != null)
            {
                if (q.next.sub != null)
                {
                    // strcpy(newstr,""); 
                    newstr = "";
                    // strcat(newstr,str); 
                    newstr = newstr + str;
                    // strcat(newstr,"\\");
                    newstr = newstr + "\\";
                    // strcat(newstr,q->next->name); 
                    newstr = newstr + q.next.name;
                    ;
                    dirs(q.next.sub, newstr);
                }
                q = q.next;
            }
            */
        }

        public void LSNOW(DirNode p, string dirname)
        {
            files.Clear();
            dirnodes.Clear();
            p = p.sub;
            if (p != null)
            {
                dirs(p, dirname);
            }
            else
            {
                //网页显示目录为空
                //window.alert("目录为空")
                // cout<<"目录为空"<<endl;
                dirnodes.Clear();
        }
        }

        public void ReName(string oldname, string newname)
        {
            DirNode now;
            now = workDir;
            now = now.sub;
            while(now != null)
            {
                if(now.name == oldname)
                {
                    now.name = newname;
                    break;
                }
                else
                {
                    //无此文件
                }
                now = now.next;
            }
        }

        public void SetContent(string name, string content)
        {
            DirNode now;
            now = workDir;
            now = now.sub;
            while (now != null)
            {
                if (now.name == name&&now.type == 1)
                {
                    now.content = content;
                    break;
                }
                else
                {
                    //无此文件
                }
                now = now.next;
            }
        }

        public string GetContent(string name)
        {
            DirNode now;
            now = workDir;
            now = now.sub;
            while (now != null)
            {
                if (now.name == name&&now.type == 1)
                {
                    return now.content;
                }
                else
                {
                    //无此文件
                }
                now = now.next;
            }
            return null;
        }

        public List<DirNode> GetNowDirs()
        {
            return dirnodes;
        }

        public ObservableCollection<File> GetNowFiles()
        {
            files.Clear();
            foreach(DirNode d in dirnodes)
            {
                File file = new File();
                file.filename = d.name;
                file.filetype = d.type;
                file.filesize = 0;
                if(d.type == 0)
                {
                    file.filepicture = "Assets/folder.png";
                }
                else if(d.type == 1)
                {
                    file.filepicture = "Assets/file_blank.png";
                    file.filesize = d.size;
                }
                else if (d.type == 2)
                {
                    file.filepicture = "Assets/file_zip.png";
                    file.filesize = d.size;
                }
                else if (d.type == 3)
                {
                    file.filepicture = "Assets/file_music.png";
                    file.filesize = d.size;
                }
                else if (d.type == 4)
                {
                    file.filepicture = "Assets/file_video.png";
                    file.filesize = d.size;
                }
                else if (d.type == 5)
                {
                    file.filepicture = "Assets/file_img.png";
                    file.filesize = d.size;
                }
                files.Add(file);
            }
            dirnodes.Clear();
            return files;
        }

        public DirNode GetWorkDir()
        {
            return workDir;
        }

      
    }
}
