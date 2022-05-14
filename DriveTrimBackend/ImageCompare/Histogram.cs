using System;
using System.Drawing;

namespace ImageCompare
{
    public class Histogram
    {
        private float total;
        
        protected float[] _redHist; //52 each
        protected float[] _greenHist; //52 each
        protected float[] _blueHist; //52 each

        //public int[] texture_direction_hist;
        //public int[] texture_scale_hist;
        
        
        public Histogram()
        {
            _redHist = new float[] {0, 0, 0, 0, 0};
            _greenHist = new float[] {0, 0, 0, 0, 0};
            _blueHist = new float[] {0, 0, 0, 0, 0};
            total = 0;
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
            total++;
        }

        public void Normalize()
        {
            normalize_arr(_redHist, total);
            normalize_arr(_greenHist, total);
            normalize_arr(_blueHist, total);
        }

        private void normalize_arr(float[] arr, float count)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = arr[i] / count;
            }
        }

        public void Print_Hist()
        {
            Console.WriteLine("Red: " + String.Join(",", _redHist) + " | Green: " + String.Join(",", _greenHist) + " | Blue: " + String.Join(",", _blueHist));
        }
        
        public float ComputeDistance(Histogram B)
        {
            float dist = 0;

            dist += ChiDistance(_redHist, B._redHist);
            dist += ChiDistance(_greenHist, B._greenHist);
            dist += ChiDistance(_blueHist, B._blueHist);

            return dist;
        }

        private static float ChiDistance(float[] A, float[] B)
        {
            float sum = 0;

            for (int i = 0; i < A.Length; i++)
            {
                sum += ((A[i] - B[i]) * (A[i] - B[i])) / (A[i] + B[i]);
            }

            return 0.5f*sum;
        }
    }
}