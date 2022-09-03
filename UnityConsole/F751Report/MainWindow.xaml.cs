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

namespace F751Report
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static private string nL = $"{Environment.NewLine}";
        static private string nLnL = $"{Environment.NewLine}--------------------{Environment.NewLine}";
        private IProgress<int> progress;
        private CancellationTokenSource cts = new CancellationTokenSource();
        public MainWindow()
        {
            InitializeComponent();
            this.progress = new Progress<int>(ProgressBarUpdate);
        }

        private void ProgressBarUpdate(int value)
        {
            this.pb.Value = value;
        }

        private async void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            pb.Value = 0;
            btnStart.IsEnabled = false;

            Operation operation = new Operation();
            try
            {
                var nums = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                int sum = await operation.SumNumbersAsync(nums, this.cts.Token, this.progress);
                txtRes.Text += $"Операция завершена. Сумма чисел ({GetNumbers(nums)}) = {sum}{nLnL}";
            } catch (OperationCanceledException ex)
            {
                this.cts = new CancellationTokenSource();
                txtRes.Text += $"Операция отменена.{nLnL}";
            }
            catch (Exception ex)
            {
                string exceptionBody = $"Ошибка: {ex.GetType()}{nL}{ex.Message}";
                txtRes.Text += $"{exceptionBody}{nLnL}";
            }
            btnStart.IsEnabled = true;
        }

        private object GetNumbers(int[] numbers)
        {
            StringBuilder sb=new StringBuilder();
            foreach (int num in numbers)
            {
                sb.Append(num).Append(' ');
            }
            return sb.ToString().TrimEnd(' ');
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
        }
    }
}
