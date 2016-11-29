
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApplication.Jobs
{
    public class IOJobPlugin : ISchedulerPlugin, IJobListener
    {
        public class Regter
        {
            static IScheduler scheduler;
            public static void RegJobs()
            {
                NameValueCollection properties = new NameValueCollection();
                properties["quartz.threadPool.threadCount"] = "5";
                properties.Add(Plugin, Location);
                ISchedulerFactory schedulerFactory = new StdSchedulerFactory(properties);
                scheduler = schedulerFactory.GetScheduler();
                scheduler.Start();



            }

            public static void Close()
            {
                scheduler.Shutdown();
                //   scheduler.Clear();
            }
        }

        NLog.ILogger logger = NLog.LogManager.GetCurrentClassLogger();
        public const string Plugin = "quartz.plugin.IOJobPlugin.type";
        public const string Location = "HomeApplication.Jobs.IOJobPlugin,HomeApplication";
        string name;
        private IScheduler scheduler;

        public string JobInitializationPluginGroup { get; private set; }
        public string ConfigFileCronExpression { get; private set; }

        public IOJobPlugin()
        {
            JobInitializationPluginGroup = "1";

            ConfigFileCronExpression = "0/10 * * * * ?";
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public void Initialize(string pluginName, IScheduler sched)
        {
            name = pluginName;
            scheduler = sched;
            logger.Info("IOJobPlugin Init");
            CopyFileServiceJob();


            logger.Info("IOJobPlugin Init end");
        }

        public void JobExecutionVetoed(IJobExecutionContext context)
        {
            Console.WriteLine("否决时执行");
            //throw new NotImplementedException();
        }

        public void JobToBeExecuted(IJobExecutionContext context)
        {
            Console.WriteLine("执行前执行");
            //throw new NotImplementedException();
        }

        public void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
        {
            JobKey jobKey = context.JobDetail.Key;
            JobDataMap data = context.JobDetail.JobDataMap;
            int rowCount = data.GetInt(jobKey.Name);
        }

        public void Shutdown()
        {
            scheduler.Shutdown();
        }

        public void Start()
        {
            scheduler.Start();
        }

        private void CopyFileServiceJob()
        {
            var jobData = new JobDataMap();
            IJobDetail jobToParaDM = JobBuilder.Create<CopyFileServiceJob>()
              .WithDescription("Job to rescan jobs from CopyFileServiceJob")
              .WithIdentity(new JobKey("1", JobInitializationPluginGroup))
              .UsingJobData(jobData)
              .Build();


            TriggerKey triggerKey = new TriggerKey("1", JobInitializationPluginGroup);

            ITrigger trigger = TriggerBuilder.Create()
                 .WithCronSchedule(ConfigFileCronExpression)
                 .StartNow()
                 .WithDescription("trigger for Update")
                 .WithIdentity(triggerKey)
                 .WithPriority(1)
                 .Build();
            scheduler.ScheduleJob(jobToParaDM, trigger);
        }

    }
}
