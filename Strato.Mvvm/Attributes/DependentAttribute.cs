// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DependentAttribute.cs" company="Strato Systems Pty. Ltd.">
//   Copyright (c) Strato Systems Pty. Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Strato.Mvvm.Attributes
{
    using System;

    /// <summary>
    ///     The <see cref="Attribute"/> used to explicitly declare a property as dependent on another property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DependentAttribute : Attribute
    {
    }
}
