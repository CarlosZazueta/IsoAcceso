namespace Iso.ViewModels
{
    using Iso.Models;

    public class MainViewModel
    {
        #region ViewModels
        public LoginViewModel Login { get; set; }
        public ForgotPasswordViewModel ForgotPassword { get; set; }
        public WorkspaceViewModel Workspace { get; set; }
        #endregion

        #region Properties
        public User SingleUser { get; set; }
        #endregion

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
