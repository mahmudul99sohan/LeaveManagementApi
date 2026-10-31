using System;
using System.Collections.Generic;
using System.Text;
using LeaveManagementApi.Domain.Entities;

namespace LeaveManagementApi.Application.Interfaces.Services;

public interface ILeaveRequestService
{
    Task<LeaveRequest?> GetByIdAsync(Guid id);

    Task<IReadOnlyList<LeaveRequest>> GetAllAsync();

    Task<IReadOnlyList<LeaveRequest>> GetByEmployeeIdAsync(Guid employeeId);

    Task<LeaveRequest> CreateAsync(LeaveRequest leaveRequest);

    Task<bool> UpdateStatusAsync(Guid id, string status);

    Task<bool> DeleteAsync(Guid id);
}