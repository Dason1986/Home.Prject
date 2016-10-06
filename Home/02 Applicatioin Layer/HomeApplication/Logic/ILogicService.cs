using NLog;
using System;
using System.Threading;

namespace HomeApplication.Logic
{

    public interface ILogicService
    {
        IOption Option { get; set; }

        void Run();
    }

    public interface IOption
    {

    }
    public interface IOptionCommandBuilder
    {
        void RumCommandLine();
        IOption GetOption();
    }
    public interface IOptionCommandBuilder<TOption> : IOptionCommandBuilder where TOption : IOption
    {


        new TOption GetOption();
    }
    public sealed class EmptyOption : IOption
    {
        static EmptyOption()
        {
            Epmty = new EmptyOption();

        }
        public static EmptyOption Epmty { get; set; }
    }
    public class EmptyOptionCommandBuilder : IOptionCommandBuilder
    {
        public IOption GetOption()
        {
            return EmptyOption.Epmty;
        }

        public void RumCommandLine()
        {
           
        }
    }
    public abstract class BaseMultiThreadingLogicService : BaseLogicService
    {
        public BaseMultiThreadingLogicService()
        {
            ThreadCount = 3;
            BatchSize = 3;
        }
        protected virtual void ThreadPross(int begin, int end, int size)
        {
            Logger.Info(string.Format("begin:{0} end:{1}", begin, end - 1));
            for (int index = begin; index < end; index = index + size)
            {
                var endindex = index + size;
                if (endindex >= end) endindex = end;
                ThreadProssSize(index, endindex);
            }
        }
        public int ThreadCount { get; protected set; }

        public int BatchSize { get; set; }

        public sealed override void Run()
        {
            Logger.Info(string.Format("開始執行"), 1);


            var filecount = GetTotalRecord();
            ThreadPross(filecount, BatchSize);



        }
        protected abstract int GetTotalRecord();
        protected virtual void ThreadPross(int totalRecord, int size)
        {


            var total = totalRecord / size;
            if (total <= ThreadCount)
            {

                ThreadPross(0, totalRecord, size);

            }
            else
            {
                Thread[] thrads = new Thread[ThreadCount];
                var totalsize = (decimal)totalRecord / ThreadCount;
                for (int i = 0; i < ThreadCount; i++)
                {
                    int currentindex = i;
                    thrads[currentindex] = new Thread(n =>
                    {
                        int beging = (int)Math.Ceiling(totalsize * currentindex);
                        var end = (int)Math.Floor(beging + totalsize);
                        if (end >= totalRecord)
                        {
                            end = totalRecord;
                        }

                        ThreadPross(beging, end, size);

                    });

                }
                for (int i = 0; i < ThreadCount; i++)
                {
                    thrads[i].Start();
                }
                for (int i = 0; i < ThreadCount; i++)
                {
                    thrads[i].Join();
                }
            }
        }
        protected abstract void ThreadProssSize(int beginindex, int endindex);
    }
    public abstract class BaseLogicService : ILogicService
    {
        public BaseLogicService()
        {
            Logger = LogManager.GetCurrentClassLogger();
        }
        protected NLog.ILogger Logger { get; set; }
        IOption ILogicService.Option
        {
            get
            {
                return ServiceOption;
            }

            set
            {
                ServiceOption = value;
            }
        }
        protected abstract IOption ServiceOption { get; set; }

        public abstract void Run();

    }
}