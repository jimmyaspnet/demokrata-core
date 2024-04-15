// <copyright file="ValidationException.cs" company="DonDoctor">
//     Copyright (c) 2022 All Rights Reserved
// </copyright>
// <author>Jimmy Rodriguez Avila</author>
namespace Demokrata.Core.Exceptions;

using System;
using System.Collections.Generic;
using System.Linq;
using Demokrata.Core.Resources;
using FluentValidation.Results;

/// <summary>
/// The validation exception
/// </summary>
/// <seealso cref="Exception" />
public class ValidationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class.
    /// </summary>
    public ValidationException()
        : base(CoreMessages.ValidationFail) => this.Failures = new Dictionary<string, object?>();

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class.
    /// </summary>
    /// <param name="failures">The failures.</param>
    public ValidationException(List<ValidationFailure> failures)
        : base(CoreMessages.ValidationFail)
    {
        this.Failures = new Dictionary<string, object?>();

        var propertyNames = failures
            .Select(e => e.PropertyName)
            .Distinct();

        foreach (var propertyName in propertyNames)
        {
            var propertyFailures = failures
                .Where(e => e.PropertyName == propertyName)
                .Select(e => e.ErrorMessage)
                .ToArray();

            this.Failures.Add(propertyName, propertyFailures);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="propertyFailure">The property failure.</param>
    public ValidationException(string propertyName, string propertyFailure)
        : base(CoreMessages.ValidationFail) => this.Failures = new Dictionary<string, object?>
            {
                { propertyName, new string[] { propertyFailure } }
            };

    /// <summary>
    /// Gets the failures.
    /// </summary>
    /// <value>
    /// The failures.
    /// </value>
    public IDictionary<string, object?> Failures { get; }
}