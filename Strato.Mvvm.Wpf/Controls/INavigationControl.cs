// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INavigationControl.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Mvvm.Wpf.Controls
{
    using Strato.Mvvm.Navigation;

    /// <summary>
    ///     The interface representing a control capable of navigating between views.
    /// </summary>
    public interface INavigationControl
    {
        /// <summary>
        ///     Gets the <see cref="INavigationContext"/> to use for managing navigation.
        /// </summary>
        INavigationContext NavigationContext { get; }

        /// <summary>
        ///     Sets the <see cref="NavigationContext"/> to the given <paramref name="navigationContext"/>.
        /// </summary>
        /// <param name="navigationContext">
        ///     The <see cref="INavigationContext"/> to use.
        /// </param>
        void UseNavigationContext(INavigationContext navigationContext);

        /// <summary>
        ///     The method raised when the <see cref="NavigationContext"/> has requested for navigation to be
        ///     conducted.
        /// </summary>
        /// <param name="view">
        ///     The <see cref="IView"/> to navigate to.
        /// </param>
        void OnNavigationRequested(IView view);
    }
}
