using System;

namespace Awm.ViewModels
{
    public class AircraftImageViewModel
    {
        public int ImageId { get; set; }
        public DateTime? DateTime { get; set; }
        public string Comment { get; set; }
        public string S3Path { get; set; }
    }
}