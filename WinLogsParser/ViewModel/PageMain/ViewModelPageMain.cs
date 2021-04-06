using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Data;
using WinLogsParser.Model.Pages;
using WinLogsParser.ViewModel.ParserWinLog;

namespace WinLogsParser.ViewModel.PageMain
{
    public class ViewModelPageMain : ViewModelMainWindow
    {
        #region Data
        public ListCollectionView ListCollectionWinLogs { get; set; }
        public List<WinLogData> ListWinLogData = new List<WinLogData>();
        public string LabelColumns { get; set; } = "Columns: ";
        public string LabelRows { get; set; } = "Rows: ";
        public string LogFileString { get; set; }
        #endregion

        #region Construct
        public ViewModelPageMain()
        {
            ListWinLogData = PesultLogFile.ParseFile(@"C:\test\ScanAgent.log");
            ListCollectionWinLogs = new ListCollectionView(ListWinLogData);
            LogFileString = new StreamReader(@"C:\test\ScanAgent.log").ReadToEnd();
            
            LabelColumns += 8; LabelRows += ListWinLogData.Count;
        }
        #endregion
    }
}
