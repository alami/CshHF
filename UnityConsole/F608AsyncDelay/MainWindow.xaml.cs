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

namespace F608AsyncDelay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int counter =0;
        public MainWindow()
        {
            InitializeComponent();
            txtClick.FontSize = 45;
            txtDealay.FontSize = 14;
        }

        private void BtnInc_Click(object sender, RoutedEventArgs e)
        {
            txtClick.Text = (++counter).ToString();
        }

        private async void BtnDelay_Click(object sender, RoutedEventArgs e)
        {
            txtDealay.Text = $"Задача будет завершена по истечению времени";
            BtnDelay.IsEnabled = false;
            await Task.Delay(5000);
            BtnDelay.IsEnabled = true;
            txtDealay.Text += Environment.NewLine;
            txtDealay.Text += $"Задача завершена!";

        }
    }
}
