using DentalCare.BLL.Interface;
using DentalCare.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Configuration;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using DentalCare.Logging;
using DentalCare.Utilities;
using DentalCare.BillingService.Utility;

namespace DentalCare.BillingService
{
    public partial class BillingService : ServiceBase
    {
        Timer _timer = new Timer();
        IBillingBLL _billingBLL;
        GlobalConfigurations _globalConfigurations;
        ILogHelper _logHelper;

        public BillingService(IBillingBLL billingBLL, GlobalConfigurations globalConfigurations, ILogHelper logHelper)
        {
            InitializeComponent();
            _billingBLL = billingBLL;
            _globalConfigurations = globalConfigurations;
            _logHelper = logHelper;
        }

        protected override void OnStart(string[] args)
        {
            _logHelper.Info("Service is started");
            _timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            _timer.Interval = 24 * 60 * 60 * 1000; //Run every day to check if it is the last day of the month
            _timer.Enabled = true;
        }

        protected override void OnStop()
        {
            _logHelper.Info("Service is stopped");
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            //Check if it is the last day of the month
            int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            if (DateTime.Now.Day != daysInMonth) return;

            _logHelper.Info("Generating bills...");
            try
            {
                List<Bill> bills = _billingBLL.GenerateBills(_globalConfigurations.AppName);
                foreach (var bill in bills)
                {
                    string billXML = bill.SerializeToXML();
                    if (!string.IsNullOrEmpty(billXML))
                    {
                        string pdfFile = string.Format(@"{0}\{1}_{2}.pdf", _globalConfigurations.PDFPath, DateTime.Now.ToString("yyyy-MM-dd"), bill.Id);
                        PDFOperator.CreatePDFFile(billXML, _globalConfigurations.XSLTPath, pdfFile);
                    }
                    _logHelper.Info(billXML);
                }
            }
            catch (Exception ex)
            {
                _logHelper.Info($"The Service occurs error - { ex.Message }");
            }
        }
    }
}
