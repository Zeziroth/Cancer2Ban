using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Cancer2Ban
{
    public class Events
    {
        private bool running = false;
        private static readonly int RDP_ID = 4625;
        private static readonly string LOG_NAME = "Security";
        private List<int> handledEvents = new List<int>();
        private Dictionary<string, byte> attacks = new Dictionary<string, byte>();

        public List<EventLogEntry> GetNewEvents()
        {
            List<EventLogEntry> entries = new List<EventLogEntry>();
            try
            {
                EventLog eventLog = EventLog.GetEventLogs().Where(x => x.Log == LOG_NAME).First();

                foreach (EventLogEntry log in eventLog.Entries)
                {
                    if (log.EventID == RDP_ID)
                    {
                        if (log.TimeGenerated > DateTime.Now.AddMinutes(((double)Form1.main.numericUpDown_AttemptObserve.Value * -1)))
                        {
                            entries.Add(log);
                        }
                    }
                }

                return entries;
            }
            catch
            {
                Form1.main.LogAction(Form1.LOG_STATE.ERROR, "Error while retrieving new Windows-Security-Eventlist");
                return entries;
            }
        }

        public void Start_CheckEvents()
        {
            if (!running)
            {
                running = true;
                Form1.main.button1.Enabled = false;
                Form1.main.button2.Enabled = true;
                Form1.main.LogAction(Form1.LOG_STATE.INFO, "Started guard");
            }

            Core.RunThread(CheckRoutine);
        }

        private void CheckRoutine()
        {
            while (running)
            {
                try
                {
                    Firewall.LiftBans();

                    List<EventLogEntry> entries = GetNewEvents();

                    foreach (EventLogEntry entry in entries.Where(x => !handledEvents.Contains(x.Index)))
                    {
                        string log = entry.Message;
                        Regex regex = new Regex(@"Quellnetzwerkadresse:\t\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
                        Match match = regex.Match(log);
                        string ipLine = match.Value;
                        string ip = ipLine.Split(':')[1].Trim(' ').Trim('\t');

                        if (attacks.Keys.Contains(ip))
                        {
                            attacks[ip] += 1;
                        }
                        else
                        {
                            attacks.Add(ip, 1);
                        }

                        int attempts = attacks[ip];

                        string logInfo = "";
                        int ruleCount = Firewall.GetRulesByName(Firewall.FW_PREFIX + ip).Count;
                        bool nowBanned = false;

                        if (attempts >= Form1.main.numericUpDown_BanAttempts.Value && ruleCount == 0)
                        {
                            logInfo = " (Banned)";
                            nowBanned = true;
                            Firewall.AddRule(ip, DateTime.Now.AddMinutes((int)Form1.main.numericUpDown_BanDuration.Value));

                            if (Form1.main.metroToggle1.Checked)
                            {
                                AbuseIPDB.ReportIP(ip);
                                logInfo += " (Auto banned on AbuseIPDB)";
                            }
                        }

                        if (ruleCount == 0 || nowBanned)
                        {
                            Form1.main.LogAction(Form1.LOG_STATE.INFO, "Received attack (Attempt #" + attacks[ip] + ")" + logInfo, ip, "https://www.abuseipdb.com/check/" + ip);
                        }

                        handledEvents.Add(entry.Index);
                    }
                }
                catch
                {
                    continue;
                }

                Thread.Sleep(2500);
            }
        }

        public void Stop_CheckEvents()
        {
            running = false;
            Form1.main.button1.Enabled = true;
            Form1.main.button2.Enabled = false;
            Form1.main.LogAction(Form1.LOG_STATE.INFO, "Stopped guard");
        }
    }
}
