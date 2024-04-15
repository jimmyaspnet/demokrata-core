// <copyright file="RequestAuthorizationBehavior.cs" company="DonDoctor">
//     Copyright (c) 2022 All Rights Reserved
// </copyright>
// <author>Jimmy Rodriguez Avila</author>
namespace Demokrata.Core.Configuration;

using System.Collections.Generic;
using System.Threading.Tasks;
using Demokrata.Core.Interfaces;
using MediatR;

/// <summary>
/// The authorization for request
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
/// <seealso cref="MediatR.IPipelineBehavior&lt;TRequest, TResponse&gt;" />
/// <remarks>
/// Initializes a new instance of the <see cref="RequestAuthorizationBehavior{TRequest, TResponse}"/> class.
/// </remarks>
/// <param name="authorizers">The authorizers.</param>
public class RequestAuthorizationBehavior<TRequest, TResponse>(IEnumerable<IAuthorizer<TRequest>> authorizers) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// The authorizers
    /// </summary>
    private readonly IEnumerable<IAuthorizer<TRequest>> _authorizers = authorizers;

    /// <summary>
    /// Pipeline handler. Perform any additional behavior and await the <paramref name="next" /> delegate as necessary
    /// </summary>
    /// <param name="request">Incoming request</param>
    /// <param name="next">Awaitable delegate for the next action in the pipeline. Eventually this delegate represents the handler.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>
    /// Awaitable task returning the <typeparamref name="TResponse" />
    /// </returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        foreach (var authorizer in this._authorizers)
        {
            await authorizer.BuildPolicy(request, cancellationToken);
        }

        return await next();
    }
}