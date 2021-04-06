using System.Collections.Generic;
using System.Text.Json;
using WinLogsParser.Model.Pages;

namespace WinLogsParser.ViewModel.ParserWinLog
{
    public static class PesultLogFile
    {
        public static List<WinLogData> ParseFile(string file)
        {
            List<string[]> ListArrayWinData = ParseWinLogFile.ParseFile(@file);
            List<WinLogData> JsonWinLogData = new List<WinLogData>(); 

            foreach (string[] Data in ListArrayWinData)
            {
                WinLogData WinLogData_ = new WinLogData
                {
                    LogName = Data[0], Time = Data[1],  Date = Data[2], Component = Data[3],
                    Context = Data[4], Type = Data[5], Theard = Data[6], File = Data[7]
                };
                JsonWinLogData.Add(WinLogData_);
            }
            return JsonWinLogData;
        }
    }
}
