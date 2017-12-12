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
    public class UnkownExceptionProvider : CustomExceptionProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public override string Name
        {
            get
            {
                if (!string.IsNullOrEmpty(base.Name)) return base.Name;
                return "UnkownException";
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
            logger.Error(error);
            return new FaultException("Failure");

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public override bool HandleError(Exception error)
        {

            return true;
        }

    }
}