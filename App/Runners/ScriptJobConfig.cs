using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebApplication2.Utils.Constants;

namespace WebApplication2.App.Runners
{
    public class ScriptJobConfig
    {
        public ScriptJobConfig()
        {

        }

        public ScriptJobConfig(Dictionary<String,String> config)

        {

            config.TryGetValue(argsEnum.pargs.ToString(),out pargs);
            
            config.TryGetValue(argsEnum.domainusername.ToString(),out domainusername);
            config.TryGetValue(argsEnum.domainuserpw.ToString(), out domainuserpw);
            config.TryGetValue(argsEnum.cronexp.ToString(), out cronexp);
            config.TryGetValue(argsEnum.domainname.ToString(), out domainname);
            config.TryGetValue(argsEnum.processpath.ToString(), out processpath);
            config.TryGetValue(argsEnum.pwdir.ToString(), out pwdir);



        }
        public String domainname, domainusername, domainuserpw, pwdir, pargs, processpath, appid,cronexp,identity,triggerid;
      
    }
}