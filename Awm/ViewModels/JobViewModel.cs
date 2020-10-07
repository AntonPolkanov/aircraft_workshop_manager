namespace Awm.ViewModels
{
    public class JobViewModel
    {
        public int JobId { get; set; }
        public string EmailAddressId { get; set; }
        public string JobDescription { get; set; }
        public int? AllocatedHours { get; set; }
        public int? CumulativeHours { get; set; }
        public byte? Status { get; set; }
    }
}