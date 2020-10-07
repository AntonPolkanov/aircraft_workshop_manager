using System;

namespace Awm.ViewModels
{
    public class SparePartViewModel
    {
        public int PartId { get; set; }
        public int JobId { get; set; }
        public DateTime? IntakeDate { get; set; }
        public DateTime? BestBeforeDate { get; set; }
        public string Gnr { get; set; }
    }
}