using System;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Text;

namespace HomeApplication.Logic.IO
{
    public static class ExceptionProvider
    {
        public static string ProvideFault(Exception error)
        {

            if (error is DbEntityValidationException == true)
            {
                var exception = error as DbEntityValidationException;
                StringBuilder builder = new StringBuilder();

                builder.AppendLine(exception.Message);
                foreach (var entityeror in exception.EntityValidationErrors)
                {
                    builder.AppendFormat("Entity:{0}", entityeror.Entry.Entity.GetType().Name);
                    builder.AppendLine();
                    foreach (DbValidationError validationError in entityeror.ValidationErrors)
                    {
                        builder.AppendFormat("PropertyName:{0}  ErrorMessage:{1}", validationError.PropertyName, validationError.ErrorMessage);
                        builder.AppendLine();
                    }
                    builder.AppendLine("-------------");
                }

                return builder.ToString();
            }
            if (error is EntityException == true)
            {
                var exception = error as EntityException;
                StringBuilder builder = new StringBuilder();

                builder.AppendLine(exception.Message);
                if (exception.InnerException != null)
                {
                    builder.AppendLine(exception.InnerException.GetType().FullName);
                    builder.AppendLine(exception.InnerException.ToString());
                }

                return builder.ToString();
            }
            return error.GetType().FullName;
        }
    }
}