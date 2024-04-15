// <copyright file="ServiceExtensions.cs" company="DonDoctor">
//     Copyright (c) 2022 All Rights Reserved
// </copyright>
// <author>Jimmy Rodriguez Avila</author>
namespace Microsoft.Extensions.DependencyInjection;

using System.Reflection;
using Demokrata.Core.Configuration;
using Demokrata.Core.Exceptions;
using Demokrata.Core.Interfaces;
using Demokrata.Core.Middlewares;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Compact;

/// <summary>
/// The service extensions
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// Adds the demokrata core.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static IServiceCollection AddDemokrataCore(this IServiceCollection services, Assembly assembly)
    {
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestAuthorizationBehavior<,>));
        services.AddScoped<IWorkContext, WorkContext>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        services.AddValidatorsFromAssembly(assembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        services.AddAutoMapper(assembly);
        services.AddAuthorizationHandlers(assembly);        

        services.AddScoped<MultitenantMiddleware>();

        return services;
    }

    /// <summary>
    /// Builds the demokrata core.
    /// </summary>
    /// <param name="hostBuilder">The host builder.</param>
    /// <returns></returns>
    public static IHostBuilder UseDemokrataCoreLog(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((context, configuration) =>
        {            
            configuration.ReadFrom.Configuration(context.Configuration);
            configuration.Filter.ByExcluding(a => a.Exception is Demokrata.Core.Exceptions.ValidationException or HttpException);
            configuration.WriteTo.File(
                new CompactJsonFormatter(),
                "./logs/logs",
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 5,
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error);
        });

        return hostBuilder;
    }

    /// <summary>
    /// Adds the authorization handlers.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="assembly">The assembly.</param>
    /// <returns></returns>
    public static IServiceCollection AddAuthorizationHandlers(this IServiceCollection services, Assembly assembly)
    {
        var authorizerType = typeof(IAuthorizer<>);
        GetTypesAssignableTo(assembly, authorizerType)?.ForEach((type) =>
        {
            foreach (var implementedInterface in type.ImplementedInterfaces)
            {
                if (!implementedInterface.IsGenericType || implementedInterface.GetGenericTypeDefinition() != authorizerType)
                {
                    continue;
                }

                services.AddScoped(implementedInterface, type);
            }
        });

        return services;
    }

    public static IApplicationBuilder UseDemokrataCore(this IApplicationBuilder app)
    {
        app.UseExceptionHandler();
        app.UseMiddleware<MultitenantMiddleware>();

        return app;
    }

    /// <summary>
    /// Gets the types assignable to.
    /// </summary>
    /// <param name="assembly">The assembly.</param>
    /// <param name="compareType">Type of the compare.</param>
    /// <returns></returns>
    private static List<TypeInfo>? GetTypesAssignableTo(Assembly assembly, Type compareType) => 
        assembly.DefinedTypes.Where(x => x.IsClass
        && !x.IsAbstract
        && x != compareType
        && x.GetInterfaces()
        .Any(i => i.IsGenericType
        && i.GetGenericTypeDefinition() == compareType))?
        .ToList();
}