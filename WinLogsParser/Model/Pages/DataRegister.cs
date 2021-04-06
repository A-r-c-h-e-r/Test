
namespace WinLogsParser.Model.Pages
{
    public class DataRegister : INPropertyChanged
    { 
        #region DataRegister
        private string FirstName_;
        public string FirstName
        {
            get { return FirstName_; }
            set
            {
                if (FirstName_ != value)
                {
                    FirstName_ = value;
                   
                    OnPropertyChanged("FirstName");
                }
            }
        }

        private string LastName_;
        public string LastName
        {
            get { return LastName_; }
            set
            {
                if (LastName_ != value)
                {
                    LastName_ = value;
                    OnPropertyChanged("LastName");
                }
            }
        }

        private string Email_;
        public string Email
        {
            get { return Email_; }
            set
            {
                if (Email_ != value)
                {
                    Email_ = value;
                    OnPropertyChanged("Email");
                }
            }
        }

        private string Password_;
        public string Password
        {
            get { return Password_; }
            set
            {
                if (Password_ != value)
                {
                    Password_ = value;
                    OnPropertyChanged("Password");
                }
            }
        }
        #endregion
    }
}
