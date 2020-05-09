// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NavigationContext.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Mvvm.Wpf.Navigation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Strato.Mvvm.Navigation;
    using Strato.Mvvm.ViewModels;
    using Strato.Mvvm.Wpf.Factories;

    /// <summary>
    ///     The <see cref="INavigationContext"/> implementation.
    /// </summary>
    public class NavigationContext : INavigationContext
    {
        /// <summary>
        ///     The lock <see cref="object"/> for the <see cref="NavigationRegistrations"/>.
        /// </summary>
        private readonly object _navigationRegistrationsLock = new object();

        /// <summary>
        ///     The <see cref="IServiceProvider"/> to use when creating new <see cref="IView"/> and
        ///     <see cref="ViewModel"/> instances.
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        ///     Gets the <see cref="IReadOnlyCollection{T}"/> of <see cref="NavigationRegistration"/>s.
        /// </summary>
        public IReadOnlyCollection<NavigationRegistration> NavigationRegistrations { get; private set; }

        /// <summary>
        ///     Gets or sets the <see cref="Action{T}"/> to execute when navigation has been requested.
        /// </summary>
        public Action<IView> OnNavigationRequestedAction { get; set; }

        /// <summary>
        ///     Gets the currently visible <see cref="ViewModel"/>.
        /// </summary>
        public ViewModel CurrentViewModel { get; private set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NavigationContext"/> class.
        /// </summary>
        /// <param name="serviceProvider">
        ///     The <see cref="IServiceProvider"/> to use when creating new <see cref="IView"/> and
        ///     <see cref="ViewModel"/> instances.
        /// </param>
        public NavigationContext(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            NavigationRegistrations = new List<NavigationRegistration>().AsReadOnly();
        }

        /// <summary>
        ///     Registers a new <see cref="IView"/> and <see cref="ViewModel"/>.
        /// </summary>
        /// <typeparam name="TView">
        ///     The type of <see cref="IView"/> to register.
        /// </typeparam>
        /// <typeparam name="TViewModel">
        ///     The type of <see cref="ViewModel"/> to register.
        /// </typeparam>
        public void Register<TView, TViewModel>()
            where TView : IView
            where TViewModel : ViewModel
        {
            // Create the registration
            NavigationRegistration registration = NavigationRegistration.Create<TView, TViewModel>();

            // Add to a new list
            List<NavigationRegistration> registrations =
                new List<NavigationRegistration>(NavigationRegistrations) { registration };

            // Re-initialize the registrations
            lock (_navigationRegistrationsLock)
            {
                NavigationRegistrations = registrations.AsReadOnly();
            }
        }

        /// <summary>
        ///     Navigates to the requested <typeparamref name="TViewModel"/>.
        /// </summary>
        /// <typeparam name="TViewModel">
        ///     The type of <see cref="ViewModel"/> to navigate to.
        /// </typeparam>
        public void NavigateTo<TViewModel>()
            where TViewModel : ViewModel
        {
            // Find the registration
            NavigationRegistration registration =
                NavigationRegistrations.FirstOrDefault(r => r.ViewModelType == typeof(TViewModel));
            if (registration == default)
            {
                // Todo: Custom domain exception
                throw new Exception($"The {typeof(TViewModel).Name} has not been registered to any view.");
            }

            // Build the View Model
            TViewModel viewModel = ViewModelFactory.BuildViewModel<TViewModel>(_serviceProvider, this);

            // Build the View
            IView targetView = (IView)ViewFactory.BuildView(registration.ViewType, _serviceProvider, viewModel, this);

            // Invoke the Navigation Requested Action
            Action<IView> action = OnNavigationRequestedAction;
            if (action != null)
            {
                OnNavigationRequestedAction?.Invoke(targetView);
                CurrentViewModel = viewModel;
            }
        }
    }
}
