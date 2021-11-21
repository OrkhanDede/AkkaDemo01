using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Akka.Actor;

namespace Unit2DataMontoring
{
    static class Program
    {
        /// <summary>
        /// ActorSystem we'll be using to publish data to charts
        /// and subscribe from performance counters
        /// </summary>
        public static ActorSystem ChartActors;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ChartActors = ActorSystem.Create("ChartActors");

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
