using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Akka.Actor;
using Akka.Util.Internal;
using Unit2DataMontoring.Actors;

namespace Unit2DataMontoring
{
    public partial class Form1 : Form
    {
        private IActorRef _chartActor;
        private readonly AtomicCounter _seriesCounter = new AtomicCounter(1);

        public Form1()
        {
            InitializeComponent();
        }

        #region Initialization


        private void Main_Load(object sender, EventArgs e)
        {
            _chartActor = Program.ChartActors.ActorOf(Props.Create(() => new ChartingActor(sysChart)), "charting");
            var series = ChartDataHelper.RandomSeries("FakeSeries" + _seriesCounter.GetAndIncrement());
            _chartActor.Tell(new ChartingActor.InitializeChart(new Dictionary<string, Series>()
            {
                {series.Name, series}
            }));
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //shut down the charting actor
            _chartActor.Tell(PoisonPill.Instance);

            //shut down the ActorSystem
            Program.ChartActors.Terminate();
        }


        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            var series = ChartDataHelper.RandomSeries("FakeSeries" + _seriesCounter.GetAndIncrement());
            _chartActor.Tell(new ChartingActor.AddSeries(series));
        }
    }
}
