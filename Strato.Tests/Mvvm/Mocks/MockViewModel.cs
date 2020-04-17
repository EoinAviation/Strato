// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MockViewModel.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Tests.Mvvm.Mocks
{
    using System;
    using System.Threading.Tasks;
    using Strato.EventAggregator;
    using Strato.Mvvm.Attributes;
    using Strato.Mvvm.Commands;
    using Strato.Mvvm.ViewModels;

    /// <summary>
    ///     The mocked <see cref="ViewModel"/>.
    /// </summary>
    public class MockViewModel : ViewModel
    {
        /// <summary>
        ///     Gets or sets a <see cref="Guid"/>.
        /// </summary>
        public Guid Guid
        {
            get => Get<Guid>();
            set => Set(value);
        }

        /// <summary>
        ///     Gets or sets an <see cref="int"/>.
        /// </summary>
        public int Integer
        {
            get => Get<int>();
            set => Set(value);
        }

        /// <summary>
        ///     Gets or sets a <see cref="TestClass"/>.
        /// </summary>
        public TestClass Class
        {
            get => Get<TestClass>();
            set => Set(value);
        }

        /// <summary>
        ///     Gets or sets the invalid property.
        /// </summary>
        public string InvalidProperty
        {
            get => Get<string>(null);
            set => Set(value, string.Empty);
        }

        /// <summary>
        ///     Gets the <see cref="int"/> which is implicitly dependent on <see cref="Integer"/>.
        /// </summary>
        public int ImplicitlyDependentInteger => Integer * 2;

        /// <summary>
        ///     Gets or sets the <see cref="int"/> which is explicitly dependent on <see cref="Integer"/>.
        /// </summary>
        [Dependent]
        public int ExplicitlyDependentInteger
        {
            get => Integer * 5;

            // ReSharper disable once ValueParameterNotUsed
            set
            {
                // Do nothing
            }
        }

        /// <summary>
        ///     Gets the <see cref="RelayCommand"/> for incrementing the <see cref="Integer"/> property.
        /// </summary>
        public RelayCommand IncrementIntegerCommand => Get(new RelayCommand(IncrementInteger));

        /// <summary>
        ///     Gets the <see cref="RelayCommand"/> for incrementing the <see cref="Integer"/> property.
        /// </summary>
        public AsyncCommand IncrementIntegerAsyncCommand => Get(new AsyncCommand(IncrementIntegerAsync));

        /// <summary>
        ///     Increments the <see cref="Integer"/> property.
        /// </summary>
        public void IncrementInteger() => Integer++;

        /// <summary>
        ///     Gets the <see cref="Guid"/> of the <see cref="MockEvent"/> which was received.
        /// </summary>
        public Guid ReceivedMockEventId { get; private set; }

        /// <summary>
        ///     Gets the <see cref="Guid"/> of the <see cref="MockEvent"/> which was received asynchronously.
        /// </summary>
        public Guid ReceivedAsyncMockEventId { get; private set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MockViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">
        ///     The <see cref="EventAggregator"/> instance.
        /// </param>
        public MockViewModel(EventAggregator eventAggregator = null)
            : base(eventAggregator)
        {
            EventAggregator.Subscribe<MockEvent>(OnMockEvent);
            EventAggregator.Subscribe<MockEvent>(OnMockEventAsync);
        }

        /// <summary>
        ///     Increments the <see cref="Integer"/> property as an asynchronous operation.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public async Task IncrementIntegerAsync()
        {
            await Task.Yield();
            IncrementInteger();
        }

        /// <summary>
        ///     Handles the <see cref="MockEvent"/>.
        /// </summary>
        /// <param name="mockEvent">
        ///     The <see cref="MockEvent"/> to handle.
        /// </param>
        public void OnMockEvent(MockEvent mockEvent)
        {
            ReceivedMockEventId = mockEvent.Guid;
        }

        /// <summary>
        ///     Handles the <see cref="MockEvent"/> as an asynchronous operation.
        /// </summary>
        /// <param name="mockEvent">
        ///     The <see cref="MockEvent"/> to handle.
        /// </param>
        /// <returns>
        ///     The <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public async Task OnMockEventAsync(MockEvent mockEvent)
        {
            await Task.Yield();
            ReceivedAsyncMockEventId = mockEvent.Guid;
        }
    }
}
