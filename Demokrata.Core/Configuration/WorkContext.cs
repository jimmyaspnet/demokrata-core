// <copyright file="WorkContext.cs" company="DonDoctor">
//     Copyright (c) 2022 All Rights Reserved
// </copyright>
// <author>Jimmy Rodriguez Avila</author>
namespace Demokrata.Core.Configuration;

using Demokrata.Core.Interfaces;

/// <summary>
/// The work context
/// </summary>
/// <seealso cref="Demokrata.Core.Interfaces.IWorkContext" />
public class WorkContext : IWorkContext
{
    /// <summary>
    /// Gets or sets the site identifier.
    /// </summary>
    /// <value>
    /// The site identifier.
    /// </value>
    public int SiteID { get; set; }

    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    /// <value>
    /// The user identifier.
    /// </value>
    public int UserID { get; set; }
}