using System;

namespace Awm.ViewModels
{
    public class MaterialViewModel
    {
        public int MaterialId { get; set; }
        public int JobId { get; set; }
        public DateTime? IntakeDate { get; set; }
        public DateTime? BestBeforeDate { get; set; }
        public string Gnr { get; set; }
    }
}