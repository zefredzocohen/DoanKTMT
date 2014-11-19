using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Miditor
{
    public partial class chartMidi : Form
    {
        public Chart chartnotes;
        public chartMidi()
        {
            InitializeComponent();
            chartnotes = chart1;
        }

        private void chartMidi_Load(object sender, EventArgs e)
            {
            
        }
    }
}
