using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
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

        public Events()
        {

        }

        public List<EventLogEntry> GetNewEvents()
        {
            List<EventLogEntry> entries = new List<EventLogEntry>();
            EventLog eventLog = EventLog.GetEventLogs().Where(x => x.Log == LOG_NAME).First();

            foreach (EventLogEntry log in eventLog.Entries)
            {
                if (log.EventID == RDP_ID && log.TimeGenerated > DateTime.Now.AddMinutes(((double)Form1.main.numericUpDown_AttemptObserve.Value * -1)))
                {
                    entries.Add(log);
                }
            }

            return entries;
        }

        public void Start_CheckEvents()
        {
            if (!running)
            {
                running = true;
            }

            Core.RunThread(CheckRoutine);
        }

        private void CheckRoutine()
        {
            while (running)
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

                    if (attempts >= Form1.main.numericUpDown_BanAttempts.Value && ruleCount == 0)
                    {
                        logInfo = " (Banned)";
                        Firewall.AddRule(ip, DateTime.Now.AddMinutes((int)Form1.main.numericUpDown_BanDuration.Value));

                        if (Form1.main.metroToggle1.Checked)
                        {
                            AbuseIPDB.ReportIP(ip);
                        }
                    }
                    if (ruleCount == 0)
                    {
                        Form1.main.LogAction(Form1.LOG_STATE.INFO, "Received attack from: " + ip + " (Attempt #" + attacks[ip] + ")" + logInfo);
                    }
                    
                    handledEvents.Add(entry.Index);
                }

                Thread.Sleep(2500);
            }
        }
        public void Stop_CheckEvents()
        {
            running = false;
        }
    }
}
