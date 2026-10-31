using System;
using System.Collections.Generic;
using System.Text;

using LeaveManagementApi.Application.Interfaces.Repositories;
using LeaveManagementApi.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LeaveManagementApi.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services)
    {
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();

        return services;
    }
}