
namespace WinLogsParser.Model.Pages
{
    public class SourceImageRegister : INPropertyChanged
    {
        #region DataSource
        public readonly string ImagePerson = "pack://application:,,,/Resources/Images/Register/Person.png";
        public readonly string ImageEmail = "pack://application:,,,/Resources/Images/Register/Email.png";
        public readonly string ImagePassword = "pack://application:,,,/Resources/Images/Register/Password.png";
        public readonly string ImageExclamation = "pack://application:,,,/Resources/Images/Register/Exclamation.png";

        private string SourceImageFirstName_;
        public string SourceImageFirstName
        {
            get { return SourceImageFirstName_; }
            set { if (SourceImageFirstName_ != value) { SourceImageFirstName_ = value; OnPropertyChanged("SourceImageFirstName"); } }
        }

        private string SourceImageLastName_;
        public string SourceImageLastName
        {
            get { return SourceImageLastName_; }
            set { if (SourceImageLastName_ != value) { SourceImageLastName_ = value; OnPropertyChanged("SourceImageLastName"); } }
        }

        private string SourceImageEmail_;
        public string SourceImageEmail
        {
            get { return SourceImageEmail_; }
            set { if (SourceImageEmail_ != value) { SourceImageEmail_ = value; OnPropertyChanged("SourceImageEmail"); } }
        }

        private string SourceImagePassword_;
        public string SourceImagePassword
        {
            get { return SourceImagePassword_; }
            set { if (SourceImagePassword_ != value) { SourceImagePassword_ = value; OnPropertyChanged("SourceImagePassword"); } }
        }
        #endregion
    }
}
