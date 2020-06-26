using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using App1test.Model;
using System.Collections.ObjectModel;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace App1test
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Model.File> files;
        private DirNodeManager dirNodeManager = new DirNodeManager();
        private Model.File nowfile;
        private int nr = 0;
        public MainPage()
        {
            this.InitializeComponent();
            dirNodeManager.initial();
            AddBaseSomeFiles();
            DirNode d = dirNodeManager.GetWorkDir();
            string name = dirNodeManager.GetWorkDir().name;
            dirNodeManager.LSNOW(d, name);
            files = dirNodeManager.GetNowFiles();
            DirNode dn = dirNodeManager.GetWorkDir();
            nowfile = new Model.File();
            nowfile.filename = dn.name;
            nowfile.filetype = dn.type;
            nowfile.filesize = dn.size;
            nowfile.filepicture = "Assets/folder.png";

        }
        //选中得到当前的file名
        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            nowfile = (Model.File)e.ClickedItem;
        }

        //先左键，再右键，调出当前的菜单
        private void Grid_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element != null) FlyoutBase.ShowAttachedFlyout(element);
        }

        //信息弹窗
        private async void ShowMessageDialog(string content)
        {
            var msgDialog = new Windows.UI.Popups.MessageDialog(content) { Title = "提示标题" };
            await msgDialog.ShowAsync();
        }

        //删除选中的文件（夹）
        private void MenuFlyoutItem_Click_Delete(object sender, RoutedEventArgs e)
        {
            //ShowMessageDialog("删除当前");
            if (nowfile.filetype == 0)
            {
                dirNodeManager.RemoveDir(nowfile.filename);

                DirNode d = dirNodeManager.GetWorkDir();
                string name = dirNodeManager.GetWorkDir().name;
                files.Clear();
                dirNodeManager.LSNOW(d, name);
                files = dirNodeManager.GetNowFiles();
            }
            else
            {
                dirNodeManager.DeleteFile(nowfile.filename,nowfile.filetype);
                DirNode d = dirNodeManager.GetWorkDir();
                string name = dirNodeManager.GetWorkDir().name;
                files.Clear();
                dirNodeManager.LSNOW(d, name);
                files = dirNodeManager.GetNowFiles();
                ShowMessageDialog("已删除文件" + nowfile.filename + "\n\n文件大小：" + nowfile.filesize);
            }
        }



        //重命名
        private async void MenuFlyoutItem_Click_ReName(object sender, RoutedEventArgs e)
        {
            nr = 1;
            if(nowfile.filetype == 0)
            {
                TitleName.Text = "重命名文件夹";
                TitleName.Text = "文件夹名";
            }
            else
            {
                TitleName.Text = "重命名文件";
                TitleName.Text = "文件名";
            }
            await InPutDialog.ShowAsync();
        }



        //双击打开进入，会先调用单击得到当前点击的变量
        private async void Grid_DoubleTappedAsync(object sender, DoubleTappedRoutedEventArgs e)
        {
            if(nowfile.filetype == 0)
            {
                dirNodeManager.CD(nowfile.filename);
                DirNode d = dirNodeManager.GetWorkDir();
                string name = dirNodeManager.GetWorkDir().name;
                files.Clear();
                dirNodeManager.LSNOW(d, name);
                files = dirNodeManager.GetNowFiles();
            }
            else
            {
                InPutContent.Text = dirNodeManager.GetContent(nowfile.filename);
                await ContentDialog.ShowAsync();
                ShowMessageDialog("已打开文件" + nowfile.filename + "\n\n文件大小：" + nowfile.filesize);
            }
         
        }


        //返回父目录
        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            if(nowfile.filename != "root")
            {
                dirNodeManager.BackDir();

                DirNode d = dirNodeManager.GetWorkDir();
                string name = dirNodeManager.GetWorkDir().name;
                files.Clear();
                dirNodeManager.LSNOW(d, name);
                files = dirNodeManager.GetNowFiles();
                DirNode dn = dirNodeManager.GetWorkDir();
                nowfile.filename = dn.name;
                nowfile.filetype = dn.type;
                nowfile.filesize = dn.size;
                nowfile.filepicture = "Assets/folder.png";
            }
            else
            {
                ShowMessageDialog("当前在根目录");
            }
        }



        //在当前文件夹下创建文件
        private async void MenuFlyoutItem_Click_CreateFile(object sender, RoutedEventArgs e)
        {
            nr = 3;
            InPutName.Text = "";
            TitleName.Text = "输入文件名";
            TitleName.Text = "文件名";
            await InPutDialog.ShowAsync();
           
        }

        //在当前文件夹下创建一个文件夹
        private async void MenuFlyoutItem_Click_CreateDir(object sender, RoutedEventArgs e)
        {
            nr = 2;
            InPutName.Text = "";
            TitleName.Text = "输入文件夹名";
            TitleName.Text = "文件夹名";
            await InPutDialog.ShowAsync();
        }


        //初始在根目录添加一些文件（夹）
        public void AddBaseSomeFiles()
        {
            Random ran = new Random();
            int n = ran.Next(2);
            int size = 0;
            for (int i = 0; i < 15; i++)
            {
                n = ran.Next(6);
                
                if (n == 0)
                {
                    dirNodeManager.MakeDir("dir" + i);
                }
                else if (n == 1)
                {
                    size = ran.Next(100);
                    dirNodeManager.CreateFile("file" + i, size, n);
                }
                else if (n == 2)
                {
                    size = ran.Next(100);
                    dirNodeManager.CreateFile("file" + i, size, n);
                }
                else if (n == 3)
                {
                    size = ran.Next(100);
                    dirNodeManager.CreateFile("file" + i, size, n);
                }
                else if (n == 4)
                {
                    size = ran.Next(100);
                    dirNodeManager.CreateFile("file" + i, size, n);
                }
                else if (n == 5)
                {
                    size = ran.Next(100);
                    dirNodeManager.CreateFile("file" + i, size, n);
                }
            }

        }



        //名命比较重要的函数
        private void Name_ReName(object sender, RoutedEventArgs e)
        {
            string name;
            if (nr == 1)
            {
                string newname = InPutName.Text;
                string oldname = nowfile.filename;
                int flags = 0;
                while (true)
                {
                    if (files.Count() == 0)
                    {
                        break;
                    }
                    foreach (Model.File file in files)
                    {
                        if (file.filename == newname && file.filetype == nowfile.filetype)
                        {
                            ShowMessageDialog("文件(夹)名重复, 请重新输入");
                        }
                    }
                    dirNodeManager.ReName(oldname, newname);
                    flags = 1;
                    if (flags == 1)
                    {
                        break;
                    }
                }
            }
            else if(nr ==2 )
            {
                Random ra = new Random();
                int n = ra.Next(100);
                name = InPutName.Text;
                if(name==""||name == null)
                {
                    ShowMessageDialog("请输入文件名");
                    return;
                }
                int flags = 0;
                int size = ra.Next(100);
                while (true)
                {
                    if (files.Count() == 0)
                    {
                        dirNodeManager.MakeDir(name);
                        break;
                    }
                    foreach (Model.File file in files)
                    {
                        if (file.filename == name && file.filetype == 0)
                        {
                            ShowMessageDialog("文件（夹）名重复");
                            flags = 1;
                            return;
                        }
                    }
                    dirNodeManager.MakeDir(name);
                    flags = 1;
                    if (flags == 1)
                    {
                        break;
                    }
                }
                
            }else if(nr == 3)
            {
                Random ra = new Random();
                int n = ra.Next(100);
                name = InPutName.Text;
                if (name == "" || name == null)
                {
                    ShowMessageDialog("请输入文件名");
                    return;
                }
                int flags = 0;
                int size = ra.Next(100);
                int type = ra.Next(4);
                while (true)
                {
                    if (files.Count() == 0)
                    {
                        dirNodeManager.CreateFile(name, size,type+1);
                        break;
                    }
                    foreach (Model.File file in files)
                    {
                        if (file.filename == name && file.filetype == type+1)
                        {
                            ShowMessageDialog("文件名重复");
                            flags = 1;
                            return;
                        }
                    }
                    dirNodeManager.CreateFile(name, size, type + 1);
                    flags = 1;
                    if (flags == 1)
                    {
                        break;
                    }
                } 
            }
            DirNode d = dirNodeManager.GetWorkDir();
            string name1 = dirNodeManager.GetWorkDir().name;
            files.Clear();
            dirNodeManager.LSNOW(d, name1);
            files = dirNodeManager.GetNowFiles();
            InPutDialog.Hide();
        }

        private void Cancle_Click(object sender, RoutedEventArgs e)
        {
            InPutDialog.Hide();
        }

        private void Bapcun_Click(object sender, RoutedEventArgs e)
        {
            dirNodeManager.SetContent(nowfile.filename, InPutContent.Text);
            ContentDialog.Hide();
        }

        private void Cancle_Click_1(object sender, RoutedEventArgs e)
        {
            ContentDialog.Hide();
        }
    }
}
