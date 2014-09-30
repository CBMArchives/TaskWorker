using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
    #########################################################################
    ##  THIS IS A SIMPLE TEST CONSOLE FOR TWK actionGeneral CLASS OBJECTS  ##
    #########################################################################
*/
namespace TaskWorker_TestConsole
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Hello. presss a key to start");
      string kpressed;
      kpressed = Console.ReadKey(true).KeyChar.ToString();

      //Bring a reference from your project and use instead of TaskWorker_TestAssembly.testlib1
       // .testlib1 t1 = new TaskWorker_TestAssembly.testlib1();

      //DLX_RR_Processes.process_PensionEligibility t1 = new DLX_RR_Processes.process_PensionEligibility();

      string loc = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).DirectoryName;
      string aStr = System.IO.File.ReadAllText(loc + "\\actionXML.xml");
      
      //Pass your <action/> defined as what your TWK action class is expecting.  My example class does care about the actionXML.  
      //t1.ActionXML = System.Xml.Linq.XElement.Parse(aStr);
      //t1.Completed += new TaskWorker_BLL.actionGeneral.CompletedEventHandler(t1_Completed);
      //t1.EventLog += new TaskWorker_BLL.actionGeneral.EventLogEventHandler(t1_EventLog);
      //t1.Start();
    }

    static void t1_EventLog(ref TaskWorker_BLL.actionGeneral sender, string Msg, TaskWorker_BLL.twk_LogLevels Level, System.Reflection.MethodBase exMethod)
    {
      Console.WriteLine( exMethod.Name +  ": "  + Msg); 
      //throw new NotImplementedException();
    }

    static void t1_Completed(object Sender, string UpdateStatus, string Comment)
    {
      Console.WriteLine("Process has complete.  Press a enter to close");
      Console.ReadLine();
      //throw new NotImplementedException();
    }




  }
}
