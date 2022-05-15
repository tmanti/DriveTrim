namespace DriveTrimBackend
{
    public class TrimRequest
    {
        public string? Access_Token { get; set; }
        public TrimDate Start_Date { get; set; }
        public TrimDate End_Date { get; set; }
    }
}