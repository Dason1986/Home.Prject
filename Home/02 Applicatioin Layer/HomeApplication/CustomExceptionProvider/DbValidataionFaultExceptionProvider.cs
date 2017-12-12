using Library;
using Library.ExceptionProviders;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.ServiceModel;
using System.Text;

namespace HomeApplication.Owin
{
    /// <summary>
    /// 
    /// </summary>
    public class DbValidataionFaultExceptionProvider : CustomExceptionProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public override string Name
        {
            get
            {
                if (!string.IsNullOrEmpty(base.Name)) return base.Name;
                return "DbValidataionFaultException";
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
            var exception = error as System.Data.Entity.Validation.DbEntityValidationException;
            if (exception == null) return null;



            StringBuilder builder = new StringBuilder();

            builder.AppendLine(exception.Message);
            foreach (var entityeror in exception.EntityValidationErrors)
            {
                builder.AppendFormat("Entity:{0}", entityeror.Entry.Entity.GetType().Name);
                builder.AppendLine();
                foreach (System.Data.Entity.Validation.DbValidationError validationError in entityeror.ValidationErrors)
                {
                    builder.AppendFormat("PropertyName:{0}  ErrorMessage:{1}", validationError.PropertyName, validationError.ErrorMessage);
                    builder.AppendLine();
                }
                builder.AppendLine("-------------");
            }

            NLog.ILogger logger = NLog.LogManager.GetLogger(this.GetLogerName(error));
            logger.Error(builder.ToString());
            message = "驗證數據庫必填字段不通過";
            return new LogicException("驗證數據庫必填字段不通過"); 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public override bool HandleError(Exception error)
        {
            if (error is DbEntityValidationException == false) return false;
            return true;
        }
    }
}