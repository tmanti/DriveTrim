using Google.Apis.PhotosLibrary.v1.Data;

namespace DriveTrimBackend
{
    public class TrimDate
    {
        public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }

        public Date getDate()
        {
            return new Date()
            {
                Day = day,
                Month = month,
                Year = year,
            };
        }
    }
}