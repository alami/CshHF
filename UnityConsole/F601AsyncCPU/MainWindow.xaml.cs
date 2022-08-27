using System;
using System.Collections.Generic;
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

namespace F601AsyncCPU
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

        private async void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            //Sync var of call
            //int result = SumItems(2, 2, 2, 5, 4);
            //Async var of call
            Task<int> task = Task.Run(()=> SumItems(2, 2, 2, 5, 4));

            //Main CPU code
            int result = await task;
            txtOutput.Text += $"Сумма последовательности = {result}{Environment.NewLine}";
        }

        private static int SumItems(params int[] items)
        {
            int sum = 0;
            for(int i = 0; i < items.Length; i++)
            {
                sum+=items[i];
                Thread.Sleep(1000);
            }
           return sum;
        }
    }
}
