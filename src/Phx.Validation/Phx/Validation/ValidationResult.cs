// -----------------------------------------------------------------------------
//  <copyright file="ValidationResult.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License 2.0 License.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Validation {
    using System;

    /// <summary> Represents the result of a validation. </summary>
    public abstract class ValidationResult {
        /// <summary> Gets a value indicating whether the validation was successful. </summary>
        /// <value> <c> true </c> if the validation was successful, otherwise false. </value>
        public bool IsSuccess { get; }

        internal ValidationResult(bool isSuccess) {
            IsSuccess = isSuccess;
        }

        private static readonly SuccessResult SUCCESS = new();

        /// <summary> Constructs a succesful <see cref="ValidationResult" /> instance. </summary>
        /// <returns> A successful <see cref="ValidationResult" /> instance. </returns>
        public static ValidationResult Success() {
            return SUCCESS;
        }

        /// <summary> Constructs an unsuccesful <see cref="ValidationResult" /> instance. </summary>
        /// <param name="cause"> The exception that describes of the cause of the failure. </param>
        /// <returns> An unsuccessful <see cref="ValidationResult" /> instance. </returns>
        public static ValidationResult Failure(Exception cause) {
            return new FailureResult(cause);
        }

        /// <summary> Constructs an unsuccesful <see cref="ValidationResult" /> instance. </summary>
        /// <param name="cause"> A description of the cause of the failure. </param>
        /// <returns> An unsuccessful <see cref="ValidationResult" /> instance. </returns>
        public static ValidationResult Failure(string cause) {
            return new FailureResult(new ValidationException(cause));
        }
    }

    /// <summary> Represents a successful result of a validation. </summary>
    public sealed class SuccessResult : ValidationResult {
        internal SuccessResult() : base(true) { }
    }

    /// <summary> Represents a failure result of a validation. </summary>
    public sealed class FailureResult : ValidationResult {
        /// <summary> Gets the exception that describes the cause of the failure. </summary>
        public Exception Cause { get; }

        internal FailureResult(Exception cause) : base(false) {
            Cause = cause;
        }
    }
}
