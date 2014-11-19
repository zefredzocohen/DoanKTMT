using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//////////////////////////////////////////////////////////////////////////
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
using MLMidi;
using Miditor.MidiAnalysis;
using System.Windows.Forms.DataVisualization.Charting;
using Midi;
using System.Threading;
using System.Media;

namespace Miditor
{
    public partial class Form1 : Form
    {
        CbReadmidiML midi;
        CbMidiInfoML midiInfo;
        /*Chart chartMidiNotes;*/
        ChartNotes mchartNotes;
        private string strFileName;
        public string StrFileName
        {
            get { return strFileName; }
            set { strFileName = value; }
        }

        public Form1()
        {
            InitializeComponent();
//             chartMidiNotes = new Chart();
//             chartMidiNotes.Titles.Add(@"Biểu đồ các nốt nhạc");
// 
//             chartMidiNotes.Enabled = false;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Mở file midi cần phân tích
            try
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Filter = "midi file(*midi)|*mid";
                    dialog.InitialDirectory = Application.StartupPath;
                    dialog.Title = "Open song midi";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        MessageBox.Show(dialog.FileName);
                        StrFileName = dialog.FileName;
                        midi = new CbReadmidiML(StrFileName);
                        midi.Normalization();
                        //////////////////////////////////////////////////////////////////////////
                        Console.WriteLine(midi.midiStruct.format);
                        Console.WriteLine(midi.midiStruct.ticks_per_quarter_note);
                        Console.WriteLine(midi.midiStruct.track);
                        midiInfo = new CbMidiInfoML(midi.midiStruct);
                        midiInfo.Normalization();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở file\n" + ex.ToString());
            }
        }
        private void analysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
//             chartMidiNotes.Enabled = true;
//             chartMidiNotes.Show();
//             chartMidiNotes.Location = new Point(10, 10);
//             chartMidiNotes.Size = new Size(100, 100);
//             chartMidiNotes.BorderlineColor = Color.Blue;
            
//             Chart mchart = new Chart();
//             initChart(mchart);
//             mchartNotes = new ChartNotes(chart1,midiInfo.NameNotes);
//             mchartNotes.Draw();
//             mchartNotes.MChart.Show();
            chartMidi midichart = new chartMidi();
            initChart(midichart.chartnotes);
            mchartNotes = new ChartNotes(midichart.chartnotes, midiInfo.NameNotes);
            mchartNotes.Draw();
            midichart.Show();

        }
        private void initChart(Chart _crt)
        {
            ((System.ComponentModel.ISupportInitialize)(_crt)).BeginInit();
            ChartArea crtArea = new ChartArea();
            crtArea.Name = "crtArea";
            _crt.ChartAreas.Add(crtArea);
            Legend crtLegend = new Legend();
            crtLegend.Name = "crtLegend";
            _crt.Legends.Add(crtLegend);
            _crt.Location = new Point(12, 27);
            _crt.Name = "crt";
            Series crtSeries = new Series();
            crtSeries.ChartArea = "crtArea";
            crtSeries.Legend = "crtLegend";
            crtSeries.Name = "crtSerier";
            _crt.Series.Add(crtSeries);
            _crt.Size = new System.Drawing.Size(568, 300);
            ((System.ComponentModel.ISupportInitialize)(_crt)).EndInit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
//             Midi.InputDevice input = InputDevice.InstalledDevices[0];
//             input.Open();
            OutputDevice output = OutputDevice.InstalledDevices[0];
            output.Open();
            output.SendNoteOn(Channel.Channel1, Pitch.C4, 80);
            
            output.SendNoteOn(Channel.Channel10, Pitch.A4, 80);
            Thread.Sleep(500);
            output.SendNoteOn(Channel.Channel11, Pitch.G0, 80);
            Thread.Sleep(500);
            output.SendNoteOn(Channel.Channel1, Pitch.C4, 80);
            Thread.Sleep(500);
            output.SendPitchBend(Channel.Channel1, 7000);
            SoundPlayer sp = new SoundPlayer(@"D:\Users\zero\Desktop\Midi Music Example\format1\999_doa_hong.mid");
            sp.SoundLocation = @"D:\\Users\\zero\\Desktop\\Midi Music Example\\format1\\999_doa_hong.mid";
            sp.Load();
            sp.Play();

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formPropertyMusic PropertyMusic = new formPropertyMusic();
            PropertyMusic._CbReadmidiML = midi;
            PropertyMusic._CbMidiInfoML = midiInfo;
            PropertyMusic.UpdateInfo();
            PropertyMusic.Show();
        }
    }
}
