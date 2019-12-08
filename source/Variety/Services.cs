using System;
using System.ServiceProcess;
using System.Linq;

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

        public static bool IsInstalled(string serviceName)
        {
            return ServiceController.GetServices().Any(serviceController => serviceController.ServiceName.Equals(serviceName));
        }

        public static bool IsRunning(string serviceName)
        {
            var sc = new ServiceController {ServiceName = serviceName};
            return sc.Status == ServiceControllerStatus.Running;
        }

        public static bool Start(string serviceName)
        {
            while (IsRunning(serviceName) == false)
            {
                var service = new ServiceController(serviceName);
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(3000));
            }
            return true;
        }

        public static bool Stop(string serviceName)
        {
            var service = new ServiceController(serviceName);
            service.Stop();
            service.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(3000));
            return true;
        }

        public static void Restart(string serviceName)
        {
            // do something
        }
    }
}
