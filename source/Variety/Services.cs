using System;
using System.ServiceProcess;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Variety
{
    internal static class Services
    {
        public static bool IsHttpServiceRun { get; set; }
        public static bool IsSmtpServiceRun { get; set; }

        static Services()
        {
            IsHttpServiceRun = false;
            IsSmtpServiceRun = false;
        }

        public static bool IsInstalled(string ServiceName)
        {
            return ServiceController.GetServices().Any(serviceController => serviceController.ServiceName.Equals(ServiceName));
        }

        public static bool IsRunning(string ServiceName)
        {
            var sc = new ServiceController();
            sc.ServiceName = ServiceName;
            if (sc.Status == ServiceControllerStatus.Running) {
                return true;
            } else {
                return false;
            }
        }

        public static void Start(string ServiceName)
        {
            // do something
        }

        public static void Stop(string ServiceName)
        {
            // do something
        }

        public static void Restart(string ServiceName)
        {
            // do something
        }
    }
}
