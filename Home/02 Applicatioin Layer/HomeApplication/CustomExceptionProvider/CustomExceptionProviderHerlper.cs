using Library;
using Library.ExceptionProviders;
using System;
using System.Linq;
using System.Reflection;

namespace HomeApplication.Owin
{
    public static class CustomExceptionProviderHelper
    {
     

        public static LogicException ProvideFault(this CustomExceptionCollection provider, Exception error, ref string mm)
        {
            var ff = string.Empty;
            var ex =
                provider.OfType<CustomExceptionElement>().Where(n => n.Provider.HandleError(error))
                    .Select(n => (LogicException)n.Provider.ProvideFault(error, ref ff))
                    .FirstOrDefault(tmpfe => tmpfe != null);
            mm = ff;
            return ex;
        }
     
    }
}