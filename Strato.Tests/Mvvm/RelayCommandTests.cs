// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RelayCommandTests.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Tests.Mvvm
{
    using NUnit.Framework;

    using Strato.Mvvm.Commands;

    /// <summary>
    ///     The <see cref="RelayCommand"/> tests.
    /// </summary>
    [TestFixture]
    public class RelayCommandTests
    {
        /// <summary>
        ///     Ensures that <see cref="RelayCommand"/>s can be executed.
        /// </summary>
        [Test]
        public void RelayCommandExecutes()
        {
            // Arrange
            bool didExecute = false;
            RelayCommand command = new RelayCommand(() => didExecute = true);

            // Act
            command.Execute(null);

            // Assert
            Assert.IsTrue(didExecute);
        }

        /// <summary>
        ///     Ensures that <see cref="RelayCommand"/>s are not executed if the <see cref="RelayCommand.CanExecute()"/>
        ///     method returns <c>false</c>.
        /// </summary>
        [Test]
        public void RelayCommandDoesNotExecuteWhenUnable()
        {
            // Arrange
            bool didExecute = false;
            RelayCommand command = new RelayCommand(() => didExecute = true, () => false);

            // Act
            command.Execute(null);

            // Assert
            Assert.IsFalse(didExecute);
        }
    }
}
