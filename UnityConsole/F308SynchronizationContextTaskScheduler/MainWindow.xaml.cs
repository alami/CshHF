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

namespace F308SynchronizationContextTaskScheduler
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool flag=false;
        public MainWindow()
        {
            InitializeComponent();
            lblThreadId.Content += $" #{Thread.CurrentThread.ManagedThreadId}";
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (flag) return;
            HandlerSetup();
            new Task(() => { 
                while (flag)
                {
                    ShowThreadPoolInfo();
                    Thread.Sleep(300);
                }
            }, TaskCreationOptions.LongRunning).Start();
            Worker();
        }



        private void HandlerSetup()
        {
            txtContinuations.Text = string.Empty;
            txtThreadPool.Text = string.Empty;
            flag = true;
        }
        private void ShowThreadPoolInfo()
        {
            ThreadPool.GetAvailableThreads(out int threads, out int completionPorts);
            ThreadPool.GetMaxThreads(out int maxThreads, out int maxCompletionPorts);
            string result = $"W[{threads}:{maxThreads}] IO[{completionPorts}:{maxCompletionPorts}] {Environment.NewLine}";
            Dispatcher.Invoke((()=>txtThreadPool.Text += result));
        }
        private void Worker()
        {
            TaskScheduler scheduler = //new SynchronizationContextTaskScheduler();
                                TaskScheduler.FromCurrentSynchronizationContext();
            Task<int>[] tasks = new Task<int>[20];

            new Task(() =>
            {
                for (int i = 0; i < tasks.Length; i++)
                {
                    int m = i;
                    tasks[i] = new Task<int>(() =>
                    {
                        Thread.Sleep(1000);
                        return m;
                    });
                    tasks[i].Start();
                    tasks[i].ContinueWith((t) =>
                    {
                        txtContinuations.Text += $"Результат - {t.Result} в потоке [{Thread.CurrentThread.ManagedThreadId}]";
                        txtContinuations.Text += Environment.NewLine;
                    }, scheduler);
                }
                Task.WaitAll(tasks);
                Thread.Sleep(2000);
                flag = false;
            },TaskCreationOptions.LongRunning).Start();
        }
    }
}
