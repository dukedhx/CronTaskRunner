using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.App.Runners;
using WebApplication2.Utils;

namespace WebApplication2.Controllers
{
    public class StatusController : ApiController
    {
        [HttpGet]
      
        public Object Get()
        {
            
            return new { jobs=CronAppHelper.Jobs};
        }
        }
}
