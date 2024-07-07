﻿using OutOfOfficeApp.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutOfOfficeApp.Models
{
    public class LeaveRequestDtoOut
    {
        public int LeaveRequestID { get; set; }
        public int Employee { get; set; }
        public AbsenceReason AbsenceReason { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Comment { get; set; }
        public LeaveRequestStatus Status { get; set; }
    }
}
