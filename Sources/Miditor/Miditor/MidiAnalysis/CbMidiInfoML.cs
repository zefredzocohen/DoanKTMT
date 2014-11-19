using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathWorks.MATLAB.NET.Arrays;

namespace Miditor.MidiAnalysis
{
    // Đầu vào là midi dạng ở matlab
    // đầu ra midiinfo đã chuẩn hóa
    //          Notes: [,]
    //          endtime: 
    public class CbMidiInfoML
    {
        private double endtime;
        private List<Notes> listNote;
        private Dictionary<string, int> nameNotes;

        public Dictionary<string, int> NameNotes
        {
            get { return nameNotes; }
            set { nameNotes = value; }
        }
        public List<Notes> ListNote
        {
            get { return listNote; }
            set { listNote = value; }
        }

        public double Endtime
        {
            get { return endtime; }
            set { endtime = value; }
        }
        public MLMidi.AnalysisFile Analysis ;
        public CbMidiNormalization midiStruct;
        public CbMidiInfoML()
        {
            Analysis = new MLMidi.AnalysisFile();
        }
        public CbMidiInfoML(CbMidiNormalization _midiStruct)
        {
            Analysis = new MLMidi.AnalysisFile();
            midiStruct = _midiStruct;
        }
        public void Normalization()
        {
            ListNote = new List<Notes>();
            var NotesEndTime = Analysis.midiInfo(2, midiStruct.midiSource,0);
            var Notes = (double[,])(((MWNumericArray)NotesEndTime.GetValue(0)).ToArray());
            for (int i = 0; i < Notes.GetLength(0);i++ )
            {
                ListNote.Add(new Notes()
                    {
                        track           = (int)Notes[i, 0],
                        channel         = (int)Notes[i, 1],
                        numberNotes     = (int)Notes[i, 2],
                        vel             = (int)Notes[i, 3],
                        timeStart       =  Math.Round(Notes[i, 4], 4),
                        timeEnd         =  Math.Round(Notes[i, 5], 4),
                        numberMessage1  = (int)Notes[i, 6],
                        numberMessage2  = (int)Notes[i, 7],
                    });
            }
            Endtime = Math.Round(((double)((MWNumericArray)NotesEndTime.GetValue(1)).ToArray().GetValue(0, 0)),4);
            getNameNote(ListNote);
        }
        public void getNameNote(List<Notes> listNotes)
        {
            int number;
            string _name = "";
            NameNotes = new Dictionary<string, int>();
            for (int i = 0; i < listNote.Count;i++ )
            {
                number = listNote[i].numberNotes % 12;
                switch (number)
                {
                    case 0:
                        _name = MidiNotesss.xC.ToString();
                        break;
                    case 1:
                        _name = MidiNotesss.xDb.ToString();
                        break;
                    case 2:
                        _name = MidiNotesss.xD.ToString();
                        break;
                    case 3:
                        _name = MidiNotesss.xEb.ToString();
                        break;
                    case 4:
                        _name = MidiNotesss.xE.ToString();
                        break;
                    case 5:
                        _name = MidiNotesss.xF.ToString();
                        break;
                    case 6:
                        _name = MidiNotesss.xGb.ToString();
                        break;
                    case 7:
                        _name = MidiNotesss.xG.ToString();
                        break;
                    case 8:
                        _name = MidiNotesss.xAb.ToString();
                        break;
                    case 9:
                        _name = MidiNotesss.xA.ToString();
                        break;
                    case 10:
                        _name = MidiNotesss.xBb.ToString();
                        break;
                    case 11:
                        _name = MidiNotesss.xB.ToString();
                        break;
                    default:
                        break;

                }
                if (!NameNotes.ContainsKey(_name))
                {
                    NameNotes.Add(_name, 1);
                }
                else
                {
                    NameNotes[_name]++;
                }

            }
        }
    }

    public class Notes
    {
        public int channel;
        public int track;
        public int numberNotes;
        public int vel;
        public double timeStart;
        public double timeEnd;
        public int numberMessage1;
        public int numberMessage2;
        /*public List<nameNotes> namenotes;*/
    }
}
