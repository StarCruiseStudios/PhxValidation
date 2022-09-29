// -----------------------------------------------------------------------------
//  <copyright file="ValidationException.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Validation {
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    /// <summary> The exception that is thrown when a validation fails without an inner exception. </summary>
    [Serializable]
    [SuppressMessage("Design",
            "RCS1194",
            Justification = "Intentionally suppressing default and wrapper constructors. ")]
    public sealed class ValidationException : Exception {
        /// <summary> Initializes a new instance of the <see cref="ValidationException" /> class. </summary>
        /// <param name="message"> The message that describes the error. </param>
        public ValidationException(string message)
                : base(message) { }

        /// <summary> Initializes a new instance of the <see cref="ValidationException" /> class. </summary>
        /// <param name="info">
        ///     The <see cref="SerializationInfo" /> that holds the serialized object data about the exception
        ///     being thrown.
        /// </param>
        /// <param name="context">
        ///     The <see cref="StreamingContext" /> that contains contextual information about the source or
        ///     destination.
        /// </param>
        public ValidationException(SerializationInfo info, StreamingContext context)
                : base(info, context) { }
    }
}
