using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Linq;

namespace Variety
{
    internal static class Services
    {
        public static bool IsInstalled(string serviceName)
        {
            return ServiceController.GetServices().Any(serviceController => serviceController.ServiceName.Equals(serviceName));
        }

        public static bool IsRunning(string serviceName)
        {
            var sc = new ServiceController {ServiceName = serviceName};
            return sc.Status == ServiceControllerStatus.Running;
        }

        public static void Start(string serviceName)
        {
            var service = new ServiceController(serviceName);
            try {
                if (IsRunning(serviceName)) return;
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromMilliseconds(20000));
            } catch (FormatException) {}
        }

        public static void Stop(string serviceName)
        {
            var service = new ServiceController(serviceName);
            try {
                if (!IsRunning(serviceName)) return;
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromMilliseconds(20000));
            } catch (FormatException) {}
        }
    }
}
