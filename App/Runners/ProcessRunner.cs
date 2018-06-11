using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using WebApplication2.Interface;
using WebApplication2.Utils;

namespace WebApplication2.App.Runners
{
    public class ProcessRunner:Runner<Dictionary<String, String>,String,Process>
    {

        public bool RedirectStandardOutput;
        public bool WaitForExit;

        public Process Stop(String processid)
        {
            throw new NotImplementedException();
        }

       public Process Run(Dictionary<String, String> args )
       {
            String domainname = null, pname=null, domainusername = null, domainuserpw = null, wpath = null, pargs = null;


            args?.TryGetValue(Constants.argsEnum.processpath.ToString(), out pname);

            args?.TryGetValue(Constants.argsEnum.domainname.ToString(), out domainname);
            args?.TryGetValue(Constants.argsEnum.domainusername.ToString(),out domainusername);
            args?.TryGetValue(Constants.argsEnum.domainuserpw.ToString(),out domainuserpw);
            args?.TryGetValue(Constants.argsEnum.pwdir.ToString(), out wpath);
                args?.TryGetValue(Constants.argsEnum.pargs.ToString(),out pargs);
            Process p = new Process();
            var startInfo = new ProcessStartInfo(pname, pargs?? "");
            startInfo.WorkingDirectory = wpath ?? System.IO.Directory.GetCurrentDirectory();


            startInfo.RedirectStandardOutput = RedirectStandardOutput;
            if (!String.IsNullOrWhiteSpace(domainname))
            {
                startInfo.Domain = domainname;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = false;

            }
            if (!String.IsNullOrWhiteSpace(domainusername))

                startInfo.UserName = domainusername;

            if (!String.IsNullOrWhiteSpace(domainuserpw))
            {
                System.Security.SecureString ssPwd = new System.Security.SecureString();
                foreach (char c in domainuserpw) ssPwd.AppendChar(c);
                startInfo.Password = ssPwd;
            }
            p.StartInfo = startInfo;
            p.Start();
            if (WaitForExit) p.WaitForExit();
            
            return p;
        }
    }
}