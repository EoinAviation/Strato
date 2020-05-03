// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManagedWindow.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Mvvm.Wpf.Windows
{
    using System;
    using System.Windows;

    using Strato.Mvvm.Navigation;

    /// <summary>
    ///     The <see cref="Window"/> which can be managed by the <see cref="Windows.WindowManager"/>.
    /// </summary>
    public abstract class ManagedWindow : Window
    {
        /// <summary>
        ///     Gets the <see cref="Guid"/> of the current <see cref="ManagedWindow"/>.
        /// </summary>
        public Guid WindowId { get; }

        /// <summary>
        ///     Gets the <see cref="INavigationContext"/>.
        /// </summary>
        protected INavigationContext NavigationContext { get; }

        /// <summary>
        ///     Gets the <see cref="Windows.WindowManager"/>.
        /// </summary>
        protected WindowManager WindowManager { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ManagedWindow"/> class.
        /// </summary>
        /// <param name="windowManager">
        ///     The <see cref="Windows.WindowManager"/>.
        /// </param>
        /// <param name="navigationContext">
        ///     The <see cref="INavigationContext"/>.
        /// </param>
        protected ManagedWindow(WindowManager windowManager, INavigationContext navigationContext)
        {
            WindowId = Guid.NewGuid();
            WindowManager = windowManager ?? throw new ArgumentNullException(nameof(windowManager));
            NavigationContext = navigationContext ?? throw new ArgumentNullException(nameof(navigationContext));
        }
    }
}
