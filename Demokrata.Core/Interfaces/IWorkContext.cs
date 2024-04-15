// <copyright file="IWorkContext.cs" company="DonDoctor">
//     Copyright (c) 2022 All Rights Reserved
// </copyright>
// <author>Jimmy Rodriguez Avila</author>
namespace Demokrata.Core.Interfaces;

/// <summary>
/// The interface for the work context
/// </summary>
public interface IWorkContext
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