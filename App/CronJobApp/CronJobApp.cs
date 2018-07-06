using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication2.App.Runners;
using WebApplication2.Interface;
using WebApplication2.Utils;
using YamlDotNet.RepresentationModel;

namespace WebApplication2.App.CronJobApp
{
    public class CronJobApp : Runner<String, String, IEnumerable<Task>>
    {
        


        public IEnumerable<Task> Run(String dirPath)
        {
            
            Logger.Log("Starting jobs at "+ dirPath);
            List<Task> tlist = new List<Task>();
            var yaml = new YamlStream();
            CronRunner cr = new CronRunner();
            try
            {
                foreach (var file in Directory.GetFiles(dirPath, "*.yaml"))
                {
                    yaml.Load(new FileInfo(file).OpenText());
                    tlist.Add(cr.Run(new ScriptJobConfig((yaml.Documents[0].RootNode as YamlMappingNode).Children.ToDictionary(e => e.Key.ToString(), e => e.Value?.ToString()))));

                }
            }catch(Exception ex)
            {
                Logger.Log(ex);
            }
            return tlist;
        }

        public void StopAll()
        {
           
            
            Task.WaitAll( CronAppHelper.stopAllRunners()?.ToArray());

        }

        public void Start(String path)
        {


            Task.WaitAll(Run(path)?.ToArray());

        }

        public void StopAllAndRemove()
        {
            
            Task.WaitAll( CronAppHelper.resumeAllRunners()?.ToArray());

        }

        public void Resume(String id)
        {
            Task.WaitAll(CronAppHelper.resumeRunner(id)?.ToArray());

        }

        public void ResumeAll()
        {
            Task.WaitAll(CronAppHelper.resumeAllRunners()?.ToArray());

        }

        public void Pause(String id)
        {
            Task.WaitAll( CronAppHelper.stopRunner(id,true)?.ToArray());

        }

        public void PauseAll()
        {
            Task.WaitAll(CronAppHelper.pauseAllRunners()?.ToArray());

        }



        public IEnumerable<Task> Stop(String id)
        {
            return CronAppHelper.stopRunner(id)?.ToArray();
            
        }

        
    }
}