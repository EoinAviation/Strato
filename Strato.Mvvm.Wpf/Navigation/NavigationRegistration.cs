// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NavigationRegistration.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Mvvm.Wpf.Navigation
{
    using System;

    using Strato.Mvvm.ViewModels;

    /// <summary>
    ///     The <see cref="IView"/> and <see cref="ViewModel"/> pair registration.
    /// </summary>
    public class NavigationRegistration
    {
        /// <summary>
        ///     Gets the <see cref="Type"/> of <see cref="IView"/>.
        /// </summary>
        public Type ViewType { get; private set; }

        /// <summary>
        ///     Gets the <see cref="Type"/> of <see cref="ViewModel"/>.
        /// </summary>
        public Type ViewModelType { get; private set; }

        /// <summary>
        ///     Creates a new instance of the <see cref="NavigationRegistration"/> class.
        /// </summary>
        /// <typeparam name="TView">
        ///     The type of <see cref="IView"/>.
        /// </typeparam>
        /// <typeparam name="TViewModel">
        ///     The type of <see cref="ViewModel"/>.
        /// </typeparam>
        /// <returns>
        ///     The new <see cref="NavigationRegistration"/> instance.
        /// </returns>
        public static NavigationRegistration Create<TView, TViewModel>()
            where TView : IView
            where TViewModel : ViewModel =>
            new NavigationRegistration { ViewType = typeof(TView), ViewModelType = typeof(TViewModel) };
    }
}
