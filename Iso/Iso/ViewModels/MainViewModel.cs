namespace Iso.ViewModels
{
    public class MainViewModel
    {
        public LoginViewModel Login { get; set; }
        public ForgotPasswordViewModel ForgotPassword { get; set; }

        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
        }

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion
    }
}
