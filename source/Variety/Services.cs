using System;
using System.ServiceProcess;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Variety
{
    internal static class Services
    {
        public static bool IsServiceInstalled(string serviceName)
        {
            var services = ServiceController.GetServices();

            return services.Any(service => service.ServiceName == serviceName);
        }

        public static bool IsServiceRunning(string serviceName)
        {
            var services = ServiceController.GetServices();

            return services.Any(service => (service.ServiceName == serviceName) && (service.Status == ServiceControllerStatus.Running));
        }

        public static void StartService(string serviceName, int timeoutMilliseconds = 200)
        {
            try
            {
                var service = new ServiceController(serviceName);
                var timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch (FormatException)
            {
                // do something
            }
        }

        public static void RestartService(string serviceName, int timeoutMilliseconds)
        {
            var service = new ServiceController(serviceName);
            
            try
            {
                var millisec1 = Environment.TickCount;
                var timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                // count the rest of the timeout
                var millisec2 = Environment.TickCount;
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch (FormatException)
            {
                // do something
            }
        }

        public static void StopService(string serviceName, int timeoutMilliseconds)
        {
            try
            {
                var service = new ServiceController(serviceName);
                var timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
            }
            catch (FormatException)
            {
                // do something
            }
        }
    }
}
