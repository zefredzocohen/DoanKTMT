using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathWorks.MATLAB.NET.Arrays;
using System.IO;

namespace Miditor.MidiAnalysis
{
    // Lấy đầu vào là file midi cần phân tích
    // đầu ra midi đã được chuẩn hóa c#
    //      midi
    //           format: 0
    //           ticks_per_quarter_note: 384
    //           track: 
    //                  8 truong nho
    public class CbReadmidiML
    {
        private string strFileNameSource;
        private MLMidi.ReadFile midi;
        private string strSize;

        public CbMidiNormalization midiStruct;
        public MLMidi.ReadFile Midi
        {
            get { return midi; }
            set { midi = value; }
        }
        public string StrFileNameSource
        {
            get { return strFileNameSource; }
            set { strFileNameSource = value; }
        }
        public string StrSize
        {
            get { return strSize; }
            set { strSize = value; }
        }
        public CbReadmidiML()
        {

        }
        public CbReadmidiML(string _strFileName)
        {
            StrFileNameSource = _strFileName;
            Midi = new MLMidi.ReadFile();
            FileInfo fileinfo = new FileInfo(StrFileNameSource);
            StrSize = fileinfo.Length.ToString();
            midiStruct = new CbMidiNormalization();
        }
        public void Normalization()
        {
            List<int> _data ;
            int temp;
            midiStruct.midiSource = (MWArray)midi.readmidi(1, StrFileNameSource).GetValue(0);
            var CbmidiArray = midiStruct.midiSource.ToArray();
            var CbmidiArray2 = midi.ConvertMatrix(1, midiStruct.midiSource).ToArray();
            midiStruct.format = (int)((double[,])(CbmidiArray.GetValue(0, 0, 0)))[0, 0];
            midiStruct.ticks_per_quarter_note = (int)((double[,])(CbmidiArray.GetValue(1, 0, 0)))[0, 0];
            midiStruct.track = (object[, ,])(((object[, ,])CbmidiArray.GetValue(2, 0, 0)))[0, 0, 0];
            for (int i = 0; i < midiStruct.track.GetLongLength(midiStruct.track.Rank - 1);i++ )
            {
                _data = new List<int>();
                var _dataArray = ((double[,])midiStruct.track[4, 0, i]);
                for (int j = 0; j < _dataArray.GetLength(0); j++)
                {
                    _data.Add((int)_dataArray[j,0]);
                } 
                var _chanel = ((double[,])midiStruct.track[5, 0, i]);
                if (_chanel.Length >= 1)
                {
                    temp = (int)_chanel[0, 0];
                }
                else
                {
                    temp = 0;
                }
                    midiStruct.trackNew.Add(new MidiTrack()
                    {
                        used_running_mode = (int)((double[,])midiStruct.track[0, 0, i])[0, 0],
                        deltatime = (int)((double[,])midiStruct.track[1, 0, i])[0, 0],
                        midimeta = (int)((double[,])midiStruct.track[2, 0, i])[0, 0],
                        type = (int)((double[,])midiStruct.track[3, 0, i])[0, 0],
                        listData = _data,
                        chan = temp
                    });
            }
        }
    }
    public class CbMidiNormalization
    {
        public MWArray midiSource;
        public int format;
        public int ticks_per_quarter_note;
        public object[, ,] track;
        public List<MidiTrack> trackNew;
        public CbMidiNormalization()
        {
            midiSource = null;
            trackNew = new List<MidiTrack>();
        }
    }
    public class MidiTrack
    {
        public int used_running_mode;
        public int deltatime;
        public int midimeta;
        public int type;
        public int n;
        public List<int> listData;
        public int chan;
        public MidiTrack()
        {
            listData = new List<int>();
        }
    }
}
