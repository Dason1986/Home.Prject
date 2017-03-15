using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using System.ServiceProcess;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace HomeApplication.Owin
{
    internal class Program
    {
        private IDictionary<string, int> pots = new Dictionary<string, int>()
        {
            { "FTP",21},{ "SSH",22},{ "FTP",21},{ "Telnet",23},{ "SMTP",25},{ "DNS",53},{ "HTTP",80},{ "POP3",110},{ "SQL 服务",118},
            { "RPC(远程过程调用)",135},{ "IMAP4",143},{ "SQL",156},{ "SNMP",161},{ "HTTPS",443},{ "SMB",445},{ "SMTP over SSL",465},{ "SMTP",587},{ "Games",666},
            { "FTP Protocol (control)over TLS / SSL",990},{ "NAS(Netnews Admin System)",991},{ "IMAP",993},{ "POP3",995},{ "SOCKS proxy",1080},{ "Microsoft SQL 服务器",1433},{ "Microsoft SQL 监视器",1434},{ "network monitoring tool",1984},{ "NFS Server",2049},
            { "Oracle database listening port for unsecure client connections to the listener",2483},{ "Oracle database listening port for SSL client connections to the listener",2484}, { "mysql",3306},{ "远程桌面协议（RDP）",3389},{ "eMule",4662},{ "eMule",4672},{ "XMPP / Jabber - client connection",5222},{ "XMPP / Jabber - default port for SSL Client Connection",5223},
            { "XMPP / Jabber - server connection",5269},{ "PostgreSQL",5432},{ "VNC remote desktop protocol - for incoming listening viewer",5500},{ "VNC remote desktop protocol - for use over HTTP",5800},{ "VNC remote desktop protocol",5900}
        };

        private static void Main(string[] args)
        {
            if (System.Environment.UserInteractive)
            {
                StartOptions option = OwinAppBootstrap.CraeteStratOptions();

                using (WebApp.Start(option))
                {
                    foreach (var item in option.Urls)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("Press [enter] to quit...");
                    Console.ReadLine();
                }
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                      new HomeOwinService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}