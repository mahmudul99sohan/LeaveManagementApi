using Microsoft.AspNetCore.Http;

using LeaveManagementApi.Application.Interfaces.Services;
using LeaveManagementApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using LeaveManagementApi.Application.DTOs.Employees;

namespace LeaveManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _employeeService.GetAllAsync();

        return Ok(employees);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var employee = await _employeeService.GetByIdAsync(id);

        if (employee is null)
        {
            return NotFound(new { message = "Employee not found." });
        }

        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeDto request)
    {
        try
        {
            var employee = new Employee
            {
                FullName = request.FullName,
                Email = request.Email,
                Department = request.Department,
                Designation = request.Designation,
                JoiningDate = request.JoiningDate
            };

            var createdEmployee = await _employeeService.CreateAsync(employee);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdEmployee.Id },
                createdEmployee);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateEmployeeDto request)
    {
        try
        {
            var employee = new Employee
            {
                FullName = request.FullName,
                Email = request.Email,
                Department = request.Department,
                Designation = request.Designation,
                JoiningDate = request.JoiningDate,
                IsActive = request.IsActive
            };

            var updated = await _employeeService.UpdateAsync(id, employee);

            if (!updated)
            {
                return NotFound(new { message = "Employee not found." });
            }

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var deleted = await _employeeService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new { message = "Employee not found." });
            }

            return NoContent();
        }
        catch (Exception)
        {
            return Conflict(new
            {
                message = "Cannot delete employee because related records may exist."
            });
        }
    }
}