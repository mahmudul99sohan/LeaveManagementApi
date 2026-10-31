using System;
using System.Collections.Generic;
using System.Text;

using LeaveManagementApi.Application.Interfaces.Services;
using LeaveManagementApi.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LeaveManagementApi.Application.DependencyInjection;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<IEmployeeService, EmployeeService>();

        services.AddScoped<ILeaveRequestService, LeaveRequestService>();

        return services;
    }
}