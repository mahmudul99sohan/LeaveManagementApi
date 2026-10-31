using System;
using System.Collections.Generic;
using System.Text;
using LeaveManagementApi.Application.Interfaces.Repositories;
using LeaveManagementApi.Domain.Entities;
using LeaveManagementApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementApi.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

public EmployeeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Employee?> GetByIdAsync(Guid id)
    {
        return await _context.Employees.FindAsync(id);
    }

    public async Task<IReadOnlyList<Employee>> GetAllAsync()
    {
        return await _context.Employees
            .AsNoTracking()
            .OrderBy(e => e.FullName)
            .ToListAsync();
    }

    public async Task<Employee> AddAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();

        return employee;
    }

    public async Task UpdateAsync(Employee employee)
    {
        var existingEmployee = await _context.Employees
            .FindAsync(employee.Id);

        if (existingEmployee is null)
        {
            return;
        }

        existingEmployee.FullName = employee.FullName;
        existingEmployee.Email = employee.Email;
        existingEmployee.Department = employee.Department;
        existingEmployee.Designation = employee.Designation;
        existingEmployee.JoiningDate = employee.JoiningDate;
        existingEmployee.IsActive = employee.IsActive;

        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(Guid id)
    {
        var employee = await _context.Employees.FindAsync(id);

        if (employee is null)
        {
            return;
        }

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Employees
            .AnyAsync(e => e.Id == id);
    }


}
