// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecondDemoView.xaml.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Mvvm.Demo.Views
{
    using Strato.Mvvm.Demo.ViewModels;
    using Strato.Mvvm.Wpf.Controls;

    /// <summary>
    ///     Interaction logic for SecondDemoView.xaml.
    /// </summary>
    public partial class SecondDemoView : View
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SecondDemoView"/> class.
        /// </summary>
        /// <param name="viewModel">
        ///     The <see cref="SecondDemoViewModel"/>.
        /// </param>
        public SecondDemoView(SecondDemoViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
