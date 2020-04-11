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
            RelayCommand command = new RelayCommand(o => didExecute = true);

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
            RelayCommand command = new RelayCommand(o => didExecute = true, o => false);

            // Act
            command.Execute(null);

            // Assert
            Assert.IsFalse(didExecute);
        }

        /// <summary>
        ///     Ensures that the <see cref="RelayCommand"/> works as expected when constructing with the  parameterless
        ///     constructors.
        /// </summary>
        [Test]
        public void ParameterlessCommandsWork()
        {
            // Arrange
            bool didExecute = false;
            RelayCommand command = new RelayCommand(() => didExecute = true);

            // Act
            command.Execute();

            // Assert
            Assert.IsTrue(didExecute);
        }
    }
}
