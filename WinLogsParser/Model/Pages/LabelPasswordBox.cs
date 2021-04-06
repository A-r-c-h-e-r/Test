
namespace WinLogsParser.Model.Pages
{
    public class LabelPasswordBox : INPropertyChanged
    {
        private string StrLabelPasswordBox_;
        public string StrLabelPasswordBox
        {
            get { return StrLabelPasswordBox_; }
            set { if (StrLabelPasswordBox_ != value) { StrLabelPasswordBox_ = value; OnPropertyChanged("StrLabelPasswordBox"); } }
        }
    }
}
