// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventHandlerAttribute.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.EventAggregator.Attributes
{
    using System;

    using Strato.EventAggregator.Abstractions;

    /// <summary>
    ///     The <see cref="Attribute"/> for automatically specifying a method as an <see cref="IEvent"/> handler for
    ///     the <see cref="IEventAggregator"/>.
    /// </summary>
    public class EventHandlerAttribute : Attribute
    {
    }
}
