using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication2.Utils;
using static WebApplication2.Utils.Constants;

namespace WebApplication2.App.Runners
{
    public class ScriptJob : IJob
    {
        
        protected Dictionary<String, String> getArgs(JobDataMap jdp) {
            
            return new Dictionary<string, string>()
            {

              {  argsEnum.domainname.ToString(), jdp.GetString(argsEnum.domainname.ToString()) },
              {  argsEnum.domainusername.ToString(), jdp.GetString(argsEnum.domainusername.ToString()) },
              {  argsEnum.domainuserpw.ToString(), jdp.GetString(argsEnum.domainuserpw.ToString()) },
              {  argsEnum.pwdir.ToString(), jdp.GetString(argsEnum.pwdir.ToString()) },
              {  argsEnum.pargs.ToString(), jdp.GetString(argsEnum.pargs.ToString()) },
              {  argsEnum.processpath.ToString(), jdp.GetString(argsEnum.processpath.ToString()) },
            };

        }
        public Task Execute(IJobExecutionContext context)
        {
            var jdp = context.JobDetail.JobDataMap;
            var appid = jdp.GetString(argsEnum.appid.ToString());
            var jobid = new JobKey(jdp.GetString(argsEnum.jobIdentity.ToString()));
            return Task.Run(()=> {
                try
                {
                    if (CronAppHelper.getRunning(appid, jobid) == false)
                    {
                        CronAppHelper.setRunning(appid, jobid);
                        Logger.Log("Running " + jobid);

                        new ProcessRunner().Run(getArgs(context.JobDetail.JobDataMap)).WaitForExit();

                        CronAppHelper.stopRunning(appid, jobid);

                        Logger.Log("Stopping " + jobid);

                    }
                }catch(Exception ex)
                {
                    Logger.Log(ex);
                }

            });
        }
    }
}