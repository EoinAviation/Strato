// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewModel.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Mvvm
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    ///     The base View Model implementing <see cref="INotifyPropertyChanging"/> and
    ///     <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public abstract class ViewModel : INotifyPropertyChanging, INotifyPropertyChanged
    {
        /// <summary>
        ///     The <see cref="Dictionary{TKey,TValue}"/> of properties and values, where the Key is the property name
        ///     and the value is the properties value.
        /// </summary>
        private readonly Dictionary<string, object> _properties;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ViewModel"/> class.
        /// </summary>
        protected ViewModel() => _properties = new Dictionary<string, object>();

        /// <summary>
        ///     Gets the value of the property.
        /// </summary>
        /// <param name="propertyName">
        ///     The name of the property.
        /// </param>
        /// <typeparam name="TValue">
        ///     The type of value expected.
        /// </typeparam>
        /// <returns>
        ///     The value of the property.
        /// </returns>
        protected TValue Get<TValue>([CallerMemberName] string propertyName = null)
        {
            // Ensure we have a property name
            EnsurePropertyNameIsValid(propertyName);

            // If we have a value, return it
            if (_properties.ContainsKey(propertyName)) return (TValue)_properties[propertyName];

            // Otherwise use the default value
            return default;
        }

        /// <summary>
        ///     Gets the value of the property or uses the given default value.
        /// </summary>
        /// <param name="defaultValue">
        ///     The default value to use if the property has not been registered yet.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property.
        /// </param>
        /// <typeparam name="TValue">
        ///     The type of value expected.
        /// </typeparam>
        /// <returns>
        ///     The value of the property.
        /// </returns>
        protected TValue Get<TValue>(TValue defaultValue, [CallerMemberName] string propertyName = null)
        {
            // Ensure we have a property name
            EnsurePropertyNameIsValid(propertyName);

            // If we have a value, return it
            if (_properties.ContainsKey(propertyName))
            {
                return (TValue)_properties[propertyName];
            }
            else
            {
                // Otherwise, set it to our default
                Set(defaultValue, propertyName);
            }

            // Return the value
            return Get<TValue>(propertyName);
        }

        /// <summary>
        ///     Sets the value of the property.
        /// </summary>
        /// <param name="value">
        ///        The value to set the property to.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property.
        /// </param>
        /// <typeparam name="TValue">
        ///        The type of value being set.
        /// </typeparam>
        protected void Set<TValue>(TValue value, [CallerMemberName] string propertyName = null)
        {
            // Ensure we have a property name
            EnsurePropertyNameIsValid(propertyName);

            // If the value is there, update it
            OnPropertyChanging(propertyName);

            // Todo: Roslyn is showing a style issue here. Need to omit it from the Analyzers project.
            if (_properties.ContainsKey(propertyName))
            {
                _properties[propertyName] = value;
            }
            else
            {
                // Otherwise, add a new entry
                _properties.Add(propertyName, value);
            }

            // Notify
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        ///     Event raised when the value of a property is about to change.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        ///     Event raised when the value of a property has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///        Method called to raise the <see cref="PropertyChanging"/> event.
        /// </summary>
        /// <param name="propertyName">
        ///        The name of the property whose value is about to change.
        /// </param>
        protected void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            // Ensure we have a property name
            EnsurePropertyNameIsValid(propertyName);

            // Invoke the event
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        ///        Method called to raise the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">
        ///        The name of the property whose value has changed.
        /// </param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Ensure we have a property name
            EnsurePropertyNameIsValid(propertyName);

            // Invoke the event
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Ensures that the given property name is valid. If the name of the property is invalid, then an
        ///     <see cref="Exception"/> will be thrown.
        /// </summary>
        /// <param name="propertyName">
        ///        The name of the property.
        /// </param>
        private void EnsurePropertyNameIsValid(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException(
                    nameof(propertyName),
                    "The property name cannot be null or empty");
            }
        }
    }
}
