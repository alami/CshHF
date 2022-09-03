using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace F742Deadlock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            //var result = DownloadAsync().Result;
            //var result = Task.Run (() => DownloadAsync().Result).Result;
            var result = await DownloadAsync();

            txtResult.Text = result;
        }

        private async Task<string> DownloadAsync()
        {
            var task = Task.Run(() => "Загружено");//.ConfigureAwait(false);
            string result = await task;
            return result;
        }
    }
}
