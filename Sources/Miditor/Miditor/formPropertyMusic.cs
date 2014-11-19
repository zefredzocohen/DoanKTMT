using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Miditor.MidiAnalysis;

namespace Miditor
{
    public partial class formPropertyMusic : Form
    {
        public CbMidiInfoML _CbMidiInfoML;
        public CbReadmidiML _CbReadmidiML; 
        public formPropertyMusic()
        {
            InitializeComponent();
        }

        private void formPropertyMusic_Load(object sender, EventArgs e)
        {

        }
        public void UpdateInfo()
        {
            if (_CbMidiInfoML != null && _CbReadmidiML !=null)
            {
                lblLength.Text = _CbMidiInfoML.Endtime.ToString()+" s";
                lblLocation.Text = _CbReadmidiML.StrFileNameSource;
                lblSize.Text = _CbReadmidiML.StrSize + "Byte";
            }
        }
    }
}
