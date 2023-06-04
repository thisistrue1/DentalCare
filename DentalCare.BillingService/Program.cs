using Autofac;
using DentalCare.BLL;
using DentalCare.BillingService.Utility;
using DentalCare.BLL.Interface;
using DentalCare.DAL.Interface;
using DentalCare.DAL.Repository;
using DentalCare.Logging;
using ibex4.logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DentalCare.BillingService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            IContainer container = IoC_Resolve();

            ServiceBase service = container.Resolve<ServiceBase>();

            if (Environment.UserInteractive)
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] { service };
                RunInteractive(ServicesToRun);
            }
            else
            {
                ServiceBase.Run(service);
            }
        }

        static void RunInteractive(ServiceBase[] servicesToRun)
        {
            Console.WriteLine("Services running in interactive mode.");
            Console.WriteLine();

            MethodInfo onStartMethod = typeof(ServiceBase).GetMethod("OnStart",
                BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (ServiceBase service in servicesToRun)
            {
                Console.Write("Starting {0}...", service.ServiceName);
                onStartMethod.Invoke(service, new object[] { new string[] { } });
                Console.Write("Started");
            }
        }

        private static IContainer IoC_Resolve()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DentalCareEntities"].ConnectionString;

            ContainerBuilder builder = new ContainerBuilder();

            GlobalConfigurations globalConfigurations = new GlobalConfigurations()
            {
                AppName = System.AppDomain.CurrentDomain.FriendlyName,
                XSLTPath = string.Format(@"{0}\xslt\DentalCare_Bill.xslt", AppDomain.CurrentDomain.BaseDirectory),
                PDFPath = ConfigurationManager.AppSettings["BillPDFFolder"]
            };

            builder.RegisterInstance(globalConfigurations).As<GlobalConfigurations>().SingleInstance();


            builder.RegisterType<BillingService>().As<ServiceBase>().InstancePerLifetimeScope();
            builder.RegisterType<BillingBLL>().As<IBillingBLL>().InstancePerLifetimeScope();
            builder.RegisterType<DentalCareRepository>().As<IDentalCareRepository>()
                .WithParameter("connectionString", connectionString).InstancePerLifetimeScope();

            builder.RegisterType<LogHelper>().As<ILogHelper>().InstancePerLifetimeScope();

            IContainer container = builder.Build();
            return container;
        }
    }
}
