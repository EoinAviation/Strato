// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecondDemoView.xaml.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Mvvm.Demo.Views
{
    using System;
    using System.Windows.Controls;

    using Strato.Mvvm.Demo.ViewModels;

    /// <summary>
    ///     Interaction logic for SecondDemoView.xaml.
    /// </summary>
    public partial class SecondDemoView : UserControl, IView
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SecondDemoView"/> class.
        /// </summary>
        /// <param name="viewModel">
        ///     The <see cref="SecondDemoViewModel"/>.
        /// </param>
        public SecondDemoView(SecondDemoViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }
    }
}
