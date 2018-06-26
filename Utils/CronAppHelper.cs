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
        private static readonly Dictionary<String,CronRunner> vault = new Dictionary<string,  CronRunner>();

        public static IEnumerable<ScriptJobDetail> Jobs { get => vault.SelectMany(v => v.Value.SJDDict.Values); }

            public static void RegisterCronRunner(String ID,CronRunner CronRunner)
        {
            vault[ID] = CronRunner;
        }

        //public static void stopJob(String ID)
        //{
        //    if (vault.ContainsKey(ID)) {
        //        var runner = vault[ID];
        //        foreach ()
        //            ;
        //    }
        //    return null;
        //}

        public static bool setRunning(String Appid,JobKey Jobid)
        {
            if (vault.ContainsKey(Appid)&& vault[Appid].SJDDict.ContainsKey(Jobid)) 
                return vault[Appid].SJDDict[Jobid].Running = true;
                   
            else
                return false;
        }

        public static bool stopRunning(String Appid, JobKey Jobid)
        {
            if (vault.ContainsKey(Appid) && vault[Appid].SJDDict.ContainsKey(Jobid))
                return !(vault[Appid].SJDDict[Jobid].Running = false);

            else
                return false;
        }

        public static bool? getRunning(String Appid, JobKey Jobid)
        {
            return vault.ContainsKey(Appid) && vault[Appid].SJDDict.ContainsKey(Jobid) ? (bool?)vault[Appid].SJDDict[Jobid].Running : null;
        }
    }
}