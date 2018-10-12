using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cancer2Ban
{
    public static class Firewall
    {
        public static readonly string FW_PREFIX = "Cancer2Ban: ";
        private static readonly string TIMEFORMAT = "yyyy.MM.dd HH:mm:ss";

        private static INetFwRules GetAllRules()
        {
            Type tNetFwPolicy2 = Type.GetTypeFromProgID("HNetCfg.FwPolicy2");
            INetFwPolicy2 fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(tNetFwPolicy2);

            return fwPolicy2.Rules;
        }

        public static List<INetFwRule> GetRulesByName(string name)
        {
            List<INetFwRule> ruleList = new List<INetFwRule>();

            foreach (INetFwRule rule in GetAllRules())
            {
                if (rule.Name.ToLower().Contains(name.ToLower()))
                {
                    ruleList.Add(rule);
                }
            }
            return ruleList;
        }

        public static void AddRule(string ip, DateTime until)
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

        public static void LiftBans()
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
    }
}
