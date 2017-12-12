using Library.ExceptionProviders;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;

namespace HomeApplication.Owin
{
    /// <summary>
    /// 
    /// </summary>
    public class NotImplementedExceptionProvider : CustomExceptionProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public override string Name
        {
            get
            {
                if (!string.IsNullOrEmpty(base.Name)) return base.Name;
                return "NotImplementedException";
            }
        }

        //   readonly IErrorLogRepository errorLogRepository = ServiceLocator.Instance.GetService<IErrorLogRepository>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public override Exception ProvideFault(Exception error, ref string message)
        {
            NLog.ILogger logger = NLog.LogManager.GetLogger(this.GetLogerName(error));
            message = "功能没实现";
            logger.Error(error,  "功能没实现，但被外部调用了！");
            return new FaultException("Failure");

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public override bool HandleError(Exception error)
        {

            return typeof(NotImplementedException).IsInstanceOfType( error) ;
        }

    }
}