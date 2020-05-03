// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Mvvm.Wpf.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Strato.Mvvm.Navigation;
    using Strato.Mvvm.Wpf.Navigation;
    using Strato.Mvvm.Wpf.Windows;

    /// <summary>
    ///     The <see cref="IServiceCollection"/> extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Configures Window Management and Navigation by adding the <see cref="WindowManager"/> and
        ///     <see cref="INavigationContext"/> to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">
        ///     The <see cref="IServiceCollection"/>.
        /// </param>
        public static void AddWindowManagementAndNavigation(this IServiceCollection services)
        {
            services.AddSingleton<WindowManager>();
            services.AddTransient<INavigationContext, NavigationContext>();
        }
    }
}
