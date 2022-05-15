using System;
using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DriveTrimBackend
{
    public class Histogram
    {
        [JsonInclude]
        public string Name;
        
        [JsonInclude]
        public float Total;
        
        [JsonInclude]
        public float[] RedHist; //52 each
        [JsonInclude]
        public float[] GreenHist; //52 each
        [JsonInclude]
        public float[] BlueHist; //52 each

        //public int[] texture_direction_hist;
        //public int[] texture_scale_hist;
        
        
        public Histogram()
        {
            Name = "";
            RedHist = new float[] {0, 0, 0, 0, 0};
            GreenHist = new float[] {0, 0, 0, 0, 0};
            BlueHist = new float[] {0, 0, 0, 0, 0};
            Total = 0;
        }

        private void Count_Red(byte val)
        {
            RedHist[val / 52]++;
        }

        private void Count_Green(byte val)
        {
            GreenHist[val / 52]++;
        }

        private void Count_Blue(byte val)
        {
            BlueHist[val / 52]++;
        }

        public void Count_Color(Color color)
        {
            byte r = color.R;
            Count_Red(r);
            byte g = color.G;
            Count_Green(g);
            byte b = color.B;
            Count_Blue(b);
            Total++;
        }

        public void Normalize()
        {
            normalize_arr(RedHist, Total);
            normalize_arr(GreenHist, Total);
            normalize_arr(BlueHist, Total);
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
            Console.WriteLine(JsonSerializer.Serialize(this));
        }
        
        public float ComputeDistance(Histogram B)
        {
            float dist = 0;

            dist += ChiDistance(RedHist, B.RedHist);
            dist += ChiDistance(GreenHist, B.GreenHist);
            dist += ChiDistance(BlueHist, B.BlueHist);

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