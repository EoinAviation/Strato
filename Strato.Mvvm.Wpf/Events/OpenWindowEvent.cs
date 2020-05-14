// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OpenWindowEvent.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Mvvm.Wpf.Events
{
    using System;
    using System.Windows;

    using Strato.EventAggregator.Abstractions;

    /// <summary>
    ///     The <see cref="IEvent"/> to raise when requesting for a new <see cref="Window"/> to be opened.
    /// </summary>
    public class OpenWindowEvent : IEvent
    {
        /// <summary>
        ///     Gets the <see cref="Type"/> of <see cref="Window"/> to open.
        /// </summary>
        public Type WindowType { get; }

        /// <summary>
        ///     Gets a value indicating whether or not to open the <see cref="Window"/> as a dialog.
        /// </summary>
        public bool ShowAsDialog { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenWindowEvent"/> class.
        /// </summary>
        /// <param name="windowType">
        ///     The <see cref="Type"/> of <see cref="Window"/> to open.
        /// </param>
        /// <param name="showAsDialog">
        ///     Whether or not to open the <see cref="Window"/> as a dialog.
        /// </param>
        public OpenWindowEvent(Type windowType, bool showAsDialog = false)
        {
            if (windowType == null) throw new ArgumentNullException(nameof(windowType));
            if (!typeof(Window).IsAssignableFrom(windowType))
            {
                throw new ArgumentException($"The type \"{windowType.Name}\" does not implement \"{typeof(Window).Name}\".");
            }

            WindowType = windowType;
            ShowAsDialog = showAsDialog;
        }

        /// <summary>
        ///     Creates a new <see cref="OpenWindowEvent"/> instance.
        /// </summary>
        /// <typeparam name="TWindow">
        ///     The type of <see cref="Window"/> to open.
        /// </typeparam>
        /// <param name="showAsDialog">
        ///     Whether or not to open the <see cref="Window"/> as a dialog.
        /// </param>
        /// <returns>
        ///     The new <see cref="OpenWindowEvent"/> instance.
        /// </returns>
        public static OpenWindowEvent Create<TWindow>(bool showAsDialog = false)
            where TWindow : Window =>
            new OpenWindowEvent(typeof(TWindow), showAsDialog);
    }
}
