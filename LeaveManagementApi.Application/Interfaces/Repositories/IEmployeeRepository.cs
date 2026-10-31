using System;
using System.Collections.Generic;
using System.Text;

using LeaveManagementApi.Domain.Entities;

namespace LeaveManagementApi.Application.Interfaces.Repositories;

public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(Guid id);

    Task<IReadOnlyList<Employee>> GetAllAsync();

    Task<Employee> AddAsync(Employee employee);

    Task UpdateAsync(Employee employee);

    Task DeleteAsync(Guid id);

    Task<bool> ExistsAsync(Guid id);
}