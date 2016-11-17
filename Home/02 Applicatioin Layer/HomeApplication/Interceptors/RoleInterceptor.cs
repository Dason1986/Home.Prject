using Castle.DynamicProxy;
using System.IO;
using System.Linq;

namespace HomeApplication.Interceptors
{
    public class RoleInterceptor : IInterceptor
    {
        TextWriter _output;
        IUserManager UserManager;
        public RoleInterceptor(IUserManager userManager)
        {
            _output = new StringWriter();
            UserManager = userManager;
        }
        public void Intercept(IInvocation invocation)
        {
            //    Logger.Trace("Method:{0}", invocation.Method.Name);
            var user = UserManager.GetCurrentUser();
            if (user != null)
            {
                _output.Write("Calling method {0} with parameters {1}... ",
      invocation.Method.Name,
      string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray()));

                invocation.Proceed();

                _output.WriteLine("Done: result was {0}.", invocation.ReturnValue);
            }

        }
    }
   
}
