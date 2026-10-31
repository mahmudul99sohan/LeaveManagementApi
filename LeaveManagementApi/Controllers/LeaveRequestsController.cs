using LeaveManagementApi.Application.DTOs.LeaveRequests;
using LeaveManagementApi.Application.Interfaces.Services;
using LeaveManagementApi.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveRequestsController : ControllerBase
{
    private readonly ILeaveRequestService _leaveRequestService;

    public LeaveRequestsController(ILeaveRequestService leaveRequestService)
    {
        _leaveRequestService = leaveRequestService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var leaveRequests = await _leaveRequestService.GetAllAsync();

        return Ok(leaveRequests);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var leaveRequest = await _leaveRequestService.GetByIdAsync(id);

        if (leaveRequest is null)
        {
            return NotFound(new { message = "Leave request not found." });
        }

        return Ok(leaveRequest);
    }

    [HttpGet("employee/{employeeId:guid}")]
    public async Task<IActionResult> GetByEmployeeId(Guid employeeId)
    {
        var leaveRequests =
            await _leaveRequestService.GetByEmployeeIdAsync(employeeId);

        return Ok(leaveRequests);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateLeaveRequestDto request)
    {
        try
        {
            var leaveRequest = new LeaveRequest
            {
                EmployeeId = request.EmployeeId,
                LeaveType = request.LeaveType,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Reason = request.Reason
            };

            var createdLeaveRequest =
                await _leaveRequestService.CreateAsync(leaveRequest);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdLeaveRequest.Id },
                createdLeaveRequest);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(
        Guid id,
        UpdateLeaveRequestStatusDto request)
    {
        try
        {
            var updated = await _leaveRequestService.UpdateStatusAsync(
                id,
                request.Status);

            if (!updated)
            {
                return NotFound(new { message = "Leave request not found." });
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
        var deleted = await _leaveRequestService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new { message = "Leave request not found." });
        }

        return NoContent();
    }
}