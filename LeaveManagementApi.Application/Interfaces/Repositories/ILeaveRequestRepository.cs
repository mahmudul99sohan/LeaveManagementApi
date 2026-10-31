using System;
using System.Collections.Generic;
using System.Text;

using LeaveManagementApi.Domain.Entities;

namespace LeaveManagementApi.Application.Interfaces.Repositories;

public interface ILeaveRequestRepository
{
    Task<LeaveRequest?> GetByIdAsync(Guid id);


Task<IReadOnlyList<LeaveRequest>> GetAllAsync();

    Task<IReadOnlyList<LeaveRequest>> GetByEmployeeIdAsync(Guid employeeId);

    Task<LeaveRequest> AddAsync(LeaveRequest leaveRequest);

    Task UpdateAsync(LeaveRequest leaveRequest);

    Task DeleteAsync(Guid id);

    Task<bool> ExistsAsync(Guid id);

}
