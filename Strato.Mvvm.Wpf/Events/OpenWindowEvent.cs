// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OpenWindowEvent.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Mvvm.Wpf.Events
{
    using System;

    using Strato.EventAggregator.Abstractions;
    using Strato.Mvvm.Wpf.Windows;

    /// <summary>
    ///     The <see cref="IEvent"/> to raise when requesting for a new <see cref="ManagedWindow"/> to be opened.
    /// </summary>
    public class OpenWindowEvent : IEvent
    {
        /// <summary>
        ///     Gets the <see cref="Type"/> of <see cref="ManagedWindow"/> to open.
        /// </summary>
        public Type WindowType { get; }

        /// <summary>
        ///     Gets a value indicating whether or not to open the <see cref="ManagedWindow"/> as a dialog.
        /// </summary>
        public bool ShowAsDialog { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenWindowEvent"/> class.
        /// </summary>
        /// <param name="windowType">
        ///     The <see cref="Type"/> of <see cref="ManagedWindow"/> to open.
        /// </param>
        /// <param name="showAsDialog">
        ///     Whether or not to open the <see cref="ManagedWindow"/> as a dialog.
        /// </param>
        public OpenWindowEvent(Type windowType, bool showAsDialog = false)
        {
            if (windowType == null) throw new ArgumentNullException(nameof(windowType));
            if (!typeof(ManagedWindow).IsAssignableFrom(windowType))
            {
                throw new ArgumentException($"The type \"{windowType.Name}\" does not implement \"{typeof(ManagedWindow).Name}\".");
            }

            WindowType = windowType;
            ShowAsDialog = showAsDialog;
        }

        /// <summary>
        ///     Creates a new <see cref="OpenWindowEvent"/> instance.
        /// </summary>
        /// <typeparam name="TWindow">
        ///     The type of <see cref="ManagedWindow"/> to open.
        /// </typeparam>
        /// <param name="showAsDialog">
        ///     Whether or not to open the <see cref="ManagedWindow"/> as a dialog.
        /// </param>
        /// <returns>
        ///     The new <see cref="OpenWindowEvent"/> instance.
        /// </returns>
        public static OpenWindowEvent Create<TWindow>(bool showAsDialog = false)
            where TWindow : ManagedWindow =>
            new OpenWindowEvent(typeof(TWindow), showAsDialog);
    }
}
