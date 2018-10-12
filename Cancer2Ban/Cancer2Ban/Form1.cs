using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics.Eventing;
using System.Diagnostics;
using NetFwTypeLib;
namespace Cancer2Ban
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public static Form1 main;
        private static readonly string SQLITE_NAME = "cancer2ban";

        private string oldAPI = "";
        private decimal oldBanAttempts = 0;
        private decimal oldAttemptMinutes = 0;
        private decimal oldBanDuration = 0;

        private Events eventInstance;
        public enum LOG_STATE
        {
            INFO = -1,
            WARNING = 0,
            ERROR = 1
        }

        public static SQLiteController sql = new SQLiteController(SQLITE_NAME);
        public Form1()
        {
            InitializeComponent();
        }


        private void Start_Routine()
        {
            main = this;
            LogAction(LOG_STATE.INFO, "Initializing database");
            if (!sql.IsConnected())
            {
                LogAction(LOG_STATE.ERROR, "Couldnt create .sqlite-Database! Application could not start.");
            }
            else
            {
                sql.ReturnQuery("CREATE TABLE IF NOT EXISTS settings (id INTEGER PRIMARY KEY AUTOINCREMENT, name VARCHAR(50), val VARCHAR(50));");
                sql.ReturnQuery("CREATE TABLE IF NOT EXISTS logs (id INTEGER PRIMARY KEY AUTOINCREMENT, ip VARCHAR(39), requesttime BIGINT(10));");
                sql.ReturnQuery("CREATE TABLE IF NOT EXISTS bans (id INTEGER PRIMARY KEY AUTOINCREMENT, ip VARCHAR(39));");

                LogAction(LOG_STATE.INFO, "Loaded .sqlite-Database!");

                SQLRecord record = sql.ReturnQuery("SELECT * FROM settings");
                for (int row = 0; row < record.NumRows(); row++)
                {
                    string val = record.GetValue(row, "val");

                    switch (record.GetValue(row, "name"))
                    {
                        case "autoban_enabled":
                            metroToggle1.Checked = (val == "1") ? true : false;
                            break;
                        case "apicheck":
                            metroCheckBox1.Checked = (val == "1") ? true : false;
                            break;

                        case "apikey":
                            metroTextBox_APIKEY.Text = val;
                            break;
                        case "banattempts":
                            numericUpDown_BanAttempts.Value = decimal.Parse(val);
                            oldBanAttempts = decimal.Parse(val);
                            break;
                        case "attemptminutes":
                            numericUpDown_AttemptObserve.Value = decimal.Parse(val);
                            oldAttemptMinutes = decimal.Parse(val);
                            break;
                        case "banduration":
                            numericUpDown_BanDuration.Value = decimal.Parse(val);
                            oldBanDuration = decimal.Parse(val);
                            break;
                    }
                }

                eventInstance = new Events();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Start_Routine();
        }

        public void LogAction(LOG_STATE state, string action)
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

            Invoker.LogEntry(richTextBox1, logBuilder.ToString());
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

            if (numericUpDown_BanDuration.Value != oldBanDuration)
            {
                ChangeSetting("banduration", numericUpDown_BanDuration.Value.ToString());
                LogAction(LOG_STATE.INFO, "Changed ban duration!");
                oldBanDuration = numericUpDown_BanDuration.Value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            eventInstance.Start_CheckEvents();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Core.CloseThreads();
            string apiEnabled = metroToggle1.Checked ? "1" : "0";
            ChangeSetting("autoban_enabled", apiEnabled);
        }

        private void metroCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            metroTextBox_APIKEY.PasswordChar = metroCheckBox2.Checked ? '\0' : '*';
        }
    }
}