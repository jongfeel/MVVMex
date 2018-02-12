using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
namespace WpfApplication1
{

    public class TestCommand : ICommand // 커맨드 이벤트 어댑터
    {

        public delegate void CommandOnExecute(object p);
        public delegate bool CommandOnCanExecute(object p);
        private CommandOnExecute _execute;
        private CommandOnCanExecute _canExecute;
        public TestCommand(CommandOnExecute onExe, CommandOnCanExecute onCanExe)
        {

            _execute = onExe;
            _canExecute = onCanExe;

        }
        public event EventHandler CanExecuteChanged
        {

            add
            {
                CommandManager.RequerySuggested += value;

            }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object p)
        {

            return _canExecute(p);
        }

        public void Execute(object p)
        {
            _execute(p);

        }

    }
    public class FileProperty : ViewModelBase
    {
        public FileProperty()
        {
            FileNameCommand = new RelayCommand(OnExcuteMethod, OnCanExcuteMethod);
        }

        private void OnExcuteMethod()
        {
            MessageBox.Show("1");
        }

        private bool OnCanExcuteMethod()
        {
            return true;
        }

        private string filename;
        private string date;
        private string type;
        private int size;

        public string FileName
        {
            get { return filename; }
            set
            {

                if (filename != value)
                {
                    filename = value;
                    RaisePropertyChanged();
                }
            }

        }

        public string Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;

                    RaisePropertyChanged();
                }
            }
        }
        public string Type
        {

            get { return type; }
            set
            {
                if (type != value)
                {
                    type = value;
                    RaisePropertyChanged();
                }
            }

        }
        public int Size
        {
            get { return size; }
            set
            {
                if (size != value)
                {
                    size = value;
                    RaisePropertyChanged();
                }
            }
        }

        public RelayCommand FileNameCommand
        {
            get;
            set;
        }
    }

    public class ItemHandle
    {

        public ItemHandle()
        {
            Items = new List<FileProperty>();
        }

        public List<FileProperty> Items { get; set; }
        public void Add(FileProperty fileProperty)
        {
            Items.Add(fileProperty);
        }
    }


    public class MainWindowViewModel : ViewModelBase
    {
        string path = @"C:\";

        public RelayCommand RemoveCommand
        {
            get
            {
                return new RelayCommand(() => { Items.Clear(); });
            }
        }

        public RelayCommand FileNameClickCommand
        {
            get
            {
                return new RelayCommand(() => { MessageBox.Show("FileNameClickCommand"); });
            }
        }


        public MainWindowViewModel()
        {

            
          
            ReadDirAndFile();
        }

        private ObservableCollection<FileProperty> items;
        public ObservableCollection<FileProperty> Items
        {

            get
            {
                if (items == null)
                {
                    items = new ObservableCollection<FileProperty>();
                }
                return items;
            }
        }
        

        private string input;
        public string Input
        {

            get { return input; }
            set
            {
                input = value; RaisePropertyChanged();
            }

        }


        private void lv_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            //FileProperty selectedItem = null;
            //selectedItem = lv.SelectedItem as FileProperty;

            //if (lv.SelectedItems.Count == 1 && selectedItem != null)
            //{


            //    if (selectedItem.Type.Equals("Folder"))   // 폴더를 더블클릭한경우
            //    {

            //        lv.Items.Clear();

            //        //현재 디렉토리 가져와서 폴더 이름 붙혀서 경로에 있는 것들 출력

            //        path += selectedItem.FileName + '\\';

            //        DirectoryInfo dirInfo = new DirectoryInfo(path);

            //        ReadDirAndFile();

            //    }
            //    else if (selectedItem.Type.Equals("Below"))  //...을 더블클릭한경우
            //    {

            //        if (path.Equals(@"C:\")) return;

            //        lv.Items.Clear();

            //        //string path =  @"C:\Test\" + selectedItem.FileName;
            //        string[] splitedPath = path.Split('\\');
            //        path = "";

            //        for (int i = 0; i < splitedPath.Length - 2; i++)
            //            path += splitedPath[i] + "\\";


            //        //MessageBox.Show(path);

            //        ReadDirAndFile();


            //    }
            //    else // 파일을 더블클릭한경우
            //    {

            //    }


            //}

        }

        public void ReadDirAndFile()
        {

            //데이터 넣기
            Items.Add(new FileProperty { FileName = "...", Type = "Below" });

            DirectoryInfo dirInfo = new DirectoryInfo(path);

            FileInfo[] fileInfoes = dirInfo.GetFiles("*.*");
            DirectoryInfo[] dirInfoes = dirInfo.GetDirectories();

            foreach (DirectoryInfo fi in dirInfoes)
            {

                Items.Add(new FileProperty { FileName = fi.Name, Date = String.Format("{0:yy-MM-dd}", fi.LastWriteTime), Type = "Folder" });

            }
            //파일이름 나열
            foreach (FileInfo fi in fileInfoes)
            {

                string date = String.Format("{0:yy-MM-dd}", fi.LastWriteTime);
                int size = (int)fi.Length;

                Items.Add(new FileProperty { FileName = fi.Name, Date = String.Format("{0:yy-MM-dd}", fi.LastWriteTime), Type = "File" });

            }


        }



    }
}
