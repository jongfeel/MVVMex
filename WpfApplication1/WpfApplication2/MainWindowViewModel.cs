using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApplication2
{

    public class TestCommand : ICommand
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

            add { CommandManager.RequerySuggested += value;
            
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
    

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public TestCommand ClickCommand
        {

            get;
            set;
           

        }
        public TestCommand ClickCommand2
        {

            get;
            set;


        }

        public MainWindowViewModel()
        {
            ClickCommand = new TestCommand(OnExcuteMethod,OnCanExcuteMethod);
            ClickCommand2 = new TestCommand(OnExcuteMethod2, OnCanExcuteMethod);
        }

        private void OnExcuteMethod(object p)
        {
            MessageBox.Show("1");
        }

        private void OnExcuteMethod2(object p)
        {
            MessageBox.Show("2");
        }

        private bool OnCanExcuteMethod(object p)
        {
            return true;
        }
        private string input;
        public string Input
        {

            get { return input; }
            set
            {
                input = value; OnPropertyChanged("Input"); // 이 이름으로 바인딩
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string p)
        {

            if (PropertyChanged != null)
            {

               
                PropertyChanged(this,new PropertyChangedEventArgs(p));

            }
        }
    }


}
