// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INavigationContext.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Mvvm.Navigation
{
    using System;

    using Strato.Mvvm.ViewModels;

    /// <summary>
    ///     The interface representing a class capable of managing navigation between different
    ///     <see cref="ViewModel"/>s.
    /// </summary>
    public interface INavigationContext
    {
        /// <summary>
        ///     Gets or sets the <see cref="Action{T}"/> to execute when navigation has been requested.
        /// </summary>
        Action<IView> OnNavigationRequestedAction { get; set; }

        /// <summary>
        ///     Gets the currently visible <see cref="ViewModel"/>.
        /// </summary>
        ViewModel CurrentViewModel { get; }

        /// <summary>
        ///     Registers a new <see cref="IView"/> and <see cref="ViewModel"/>.
        /// </summary>
        /// <typeparam name="TView">
        ///     The type of <see cref="IView"/> to register.
        /// </typeparam>
        /// <typeparam name="TViewModel">
        ///     The type of <see cref="ViewModel"/> to register.
        /// </typeparam>
        void Register<TView, TViewModel>()
            where TView : IView
            where TViewModel : ViewModel;

        /// <summary>
        ///     Navigates to the requested <typeparamref name="TViewModel"/>.
        /// </summary>
        /// <typeparam name="TViewModel">
        ///     The type of <see cref="ViewModel"/> to navigate to.
        /// </typeparam>
        void NavigateTo<TViewModel>()
            where TViewModel : ViewModel;
    }
}
