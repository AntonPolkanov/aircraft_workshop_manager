using System;


namespace Awm.ViewModels
{
    public class ServiceViewModel
    {
        public int ServiceId { get; set; }
        public int AircraftId { get; set; }
        public DateTime? Date { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ClientQuotesHrs { get; set; }
    }
}