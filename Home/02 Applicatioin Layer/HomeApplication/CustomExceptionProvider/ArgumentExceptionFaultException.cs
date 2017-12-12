using Library;
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
    public class ArgumentExceptionProvider : CustomExceptionProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public override string Name
        {
            get
            {
                if (!string.IsNullOrEmpty(base.Name)) return base.Name;
                return "ArgumentException";
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

            if (error is ArgumentException == false) return null;
            var argumentNull = (ArgumentException)error;

            message = argumentNull.Message;
            return new LogicException(argumentNull.Message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public override bool HandleError(Exception error)
        {
            return error is ArgumentException;
        }
    }
}