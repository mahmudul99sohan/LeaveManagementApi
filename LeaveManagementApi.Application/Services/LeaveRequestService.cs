using System;
using System.Collections.Generic;
using System.Text;

using LeaveManagementApi.Application.Interfaces.Repositories;
using LeaveManagementApi.Application.Interfaces.Services;
using LeaveManagementApi.Domain.Entities;

namespace LeaveManagementApi.Application.Services;

public class LeaveRequestService : ILeaveRequestService
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public LeaveRequestService(
        ILeaveRequestRepository leaveRequestRepository,
        IEmployeeRepository employeeRepository)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<LeaveRequest?> GetByIdAsync(Guid id)
    {
        return await _leaveRequestRepository.GetByIdAsync(id);
    }

    public async Task<IReadOnlyList<LeaveRequest>> GetAllAsync()
    {
        return await _leaveRequestRepository.GetAllAsync();
    }

    public async Task<IReadOnlyList<LeaveRequest>> GetByEmployeeIdAsync(
        Guid employeeId)
    {
        return await _leaveRequestRepository.GetByEmployeeIdAsync(employeeId);
    }

    public async Task<LeaveRequest> CreateAsync(LeaveRequest leaveRequest)
    {
        var employeeExists = await _employeeRepository.ExistsAsync(
            leaveRequest.EmployeeId);

        if (!employeeExists)
        {
            throw new InvalidOperationException(
                "Employee does not exist.");
        }

        if (leaveRequest.StartDate.Date > leaveRequest.EndDate.Date)
        {
            throw new InvalidOperationException(
                "Start date cannot be after end date.");
        }

        leaveRequest.Id = Guid.NewGuid();
        leaveRequest.Status = "Pending";
        leaveRequest.AppliedAt = DateTime.UtcNow;

        return await _leaveRequestRepository.AddAsync(leaveRequest);
    }

    public async Task<bool> UpdateStatusAsync(Guid id, string status)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(id);

        if (leaveRequest is null)
        {
            return false;
        }

        var validStatuses = new[] { "Pending", "Approved", "Rejected" };

        if (!validStatuses.Contains(status, StringComparer.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                "Invalid leave request status.");
        }

        leaveRequest.Status = status;

        await _leaveRequestRepository.UpdateAsync(leaveRequest);

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var exists = await _leaveRequestRepository.ExistsAsync(id);

        if (!exists)
        {
            return false;
        }

        await _leaveRequestRepository.DeleteAsync(id);

        return true;
    }
}