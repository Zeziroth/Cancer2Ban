using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cancer2Ban
{
    public class AbuseIPDB
    {
        private static readonly byte BRUTEFORCE_CATEGORY = 18;
        private static readonly string COMMENT = "Cancer2Ban-Autoban for Windows (see: https://github.com/Zeziroth/Cancer2Ban)";
        private static readonly string REPORT_URL = "https://www.abuseipdb.com/report/json?key={0}&category={1}&comment={2}&ip={3}";
        private static WebClient client = new WebClient();

        public static void Init()
        {
            client.Proxy = null;
        }
        public static void ReportIP(string ip)
        {
            string apiKey = GET_AbuseIPDB_KEY();
            if (apiKey == "")
            {
                Form1.main.LogAction(Form1.LOG_STATE.ERROR, "No valid AbuseIPDB-ApiKey found!");
                return;
            }

            if (!AlreadyReported(ip))
            {
                Form1.sql.ReturnQuery("INSERT INTO bans (ip) VALUES ('" + ip + "')");
                client.DownloadString(String.Format(REPORT_URL, apiKey, BRUTEFORCE_CATEGORY, COMMENT, ip));
            }
        }

        private static bool AlreadyReported(string ip)
        {
            try
            {
                SQLRecord record = Form1.sql.ReturnQuery("SELECT * FROM bans WHERE ip = '" + ip + "' LIMIT 1");
                return record.NumRows() == 1;
            }
            catch
            {
                return false;
            }
        }

        private static string GET_AbuseIPDB_KEY()
        {
            string apiKey = "";

            SQLRecord record = Form1.sql.ReturnQuery("SELECT * FROM settings WHERE name = 'apikey' LIMIT 1");
            if (record.NumRows() == 1)
            {
                apiKey = record.GetValue(0, "val");
            }

            return apiKey;
        }
    }
}
