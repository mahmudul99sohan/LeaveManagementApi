using System;
using System.Collections.Generic;
using System.Text;

using LeaveManagementApi.Application.Interfaces.Repositories;
using LeaveManagementApi.Application.Interfaces.Services;
using LeaveManagementApi.Domain.Entities;

namespace LeaveManagementApi.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Employee?> GetByIdAsync(Guid id)
    {
        return await _employeeRepository.GetByIdAsync(id);
    }

    public async Task<IReadOnlyList<Employee>> GetAllAsync()
    {
        return await _employeeRepository.GetAllAsync();
    }

    public async Task<Employee> CreateAsync(Employee employee)
    {
        var existingEmployee = await _employeeRepository.GetAllAsync();

        var emailExists = existingEmployee.Any(e =>
            e.Email.Equals(employee.Email, StringComparison.OrdinalIgnoreCase));

        if (emailExists)
        {
            throw new InvalidOperationException(
                "An employee with this email already exists.");
        }

        employee.Id = Guid.NewGuid();

        return await _employeeRepository.AddAsync(employee);
    }

    public async Task<bool> UpdateAsync(Guid id, Employee employee)
    {
        var existingEmployee = await _employeeRepository.GetByIdAsync(id);

        if (existingEmployee is null)
        {
            return false;
        }

        var allEmployees = await _employeeRepository.GetAllAsync();

        var emailExists = allEmployees.Any(e =>
            e.Id != id &&
            e.Email.Equals(employee.Email, StringComparison.OrdinalIgnoreCase));

        if (emailExists)
        {
            throw new InvalidOperationException(
                "This email is already used by another employee.");
        }

        employee.Id = id;

        await _employeeRepository.UpdateAsync(employee);

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var exists = await _employeeRepository.ExistsAsync(id);

        if (!exists)
        {
            return false;
        }

        await _employeeRepository.DeleteAsync(id);

        return true;
    }
}