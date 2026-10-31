using System;
using System.Collections.Generic;
using System.Text;

using LeaveManagementApi.Domain.Entities;

namespace LeaveManagementApi.Application.Interfaces.Services;

public interface IEmployeeService
{
    Task<Employee?> GetByIdAsync(Guid id);

    Task<IReadOnlyList<Employee>> GetAllAsync();

    Task<Employee> CreateAsync(Employee employee);

    Task<bool> UpdateAsync(Guid id, Employee employee);

    Task<bool> DeleteAsync(Guid id);
}