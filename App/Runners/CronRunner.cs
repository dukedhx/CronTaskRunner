using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication2.Interface;
using WebApplication2.Utils;
using static WebApplication2.Utils.Constants;

namespace WebApplication2.App.Runners
{
    public class CronRunner:Runner<ScriptJobConfig,JobKey,Task>
    {
        
        public Boolean isRunning { get => scheduler?.IsStarted??false; }
        private IScheduler scheduler;
        private Dictionary<JobKey, ScripJobDetail> SJDDict ;

        public String ID;

        public CronRunner() : this(Guid.NewGuid().ToString())
        {
           

        }

        protected void RegisterCronRunner()
        {
            CronAppHelper.RegisterCronRunner(ID, SJDDict);


        }

        public CronRunner(String ID)
        {
            this.ID = ID;
            SJDDict = new Dictionary<JobKey, ScripJobDetail>();
            RegisterCronRunner();
        }

        public Task Stop(JobKey processid)
        {
            if (scheduler == null|| !scheduler.IsStarted || !SJDDict.ContainsKey(processid)) return null;
            var dtask=scheduler.DeleteJob(processid);
            SJDDict.Remove(processid);
            return dtask;

        }
            public Task Run(ScriptJobConfig config)
        {

            

          

                if (scheduler == null)
                {
                    ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
                    scheduler = schedulerFactory.GetScheduler().Result;
                }
            var identity = config.identity ?? Guid.NewGuid().ToString();
            var triggerid= config.triggerid?? Guid.NewGuid().ToString();
            var jobDetail = JobBuilder.Create<ScriptJob>()
                    .WithIdentity(identity).UsingJobData(new JobDataMap(new Dictionary<string, string>() {
                        { argsEnum.appid.ToString(),config.appid??ID},
                         {  argsEnum.domainname.ToString(), config.domainname},
              {  argsEnum.domainusername.ToString(), config.domainusername},
              {  argsEnum.domainuserpw.ToString(), config.domainuserpw },
              {  argsEnum.pwdir.ToString(), config.pwdir },
              {  argsEnum.pargs.ToString(), config.pargs},
              {  argsEnum.jobIdentity.ToString(), identity},
              {  argsEnum.triggerIdentity.ToString(), triggerid},
              {  argsEnum.processpath.ToString(), config.processpath},
                    }))
                    .Build();
                
                ITrigger trigger = TriggerBuilder.Create()
                    .ForJob(jobDetail)
                    .WithCronSchedule(config.cronexp)
                    .WithIdentity(triggerid)
                    .StartNow()
                    .Build();
                scheduler.ScheduleJob(jobDetail, trigger).Wait();
                SJDDict.Add(jobDetail.Key, new ScripJobDetail() { scriptJobConfig=config});

               return scheduler.Start();
              

            
            
        }
    }
}