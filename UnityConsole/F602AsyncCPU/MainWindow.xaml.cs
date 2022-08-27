using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace F602AsyncCPU
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtOutput.Text += $"Суммирование всех чисел 3 500 000 000 должно равняиться числу 6 124 999 998 250 000 000,00" +
                $"{Environment.NewLine}";
        }

        private void BtnStartSync_Click(object sender, RoutedEventArgs e)
        {
            BtnStartSync.IsEnabled = false;
            Stopwatch timer = Stopwatch.StartNew();
            long result = SumSequence(to:3_500_000_000);
            txtOutput.Text += $"[Synchronous ] Результат операции = {result:N}. " +
                $"Результат готов за {timer.Elapsed.Seconds} секунд и {timer.Elapsed.Milliseconds} mcek" +
                $"{Environment.NewLine}";
            BtnStartSync.IsEnabled = true;
        }

        private long SumSequence(long from = 0,long to = 0)
        {
            long sum = 0;
            for (long i = 0; i < to; i++) sum += i;
            return sum;
        }

        private async void BtnStartAsync_Click(object sender, RoutedEventArgs e)
        {
            BtnStartAsync.IsEnabled = false;
            Stopwatch timer = Stopwatch.StartNew();
            long result = await ParallelSumSequenceAsync(3_500_000_000);
            txtOutput.Text += $"[Async&Parallel ] Результат операции = {result:N}. " +
                $"Результат готов за {timer.Elapsed.Seconds} секунд и {timer.Elapsed.Milliseconds} mcek" +
                $"{Environment.NewLine}";
            BtnStartAsync.IsEnabled = true;

        }

        private async Task<long> ParallelSumSequenceAsync(long ceiling)
        {            
            Task<long> computeTask1 = Task<long>.Run(()=> SumSequence(to: ceiling/2));
            Task<long> computeTask2 = Task<long>.Run(()=> SumSequence(ceiling/2, ceiling));
            long compute = await computeTask1 + await computeTask2;
            return compute;
        }
    }
}
