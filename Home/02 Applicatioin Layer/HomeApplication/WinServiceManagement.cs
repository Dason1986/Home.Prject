using System;
using System.Configuration.Install;
using System.ServiceProcess;

namespace HomeApplication
{
    public static class WinServiceManagement
    {
        #region 检查服务存在的存在性

        /// <summary>
        /// 检查服务存在的存在性
        /// </summary>
        /// <param name=" NameService ">服务名</param>
        /// <returns>存在返回 true,否则返回 false;</returns>
        public static bool IsServiceExisted(string NameService)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController s in services)
            {
                if (s.ServiceName.ToLower() == NameService.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        #endregion 检查服务存在的存在性

        public static void Run(string[] args, string name)
        {
            ServiceController service = new ServiceController(name);
            if (args[0].ToLower() == "/i" || args[0].ToLower() == "-i")
            {
                #region 安装服务

                if (!IsServiceExisted(name))
                {
                    try

                    {
                        string[] cmdline = { };

                        string serviceFileName = System.Reflection.Assembly.GetCallingAssembly().Location;
                        TransactedInstaller transactedInstaller = new TransactedInstaller();
                        AssemblyInstaller assemblyInstaller = new AssemblyInstaller(serviceFileName, cmdline);
                        transactedInstaller.Installers.Add(assemblyInstaller);

                        transactedInstaller.Install(new System.Collections.Hashtable());
                        TimeSpan timeout = TimeSpan.FromMilliseconds(1000 * 10);
                        service.Start();
                        service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                #endregion 安装服务
            }
            else if (args[0].ToLower() == "/u" || args[0].ToLower() == "-u")

            {
                #region 删除服务

                try
                {
                    if (IsServiceExisted(name))
                    {
                        string[] cmdline = { };
                        string serviceFileName = System.Reflection.Assembly.GetCallingAssembly().Location;

                        TransactedInstaller transactedInstaller = new TransactedInstaller();
                        AssemblyInstaller assemblyInstaller = new AssemblyInstaller(serviceFileName, cmdline);
                        transactedInstaller.Installers.Add(assemblyInstaller);
                        transactedInstaller.Uninstall(null);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion 删除服务
            }
        }
    }
}