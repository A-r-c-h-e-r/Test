using System.ComponentModel.DataAnnotations;

namespace WinLogsParser.ViewModel.PagesRegister
{
    public static class ValidationDataRegister
    {
        #region MethodsValidationData
        public static bool CheckNickname(string value)
        {
            if (value != null && value.Length > 4)
            {
                char[] KeyCode = value.ToCharArray();
                foreach (char c in KeyCode)
                    if (((c >= 48 && c <= 57) || (c >= 65 && c <= 90) || (c >= 97 && c <= 122) || c == 95) == false)
                        return false;
                foreach (char c in KeyCode)
                    if (((c >= 65 && c <= 90) || (c >= 97 && c <= 122)) == true)
                        return true;
            }
            return false;
        }

        public static bool CheckName(string value)
        {
            if (value != null && value.Length > 2)
            {
                char[] KeyCode = value.ToCharArray();
                for (int i = 1; i < KeyCode.Length; i++)
                    if ((KeyCode[i] >= 97 && KeyCode[i] <= 122) == false)
                        return false;
                if ((KeyCode[0] >= 97 && KeyCode[0] <= 122) || ((KeyCode[0] >= 65 && KeyCode[0] <= 90)))
                    return true;
            }
            return false;
        }

        public static bool CheckEmail(string value)
        {
            return (value != null && value.Length > 10) ? ((new EmailAddressAttribute().IsValid(value)) ? true : false) : false;
        }

        public static bool CheckPassword(string value)
        {
            if (value != null && value.Length > 5)
            {
                char[] KeyCode = value.ToCharArray();
                bool[] Check = new bool[2] { false, false };
                foreach (char c in KeyCode)
                    if (((c >= 48 && c <= 57) || (c >= 65 && c <= 90) || (c >= 97 && c <= 122)) == false)
                        return false;

                foreach (char c in KeyCode) if (c >= 48 && c <= 57) { Check[0] = true; break; }
                foreach (char c in KeyCode) if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122)) { Check[1] = true; break; }

                if (Check[0] == true && Check[1] == true) return true;
            }
            return false;
        }
        #endregion
    }
}
