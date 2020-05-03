// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindowManager.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Mvvm.Wpf.Windows
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Extensions.DependencyInjection;

    using Strato.EventAggregator.Abstractions;
    using Strato.Mvvm.Wpf.Events;

    /// <summary>
    ///     The class for managing <see cref="ManagedWindow"/>s.
    /// </summary>
    public class WindowManager
    {
        /// <summary>
        ///     The lock <see cref="object"/> for the <see cref="Windows"/>.
        /// </summary>
        private readonly object _windowsLock = new object();

        /// <summary>
        ///     Gets the <see cref="IReadOnlyCollection{T}"/> of <see cref="ManagedWindow"/>s.
        /// </summary>
        public IReadOnlyCollection<ManagedWindow> Windows { get; private set; }

        /// <summary>
        ///     Gets the <see cref="IServiceProvider"/>.
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        ///     Gets the <see cref="IEventAggregator"/>.
        /// </summary>
        public IEventAggregator EventAggregator { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="WindowManager"/> class.
        /// </summary>
        /// <param name="serviceProvider">
        ///     The <see cref="IServiceProvider"/> to use when instantiating new <see cref="ManagedWindow"/>s.
        /// </param>
        /// <param name="eventAggregator">
        ///     The <see cref="IEventAggregator"/> to subscribe to.
        /// </param>
        public WindowManager(IServiceProvider serviceProvider, IEventAggregator eventAggregator = null)
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(ServiceProvider));

            // Setup the Event Aggregator
            EventAggregator = eventAggregator;
            EventAggregator?.Subscribe<OpenWindowEvent>(OnOpenWindowRequested);

            Windows = new List<ManagedWindow>().AsReadOnly();
        }

        /// <summary>
        ///     Opens a new <typeparamref name="TWindow"/>.
        /// </summary>
        /// <typeparam name="TWindow">
        ///     The type of <see cref="ManagedWindow"/> to open.
        /// </typeparam>
        /// <param name="showAsDialog">
        ///     Whether or not to open the window as a dialog.
        /// </param>
        public void OpenWindow<TWindow>(bool showAsDialog = false)
            where TWindow : ManagedWindow =>
            OpenWindow(typeof(TWindow), showAsDialog);

        /// <summary>
        ///     Opens a new <see cref="ManagedWindow"/>.
        /// </summary>
        /// <param name="windowType">
        ///     The <see cref="Type"/> of <see cref="ManagedWindow"/> to open.
        /// </param>
        /// <param name="showAsDialog">
        ///     Whether or not to open the window as a dialog.
        /// </param>
        public void OpenWindow(Type windowType, bool showAsDialog = false)
        {
            // Ensure the type is correct
            if (windowType == null) throw new ArgumentNullException(nameof(windowType));
            if (!typeof(ManagedWindow).IsAssignableFrom(windowType))
            {
                throw new ArgumentException($"The type \"{windowType.Name}\" does not implement \"{typeof(ManagedWindow).Name}\".");
            }

            // Create the window
            ManagedWindow window = (ManagedWindow)ServiceProvider.GetRequiredService(windowType);

            // Add to a new list
            List<ManagedWindow> registrations =
                new List<ManagedWindow>(Windows) { window };

            // Re-initialize the registrations
            lock (_windowsLock)
            {
                Windows = registrations.AsReadOnly();
            }

            if (showAsDialog)
            {
                window.ShowDialog();
            }
            else
            {
                window.Show();
            }
        }

        /// <summary>
        ///     Handles the <see cref="OpenWindowEvent"/> raised from the <see cref="IEventAggregator"/>.
        /// </summary>
        /// <param name="event">
        ///     The <see cref="OpenWindowEvent"/> to handle.
        /// </param>
        public void OnOpenWindowRequested(OpenWindowEvent @event)
        {
            OpenWindow(@event.WindowType, @event.ShowAsDialog);
        }
    }
}
