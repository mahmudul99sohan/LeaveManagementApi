using System;
using System.Collections.Generic;
using System.Text;

using LeaveManagementApi.Application.Interfaces.Repositories;
using LeaveManagementApi.Domain.Entities;
using LeaveManagementApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementApi.Infrastructure.Repositories;

public class LeaveRequestRepository : ILeaveRequestRepository
{
    private readonly ApplicationDbContext _context;

    public LeaveRequestRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<LeaveRequest?> GetByIdAsync(Guid id)
    {
        return await _context.LeaveRequests
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<IReadOnlyList<LeaveRequest>> GetAllAsync()
    {
        return await _context.LeaveRequests
            .AsNoTracking()
            .OrderByDescending(l => l.AppliedAt)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<LeaveRequest>> GetByEmployeeIdAsync(
        Guid employeeId)
    {
        return await _context.LeaveRequests
            .AsNoTracking()
            .Where(l => l.EmployeeId == employeeId)
            .OrderByDescending(l => l.AppliedAt)
            .ToListAsync();
    }

    public async Task<LeaveRequest> AddAsync(LeaveRequest leaveRequest)
    {
        await _context.LeaveRequests.AddAsync(leaveRequest);
        await _context.SaveChangesAsync();

        return leaveRequest;
    }

    public async Task UpdateAsync(LeaveRequest leaveRequest)
    {
        _context.LeaveRequests.Update(leaveRequest);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var leaveRequest = await _context.LeaveRequests.FindAsync(id);

        if (leaveRequest is null)
        {
            return;
        }

        _context.LeaveRequests.Remove(leaveRequest);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.LeaveRequests
            .AnyAsync(l => l.Id == id);
    }
}