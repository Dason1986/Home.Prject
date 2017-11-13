using Home.DomainModel.Repositories;
using Library;
using Library.Domain.DomainEvents;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApplication.Jobs
{


    public sealed class DomainEventBusJob : IJob
    {
        readonly static object _sync = new object();

        public string Name { get { return ""; } }

        public void Execute(IJobExecutionContext context)
        {
            lock (_sync)
            {

                // 執行條件：0/5 * * * * ?    每天的早上8-9，每半個鍾執行一次生成發送郵件列表


                var repository = Bootstrap.Currnet.GetService<IDomainEventArgsLogRepository>();
                var domainServiceManagement = new DomainServiceManagement();
                var events = repository.GetAllEvents();
                if (events.Length == 0) return;
                foreach (var item in events)
                {
                    var eventtype = Type.GetType(item.DomainEventType);

                    if (eventtype == null)
                    {
                        item.HasError = true;
                        item.ErrorMsg = "信息不齊全";
                        continue;
                    }
                    //  typeof(IEventHandler<>).
                    try
                    {
                        var domainEvent = Newtonsoft.Json.JsonConvert.DeserializeObject(item.DomainEvent, eventtype) as DomainEventArgs;

                        var domainService = domainServiceManagement.GetDomainService(eventtype);
                        domainService.Handle(domainEvent);
                        item.IsExecuted = true;
                    }
                    catch (Exception ex)
                    {
                        item.HasError = true;
                        item.ErrorMsg = ex.ToString();

                    }

                }
                repository.UnitOfWork.Commit();
                ///
            }
        }

        public void JobExecutionVetoed(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }

        public void JobToBeExecuted(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }

        public void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
        {
            throw new NotImplementedException();
        }
    }
}
