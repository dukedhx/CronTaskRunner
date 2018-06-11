using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication2.App.Runners;
using WebApplication2.Interface;
namespace WebApplication2.App.CronJobApp
{
    public class CronJobApp : Runner<String, bool, Task>
    {
        


        public Task Run(String dirPath)
        {

        }

        

        public Task Stop(bool id)
        {
            throw new NotImplementedException();
        }
    }
}