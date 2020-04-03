// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestClass.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Tests.Mvvm.Mocks
{
    using System;

    /// <summary>
    ///     A class used to test the <see cref="MockViewModel"/>.
    /// </summary>
    public class TestClass
    {
        /// <summary>
        ///     Gets the <see cref="System.Guid"/> of the current <see cref="TestClass"/>.
        /// </summary>
        public Guid Guid { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TestClass"/> class.
        /// </summary>
        public TestClass() => Guid = Guid.NewGuid();
    }
}
