﻿using System;
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

namespace F609AsyncWaitAll
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random random = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            txtOutput.Text += new string('-', 35);
            txtOutput.Text += Environment.NewLine;

            Task<string> t1 = OperationAsync(random.Next(400, 6000), 1);
            Task<string> t2 = OperationAsync(random.Next(400, 6000), 2);
            Task<string> t3 = OperationAsync(random.Next(400, 6000), 3);
            Task<string> t4 = OperationAsync(random.Next(400, 6000), 4);
            Task<string> t5 = OperationAsync(random.Next(400, 6000), 5);
            Task<string> t6 = OperationAsync(random.Next(400, 6000), 6);

            BtnStart.IsEnabled = false;
            //string[] results = await Task.WhenAll(t1, t2, t3, t4, t5, t6);
            Task<string> result = await Task.WhenAny(t1, t2, t3, t4, t5, t6);
            BtnStart.IsEnabled = true;
            /*foreach (var result in results)
            {
                txtOutput.Text += result;
                txtOutput.Text += Environment.NewLine;
            }
            txtOutput.Text += new string('-', 35);*/
            txtOutput.Text += await result;
            txtOutput.Text += Environment.NewLine;
        }

        private async Task<string> OperationAsync(int delay, int number)
        {
            await Task.Delay(delay);
            return $"Задача #{number} завершена после {delay} секунд ожидания.";
        }
    }
}
