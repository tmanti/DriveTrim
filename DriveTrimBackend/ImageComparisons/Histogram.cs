using System;

namespace ImageComparisons
{
    public class Histogram
    {
        private int[] _redHist; //52 each
        private int[] _greenHist; //52 each
        private int[] _blueHist; //52 each

        //public int[] texture_direction_hist;
        //public int[] texture_scale_hist;
        
        
        public Histogram()
        {
            _redHist = new [] {0, 0, 0, 0, 0};
            _greenHist = new [] {0, 0, 0, 0, 0};
            _blueHist = new[] {0, 0, 0, 0, 0};
        }

        public void Count_Red(int val)
        {
            _redHist[val / 52]++;
        }

        public void Count_Green(int val)
        {
            _greenHist[val / 52]++;
        }

        public void Count_Blue(int val)
        {
            _blueHist[val / 52]++;
        }

        public void Print_Hist()
        {
            Console.WriteLine("Red: " + String.Join(",", _redHist) + " | Green: " + String.Join(",", _greenHist) + " | Blue: " + String.Join(",", _blueHist));
        }
    }
}