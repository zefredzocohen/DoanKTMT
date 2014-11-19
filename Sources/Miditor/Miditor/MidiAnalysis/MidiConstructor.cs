using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Miditor.MidiAnalysis
{
    public class MidiConstructor
    {
        Dictionary<string, int> listNotes ;
        public MidiConstructor()
        {
            listNotes = new Dictionary<string, int>();
        }
    }
    public enum MidiNotesss
    {
        xC = 0,// do
        xDb =1,// do thang
        xD = 2,// Re 
        xEb = 3,// Re thang
        xE = 4,// Mi
        xF = 5,// Pha
        xGb = 6,// Pha thang
        xG = 7,// Son
        xAb = 8,// Son thang
        xA = 9,// La
        xBb = 10,// La thang
        xB = 11// Si
    }
}
