// <copyright file="HttpException.cs" company="DonDoctor">
//     Copyright (c) 2022 All Rights Reserved
// </copyright>
// <author>Jimmy Rodriguez Avila</author>
namespace Demokrata.Core.Exceptions;

using System;
using System.Net;

/// <summary>
/// The http exception
/// </summary>
/// <seealso cref="Exception" />
public class HttpException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HttpException"/> class.
    /// </summary>
    /// <param name="httpStatus">The HTTP status.</param>
    public HttpException(HttpStatusCode httpStatus) => this.HttpStatus = httpStatus;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpException"/> class.
    /// </summary>
    /// <param name="httpStatus">The HTTP status.</param>
    /// <param name="message">The message.</param>
    public HttpException(HttpStatusCode httpStatus, string message)
        : base(message) => this.HttpStatus = httpStatus;

    /// <summary>
    /// Gets or sets the HTTP status.
    /// </summary>
    /// <value>
    /// The HTTP status.
    /// </value>
    public HttpStatusCode HttpStatus { get; set; }
}