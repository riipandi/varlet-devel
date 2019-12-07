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
        public static bool IsServiceInstalled(string ServiceName)
        {
            return ServiceController.GetServices().Any(serviceController => serviceController.ServiceName.Equals(ServiceName));
        }

        /// <summary>
        /// Start a service by it's name
        /// </summary>
        /// <param name="ServiceName"></param>
        public static void StartService(string ServiceName)
        {
            ServiceController sc = new ServiceController();
            sc.ServiceName = ServiceName;

            Console.WriteLine("The {0} service status is currently set to {1}", ServiceName, sc.Status.ToString());

            if (sc.Status == ServiceControllerStatus.Stopped)
            {
                // Start the service if the current status is stopped.
                Console.WriteLine("Starting the {0} service ...", ServiceName);
                try
                {
                    // Start the service, and wait until its status is "Running".
                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running);

                    // Display the current service status.
                    Console.WriteLine("The {0} service status is now set to {1}.", ServiceName , sc.Status.ToString());
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine("Could not start the {0} service.", ServiceName);
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Service {0} already running.", ServiceName);
            }
        }

        /// <summary>
        /// Stop a service that is active
        /// </summary>
        /// <param name="ServiceName"></param>
        public static void StopService(string ServiceName)
        {
            ServiceController sc = new ServiceController();
            sc.ServiceName = ServiceName;

            Console.WriteLine("The {0} service status is currently set to {1}", ServiceName , sc.Status.ToString());

            if (sc.Status == ServiceControllerStatus.Running)
            {
                // Start the service if the current status is stopped.
                Console.WriteLine("Stopping the {0} service ...", ServiceName);
                try
                {
                    // Start the service, and wait until its status is "Running".
                    sc.Stop();
                    sc.WaitForStatus(ServiceControllerStatus.Stopped);

                    // Display the current service status.
                    Console.WriteLine("The {0} service status is now set to {1}.", ServiceName, sc.Status.ToString());
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine("Could not stop the {0} service.", ServiceName);
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Cannot stop service {0} because it's already inactive.", ServiceName);
            }
        }

        /// <summary>
        ///  Verify if a service is running.
        /// </summary>
        /// <param name="ServiceName"></param>
        public static bool IsServiceRunning(string ServiceName)
        {
            var sc = new ServiceController();
            sc.ServiceName = ServiceName;

            if (sc.Status == ServiceControllerStatus.Running)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Reboots a service
        /// </summary>
        /// <param name="ServiceName"></param>
        public static void RestartService(string ServiceName)
        {
            if (IsServiceInstalled(ServiceName))
            {
                if (IsServiceRunning(ServiceName))
                {
                    StopService(ServiceName);
                }
                else
                {
                    StartService(ServiceName);
                }
            }else
            {
                Console.WriteLine("The given service {0} doesn't exists", ServiceName);
            }
        }
    }
}
