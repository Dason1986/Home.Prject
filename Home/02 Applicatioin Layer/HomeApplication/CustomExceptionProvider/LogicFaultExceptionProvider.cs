using Library;
using Library.ExceptionProviders;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace HomeApplication.Owin
{
    /// <summary>
    /// 
    /// </summary>
    public class LogicFaultExceptionProvider : CustomExceptionProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public override string Name
        {
            get
            {
                if (!string.IsNullOrEmpty(base.Name)) return base.Name;
                return "LogicFaultException";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public override Exception ProvideFault(Exception error, ref string message)
        {
            if (error is LogicException == false) return null;
            var current = Thread.CurrentThread.CurrentUICulture;
      
            var logicex = (LogicException)error;
            try
            {
                message = error.Message;//  ResourceManager.GetString(error.Message, current);
                if (logicex.Args != null && logicex.Args.Length > 0 && message != null)
                {
                    message = string.Format(message, logicex.Args);
                }
            }
            catch (Exception)
            {
            }
            if (string.IsNullOrWhiteSpace(message))
            {
                message = error.Message;
            }
        
            LogicException fe = new LogicException(message);
            return fe;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public override bool HandleError(Exception error)
        {
            if (error is LogicException == false) return false;
            return true;
        }
    }
}