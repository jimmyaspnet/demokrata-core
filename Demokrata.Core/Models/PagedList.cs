// <copyright file="PagedList.cs" company="DonDoctor">
//     Copyright (c) 2022 All Rights Reserved
// </copyright>
// <author>Jimmy Rodriguez Avila</author>
namespace Demokrata.Core.Models;

using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// The model to get a paged list of entities
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="T" />
public class PagedList<T>
{
    /// <summary>
    /// Gets the asynchronous.
    /// </summary>
    /// <param name="list">The list.</param>
    /// <param name="pageIndex">Index of the page.</param>
    /// <param name="pageSize">Size of the page.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public static async Task<PagedList<T>> GetAsync(
        IQueryable<T> list,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var pagedList = new PagedList<T>
        {
            Meta = new PaginationInformationModel
            {
                Count = await list.CountAsync(cancellationToken),
                TotalCount = await list.CountAsync(cancellationToken)
            },
            PageIndex = pageIndex,
            PageSize = pageSize,            
        };

        if (pageSize > 0)
        {
            pagedList.TotalPages = pagedList.Meta.TotalCount / pagedList.PageSize;

            if (pagedList.Meta.TotalCount % pagedList.PageSize > 0)
            {
                pagedList.TotalPages++;
            }

            pagedList.Meta.HasNextPage = pagedList.TotalPages > pagedList.PageIndex + 1;
            pagedList.Results = await list.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        }
        else
        {
            pagedList.TotalPages = 1;
            pagedList.Results = await list.ToListAsync(cancellationToken);
        }

        return pagedList;
    }

    /// <summary>
    /// Gets or sets the meta.
    /// </summary>
    /// <value>
    /// The meta.
    /// </value>
    public PaginationInformationModel? Meta { get; set; }

    /// <summary>
    /// Gets or sets the results.
    /// </summary>
    /// <value>
    /// The results.
    /// </value>
    public IList<T>? Results { get; set; }

    /// <summary>
    /// Gets the index of the page.
    /// </summary>
    /// <value>
    /// The index of the page.
    /// </value>
    public int PageIndex
    {
        get; private set;
    }

    /// <summary>
    /// Gets the size of the page.
    /// </summary>
    /// <value>
    /// The size of the page.
    /// </value>
    public int PageSize
    {
        get; private set;
    }

    /// <summary>
    /// Gets the total pages.
    /// </summary>
    /// <value>
    /// The total pages.
    /// </value>
    public int TotalPages
    {
        get; private set;
    }
}