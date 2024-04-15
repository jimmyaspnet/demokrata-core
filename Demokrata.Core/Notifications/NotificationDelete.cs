// <copyright file="NotificationDelete.cs" company="DonDoctor">
//     Copyright (c) 2022 All Rights Reserved
// </copyright>
// <author>Jimmy Rodriguez Avila</author>
namespace Demokrata.Core.Notifications;

using MediatR;

/// <summary>
/// The notification to publish a delete event
/// </summary>
/// <typeparam name="T">The type of entity</typeparam>
/// <seealso cref="MediatR.INotification" />
public class NotificationDelete<T>(T notification) : INotification
{
    /// <summary>
    /// Gets the notification.
    /// </summary>
    /// <value>
    /// The notification.
    /// </value>
    public T Notification { get; } = notification;
}