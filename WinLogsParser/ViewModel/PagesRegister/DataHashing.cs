using System;
using System.Security.Cryptography;
using System.Text;

namespace WinLogsParser.ViewModel.PagesRegister
{
    public static class DataHashing
    {
        public static string GetDataHashing(string data)
        {
            return Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(data)));
        }
    }
}
