
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow
    {



        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
            
         
        }

        private void lv_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private bool stopToggle = true;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (stopToggle)
            {
                airwind.Source = new BitmapImage(new Uri(@"C:\Users\jongfeel.kim\Downloads\WpfApplication1\WpfApplication1\WpfApplication1\taskworld_bug.png"));
                airwindStoryboard.Stop();
            }
            else
            {
                airwind.Source = new BitmapImage(new Uri(@"C:\Users\jongfeel.kim\Downloads\WpfApplication1\WpfApplication1\WpfApplication1\GRT_청라국제도시역_버스정류장.png"));
                airwindStoryboard.Begin();
            }

            stopToggle = !stopToggle;
        }
    }





}
