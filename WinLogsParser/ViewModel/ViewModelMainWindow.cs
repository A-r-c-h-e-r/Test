using WinLogsParser.Model;
using WinLogsParser.Model.Pages;
using WinLogsParser.ViewModel.InteractionDatabase;

namespace WinLogsParser.ViewModel
{
    public class ViewModelMainWindow
    {
        #region DataSourcePages
        public static DataUser ObjDataUser { get; set; }
        public static SourcePages ObjSourcePages { get; set; }
        public static string StartSourcePages { get; set; }
        #endregion

        #region Constructor
        static ViewModelMainWindow()
        {
            InteractionPostgreSQL.ConnectionString = "Server=localhost;Port=5432;Username=postgres;Password=2838787Danil;Database=WinLogsParser";
            string[] Result = InteractionPostgreSQL.AutoLogin();
            StartSourcePages = (Result[0] == "") ? "Pages/PageLogin.xaml" : "Pages/PageMain.xaml";
            ObjSourcePages = new SourcePages { source = StartSourcePages };
            ObjDataUser = (Result[0] != "") ? new DataUser { FirstName = Result[0], LastName = Result[1], Email = Result[2], Password = Result[3] } : new DataUser();
        }
        #endregion
    }
}
