using System;
using System.Drawing;

namespace ImageCompare
{
    public class ImageComparison
    {
        public static Histogram ComputeHistogram(String pathToFile)
        {
            Histogram histogram = new Histogram();

            Image image = Image.FromFile(pathToFile);
            Bitmap bmp = new Bitmap(image);

            Size size = bmp.Size;
            int h = size.Height;
            int w = size.Width;

            int pixelcnt = 0;

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    Color clr = bmp.GetPixel(x, y);
                    histogram.Count_Color(clr);
                    pixelcnt++;
                }
            }

            histogram.Print_Hist();
            Console.WriteLine(pixelcnt);
            
            return histogram;
        }
    }
}