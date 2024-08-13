using HomeAutomationServiceLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomationServiceConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            HomeAutomationServiceWrapper.OnStart(null);
            HomeAutomationServiceWrapper.OnStop();

            Console.ReadLine();
        }
    }
}
