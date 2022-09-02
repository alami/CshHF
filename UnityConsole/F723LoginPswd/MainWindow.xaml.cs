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

namespace F723LoginPswd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource cts = new CancellationTokenSource();
        private string nLine = Environment.NewLine;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Authorize(object sender, RoutedEventArgs e)
        {
            txtRes.Text = string.Empty;
            btnEnter.IsEnabled = false;
            string login =txtLog.Text;
            string password =txtPass.Text;

            try
            {
                bool isAuthorize = await Task.Run(() => Authorize(login, password, cts.Token), cts.Token);
                if (isAuthorize)
                {
                    txtRes.Text += $"Добро пожаловать в систему, {login}!{nLine}";
                } 
            }catch (OperationCanceledException)
            {
                txtRes.Text += $"Отмена авторизации!{nLine}";
            } catch (Exception ex)
            {
                txtRes.Text += $"Ошибка! {ex.Message}{nLine}";
            } finally { 
                btnEnter.IsEnabled = true;
            }
        }

        private async Task<bool>? Authorize(string login, string password, CancellationToken cancellationToken)
        {
            List<string> passwords = new List<string>() { "admin","123","12345","adm","@dm1n"};
            if (!login.Equals("admin"))
            {
                throw new UnauthorizedAccessException("Неправильный логин");
            }
            foreach (var pass in passwords)
            {
                Thread.Sleep(1000);
                if (pass.Equals(password)) {
                    return true;
                }
                cancellationToken.ThrowIfCancellationRequested();
            }
            throw new UnauthorizedAccessException("Неправильный логин");
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            this.cts.Cancel();
            txtRes.Text += $"Система готова к повторной отмене до пересоздания?" +
                $" - {!this.cts.IsCancellationRequested}{nLine}";
            this.cts = new CancellationTokenSource();
            txtRes.Text += $"Система готова к повторной отмене после пересоздания?" +
                $" - {!this.cts.IsCancellationRequested}{nLine}";

        }
    }
}
