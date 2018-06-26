using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.App.Runners;

namespace WebApplication2.Models
{
    public class JobStatus
    {
        public ScriptJobDetail jobDetail;
        public DateTime FirstRun;
        public DateTime LastRun;
        public String LastOutput;
        public Boolean Running;
    }
}