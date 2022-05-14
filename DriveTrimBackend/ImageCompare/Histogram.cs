using System;
using System.Drawing;

namespace ImageCompare
{
    public class Histogram
    {
        private float total;
        
        private float[] _redHist; //52 each
        private float[] _greenHist; //52 each
        private float[] _blueHist; //52 each

        //public int[] texture_direction_hist;
        //public int[] texture_scale_hist;
        
        
        public Histogram()
        {
            _redHist = new float[] {0, 0, 0, 0, 0};
            _greenHist = new float[] {0, 0, 0, 0, 0};
            _blueHist = new float[] {0, 0, 0, 0, 0};
        }

        private void Count_Red(byte val)
        {
            _redHist[val / 52]++;
        }

        private void Count_Green(byte val)
        {
            _greenHist[val / 52]++;
        }

        private void Count_Blue(byte val)
        {
            _blueHist[val / 52]++;
        }

        public void Count_Color(Color color)
        {
            byte r = color.R;
            Count_Red(r);
            byte g = color.G;
            Count_Green(g);
            byte b = color.B;
            Count_Blue(b);
        }

        public void Normalize()
        {
            
        }

        private void normalize_arr()
        {
            
        }

        public void Print_Hist()
        {
            Console.WriteLine("Red: " + String.Join(",", _redHist) + " | Green: " + String.Join(",", _greenHist) + " | Blue: " + String.Join(",", _blueHist));
        }
    }
}