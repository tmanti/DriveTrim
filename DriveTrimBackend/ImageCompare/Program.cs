using System;
using System.IO;

namespace ImageCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing histogram: ");
            
            Histogram h1 = ImageComparison.ComputeHistogram("D:\\histogram\\IMG_4929.jpg");
            Histogram h2 = ImageComparison.ComputeHistogram("D:\\histogram\\IMG_4933.jpg");
            Histogram h8 = ImageComparison.ComputeHistogram("D:\\histogram\\IMG_4935.jpg");

            float distance = h1.ComputeDistance(h2);
            Console.WriteLine("Distance: " + distance);
            float distance5 = h1.ComputeDistance(h8);
            Console.WriteLine("Distance: " + distance5);
            
            Histogram h3 = ImageComparison.ComputeHistogram("D:\\histogram\\IMG_4975.jpg");
            
            float distance2 = h1.ComputeDistance(h3);
            Console.WriteLine("Distance: " + distance2);

            Histogram h4 = ImageComparison.ComputeHistogram("D:\\histogram\\20210505_130309(2).jpg");
            
            
            float distance3 = h1.ComputeDistance(h4);
            Console.WriteLine("Distance: " + distance3);
            
            float distance4 = h4.ComputeDistance(h3);
            Console.WriteLine("Distance: " + distance4);
            
            Histogram h5 = ImageComparison.ComputeHistogram("D:\\histogram\\20210505_130232.jpg");
            Console.WriteLine("Distance: " + h5.ComputeDistance(h4));
            
            Histogram h6 = ImageComparison.ComputeHistogram("D:\\histogram\\20220507_192558.jpg");
            Histogram h7 = ImageComparison.ComputeHistogram("D:\\histogram\\20220507_192547.jpg");
            Console.WriteLine("Distance: " + h6.ComputeDistance(h7));
        }
    }
}