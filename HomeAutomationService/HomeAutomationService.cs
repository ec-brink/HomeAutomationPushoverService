using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using HomeAutomationServiceLogic;

namespace HomeAutomationService
{
    public partial class HomeAutomationService : ServiceBase
    {
        public HomeAutomationService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            HomeAutomationServiceWrapper.OnStart(args);
        }

        protected override void OnStop()
        {
            HomeAutomationServiceWrapper.OnStop();
        }

        protected override void OnShutdown()
        {
            HomeAutomationServiceWrapper.OnShutdown();
        }
    }
}
