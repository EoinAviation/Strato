// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewModelFactory.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Mvvm.Wpf.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Microsoft.Extensions.DependencyInjection;

    using Strato.Mvvm.Navigation;
    using Strato.Mvvm.ViewModels;

    /// <summary>
    ///     The <see cref="ViewModel"/> factory.
    /// </summary>
    internal static class ViewModelFactory
    {
        /// <summary>
        ///     Builds a new <typeparamref name="TViewModel"/> instance using the given <see cref="IServiceProvider"/>.
        /// </summary>
        /// <typeparam name="TViewModel">
        ///     The type of <see cref="ViewModel"/>.
        /// </typeparam>
        /// <param name="serviceProvider">
        ///     The <see cref="IServiceProvider"/> containing dependencies to be used by the
        ///     <typeparamref name="TViewModel"/>.
        /// </param>
        /// <param name="navigationContext">
        ///     The <see cref="INavigationContext"/> to use.
        /// </param>
        /// <returns>
        ///     The new <typeparamref name="TViewModel"/> instance.
        /// </returns>
        public static TViewModel BuildViewModel<TViewModel>(
            IServiceProvider serviceProvider,
            INavigationContext navigationContext = null)
            where TViewModel : ViewModel
        {
            // Try to build it straight away
            TViewModel viewModel = serviceProvider.GetService<TViewModel>();
            if (viewModel != null)
            {
                // Worked? Cool!
                return viewModel;
            }

            // Didn't work? Now we do some "fun" stuff...
            // Todo: Figure out how to handle multiple constructors
            Type viewModelType = typeof(TViewModel);
            if (viewModelType.GetConstructors().Length > 1)
            {
                throw new NotSupportedException($"{nameof(ViewModel)}s with more than 1 constructor are not supported.");
            }

            // Build a list of all the parameters
            ConstructorInfo viewModelConstructorMethod = viewModelType.GetConstructors().First();

            // No constructors, or just a parameterless constructor, then just create the instance
            if (viewModelType.GetConstructors().Length == 0 ||
                viewModelConstructorMethod == null ||
                viewModelConstructorMethod.GetParameters().Length == 0)
            {
                return (TViewModel)Activator.CreateInstance(viewModelType);
            }

            // Have parameters, gotta get em'
            List<object> viewModelConstructorParameters = new List<object>();
            foreach (ParameterInfo parameter in viewModelConstructorMethod.GetParameters())
            {
                // Use the navigation context if we can
                if (navigationContext != null &&
                    typeof(INavigationContext).IsAssignableFrom(parameter.ParameterType))
                {
                    viewModelConstructorParameters.Add(navigationContext);
                    continue;
                }

                // Create a new instance of the parameter
                object service = serviceProvider.GetRequiredService(parameter.ParameterType);
                viewModelConstructorParameters.Add(service);
            }

            // Create a new instance of the ViewModel using the parameters
            return (TViewModel)Activator.CreateInstance(viewModelType, viewModelConstructorParameters.ToArray());
        }
    }
}
