using NetFwTypeLib;
using System;
using System.Collections.Generic;

namespace Cancer2Ban
{
    public static class Firewall
    {
        public static readonly string FW_PREFIX = "Cancer2Ban: ";
        private static readonly string TIMEFORMAT = "yyyy.MM.dd HH:mm:ss";

        private static INetFwRules GetAllRules()
        {
            try
            {
                Type tNetFwPolicy2 = Type.GetTypeFromProgID("HNetCfg.FwPolicy2");
                INetFwPolicy2 fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(tNetFwPolicy2);

                return fwPolicy2.Rules;
            }
            catch
            {
                Form1.main.LogAction(Form1.LOG_STATE.ERROR, "Error while retrieving Firewall rulelist");
                return null;
            }
        }

        public static List<INetFwRule> GetRulesByName(string name)
        {

            List<INetFwRule> ruleList = new List<INetFwRule>();
            try
            {

                foreach (INetFwRule rule in GetAllRules())
                {
                    if (rule.Name.ToLower().Contains(name.ToLower()))
                    {
                        ruleList.Add(rule);
                    }
                }
                return ruleList;
            }
            catch
            {
                Form1.main.LogAction(Form1.LOG_STATE.ERROR, "Error while retrieving Firewall rule by name (" + name + ")");
                return ruleList;
            }
        }

        public static void AddRule(string ip, DateTime until)
        {
            try
            {
                string ruleName = "Cancer2Ban: " + ip;

                INetFwRules rules = GetAllRules();
                INetFwRule rule = (INetFwRule)Activator.CreateInstance(Type.GetTypeFromProgID("HnetCfg.FWRule"));
                rule.Name = ruleName;
                rule.RemoteAddresses = ip;
                rule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
                rule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY;
                rule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
                rule.Description = "Cancer2Ban-" + until.ToString(TIMEFORMAT);
                rule.Enabled = true;
                rules.Add(rule);
            }
            catch
            {
                Form1.main.LogAction(Form1.LOG_STATE.ERROR, "Error while adding Firewalrule for " + ip);
            }
        }

        public static void LiftBans()
        {
            try
            {
                DateTime now = DateTime.Now;
                List<INetFwRule> rules = GetRulesByName(Firewall.FW_PREFIX);

                foreach (INetFwRule rule in rules)
                {
                    DateTime until = DateTime.ParseExact(rule.Description.Split('-')[1], TIMEFORMAT, System.Globalization.CultureInfo.InvariantCulture);

                    if (now > until)
                    {
                        GetAllRules().Remove(rule.Name);
                    }
                }
            }
            catch
            {
                Form1.main.LogAction(Form1.LOG_STATE.ERROR, "Error while lifting old bans");
            }
        }
    }
}
