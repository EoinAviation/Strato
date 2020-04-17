// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MockEvent.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Tests.Mvvm.Mocks
{
    using System;

    using Strato.EventAggregator.Abstractions;

    /// <summary>
    ///     The mocked <see cref="IEvent"/>.
    /// </summary>
    public class MockEvent : IEvent
    {
        /// <summary>
        ///     Gets the <see cref="Guid"/> of the current <see cref="MockEvent"/>.
        /// </summary>
        public Guid Guid { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MockEvent"/> class.
        /// </summary>
        public MockEvent() => Guid = Guid.NewGuid();
    }
}
