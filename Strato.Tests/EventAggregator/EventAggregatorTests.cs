// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventAggregatorTests.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Tests.EventAggregator
{
    using System;
    using System.Threading.Tasks;

    using NUnit.Framework;
    using Strato.EventAggregator;
    using Strato.EventAggregator.Abstractions;
    using Strato.Tests.Mvvm.Mocks;

    /// <summary>
    ///     The <see cref="Strato.Tests.EventAggregator"/> tests.
    /// </summary>
    [TestFixture]
    public class EventAggregatorTests
    {
        /// <summary>
        ///     Ensures <see cref="IEvent"/>s can be published over the <see cref="Strato.Tests.EventAggregator"/>, and subscribers
        ///     can handle the <see cref="IEvent"/>s.
        /// </summary>
        [Test]
        public void EventsCanBePublished()
        {
            // Arrange
            EventAggregator aggregator = new EventAggregator();
            MockViewModel viewModel = new MockViewModel(aggregator);
            MockEvent @event = new MockEvent();

            // Act
            aggregator.Publish(@event);

            // Assert
            Assert.IsNotNull(viewModel.ReceivedMockEventId);
            Assert.AreEqual(@event.Guid, viewModel.ReceivedMockEventId);
            Assert.AreEqual(default(Guid), viewModel.ReceivedAsyncMockEventId);
        }

        /// <summary>
        ///     Ensures <see cref="IEvent"/>s can be published asynchronously over the <see cref="Strato.Tests.EventAggregator"/>,
        ///     and subscribers can handle the <see cref="IEvent"/>s.
        /// </summary>
        [Test]
        public async Task EventsCanBePublishedAsync()
        {
            // Arrange
            EventAggregator aggregator = new EventAggregator();
            MockViewModel viewModel = new MockViewModel(aggregator);
            MockEvent @event = new MockEvent();

            // Act
            await aggregator.PublishAsync(@event);

            // Assert
            Assert.IsNotNull(viewModel.ReceivedAsyncMockEventId);
            Assert.AreEqual(@event.Guid, viewModel.ReceivedAsyncMockEventId);
            Assert.AreEqual(default(Guid), viewModel.ReceivedMockEventId);
        }
    }
}
