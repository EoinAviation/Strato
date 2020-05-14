// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindowManager.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Mvvm.Wpf.Windows
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    using Microsoft.Extensions.DependencyInjection;

    using Strato.EventAggregator.Abstractions;
    using Strato.Mvvm.Wpf.Events;

    /// <summary>
    ///     The class for managing <see cref="Window"/>s.
    /// </summary>
    public class WindowManager
    {
        /// <summary>
        ///     The lock <see cref="object"/> for the <see cref="Windows"/>.
        /// </summary>
        private readonly object _windowsLock = new object();

        /// <summary>
        ///     Gets the <see cref="IReadOnlyCollection{T}"/> of <see cref="Window"/>s.
        /// </summary>
        public IReadOnlyCollection<Window> Windows { get; private set; }

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
        ///     The <see cref="IServiceProvider"/> to use when instantiating new <see cref="Window"/>s.
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

            Windows = new List<Window>().AsReadOnly();
        }

        /// <summary>
        ///     Opens a new <typeparamref name="TWindow"/>.
        /// </summary>
        /// <typeparam name="TWindow">
        ///     The type of <see cref="Window"/> to open.
        /// </typeparam>
        /// <param name="showAsDialog">
        ///     Whether or not to open the window as a dialog.
        /// </param>
        public void OpenWindow<TWindow>(bool showAsDialog = false)
            where TWindow : Window =>
            OpenWindow(typeof(TWindow), showAsDialog);

        /// <summary>
        ///     Opens a new <see cref="Window"/>.
        /// </summary>
        /// <param name="windowType">
        ///     The <see cref="Type"/> of <see cref="Window"/> to open.
        /// </param>
        /// <param name="showAsDialog">
        ///     Whether or not to open the window as a dialog.
        /// </param>
        public void OpenWindow(Type windowType, bool showAsDialog = false)
        {
            // Ensure the type is correct
            if (windowType == null) throw new ArgumentNullException(nameof(windowType));
            if (!typeof(Window).IsAssignableFrom(windowType))
            {
                throw new ArgumentException($"The type \"{windowType.Name}\" does not implement \"{typeof(Window).Name}\".");
            }

            // Create the window
            Window window = (Window)ServiceProvider.GetRequiredService(windowType);

            // Add to a new list
            List<Window> registrations =
                new List<Window>(Windows) { window };

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
