using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows;
using System.Drawing;

namespace Miditor.MidiAnalysis
{
    public class ChartNotes
    {
        private Chart mChart;
        private Dictionary<string, int> nameNotes;

        public Dictionary<string, int> NameNotes
        {
            get { return nameNotes; }
            set { nameNotes = value; }
        }

        public Chart MChart
        {
            get { return mChart; }
            set { mChart = value; }
        }
        public ChartNotes()
        {
            MChart = new Chart();
        }
        public ChartNotes(Chart _mychart, Dictionary<string, int> _nameNotes)
        {
            MChart = _mychart;
            NameNotes = _nameNotes;
            MChart.Titles.Add(@"Biểu đồ các nốt nhạc ");
            MChart.Titles[0].Text = "ab";
            MChart.Enabled = true;
        }
        public void Draw()
        {
            /*Series s = new Series();*/
            MChart.ChartAreas[0].AxisY.Maximum = NameNotes.Values.Max();
            
            /*MChart.Series.Clear();*/
            MChart.ChartAreas[0].AxisX.Interval = 1;
            foreach (var item in NameNotes)
            {
                MChart.Series[0].Points.AddXY(item.Key.ToString(),item.Value);   
                
            }
        }
    }
}
