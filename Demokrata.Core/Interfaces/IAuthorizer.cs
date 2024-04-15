// <copyright file="IAuthorizer.cs" company="Jimmy">
//     Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>Jimmy Rodriguez Avila</author>
namespace Demokrata.Core.Interfaces;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// The interface to implements custom authorization pipes fot handlers
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IAuthorizer<in T>
{
    /// <summary>
    /// Builds the policy.
    /// </summary>
    /// <param name="instance">The instance.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task BuildPolicy(T instance, CancellationToken cancellationToken);
}
