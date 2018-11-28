using NewRelic.Api.Agent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsService1
{
    public partial class MyNewService1 : ServiceBase
    {
        private System.Diagnostics.EventLog eventLog1;
        private int eventId = 0;

        public MyNewService1()
        {
            InitializeComponent();
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("MySource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "MySource", "MyNewLog");
            }
            eventLog1.Source = "MySource";
            eventLog1.Log = "MyNewLog";
        }

        //[Transaction]
        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("In OnStart");
            // Set up a timer to trigger every minute.  
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 60000; // 60 seconds  
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }

        //[Transaction]
        //[Trace]
        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.  
            eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);

            ProcessingStepCollection psc = new ProcessingStepCollection();
            psc.Add(new ProcessingStep());
            psc.Add(new ProcessingStep());
            psc.Add(new ProcessingStep());
            ExecutionSteps(psc);
        }

        public void ExecutionSteps(ProcessingStepCollection steps)
        {
            foreach (ProcessingStep step in steps)
            {
                ExecutionStep(null);
            }
        }

        public void ExecutionStep(object mypar)
        {
            DoSomeHeavyLifting();
            Thread.Sleep(500);
            DoSomeHeavyLifting(false);
            Thread.Sleep(500);
            DoSomeHeavyLifting("");
        }

        public HeavyLiftingResponse DoSomeHeavyLifting()
        {
            eventLog1.WriteEntry("In DoSomeHeavyLifting.");
            Thread.Sleep(250);

            HeavyLiftingResponse res = new HeavyLiftingResponse();
            res.result = true;

            return res;
        }

        public HeavyLiftingResponse DoSomeHeavyLifting(bool test)
        {
            eventLog1.WriteEntry("In DoSomeHeavyLifting (bool).");
            Thread.Sleep(375);

            HeavyLiftingResponse res = new HeavyLiftingResponse();
            res.result = true;

            return res;
        }

        public HeavyLiftingResponse DoSomeHeavyLifting(string test)
        {
            eventLog1.WriteEntry("In DoSomeHeavyLifting (string).");
            Thread.Sleep(125);

            HeavyLiftingResponse res = new HeavyLiftingResponse();
            res.result = true;

            return res;
        }

        //[Transaction]
        protected override void OnStop()
        {
            eventLog1.WriteEntry("In onStop.");
        }
    }

    public class HeavyLiftingResponse
    {
        public bool result { get; set; }
    }
}
