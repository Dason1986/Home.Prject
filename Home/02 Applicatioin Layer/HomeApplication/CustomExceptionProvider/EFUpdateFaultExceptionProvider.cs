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
    public class EFUpdateFaultExceptionProvider : CustomExceptionProvider
    {

        /// <summary>
        /// 
        /// </summary>
        public override string Name
        {
            get
            {
                if (!string.IsNullOrEmpty(base.Name)) return base.Name;
                return "EFUpdateFaultException";
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
            var exception = error as System.Data.Entity.Core.UpdateException;
            if (exception == null) return null;



            StringBuilder builder = new StringBuilder();

            builder.AppendLine(exception.Message);
            if (exception.InnerException != null)
                builder.AppendLine(exception.InnerException.Message);
            foreach (var entityeror in exception.StateEntries)
            {
                builder.AppendFormat("Entity:{0}", entityeror.EntitySet.Name);
                builder.AppendLine();
                foreach (var propertyName in entityeror.GetModifiedProperties())
                {
                    var newvalue = entityeror.CurrentValues[propertyName];
                    builder.AppendFormat("PropertyName:{0}  value:{1}   ", propertyName, newvalue);
                    if (entityeror.State == System.Data.Entity.EntityState.Modified)
                    {
                        var oldvlaue = entityeror.OriginalValues[propertyName];
                        if (Equals(newvalue.ToString(), oldvlaue.ToString())) continue;
                        builder.AppendFormat(" old:{0}   ", oldvlaue);
                    }
                    builder.AppendLine();
                }
                builder.AppendLine("-------------");
            }
            var ex = new LogicException("更新數據庫失敗");
            NLog.ILogger logger = NLog.LogManager.GetLogger(this.GetLogerName(error));
            logger.Error(ex, builder.ToString());
            message = ex.Message;


            return ex;
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
    } /// <summary>
      /// 
      /// </summary>
    public class EFDbUpdateFaultExceptionProvider : CustomExceptionProvider
    {

        /// <summary>
        /// 
        /// </summary>
        public override string Name
        {
            get
            {
                if (!string.IsNullOrEmpty(base.Name)) return base.Name;
                return "DbUpdateException";
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
            var exception = error as System.Data.Entity.Infrastructure.DbUpdateException;
            if (exception == null) return null;



            StringBuilder builder = new StringBuilder();

            builder.AppendLine(exception.Message);
            if (exception.InnerException != null)
            {
                if (exception.InnerException.Message != exception.Message)
                    builder.AppendLine(exception.InnerException.Message);
                if (exception.InnerException.InnerException != null)
                    builder.AppendLine(exception.InnerException.InnerException.Message);
            }

            foreach (var entityeror in exception.Entries)
            {
                builder.AppendFormat("EntityState:{0}", entityeror.State);
                builder.AppendLine();

                foreach (var propertyName in entityeror.CurrentValues.PropertyNames)
                {
                    var currentvalue = entityeror.CurrentValues[propertyName];
                    if (currentvalue is System.Data.Entity.Infrastructure.DbPropertyValues)
                    {
                        var valueschild = currentvalue as System.Data.Entity.Infrastructure.DbPropertyValues;

                        System.Data.Entity.Infrastructure.DbPropertyValues originalValues = null;
                        if (entityeror.State == System.Data.Entity.EntityState.Modified)
                            originalValues = entityeror.OriginalValues[propertyName] as System.Data.Entity.Infrastructure.DbPropertyValues;
                        foreach (var item in valueschild.PropertyNames)
                        {
                            builder.AppendFormat("PropertyName:{0}_{1}  value:{2} ", propertyName, item, valueschild[item]);
                            if (originalValues != null)
                            {
                                builder.AppendFormat("   olld :{0}", originalValues[item]);
                            }
                            builder.AppendLine();
                        }

                    }
                    else
                    {
                        builder.AppendFormat("PropertyName:{0}  value:{1} ", propertyName, currentvalue);
                        if (entityeror.State == System.Data.Entity.EntityState.Modified)
                        {
                            builder.AppendFormat("   olld :{0}", entityeror.OriginalValues[propertyName]);
                        }
                        builder.AppendLine();
                    }

                }
                builder.AppendLine("-------------");
            }

            var ex = new LogicException("更新數據庫失敗");
            NLog.ILogger logger = NLog.LogManager.GetLogger(this.GetLogerName(error));
            logger.Error(ex, builder.ToString());
            message = ex.Message;


            return ex;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public override bool HandleError(Exception error)
        {
            if (error is System.Data.Entity.Infrastructure.DbUpdateException == false) return false;
            return true;
        }
    }
}