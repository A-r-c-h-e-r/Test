using Npgsql;
using System;

namespace WinLogsParser.ViewModel.InteractionDatabase
{
    public static class InteractionPostgreSQL
    {
        #region Data
        public static Exception exception { private set; get; }
        private static NpgsqlConnection Connection;
        private static string ConnectionString_;
        public static string ConnectionString
        {
            get { return ConnectionString_; }
            set
            {
                ConnectionString_ = value;
                Connection = new NpgsqlConnection(value);
            }
        }
        #endregion

        #region MethodsTemplate
        ///--> Метот запросник
        private static NpgsqlCommand QueriesPostgreSQL(string queries)
        {
            NpgsqlCommand Command = Connection.CreateCommand();
            Command.CommandText = queries;
            Command.ExecuteNonQuery();
            return Command;
        }
        #endregion

        #region Methods
        ///--> Подсчёт количества пользователей
        public static int FindFieldsCount()
        {
            try
            {
                Connection.Open();
                NpgsqlCommand Command = QueriesPostgreSQL("select count(*) from table_users");
                int Count = Convert.ToInt32(Command.ExecuteScalar());
                Connection.Close();
                return Count;
            }
            catch (Exception ex) { exception = ex; return 0; }
        }

        ///--> Удаление аккаунта по логигу и паролю 
        public static bool DeleteWherwActive(string firstname, string password)
        {
            try
            {
                Connection.Open();
                NpgsqlCommand Command = QueriesPostgreSQL($"delete from table_users where firstname='{firstname}' and password='{password}'");
                Connection.Close();
                return true;
            }
            catch (Exception ex) { exception = ex; return false; }
        }

        ///--> Проверка на уникальность логина при реристрации
        public static bool UniquenessCheck(string where, string data)
        {
            try
            {
                Connection.Open();
                NpgsqlCommand Command = QueriesPostgreSQL($"select * from table_users where {where} not in ('{data}')");
                bool resolt = (Command.ExecuteScalar() != null) ? true : false;
                Connection.Close();
                return resolt;
            }
            catch (Exception ex) { exception = ex; return false; }
        }

        ///--> Выход из активного аккаунта
        public static bool SetActiveFieldExit()
        {
            try
            {
                Connection.Open();
                NpgsqlCommand Command = QueriesPostgreSQL($"update table_users set active=false where true");
                Connection.Close();
                return true;
            }
            catch (Exception ex) { exception = ex; return false; }
        }

        ///--> Вход в аккаунт по логину и паролю
        public static bool SetActiveFieldEnter(string firstname, string password)
        {
            try
            {
                SetActiveFieldExit();
                Connection.Open();
                NpgsqlCommand Command = QueriesPostgreSQL($"update table_users set active=true " +
                                                          $"where firstname='{firstname}' and password='{password}'");
                bool ResoltCommand = Command.ExecuteNonQuery() == 1 ? true : false;
                Connection.Close();
                return ResoltCommand;
            }
            catch (Exception ex) { exception = ex; return false; }
        }

        ///--> Добавление пользоватиля в бд (регистрация)
        public static bool AddWhereEnd(params string[] data)
        {
            try
            {
                if (UniquenessCheck("firstname", data[0]) || FindFieldsCount() == 0)
                {
                    SetActiveFieldExit();
                    Connection.Open();
                    NpgsqlCommand Command = QueriesPostgreSQL($"insert into table_users(firstname, lastname, email, password, active)" +
                                                              $" values('{data[0]}','{data[1]}','{data[2]}','{data[3]}','true')");
                    Connection.Close();
                    return true;
                }
                else return false;
            }
            catch (Exception ex) { exception = ex; return false; }
        }

        ///--> Изменить логин имя почту и пароль в активном аккаунте
        public static bool UpdateWhereActive(params string[] data)
        {
            try
            {
                if (UniquenessCheck("firstname", data[0]))
                {
                    Connection.Open();
                    NpgsqlCommand Command = QueriesPostgreSQL($"update table_users set firstname = '{data[0]}',lastname = '{data[1]}'," +
                                                              $"email = '{data[2]}',password = '{data[3]}' where active = true");
                    Connection.Close();
                    return true;
                }
                else return false;
            }
            catch (Exception ex) { exception = ex; return false; }
        }
        
        public static string[] AutoLogin()
        {
            try
            {
                Connection.Open();
                string[] CommandResult = new string[]
                {
                    Convert.ToString(QueriesPostgreSQL("select firstname from table_users where active = true").ExecuteScalar()),
                    Convert.ToString(QueriesPostgreSQL("select lastname from table_users where active = true").ExecuteScalar()),
                    Convert.ToString(QueriesPostgreSQL("select email from table_users where active = true").ExecuteScalar()),
                    Convert.ToString(QueriesPostgreSQL("select password from table_users where active = true").ExecuteScalar())
                };
                Connection.Close();
                return CommandResult;
            }
            catch (Exception ex) { exception = ex; return null; }
        }
        #endregion
    }
}
