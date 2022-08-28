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

namespace F604AsyncIO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HttpClient httpClient = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
            MonitorIOCP();
        }

        private async void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            //li.Visibility = Visibility.Visible;
            string httpContent = await httpClient.GetStringAsync("http://microsoft.com");
            //li.Visibility = Visibility.Hidden;
            txtOutput.Text = httpContent;

        }

        private void MonitorIOCP()
        {
            new Thread(() => {
                while (true)
                {
                    ThreadPool.GetAvailableThreads(out int threads, out int IOCP);
                    ThreadPool.GetMaxThreads(out int maxThreads, out int maxIOCP);
                    if (IOCP < maxIOCP)
                    {
                        Dispatcher.Invoke(() => {
                            txtIOCP.Text += $"IOCP[{IOCP}  из {maxIOCP}]{Environment.NewLine}";
                        });
                    }
                }
            })
            { IsBackground = true }.Start();
        }
    }
}
