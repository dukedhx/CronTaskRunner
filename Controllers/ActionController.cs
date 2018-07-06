using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

using System.Web.Http;
using WebApplication2.App.CronJobApp;
using WebApplication2.Utils;

namespace WebApplication2.Controllers
{
    public class ActionController : ApiController
    {
        [HttpGet]
        [Route("stop")]
        public Object Stop()
        {
            new CronJobApp().StopAll();
            return new { stoppedJobs = CronAppHelper.Jobs };
        }

        [HttpGet]
        [Route("pause")]
        public Object Pause()
        {
            new CronJobApp().ResumeAll();
            return new { pauseJobs = CronAppHelper.Jobs };
        }

        [HttpGet]
        [Route("start")]
        public Object Start()
        {
            if (CronAppHelper.RunnerIds.Length > 0)
                new CronJobApp().ResumeAll();
            else
                new CronJobApp().Start(Properties.Settings.Default.jobConfigPath);
            return new { startedJobs = CronAppHelper.Jobs };
        }
    }
}
