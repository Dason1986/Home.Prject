using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApplication.Services
{
    /// <summary>
    ///
    /// </summary>
    public class PMSServiceImpl : ServiceImpl, IPMSService
    {
        public override string ServiceName { get { return "PMS Service"; } }

        public bool ValidateUser(string name, string pass)
        {
            return true;
        }
    }
}