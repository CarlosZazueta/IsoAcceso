namespace Iso.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;
    using Views;
    using Iso.Services;

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
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.apiService = new ApiService();

            this.IsRemembered = true;
            this.isEnabled = true;

            /*this.Email = "carlos@gmail.com";
            this.Password = "1234";*/
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

        public async void Login()
        {
            var data = await apiService.Get<string>("http://172.16.2.104/apiIso/sistema/login/acceso");
            await Application.Current.MainPage.DisplayAlert(
                "Hurray!",
                data,
                "Accept");
            await Application.Current.MainPage.Navigation.PopAsync();

            this.IsRunning = false;
            this.IsEnabled = true;

            this.Company = string.Empty;
            this.User = string.Empty;
            this.Password = string.Empty;
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
        #endregion
    }
    
}
