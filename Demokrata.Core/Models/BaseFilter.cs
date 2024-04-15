// <copyright file="BaseFilter.cs" company="DonDoctor">
//     Copyright (c) 2022 All Rights Reserved
// </copyright>
// <author>Jimmy Rodriguez Avila</author>
namespace Demokrata.Core.Models;

/// <summary>
/// The base filter for all paged queries
/// </summary>
public abstract class BaseFilter
{
    /// <summary>
    /// Gets or sets the order by.
    /// </summary>
    /// <value>
    /// The order by.
    /// </value>
    public string? OrderBy { get; set; }

    /// <summary>
    /// Gets or sets the page.
    /// </summary>
    /// <value>
    /// The page.
    /// </value>
    public int Page { get; set; } = 0;

    /// <summary>
    /// Gets or sets the size of the page.
    /// </summary>
    /// <value>
    /// The size of the page.
    /// </value>
    public int PageSize { get; set; } = 5;
}