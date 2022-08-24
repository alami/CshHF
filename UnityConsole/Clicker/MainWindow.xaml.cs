using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Clicker
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int counter;

        public MainWindow()
        {
            InitializeComponent();
            counter = 0;
            DispatcherSynchronizationContext 
        }

        private void BtnClick_Click(object sender, RoutedEventArgs e)
        {
            TxtClick.Text = Convert.ToString(++counter);
        }

        private async void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            var result = await Task.Run(() => DownloadString("http://microsoft.com"));
            TxtDownload.Text += result;
            /*try
            {
                TxtDownload.Text += //"http://microsoft.com";//
                                    DownloadString("http://microsoft.com");
            }
            catch (Exception ex)
            {
                TxtException.Text += ex.Message;
            }*/
        }

        private string DownloadString(string v)
        {
            //Thread.Sleep(5000);
            HttpClient httpClient = new HttpClient();
            return httpClient.GetStringAsync(v).Result;
        }
    }
}
