// <copyright file="MultitenantMiddleware.cs" company="DonDoctor">
//     Copyright (c) 2022 All Rights Reserved
// </copyright>
// <author>Jimmy Rodriguez Avila</author>
namespace Demokrata.Core.Middlewares;

using System;
using System.Threading.Tasks;
using Demokrata.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

/// <summary>
/// The middleware to set the site id in the scope
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Http.IMiddleware" />
public class MultitenantMiddleware(IWorkContext workContext) : IMiddleware
{
    /// <summary>
    /// The work context
    /// </summary>
    private readonly IWorkContext workContext = workContext;

    /// <summary>
    /// Request handling method.
    /// </summary>
    /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Http.HttpContext" /> for the current request.</param>
    /// <param name="next">The delegate representing the remaining middleware in the request pipeline.</param>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        this.BuildUserInfo(context);

        if (context.Request.Headers.TryGetValue("SiteKey", out StringValues value))
        {
            //string key = context.Request.Headers["SiteKey"]; //decrypt

            this.workContext.SiteID = Convert.ToInt32(value);
        }
        else
        {
            this.workContext.SiteID = 1;
        }

        await next(context);
    }

    /// <summary>
    /// Builds the user information.
    /// </summary>
    /// <param name="context">The context.</param>
    private void BuildUserInfo(HttpContext context)
    {
        if (context.User.Identity is not null && context.User.Identity.IsAuthenticated)
        {
            this.workContext.UserID = Convert.ToInt32(context.User.Identity.Name);
        }
    }
}