namespace Iso.ViewModels
{
    using System;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Iso.Services;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Views;
    using Xamarin.Essentials;
    using Xamarin.Forms;
    using ZXing.Net.Mobile.Forms;

    public class LoginViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private string company;
        private string password;
        private string user;
        private bool isRunning;
        private bool isEnabled;
        private bool isRefreshing;
        private string url;
        #endregion

        #region Properties
        public string Company
        {
            get { return this.company; }
            set { SetValue(ref this.company, value); }
        }

        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }

        public string Url
        {
            get { return this.url; }
            set { SetValue(ref this.url, value); }
        }

        public string User
        {
            get { return this.user; }
            set { SetValue(ref this.user, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public bool IsRemembered
        {
            get;
            set;
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        public bool IsRefreshing
        {
            get { return this.IsRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.apiService = new ApiService();

            this.Company = "10";
            this.User = "Pruebas";
            this.Password = "pbe";

            this.IsRemembered = true;
            this.isEnabled = true;

            /*this.Email = "carlos@gmail.com";
            this.Password = "1234";*/
        }
        #endregion

        #region Consumers
        /*
         * ====================
         * ||--((*[TODO]*))--||
         * ====================
         * 
         * Implementar:
         *** El envio de datos para el control de acceso [DONE]
         *** Escribir la URL para el GET y POST          [DONE]
         */

        public async void Login()
        {
            this.IsRefreshing = true;
            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            this.Url = "https://isoapi.azurewebsites.net/SISTEMA/Login/Acceso";

            var response = await apiService.AccessRegister(
                this.Url,
                this.Password,
                this.User,
                int.Parse(this.Company),
                DeviceInfo.Name);

            var obj = (JObject)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(response));
            Console.WriteLine(obj);

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            
            MainViewModel.GetInstance().Workspace = new WorkspaceViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new WorkspacePage());

            this.IsRunning = false;
            this.IsEnabled = true;

            this.Company = string.Empty;
            this.User = string.Empty;
            this.Password = string.Empty;
        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        public ICommand QRLoginCommand
        {
            get
            {
                return new RelayCommand(QRLogin);
            }
        }

        public ICommand ForgotPasswordCommand
        {
            get
            {
                return new RelayCommand(ForgotPassword);
            }
        }

        private async void ForgotPassword()
        {
            MainViewModel.GetInstance().ForgotPassword = new ForgotPasswordViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ForgotPage());

        }

        public void QRLogin()
        {
            var scannerPage = new ZXingScannerPage
            {
                Title = "Login"
            };
            scannerPage.OnScanResult += (result) =>
            {
                scannerPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage.Navigation.PopAsync();
                    Application.Current.MainPage.DisplayAlert(
                        "Valor Obtenido",
                        result.Text,
                        "OK");
                });
            };

            Application.Current.MainPage.Navigation.PushAsync(scannerPage);
        }
        #endregion
    }
    
}
