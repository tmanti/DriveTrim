using System;
using System.IO;

namespace ImageCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing histogram: ");
            
            ImageComparison.ComputeHistogram("D:\\histogram\\IMG_4929.jpg");
        }
    }
}