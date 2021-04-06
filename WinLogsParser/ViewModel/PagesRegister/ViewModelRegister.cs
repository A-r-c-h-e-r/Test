using System.Windows.Controls;
using WinLogsParser.Model.Pages;
using WinLogsParser.ViewModel.InteractionDatabase;
using WinLogsParser.ViewModel.PagesRegister;

namespace WinLogsParser.ViewModel
{
    public class ViewModelRegister : ViewModelMainWindow
    {
        #region DataSourcePages
        public DataRegister ObjDataRegister { get; set; }
        public SourceImageRegister ObjSourceImageRegister { get; set; }
        public LabelPasswordBox ObjLabelPasswordBox { get; set; }
        #endregion

        #region Events
        private Command ClickNewPage_;
        public Command ClickNewPage =>
            ClickNewPage_ ??= new Command(obj => { CommandPageSource(); });

        private Command ClickSignUp_;
        public Command ClickSignUp =>
            ClickSignUp_ ??= new Command(obj => { CommandCheckSignUp(); });

        private Command ClickLogIn_;
        public Command ClickLogIn =>
            ClickLogIn_ ??= new Command(obj => { CommandCheckLogIn(); });

        private Command PasswordChanged_;
        public Command PasswordChanged =>
            PasswordChanged_ ??= new Command(obj => { CommandPasswordChanged(obj); });

        private Command TextChanged_;
        public Command TextChanged =>
            TextChanged_ ??= new Command(obj => { CommandTextChanged(); });

        private Command Exit_; 
        public Command Exit =>
            Exit_ ??= new Command(obj => { CommandExit(); });
        #endregion

        #region Constructor
        public ViewModelRegister()
        {
            ObjDataRegister = new DataRegister();
            ObjLabelPasswordBox = new LabelPasswordBox { StrLabelPasswordBox = "Password*" };
            ObjSourceImageRegister = new SourceImageRegister();
            ObjSourceImageRegister = new SourceImageRegister
            {
                SourceImageFirstName = ObjSourceImageRegister.ImagePerson,
                SourceImageLastName = ObjSourceImageRegister.ImagePerson,
                SourceImageEmail = ObjSourceImageRegister.ImageEmail,
                SourceImagePassword = ObjSourceImageRegister.ImagePassword
            };
        }
        #endregion

        #region MethodsEvents
        private static void CommandPageSource() =>
            ObjSourcePages.source = StartSourcePages = (StartSourcePages == "Pages/PageRegister.xaml") ? "Pages/PageLogin.xaml" : "Pages/PageRegister.xaml";

        private void CommandPasswordChanged(object parameter)
        {
            ObjDataRegister.Password = (parameter as PasswordBox).Password;
            ObjLabelPasswordBox.StrLabelPasswordBox = (ObjDataRegister.Password.Length > 0) ? "" : "Password*";
            ValidationDataImage();
        }

        private void CommandTextChanged() => ValidationDataImage();


        private static void CommandExit()
        {
            InteractionPostgreSQL.SetActiveFieldExit();
            ObjSourcePages.source = StartSourcePages = "Pages/PageLogin.xaml";
        }

        private void CommandCheckSignUp()
        {
            ValidationDataImage();
            if (ArrayAnalysis(ValidationDataCheck(false, false, false, false)) &&
                InteractionPostgreSQL.AddWhereEnd(ObjDataRegister.FirstName, ObjDataRegister.LastName, ObjDataRegister.Email, DataHashing.GetDataHashing(ObjDataRegister.Password)))
                CompletionEntry();
        }

        private void CommandCheckLogIn()
        {
            ValidationDataImage();
            if (ArrayAnalysis(ValidationDataCheck(false, false)) && InteractionPostgreSQL.SetActiveFieldEnter(ObjDataRegister.FirstName, DataHashing.GetDataHashing(ObjDataRegister.Password)))
                CompletionEntry();
        }
        #endregion

        #region Methods
        private void CompletionEntry()
        {
            ObjDataRegister.Password = DataHashing.GetDataHashing(ObjDataRegister.Password);
            ObjSourcePages.source = StartSourcePages = "Pages/PageMain.xaml";
            string[] Result = InteractionPostgreSQL.AutoLogin();
            ObjDataUser = (Result[0] != "") ? new DataUser { FirstName = Result[0], LastName = Result[1], Email = Result[2], Password = Result[3] } : new DataUser();
            ObjDataRegister.FirstName = ObjDataRegister.LastName = ObjDataRegister.Email = ObjDataRegister.Password = "";
        }

        private void ValidationDataImage()
        {
            ObjSourceImageRegister.SourceImageFirstName = ValidationDataRegister.CheckNickname(ObjDataRegister.FirstName) ? ObjSourceImageRegister.ImagePerson : ObjSourceImageRegister.ImageExclamation;
            ObjSourceImageRegister.SourceImageLastName = ValidationDataRegister.CheckName(ObjDataRegister.LastName) ? ObjSourceImageRegister.ImagePerson : ObjSourceImageRegister.ImageExclamation;
            ObjSourceImageRegister.SourceImageEmail = ValidationDataRegister.CheckEmail(ObjDataRegister.Email) ? ObjSourceImageRegister.ImageEmail : ObjSourceImageRegister.ImageExclamation;
            ObjSourceImageRegister.SourceImagePassword = ValidationDataRegister.CheckPassword(ObjDataRegister.Password) ? ObjSourceImageRegister.ImagePassword : ObjSourceImageRegister.ImageExclamation;
        }

        private bool[] ValidationDataCheck(params bool[] CheckParams)
        {
            bool[] ValidationCheck = CheckParams;

            if (ValidationCheck.Length == 2)
            {
                ValidationCheck[0] = ValidationDataRegister.CheckNickname(ObjDataRegister.FirstName);
                ValidationCheck[1] = ValidationDataRegister.CheckPassword(ObjDataRegister.Password);
            }
            else  if (ValidationCheck.Length == 4)
            {
                ValidationCheck[0] = ValidationDataRegister.CheckNickname(ObjDataRegister.FirstName);
                ValidationCheck[1] = ValidationDataRegister.CheckPassword(ObjDataRegister.Password);
                ValidationCheck[2] = ValidationDataRegister.CheckName(ObjDataRegister.LastName);
                ValidationCheck[3] = ValidationDataRegister.CheckEmail(ObjDataRegister.Email);
            }
           
            return ValidationCheck;
        }

        private bool ArrayAnalysis (bool[] Array)
        {
            foreach (bool check in Array) 
                if (!check) return false;
            return true;
        }
        #endregion
    }
}