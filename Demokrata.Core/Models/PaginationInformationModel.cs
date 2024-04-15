// <copyright file="PaginationInformationModel.cs" company="DonDoctor">
//     Copyright (c) 2022 All Rights Reserved
// </copyright>
// <author>Jimmy Rodriguez Avila</author>
namespace Demokrata.Core.Models;

/// <summary>
/// The model to get a meta data from paged list
/// </summary>
public class PaginationInformationModel
{
    /// <summary>
    /// Gets or sets the count.
    /// </summary>
    /// <value>
    /// The count.
    /// </value>
    public int Count { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance has next page.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance has next page; otherwise, <c>false</c>.
    /// </value>
    public bool HasNextPage { get; set; }

    /// <summary>
    /// Gets or sets the modified date.
    /// </summary>
    /// <value>
    /// The modified date.
    /// </value>
    public string? ModifiedDate { get; set; }

    /// <summary>
    /// Gets or sets the total count.
    /// </summary>
    /// <value>
    /// The total count.
    /// </value>
    public int TotalCount { get; set; }
}