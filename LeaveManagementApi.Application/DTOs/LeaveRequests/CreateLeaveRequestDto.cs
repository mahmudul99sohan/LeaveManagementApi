using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagementApi.Application.DTOs.LeaveRequests;

public class CreateLeaveRequestDto : IValidatableObject
{
    [Required]
    public Guid EmployeeId { get; set; }

    [Required]
    public string LeaveType { get; set; } = string.Empty;

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    [StringLength(500)]
    public string Reason { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
    {
        if (StartDate.Date > EndDate.Date)
        {
            yield return new ValidationResult(
                "Start date cannot be after end date.",
                new[] { nameof(StartDate), nameof(EndDate) });
        }

        var allowedLeaveTypes = new[]
        {
        "Casual Leave",
        "Sick Leave",
        "Annual Leave",
        "Emergency Leave"
    };

        if (!string.IsNullOrWhiteSpace(LeaveType) &&
            !allowedLeaveTypes.Contains(
                LeaveType.Trim(),
                StringComparer.OrdinalIgnoreCase))
        {
            yield return new ValidationResult(
                "Invalid leave type. Allowed types are Casual Leave, Sick Leave, Annual Leave, and Emergency Leave.",
                new[] { nameof(LeaveType) });
        }
    }
}