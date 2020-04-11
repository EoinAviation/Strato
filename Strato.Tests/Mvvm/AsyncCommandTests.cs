// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsyncCommandTests.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Tests.Mvvm
{
    using System.Threading.Tasks;

    using NUnit.Framework;

    using Strato.Mvvm.Commands;

    /// <summary>
    ///     The <see cref="AsyncCommand"/> tests.
    /// </summary>
    [TestFixture]
    public class AsyncCommandTests
    {
        /// <summary>
        ///     Ensures that <see cref="AsyncCommand"/>s can be executed.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task"/>.
        /// </returns>
        [Test]
        public async Task RelayCommandExecutes()
        {
            // Arrange
            bool didExecute = false;
            AsyncCommand command = new AsyncCommand(
                async () =>
                {
                    await Task.Yield();
                    didExecute = true;
                });

            // Act
            await command.ExecuteAsync();

            // Assert
            Assert.IsTrue(didExecute);
        }

        /// <summary>
        ///     Ensures that <see cref="RelayCommand"/>s are not executed if the <see cref="RelayCommand.CanExecute()"/>
        ///     method returns <c>false</c>.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task"/>.
        /// </returns>
        [Test]
        public async Task RelayCommandDoesNotExecuteWhenUnable()
        {
            // Arrange
            bool didExecute = false;
            AsyncCommand command = new AsyncCommand(
                async () =>
                {
                    await Task.Yield();
                    didExecute = true;
                },
                () => false);

            // Act
            await command.ExecuteAsync();

            // Assert
            Assert.IsFalse(didExecute);
        }
    }
}
