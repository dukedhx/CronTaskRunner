using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.App.Runners
{
    public class ScriptJobDetail
    {


        public ScriptJobConfig scriptJobConfig;
        public Boolean Running;
        public DateTime FirstRun;
        public DateTime LastRun;
        public String LastOutPut;
    }
}