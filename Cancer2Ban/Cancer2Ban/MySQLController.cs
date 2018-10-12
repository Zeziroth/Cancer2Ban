using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Cancer2Ban
{
    public class SQLiteController
    {
        private SQLiteConnection connection;
        private string _filename;
        private bool _connected = false;

        public SQLiteController(string filename)
        {
            _filename = filename;
            connection = new SQLiteConnection(@"Data Source=" + _filename + ".sqlite;Version=3;");
            if (OpenConnection())
            {
                _connected = true;
            }
        }

        public bool IsConnected()
        {
            return _connected;
        }

        private void ExecuteCommand(string query)
        {
            if (_connected)
            {
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                cmd.ExecuteNonQuery();
            }
        }
        public void Insert(string table, Dictionary<string, string> param)
        {
            if (param == null || param.Count == 0)
            {
                return;
            }

            List<string> vals = new List<string>();

            foreach (string val in param.Values)
            {
                vals.Add("'" + val + "'");
            }

            string query = "INSERT INTO " + table + " (" + String.Join(",", param.Keys.ToArray()) + ") VALUES(" + String.Join(",", vals.ToArray()) + ") ";

            ExecuteCommand(query);
        }

        public SQLRecord ReturnQuery(string query)
        {
            Dictionary<int, Dictionary<string, string>> retStruct = new Dictionary<int, Dictionary<string, string>>();

            if (_connected)
            {
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());


                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    for (int column = 0; column < dt.Columns.Count; column++)
                    {
                        if (!retStruct.ContainsKey(row))
                        {
                            retStruct.Add(row, new Dictionary<string, string>());
                        }

                        retStruct[row].Add(dt.Columns[column].ToString(), dt.Rows[row][column].ToString());
                    }
                }
            }

            SQLRecord ret = new SQLRecord(retStruct);
            return ret;
        }

        public SQLRecord Select(string table, bool where = false, Dictionary<string, string> conditions = null)
        {
            string query = "SELECT * FROM " + table;
            Dictionary<int, Dictionary<string, string>> retStruct = new Dictionary<int, Dictionary<string, string>>();

            if (where)
            {
                query += " WHERE ";
                for (int i = 0; i < conditions.Count; i++)
                {
                    string column = conditions.Keys.ElementAt(i);
                    query += column + "='" + conditions[column] + "'";

                    if (i != conditions.Count - 1)
                    {
                        query += " AND ";
                    }
                }
            }

            if (_connected)
            {

                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());


                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    for (int column = 0; column < dt.Columns.Count; column++)
                    {
                        if (!retStruct.ContainsKey(row))
                        {
                            retStruct.Add(row, new Dictionary<string, string>());
                        }

                        retStruct[row].Add(dt.Columns[column].ToString(), dt.Rows[row][column].ToString());
                    }
                }
            }

            SQLRecord ret = new SQLRecord(retStruct);
            return ret;
        }

        public int Count(string table, bool where = false, Dictionary<string, string> conditions = null)
        {
            if (where && conditions == null || where && conditions.Count == 0)
            {
                return 0;
            }

            string query = "SELECT Count(*) FROM " + table;

            if (where)
            {
                query += " WHERE ";

                for (int i = 0; i < conditions.Count; i++)
                {
                    string column = conditions.Keys.ElementAt(i);
                    query += column + "='" + conditions[column] + "'";

                    if (i != conditions.Count - 1)
                    {
                        query += " AND ";
                    }
                }
            }

            int count = 0;


            if (_connected)
            {
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                count = int.Parse(cmd.ExecuteScalar() + "");
            }

            return count;
        }
        public void Update(string table, Dictionary<string, string> param, bool where = false, Dictionary<string, string> conditions = null)
        {
            if (param == null || param.Count == 0 || where && conditions == null || where && conditions.Count == 0)
            {
                return;
            }

            string setQuery = "";

            foreach (string column in param.Keys)
            {
                setQuery += column + "='" + param[column] + "'";
            }

            string query = "UPDATE " + table + " SET " + setQuery;

            if (where)
            {
                query += " WHERE ";

                for (int i = 0; i < conditions.Count; i++)
                {
                    string column = conditions.Keys.ElementAt(i);
                    query += column + "='" + conditions[column] + "'";
                    if (i != conditions.Count - 1)
                    {
                        query += " AND ";
                    }
                }
            }

            ExecuteCommand(query);
        }

        public void Delete(string table, Dictionary<string, string> conditions)
        {
            if (conditions == null || conditions.Count == 0)
            {
                return;
            }

            string query = "DELETE FROM " + table + " WHERE ";

            for (int i = 0; i < conditions.Count; i++)
            {
                string column = conditions.Keys.ElementAt(i);
                query += column + "='" + conditions[column] + "'";

                if (i < conditions.Count)
                {
                    query += " AND ";
                }
            }

            ExecuteCommand(query);
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (SQLiteException ex)
            {
                switch (ex.ErrorCode)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password.");
                        break;

                    default:
                        MessageBox.Show(ex.Message);
                        break;
                }
                return false;
            }
        }
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}