namespace WinLogsParser.Model
{
    public class SourcePages : INPropertyChanged
    {
     
        #region DataSourcePages
        private string source_ { get; set; }
        public string source
        {
            get { return source_; }
            set
            {
                if (source_ != value)
                {
                    source_ = value;
                    OnPropertyChanged("source");
                }
            }
        }
        #endregion
    }
}