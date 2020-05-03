// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewFactory.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Mvvm.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Microsoft.Extensions.DependencyInjection;
    using Strato.Mvvm.Navigation;
    using Strato.Mvvm.ViewModels;

    /// <summary>
    ///     The <see cref="IView"/> factory.
    /// </summary>
    public static class ViewFactory
    {
        /// <summary>
        ///     Builds a new <typeparamref name="TView"/> instance.
        /// </summary>
        /// <typeparam name="TView">
        ///     The type of <see cref="IView"/>.
        /// </typeparam>
        /// <param name="serviceProvider">
        ///     The <see cref="IServiceProvider"/> containing dependencies to be used by the
        ///     <typeparamref name="TView"/>.
        /// </param>
        /// <param name="viewModel">
        ///     The <see cref="ViewModel"/> to use.
        /// </param>
        /// <param name="navigationContext">
        ///     The <see cref="INavigationContext"/> to use.
        /// </param>
        /// <returns>
        ///     The new <typeparamref name="TView"/> instance.
        /// </returns>
        public static TView BuildView<TView>(
            IServiceProvider serviceProvider,
            ViewModel viewModel,
            INavigationContext navigationContext = null)
            where TView : IView =>
            (TView)BuildView(typeof(TView), serviceProvider, viewModel, navigationContext);

        /// <summary>
        ///     Builds a new <see cref="IView"/> instance.
        /// </summary>
        /// <param name="viewType">
        ///     The <see cref="Type"/> of <see cref="IView"/>.
        /// </param>
        /// <param name="serviceProvider">
        ///     The <see cref="IServiceProvider"/> containing dependencies to be used by the <see cref="IView"/>.
        /// </param>
        /// <param name="viewModel">
        ///     The <see cref="ViewModel"/> to use.
        /// </param>
        /// <param name="navigationContext">
        ///     The <see cref="INavigationContext"/> to use.
        /// </param>
        /// <returns>
        ///     The new <see cref="IView"/> instance.
        /// </returns>
        public static object BuildView(
            Type viewType,
            IServiceProvider serviceProvider,
            ViewModel viewModel,
            INavigationContext navigationContext = null)
        {
            // Try to build it straight away
            object view = serviceProvider.GetService(viewType);
            if (view != null)
            {
                // Worked? Cool!
                return view;
            }

            // Didn't work? Now we do some "fun" stuff...
            // Todo: Figure out how to handle multiple constructors
            if (viewType.GetConstructors().Length > 1)
            {
                throw new NotSupportedException($"{nameof(IView)}s with more than 1 constructor are not supported.");
            }

            // Build a list of all the parameters
            ConstructorInfo viewConstructorMethod = viewType.GetConstructors().First();

            // No constructors, or just a parameterless constructor, then just create the instance
            if (viewType.GetConstructors().Length == 0 ||
                viewConstructorMethod == null ||
                viewConstructorMethod.GetParameters().Length == 0)
            {
                return Activator.CreateInstance(viewType);
            }

            // Have parameters, gotta get em'
            List<object> viewConstructorParameters = new List<object>();
            foreach (ParameterInfo parameter in viewConstructorMethod.GetParameters())
            {
                // If the type is a Navigation Context, use the one provided
                if (typeof(INavigationContext).IsAssignableFrom(parameter.ParameterType))
                {
                    viewConstructorParameters.Add(navigationContext);
                    continue;
                }

                // If the type is a ViewModel, ensure it's the correct type, and use the one provided
                if (typeof(ViewModel).IsAssignableFrom(parameter.ParameterType))
                {
                    if (parameter.ParameterType != viewModel.GetType())
                    {
                        // Todo: Custom Domain exception
                        throw new Exception($"The View \"{viewType.Name}\" expects a {nameof(ViewModel)} of type \"{parameter.ParameterType.Name}\", but is registered with a \"{viewModel.GetType().Name}\".");
                    }

                    viewConstructorParameters.Add(viewModel);
                    continue;
                }

                // Create a new instance of the parameter
                object service = serviceProvider.GetRequiredService(parameter.ParameterType);
                viewConstructorParameters.Add(service);
            }

            // Create a new instance of the View using the parameters
            return Activator.CreateInstance(viewType, viewConstructorParameters.ToArray());
        }
    }
}
