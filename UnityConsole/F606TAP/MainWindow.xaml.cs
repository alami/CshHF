using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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

namespace F606TAP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string requestUrl = "http://microsoft.com";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Apm_Click(object sender, RoutedEventArgs e)
        {
            WebRequest webRequest = WebRequest.Create(requestUrl);
            webRequest.BeginGetResponse((IAsyncResult) => 
            {
                var webResponse = webRequest.EndGetResponse(IAsyncResult);
                using (var reader = new StreamReader(webResponse.GetResponseStream()))
                {
                    Dispatcher.Invoke(() => txtAPMResult.Text = reader.ReadToEnd());
                }
            },null);
        }

        private void Eap_Click(object sender, RoutedEventArgs e)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += WebClient_DownloadStringCompleted;
            webClient.DownloadStringAsync(new Uri(requestUrl));
        }

        private void WebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            txtEAPResult.Text = e.Result;
        }

        private async void Tap_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            var result = await httpClient.GetStringAsync(requestUrl);
            txtTAPResult.Text = result;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            txtAPMResult.Clear();   
            txtEAPResult.Clear();
            txtTAPResult.Clear();
        }
    }
}
