﻿namespace Iso.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;
    using Views;

    public class LoginViewModel : BaseViewModel
    {
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

            this.IsRemembered = true;
            this.isEnabled = true;

            /*this.Email = "carlos@gmail.com";
            this.Password = "1234";*/
        }
        #endregion

        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        public async void Login()
        {
            var mainViewModel = MainViewModel.GetInstance();

            mainViewModel.ForgotPassword = new ForgotPasswordViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ForgotPage());

            this.IsRunning = false;
            this.IsEnabled = true;

            this.User = string.Empty;
            this.Password = string.Empty;
        }
    }
    
}