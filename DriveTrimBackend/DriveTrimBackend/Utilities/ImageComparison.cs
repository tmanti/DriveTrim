using System;
using System.Drawing;

namespace DriveTrimBackend
{
    public class ImageComparison
    {
        public static async void Compare(GoogleAPI googleApi, TrimRequest trim)
        {
            
        }
        
        public static Histogram ComputeHistogram(String pathToFile)
        {
            Histogram histogram = new Histogram();

            Image image = Image.FromFile(pathToFile);
            Bitmap bmp = new Bitmap(image);

            Size size = bmp.Size;
            int h = size.Height;
            int w = size.Width;


            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    Color clr = bmp.GetPixel(x, y);
                    histogram.Count_Color(clr);
                }
            }

            histogram.Print_Hist();
            histogram.Normalize();
            histogram.Print_Hist();

            return histogram;
        }

        public static void ComputeBaskets(int[] ids)//put baskets into db
        {
            
        }
    }
}