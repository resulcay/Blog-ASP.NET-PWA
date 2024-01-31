using System.ServiceProcess;

namespace ReporterWindowsService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new BlogReporter()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
