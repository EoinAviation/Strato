// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewModelTests.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Tests.Mvvm
{
    using System;
    using System.ComponentModel;

    using NUnit.Framework;

    using Strato.Mvvm;
    using Strato.Tests.Mvvm.Mocks;

    /// <summary>
    ///     The <see cref="ViewModel"/> tests.
    /// </summary>
    [TestFixture]
    public class ViewModelTests
    {
        /// <summary>
        ///     Ensures that property values can be set, and retrieved.
        /// </summary>
        [Test]
        public void CanGetAndSetPropertyValues()
        {
            // Arrange
            Guid setGuid = Guid.NewGuid();
            int setInt = 1;
            TestClass setClass = new TestClass();

            // Act
            MockViewModel viewModel = new MockViewModel { Guid = setGuid, Integer = setInt, Class = setClass };
            Guid getGuid = viewModel.Guid;
            int getInt = viewModel.Integer;
            TestClass getClass = viewModel.Class;

            // Assert
            Assert.AreEqual(setGuid, getGuid);
            Assert.AreEqual(setInt, getInt);
            Assert.AreEqual(setClass, getClass);
        }

        /// <summary>
        ///     Ensures that the default values can be retrieved when no value has been set.
        /// </summary>
        [Test]
        public void CanGetDefaultValues()
        {
            // Arrange
            MockViewModel viewModel = new MockViewModel();

            // Act / Assert
            Assert.AreEqual(viewModel.Guid, default(Guid));
            Assert.AreEqual(viewModel.Integer, 0);
            Assert.IsNull(viewModel.Class);
        }

        /// <summary>
        ///     Ensures that the Get and Set methods will throw an <see cref="ArgumentNullException"/> when the property
        ///     name is not provided.
        /// </summary>
        [Test]
        public void GetAndSetMethodsThrowWithoutPropertyName()
        {
            // Arrange
            MockViewModel viewModel = new MockViewModel();

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => Console.WriteLine(viewModel.InvalidProperty));
            Assert.Throws<ArgumentNullException>(() => viewModel.InvalidProperty = "Test");
        }

        /// <summary>
        ///     Ensures that the Get method sets the default value if one is not already present, and does not duplicate
        ///     new objects.
        /// </summary>
        [Test]
        public void DefaultGettersWork()
        {
            // Arrange
            MockViewModel viewModel = new MockViewModel();

            // Act / Assert
            for (int i = 1; i <= 10; i++)
            {
                viewModel.IncrementIntegerCommand.Execute();
                Assert.AreEqual(i, viewModel.Integer);
            }
        }

        /// <summary>
        ///     Ensures the <see cref="INotifyPropertyChanging.PropertyChanging"/> and
        ///     <see cref="INotifyPropertyChanged.PropertyChanged"/> events are raised correctly.
        /// </summary>
        [Test]
        public void PropertyUpdateEventsAreRaised()
        {
            // Arrange
            // Set up the ViewModel
            int startingValue = 2;
            int finishingValue = 5;
            MockViewModel viewModel = new MockViewModel { Integer = startingValue };

            // Some variables to track whether or not the events were raised
            bool propertyChangingWasCalled = false;
            string propertyChangingName = string.Empty;
            bool propertyChangedWasCalled = false;
            string propertyChangedName = string.Empty;

            // Local function to handle the PropertyChanging event
            void OnPropertyChanging(object sender, PropertyChangingEventArgs args)
            {
                if (sender is MockViewModel testViewModel)
                {
                    // Ensure the value hasn't been set yet
                    Assert.AreEqual(startingValue, testViewModel.Integer);

                    // Set these for later
                    propertyChangingWasCalled = true;
                    propertyChangingName = args.PropertyName;
                }
                else
                {
                    // Not the ViewModel, fail!
                    Assert.Fail();
                }
            }

            // Local function to handle the PropertyChanged event
            void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
            {
                if (sender is MockViewModel testViewModel)
                {
                    // Ensure the value has been set
                    Assert.AreEqual(finishingValue, testViewModel.Integer);

                    // Set these for later
                    propertyChangedWasCalled = true;
                    propertyChangedName = args.PropertyName;
                }
                else
                {
                    // Not the ViewModel, fail!
                    Assert.Fail();
                }
            }

            // Setup the events
            viewModel.PropertyChanging += OnPropertyChanging;
            viewModel.PropertyChanged += OnPropertyChanged;

            try
            {
                // Act
                viewModel.Integer = finishingValue;
            }
            finally
            {
                // Clean up, just in case
                viewModel.PropertyChanging -= OnPropertyChanging;
                viewModel.PropertyChanged -= OnPropertyChanged;
            }

            // Assert
            Assert.IsTrue(propertyChangingWasCalled);
            Assert.IsTrue(propertyChangedWasCalled);
            Assert.AreEqual(nameof(viewModel.Integer), propertyChangingName);
            Assert.AreEqual(nameof(viewModel.Integer), propertyChangedName);
        }
    }
}
