// <copyright file="GlobalExceptionHandler.cs" company="DonDoctor">
//     Copyright (c) 2022 All Rights Reserved
// </copyright>
// <author>Jimmy Rodriguez Avila</author>
namespace Demokrata.Core.Configuration;

using System;
using System.Net;
using System.Threading.Tasks;
using Demokrata.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

/// <summary>
/// The global exceptions
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Diagnostics.IExceptionHandler" />
public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<GlobalExceptionHandler> logger = logger;

    /// <summary>
    /// Tries to handle the specified exception asynchronously within the ASP.NET Core pipeline.
    /// Implementations of this method can provide custom exception-handling logic for different scenarios.
    /// </summary>
    /// <param name="httpContext">The <see cref="T:Microsoft.AspNetCore.Http.HttpContext" /> for the request.</param>
    /// <param name="exception">The unhandled exception.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous read operation. The value of its <see cref="P:System.Threading.Tasks.ValueTask`1.Result" />
    /// property contains the result of the handling operation.
    /// <see langword="true" /> if the exception was handled successfully; otherwise <see langword="false" />.
    /// </returns>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Server error"
        };

        if (exception is ValidationException validationException)
        {
            problemDetails.Status = (int)HttpStatusCode.BadRequest;
            problemDetails.Extensions = validationException.Failures;
            problemDetails.Title = "Validation error";
        }
        else if (exception is HttpException ex)
        {
            problemDetails.Title = "Http response";
            problemDetails.Status = (int)ex.HttpStatus;
        }
        else
        {
            logger.LogError(
            exception, "Exception occurred: {Message}", exception.ToString());
        }

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}