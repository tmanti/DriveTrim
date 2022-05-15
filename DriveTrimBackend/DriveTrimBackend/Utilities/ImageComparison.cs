using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using Google.Apis.PhotosLibrary.v1.Data;

namespace DriveTrimBackend
{
    public class ImageComparison
    {
        public static async void Compare(GoogleAPI googleApi, TrimRequest trim)
        {
            IList<MediaItem> col = googleApi.get_range(trim.Access_Token, trim.Start_Date, trim.End_Date);

            HttpClient client = new HttpClient();
            
            //get name from client name
            string job = "testjob";
            googleApi.DB.StartJob(job);
            
            Dictionary<string, Histogram> hists = new Dictionary<string, Histogram>();

            foreach(MediaItem mi in col)
            {
                string baseurl = mi.BaseUrl+"=500w-500h";
                var bytes = await client.GetByteArrayAsync(baseurl);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "temp.jpg");
                await File.WriteAllBytesAsync(path, bytes);
                Histogram hist = ComputeHistogram(path);
                hist.Name = mi.Filename;
                hists.Add(mi.Id, hist);
                File.Delete(path);
            }
            
            client.Dispose();
            
            ComputeBaskets(googleApi, trim, hists);
        }
        
        public static void ComputeBaskets(GoogleAPI googleApi, TrimRequest trim, Dictionary<string, Histogram> hists)//put baskets into db
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
    }
}