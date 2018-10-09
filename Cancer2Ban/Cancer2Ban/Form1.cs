using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cancer2Ban
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        private static readonly string SQLITE_NAME = "cancer2ban";

        private string oldAPI = "";
        private decimal oldBanAttempts = 0;
        private decimal oldAttemptMinutes = 0;

        public enum LOG_STATE
        {
            INFO = -1,
            WARNING = 0,
            ERROR = 1
        }

        SQLiteController sql = null;
        public Form1()
        {
            InitializeComponent();
        }


        private void Start_Routine()
        {
            sql = new SQLiteController(SQLITE_NAME);
            LogAction(LOG_STATE.INFO, "Initializing database");
            if (!sql.IsConnected())
            {
                LogAction(LOG_STATE.ERROR, "Couldnt create .sqlite-Database! Application could not start.");
            }
            else
            {
                sql.ReturnQuery("CREATE TABLE IF NOT EXISTS settings (id INTEGER PRIMARY KEY AUTOINCREMENT, name VARCHAR(50), val VARCHAR(50));");
                sql.ReturnQuery("CREATE TABLE IF NOT EXISTS logs (id INTEGER PRIMARY KEY AUTOINCREMENT, ip VARCHAR(39), requesttime BIGINT(10));");
                sql.ReturnQuery("CREATE TABLE IF NOT EXISTS bans (id INTEGER PRIMARY KEY AUTOINCREMENT, ip VARCHAR(39), bantime BIGINT(10), permban INT(2));");

                LogAction(LOG_STATE.INFO, "Loaded .sqlite-Database!");

                SQLRecord record = sql.ReturnQuery("SELECT * FROM settings");
                for (int row = 0; row < record.NumRows(); row++)
                {
                    string val = record.GetValue(row, "val");

                    switch (record.GetValue(row, "name"))
                    {
                        case "apicheck":
                            metroCheckBox1.Checked = (val == "1") ? true : false;
                            break;

                        case "apikey":
                            metroTextBox_APIKEY.Text = val;
                            break;
                        case "banattempts":
                            numericUpDown_BanAttempts.Value = int.Parse(val);
                            oldBanAttempts = int.Parse(val);
                            break;
                        case "attemptminutes":
                            numericUpDown_AttemptObserve.Value = int.Parse(val);
                            oldAttemptMinutes = int.Parse(val);
                            break;
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Start_Routine();
        }

        private void LogAction(LOG_STATE state, string action)
        {
            StringBuilder logBuilder = new StringBuilder();

            switch (state)
            {
                case LOG_STATE.INFO:
                    logBuilder.Append("[INFO]");
                    break;
                case LOG_STATE.WARNING:
                    logBuilder.Append("[WARNING]");
                    break;
                case LOG_STATE.ERROR:
                    logBuilder.Append("[ERROR]");
                    break;
            }

            logBuilder.Append(DateTime.Now.ToString(" dd.mm.yyyy - HH:mm:ss\t"));

            logBuilder.Append(action);

            richTextBox1.Text = logBuilder.ToString() + Environment.NewLine + richTextBox1.Text;
        }

        private void metroToggle1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = metroToggle1.Checked;
        }

        private void metroCheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Trigger_APIEdit(bool edit)
        {
            metroButton_Edit.Enabled = !edit;
            metroButton_Apply.Enabled = edit;
            metroTextBox_APIKEY.ReadOnly = !edit;
            metroCheckBox1.Enabled = edit;

            if (edit)
            {
                oldAPI = metroTextBox_APIKEY.Text;
                metroTextBox_APIKEY.Focus();
            }
            else
            {
                if (metroTextBox_APIKEY.Text != oldAPI)
                {
                    LogAction(LOG_STATE.INFO, "Changed API-KEY for AbuseIPDB.com");
                }

                string indi = metroCheckBox1.Checked ? "1" : "0";
                ChangeSetting("apicheck", indi);
                

                if (metroCheckBox1.Checked)
                {
                    ChangeSetting("apikey", metroTextBox_APIKEY.Text);
                }
            }
        }
        private void ChangeSetting(string settingName, string val)
        {
            if (sql.Count("settings", true, new Dictionary<string, string>() { { "name", settingName } }) > 0)
            {
                sql.Update("settings", new Dictionary<string, string>() { { "val", val } }, true, new Dictionary<string, string>() { { "name", settingName } });
            }
            else
            {
                sql.Insert("settings", new Dictionary<string, string>() { { "name", settingName }, { "val", val } });
            }
        }

        private void metroButton_Edit_Click(object sender, EventArgs e)
        {
            Trigger_APIEdit(true);
        }

        private void metroButton_Apply_Click(object sender, EventArgs e)
        {
            Trigger_APIEdit(false);
        }

        private void metroButton_APPLYGLOBAL_Click(object sender, EventArgs e)
        {
            if (numericUpDown_BanAttempts.Value != oldBanAttempts)
            {
                ChangeSetting("banattempts", numericUpDown_BanAttempts.Value.ToString());
                LogAction(LOG_STATE.INFO, "Changed number of required attempts for ban!");
                oldBanAttempts = numericUpDown_BanAttempts.Value;
            }

            if (numericUpDown_AttemptObserve.Value != oldAttemptMinutes)
            {
                ChangeSetting("attemptminutes", numericUpDown_AttemptObserve.Value.ToString());
                LogAction(LOG_STATE.INFO, "Changed observing minutes between attempts!");
                oldAttemptMinutes = numericUpDown_AttemptObserve.Value;
            }
        }
    }
}
