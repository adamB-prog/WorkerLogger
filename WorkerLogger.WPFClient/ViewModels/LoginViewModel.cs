using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Input;
using WorkerLogger.Domain.Entities.Authentication;

namespace WorkerLogger.WPFClient.ViewModels
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string UserName { get; set; }

        public SecureString Password { get; set; }

        public ICommand LoginButton { get; set; }

        public ICommand RegisterButton { get; set; }

        public LoginViewModel() {
            LoginButton = new RelayCommand(async () =>
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5097");
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string password = new System.Net.NetworkCredential(string.Empty, Password).Password;

                var response = await client.PostAsJsonAsync<UserDataModel>("Auth/login", new UserDataModel()
                {
                    //ezeket a paramétereket küldjük el, itt a jelszó plaintextként van jelen
                    UserName = this.UserName,
                    Password = password

                });
                //ha nem kapunk tokent a szolgáltatástól, az azt jelenti, hogy nem megfelelő a felhasználónév, vagy ahhoz a jelszó
                try
                {
                    var token = await response.Content.ReadAsAsync<TokenModel>();

                    if (token != null)
                    {
                        new MainWindow(token).ShowDialog();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("A felhasználónév vagy jelszó nem megfelelő!");
                }
                

            });
            RegisterButton = new RelayCommand(async () =>
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5097");
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string password = new System.Net.NetworkCredential(string.Empty, Password).Password;

                var response = await client.PostAsJsonAsync<UserDataModel>("Auth/register", new UserDataModel()
                {
                    //ezeket a paramétereket küldjük el, itt a jelszó plaintextként van jelen
                    UserName = this.UserName,
                    Password = password

                });
                
            });
        }
    }
}
