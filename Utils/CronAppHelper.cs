using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.App.Runners;

namespace WebApplication2.Utils
{
    public static class CronAppHelper
    {
        private static readonly Dictionary<String, Dictionary<JobKey, ScripJobDetail>> vault = new Dictionary<string, Dictionary<JobKey, ScripJobDetail>>();

            public static void RegisterCronRunner(String ID,Dictionary<JobKey, ScripJobDetail> runnerDict)
        {
            vault[ID] = runnerDict;
        }

        public static bool setRunning(String Appid,JobKey Jobid)
        {
            if (vault.ContainsKey(Appid)&& vault[Appid].ContainsKey(Jobid)) 
                return vault[Appid][Jobid].Running = true;
                   
            else
                return false;
        }

        public static bool stopRunning(String Appid, JobKey Jobid)
        {
            if (vault.ContainsKey(Appid) && vault[Appid].ContainsKey(Jobid))
                return !( vault[Appid][Jobid].Running = false);

            else
                return false;
        }

        public static bool? getRunning(String Appid, JobKey Jobid)
        {
            return vault.ContainsKey(Appid) && vault[Appid].ContainsKey(Jobid) ? (bool?)vault[Appid][Jobid].Running : null;
        }
    }
}