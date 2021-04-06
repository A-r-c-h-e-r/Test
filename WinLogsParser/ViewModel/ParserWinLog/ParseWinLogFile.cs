using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinLogsParser.ViewModel.ParserWinLog
{
    public static class ParseWinLogFile
    {
        #region Data
        private static readonly string[] parseLOGArr = { "<!", "!>", "[", "]", "LOG", "LOG", "[", "]" };
        private static readonly string[] parseINFOArrKeyWord = { "time", "date", "component", "context", "type", "thread", "file" };
        #endregion

        #region Methods
        //! Получить индекс строки где был найден искомый участок (ищет с начала)
        private static int GetSearchIndexInitially(string data, string where, bool from)
        {
            for (int i = 0; i < where.Length; i++) data += " ";
            for (int i = 0; i < data.Length - where.Length; i++)
                if (data.Substring(i, where.Length) == where)
                    return (from) ? i + where.Length : i;
            return -1;
        }

        //! Получить индекс строки где был найден искомый участок (ищет с начала или с конца в зависимости от "bool from")
        private static int GetSearchIndex(string data, string where, bool from)
        {
            for (int i = 0; i < where.Length; i++) data += " ";

            if (where == parseLOGArr[0] || where == parseLOGArr[1]) return GetSearchIndexInitially(data, where, from);
            else if (from == true) //! Поиск с начала
            {
                for (int i = 0; i < data.Length - where.Length; i++)
                    if (data.Substring(i, where.Length) == where)
                        return i + where.Length;
            }
            else if (from == false) //! Поиск с конца
            {
                for (int i = data.Length - where.Length; i >= 0; i--)
                    if (data.Substring(i, where.Length) == where)
                        return i;
            }
            return -1;
        }

        //! Парсинг части лог сктроки в котором имеется "LOG", этот метод достаёт данные с "LOG"
        private static string ParseLOGString(string data, int count = 0)
        {
            int indexFrom = (GetSearchIndex(data, parseLOGArr[count], true));
            int indexTo = (GetSearchIndex(data, parseLOGArr[count + 1], false));
            if (!(indexFrom == -1 || indexTo == -1))
                return (count == 6) ? data.Substring(indexFrom, indexTo - indexFrom) : ParseLOGString(data.Substring(indexFrom, indexTo - indexFrom), count + 2);
            else return "null";
        }

        //! Парсинг части информации лог сктроки (то что в < >)
        private static string[] ParseLOGInfoString(string data)
        {
            string[] ArrayWinLogDataInfo = new string[parseINFOArrKeyWord.Length];
            int indexFrom = (GetSearchIndexInitially(data, "<", true));
            int indexTo = (GetSearchIndexInitially(data, ">", false));

            string dataInfo = "";
            try { dataInfo = data.Substring(indexFrom, indexTo - indexFrom); }
            catch { for (int i = 0; i < ArrayWinLogDataInfo.Length; i++) ArrayWinLogDataInfo[i] = "null"; return ArrayWinLogDataInfo; }

            for (int i = 0; i < ArrayWinLogDataInfo.Length; i++)
            {
                bool checkEquals = false;
                int startIndex = GetSearchIndex(dataInfo, parseINFOArrKeyWord[i], true);

                if (dataInfo.Length < 1 || startIndex == -1) { ArrayWinLogDataInfo[i] = "null"; continue; }

                for (int j = startIndex; j < dataInfo.Length; j++)
                    if (dataInfo.Substring(j, 1) == "=") { checkEquals = true; break; }
                    else if (dataInfo.Substring(j, 1) == "\"") break;

                if (checkEquals)
                {
                    int indexFromQuotes = -1, indexToQuotes = -1, count = 0;
                    for (int j = startIndex; j < dataInfo.Length; j++)
                    {
                        if (dataInfo.Substring(j, 1) == "\"")
                        {
                            if (count == 0) { indexFromQuotes = j + 1; count++; }
                            else { indexToQuotes = j; count++; }
                        }
                        if (count == 2) break;
                    }
                    ArrayWinLogDataInfo[i] = (!(indexFrom == -1 || indexTo == -1)) ? dataInfo.Substring(indexFromQuotes, indexToQuotes - indexFromQuotes) : "null";
                    // ? ArrayWinLogDataInfo[i] = (ArrayWinLogDataInfo[i].Length > 0) ? ArrayWinLogDataInfo[i] : "null";
                }
                else ArrayWinLogDataInfo[i] = "null";
            }
            return ArrayWinLogDataInfo;
        }

        //! Убирает то что уже было распарсенно
        private static void SubstringData(ref string data, string whereFrom, string whereTo)
        {
            string context = data;
            int indexFrom = (GetSearchIndexInitially(data, whereFrom, true));
            int indexTo = (GetSearchIndexInitially(data, whereTo, false));
            data = data.Substring(indexTo + whereTo.Length);
        }

        //! Подсчёт операторов* лог строк на правильную последовательность
        private static int CountSearchOperator(string data, string first, string last)
        {
            int countOperator = 0;
            bool checkOrder = true;
            for (int i = 0; i < data.Length - first.Length; i++)
            {
                if (data.Substring(i, first.Length) == first) if (checkOrder) { countOperator++; checkOrder = false; } else return countOperator;
                else if (data.Substring(i, last.Length) == last) if (!checkOrder) { countOperator++; checkOrder = true; } else return countOperator;
            }
            return countOperator;
        }

        //! Непосредственно сам парсер строки который в итоге возвращает массив всех обнаруженных логов
        public static List<string[]> ParseFile(string SourceFile)
        {

            using (StreamReader LogFile = new StreamReader(SourceFile))
            {
                string data = LogFile.ReadToEnd();
                List<string[]> ListArrayWinData = new List<string[]>();

                int CountLogOperator = CountSearchOperator(data, "<!", "!>");

                if (CountLogOperator % 2 == 0) CountLogOperator /= 2; else return ListArrayWinData;

                for (int i = 0; i < CountLogOperator; i++)
                {
                    string[] ArrayWinLogData = new string[8];
                    ArrayWinLogData[0] = ParseLOGString(data);
                    SubstringData(ref data, parseLOGArr[0], parseLOGArr[1]);

                    string[] ArrayWinLogDataInfo = ParseLOGInfoString(data);
                    for (int j = 1; j < ArrayWinLogData.Length; j++)
                        ArrayWinLogData[j] = ArrayWinLogDataInfo[j - 1];

                    ListArrayWinData.Add(ArrayWinLogData);
                }
                return ListArrayWinData;
            }
        }
        #endregion
    }
}
