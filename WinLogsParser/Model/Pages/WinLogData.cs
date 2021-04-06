
namespace WinLogsParser.Model.Pages
{
    public class WinLogData : INPropertyChanged
    {
        #region Data
        private string LogName_, Time_, Date_, Component_, Context_, Type_, Theard_, File_;

        public string LogName { get { return LogName_; } set { LogName_ = value; OnPropertyChanged("LogName"); } }
        public string Time { get { return Time_; } set { Time_ = value; OnPropertyChanged("Time"); } }        
        public string Date { get { return Date_; } set { Date_ = value; OnPropertyChanged("Date"); } }
        public string Component { get { return Component_; } set { Component_ = value; OnPropertyChanged("Component"); } }
        public string Context { get { return Context_; } set { Context_ = value; OnPropertyChanged("Context"); } }
        public string Type { get { return Type_; } set { Type_ = value; OnPropertyChanged("Type"); } }
        public string Theard { get { return Theard_; } set { Theard_ = value; OnPropertyChanged("Theard"); } }
        public string File { get { return File_; } set { File_ = value; OnPropertyChanged("File"); } }
        #endregion
    }
}
