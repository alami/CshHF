using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace F603AsyncIO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            File.Create("text.txt").Dispose();
            MonitorIOCP();
        }


        private async void BtnRead_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = new FileStream("text.txt", FileMode.Open,
                FileAccess.Read, FileShare.ReadWrite, 4096, FileOptions.Asynchronous))
            {
                //liRead.Visibility = Visibility.Visible;

                byte[] bytes = new byte[fs.Length];
                await fs.ReadAsync(bytes, 0, bytes.Length);
                string fileContent = Encoding.UTF8.GetString(bytes);

                //liRead.Visibility = Visibility.Hidden;
                txtOutput.Text = fileContent;
            }
        }

        private async void BtnWrite_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream fs = new FileStream("text.txt", FileMode.Open,
                FileAccess.Write, FileShare.ReadWrite, 4096, FileOptions.Asynchronous))
            {
                //liWrite.Visibility = Visibility.Visible;

                string textBoxContent = txtOutput.Text;
                byte[] bytes = Encoding.UTF8.GetBytes(textBoxContent);
                await fs.WriteAsync(bytes, 0, bytes.Length);

                //liWrite.Visibility = Visibility.Hidden;
                txtOutput.Clear();
            }
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
