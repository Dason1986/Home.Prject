
using Home.DomainModel.Aggregates.LogsAgg;
using Home.DomainModel.Aggregates.SystemAgg;
using Home.DomainModel.Repositories;
using Library;
using Library.ComponentModel.Model;
using NLog;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApplication.Jobs
{
    public interface IScheduleJobProvider
    {
    }

    public static class ScheduleJobHelper
    {
        public static void SetScheduleClass(this ScheduleJob ScheduleJob, Type classType)
        {
            if (classType == null) throw new ArgumentNullException("classType");
            if (!typeof(IScheduleJobProvider).IsAssignableFrom(classType)) throw new ArgumentNullException("classType");
            ScheduleJob.ScheduleJobClass = string.Format("{0},{1}", classType.FullName, classType.Assembly.GetName().Name);
        }

        public static void SetScheduleClass<TClass>(this ScheduleJob scheduleJob) where TClass : IScheduleJobProvider
        {
            SetScheduleClass(scheduleJob, typeof(TClass));
        }
    }

    public delegate void ScheduleJobLoadErrorEventHandler(object sender, ScheduleJobErrorEventArgs e);

    public delegate void ScheduleJobLoadRunningEventHandler(object sender, ScheduleJobErrorEventArgs e);

    public class ScheduleJobErrorEventArgs : EventArgs
    {
        public Exception Error { get; protected set; }

        public ScheduleJobErrorEventArgs(Exception error)
        {
            this.Error = error;
        }
    }

    public class ScheduleJobManagement
    {
        private IScheduleJobRepository _scheduleJobRepository;


        public ScheduleJobManagement(IScheduleJobRepository scheduleJobRepository)
        {
            _scheduleJobRepository = scheduleJobRepository;

        }

        private readonly ScheduleJobProviderCollection _providers = new ScheduleJobProviderCollection();

        public event ScheduleJobLoadErrorEventHandler ScheduleJobLoadError;

        public event ScheduleJobLoadRunningEventHandler ScheduleJobLoadRunning;

        private NLog.ILogger Logger = NLog.LogManager.GetCurrentClassLogger();

        protected void OnScheduleJobLoadRunning(Exception error)
        {
            ScheduleJobLoadRunningEventHandler handler = ScheduleJobLoadRunning;

            if (ScheduleJobLoadError == null) return;
            handler.Invoke(this, new ScheduleJobErrorEventArgs(error));
        }

        protected void OnScheduleJobLoadError(Exception error)
        {
            ScheduleJobLoadErrorEventHandler handler = ScheduleJobLoadError;
            if (ScheduleJobLoadError == null) return;
            handler.Invoke(this, new ScheduleJobErrorEventArgs(error));
        }

        public void LoadProvider()
        {
            if (_scheduleJobRepository == null) throw new ArgumentNullException("scheduleJobRepository");

            var scheduleJobs = _scheduleJobRepository.GetAll().Where(n => n.StatusCode == StatusCode.Enabled).ToArray();

            foreach (var item in scheduleJobs)
            {
                if (string.IsNullOrEmpty(item.ScheduleJobClass)) throw new Exception("Job class is null");




                try
                {
                    var itemScheduleJob = ScheduleJobProvider.CreateInstance(item.ScheduleJobClass);
                    if (itemScheduleJob == null) throw new Exception("Create Instance error");
                   
                    itemScheduleJob.ScheduleCronExpression = item.ScheduleCronExpression;
                    itemScheduleJob.Title = item.Title;
                    itemScheduleJob.Initialize();
                    if (string.IsNullOrEmpty(item.ScheduleCronExpression))
                    {
                        Logger.Warn("{0} not CronExpression config", item.Title);
                        continue;

                    }

                    _providers.Add(new ScheduleJob(itemScheduleJob) { Id = item.ID });

                }
                catch (Exception e)
                {
                    Logger.Info(e, "Load job eror:" + item.Title);
                    OnScheduleJobLoadRunning(e);
                }
            }
            Logger.Info("LoadProvider count:" + _providers.Count);

        }
        private void EmailJob()
        {
            var jobData = new JobDataMap();
            IJobDetail jobDomainEventBus = JobBuilder.Create<DomainEventBusJob>()
              .WithDescription("Job to rescan jobs from DomainEventBusJob")
              .WithIdentity(new JobKey("1", "1"))
              .UsingJobData(jobData)
              .Build();


            TriggerKey triggerKey = new TriggerKey("DomainEventBusJob", "1");

            ITrigger trigger = TriggerBuilder.Create()
                 .WithCronSchedule("0/5 * * * * ?")
                 .StartNow()
                 .WithDescription("trigger for DomainEventBusJob")
                 .WithIdentity(triggerKey)
                 .WithPriority(1)
                 .Build();
            scheduler.ScheduleJob(jobDomainEventBus, trigger);
            Logger.Trace("Load job :DomainEventBusJob");
        }
        public bool IsRuning { get; private set; }

        public void Run()
        {
            if (IsRuning) return;
            Logger.Trace("run schedulejob");
            IsRuning = true;
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

            scheduler = schedulerFactory.GetScheduler();
            int count = 1;
            EmailJob();
            foreach (var item in _providers)
            {
                var jobbuilder = JobBuilder.Create<JobItem>().WithIdentity(item.Provider.Title, "AppLogJobGroup");
                var job = jobbuilder.Build();
                job.JobDataMap.Put("instance", item);
                TriggerBuilder triggerbuilder = TriggerBuilder.Create();

                triggerbuilder.WithDescription(item.Provider.Title).WithPriority(count);
                count++;
                triggerbuilder.WithCronSchedule(item.Provider.ScheduleCronExpression);


                triggerbuilder = triggerbuilder.StartNow();
                //将任务与触发器关联起来放到调度器中
                scheduler.ScheduleJob(job, triggerbuilder.Build());
                Logger.Trace("Load job :{0}", item.Provider.Title);

            }

            scheduler.Start();
        }

        public void Pause()
        {
            if (scheduler != null)
                scheduler.PauseAll();
        }

        public void Continue()
        {
            if (scheduler != null)
                scheduler.ResumeAll();
        }

        private IScheduler scheduler;

        public void Stop()
        {
            if (!IsRuning) return;
            IsRuning = false;
            Logger.Info("stop schedulejob");
            scheduler.Shutdown(false);
        }


        //private void OnExecute(ScheduleJob item)
        //{
        //    if (item.IsRunning) return;
        //    item.RunTimes++;
        //    item.LastTime = DateTime.Now;
        //    ThreadPool.QueueUserWorkItem(n =>
        //    {
        //        item.IsRunning = true;
        //        try
        //        {
        //            Logger.Info("Execute schedulejob:" + item.Provider.GetType().ToString());
        //            item.Provider.Execute();
        //        }
        //        catch (Exception ex)
        //        {
        //            OnScheduleJobLoadRunning(ex);
        //        }
        //        item.IsRunning = false;
        //    });
        //}

        internal class ScheduleJob
        {
            public ScheduleJob(ScheduleJobProvider provider)
            {
                this.Provider = provider;
            }



            /// <summary>
            ///
            /// </summary>
            public int RunTimes { get; set; }

            /// <summary>
            ///
            /// </summary>
            public DateTime? LastTime { get; set; }

            /// <summary>
            ///
            /// </summary>
            public ScheduleJobProvider Provider { get; set; }

            public bool IsLock { get; internal set; }
            public TimeSpan LastElapsedTime { get; internal set; }
            public Guid Id { get; internal set; }

            internal void Lock()
            {
                IsLock = true;
            }
        }

        private class ScheduleJobProviderCollection : Collection<ScheduleJob>
        {
        }
    }

    public abstract class ScheduleJobProvider : IScheduleJobProvider
    {
        /// <summary>
        ///
        /// </summary>
    
        protected ILogger Logger = LogManager.GetCurrentClassLogger();

        public string Title { get; set; }
        public string ScheduleCronExpression { get; set; }

        public abstract void Initialize();

        public static ScheduleJobProvider CreateInstance(string className)
        {
            var type = Type.GetType(className);
            if (type == null) throw new TypeUnloadedException(className);
            return CreateInstance(type);
        }

        public static ScheduleJobProvider CreateInstance(Type classType)
        {
            if (classType == null) throw new ArgumentNullException("classType");
            if (!typeof(ScheduleJobProvider).IsAssignableFrom(classType)) throw new Exception("classType");
            var scheduleJobProvider = (ScheduleJobProvider)Activator.CreateInstance(classType);
            return scheduleJobProvider;
        }

        public abstract void Execute(IJobExecutionContext context);
    }

    [PersistJobDataAfterExecution]
    [DisallowConcurrentExecution]
    internal class JobItem : IJob
    {
        private ScheduleJobManagement.ScheduleJob job;

        public JobItem()
        {
        }



        public void Execute(IJobExecutionContext context)
        {
            if (job == null)
            {
                JobDataMap data = context.JobDetail.JobDataMap;
                job = data.Get("instance") as ScheduleJobManagement.ScheduleJob;
            }
            Stopwatch stopwatch = Stopwatch.StartNew();
            var logger = LogManager.GetLogger("ScheduleJobManagement");
            ScheduleJobLog log = new ScheduleJobLog(CreatedInfo.ScheduleJob) { ScheduleId = job.Id };
            try
            {

                job.RunTimes++;
                job.LastTime = DateTime.Now;
                job.IsLock = true;
                job.Provider.Execute(context);
            }
            catch (Exception ex)
            {
                if (ex is LogicException)
                {
                    logger.Warn(ex, "排程任務失敗,業務邏輯不通過");
                }
                //else if (ex is Infrastructure.ApplicationException)
                //{

                //}
                else
                {
                    logger.Error(ex, "排程任務失敗");
                }
                log.HasError = true;

            }
            finally
            {
                job.IsLock = false;
                stopwatch.Stop();
                job.LastElapsedTime = stopwatch.Elapsed;
                log.ElapsedTime = stopwatch.Elapsed;
                log.Created = DateTime.Now;
                if (stopwatch.Elapsed.TotalDays > 1)
                {
                    log.ElapsedTime = new TimeSpan(stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds);
                }
                logger.Info("run job:{0} runtimes:{1} LastElapsedTime:{2}", job.Provider.Title, job.RunTimes, job.LastElapsedTime);
          
                IScheduleJobLogRepository logRepository =        Bootstrap.Currnet.GetService< IScheduleJobLogRepository>();
                logRepository.Add(log);
                logRepository.UnitOfWork.Commit();

            }

        }
    }
}
