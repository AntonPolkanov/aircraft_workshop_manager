using System;

namespace Awm.ViewModels
{
    public class ServiceTimerViewModel
    {
        public int ServiceTimerId { get; set; }
        public int AircraftId { get; set; }
        public DateTime? NextServiceDate { get; set; }
        public byte? Status { get; set; }
    }
}