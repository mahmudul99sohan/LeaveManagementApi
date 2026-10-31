using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagementApi.Application.DTOs.LeaveRequests;

public class UpdateLeaveRequestStatusDto
{
    [Required]
    public string Status { get; set; } = string.Empty;
}