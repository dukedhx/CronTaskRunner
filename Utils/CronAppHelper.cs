using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication2.App.Runners;

namespace WebApplication2.Utils
{
    public static class CronAppHelper
    {
        private static readonly Dictionary<String, CronRunner> vault = new Dictionary<string, CronRunner>();

        public static String[] RunnerIds { get => vault.Keys.ToArray(); }

        public static IEnumerable<ScriptJobDetail> Jobs { get => vault.SelectMany(v => v.Value.SJDDict.Values); }

        public static void RegisterCronRunner(String ID, CronRunner CronRunner)
        {
            vault[ID] = CronRunner;
        }

        public static Task removeRunner(string id)
        {
            var tasks = stopRunner(id);
            if (tasks == null) return null;

            return Task.Factory.StartNew(() => Task.WaitAll(tasks.ToArray())).ContinueWith((ante) => vault.Remove(id));

        }

        public static IEnumerable<Task> clearRunners()
        {

            return getRunnerIdsTasks((tlist, e) => { tlist.Add(removeRunner(e)); return tlist; });
        }

        public static IEnumerable<Task> stopAllRunners()
        {


            return getRunnerIdsTasks((tlist, e) => tlist.Concat(stopRunner(e)).ToList());
        }


        public static IEnumerable<Task> pauseAllRunners()
        {


            return getRunnerIdsTasks((tlist, e) => tlist.Concat(stopRunner(e, true)).ToList());
        }

        

        public static IEnumerable<Task> resumeAllRunners()
        {


            return getRunnerIdsTasks((tlist, e) => tlist.Concat(stopRunner(e)).ToList());
        }


        public static IEnumerable<Task> resumeRunner(String id)
        {


            return getRunnerTasks(id, (cr, e) => cr.Resume(e.Key));
        }


        public static IEnumerable<Task> stopRunner(string id, Boolean pause = false)
        {
            return getRunnerTasks(id, (cr,e) => cr.Stop(e.Key, pause));
        }

        public static bool setRunning(String Appid, JobKey Jobid)
        {
            return vault.ContainsKey(Appid) && vault[Appid].SJDDict.ContainsKey(Jobid)? vault[Appid].SJDDict[Jobid].Running = true:false;
            
        }

        public static bool unsetRunning(String Appid, JobKey Jobid)
        {
         
                return vault.ContainsKey(Appid) && vault[Appid].SJDDict.ContainsKey(Jobid) ? !(vault[Appid].SJDDict[Jobid].Running = false) : false;

        }

        public static bool? getRunning(String Appid, JobKey Jobid)
        {
            return vault.ContainsKey(Appid) && vault[Appid].SJDDict.ContainsKey(Jobid) ? (bool?)vault[Appid].SJDDict[Jobid].Running : null;
        }


        private static IEnumerable<Task> getRunnerIdsTasks(Func<List<Task>, string, List<Task>> func)
        {
            return RunnerIds.Aggregate(new List<Task>(), func);
        }


        private static IEnumerable<Task> getRunnerTasks(string id,Func< CronRunner,KeyValuePair<JobKey,ScriptJobDetail>, Task> func)
        {

            CronRunner cr = null;
            
            return vault.TryGetValue(id, out cr) ? cr.SJDDict.Select(e=>func(cr,e)):null;
        }

       

    }
}