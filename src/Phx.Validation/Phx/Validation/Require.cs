// -----------------------------------------------------------------------------
//  <copyright file="Require.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Validation {
    using System;

    /// <summary> Contains validation methods that throw an exception if evaluated unsuccessfully. </summary>
    public static class Require {
        /// <summary> Throws an <see cref="ArgumentException" /> if a validation fails on an argument. </summary>
        /// <param name="argumentName"> The name of the argument. </param>
        /// <param name="result"> The validation result to evaluate. </param>
        /// <param name="failureMessageFormat">
        ///     The message to use in case of a validation failure. The message
        ///     can be a format string and will be passed the provided name of the argument as argument
        ///     <c> {0} </c>.
        /// </param>
        /// <exception cref="ArgumentException"> Thrown when the provided result is a failure. </exception>
        public static void ThatArgument(
                string argumentName,
                ValidationResult result,
                string failureMessageFormat = "Argument '{0}' is invalid."
        ) {
            switch (result) {
                case SuccessResult:
                    break;
                case FailureResult failure:
                    throw new ArgumentException(string.Format(failureMessageFormat, argumentName), failure.Cause);
                default:
#pragma warning disable RCS1140
                    // Add exception to documentation comment: NotSupportedException should never be thrown.
                    throw new NotSupportedException(result.GetType().AssemblyQualifiedName);
#pragma warning restore RCS1140
            }
        }

        /// <summary> Throws an <see cref="ArgumentException" /> if a validation fails on an argument. </summary>
        /// <param name="argumentName"> The name of the argument. </param>
        /// <param name="result"> The validation result to evaluate. </param>
        /// <param name="failureMessage">
        ///     The lazily evaluated message to use in case of a validation failure.
        ///     The function will be passed the provided name of the argument when evaluated.
        /// </param>
        /// <exception cref="ArgumentException"> Thrown when the provided result is a failure. </exception>
        public static void ThatArgument(
                string argumentName,
                ValidationResult result,
                Func<string, string> failureMessage
        ) {
            switch (result) {
                case SuccessResult:
                    break;
                case FailureResult failure:
                    throw new ArgumentException(failureMessage(argumentName), failure.Cause);
                default:
#pragma warning disable RCS1140
                    // Add exception to documentation comment: NotSupportedException should never be thrown.
                    throw new NotSupportedException(result.GetType().AssemblyQualifiedName);
#pragma warning restore RCS1140
            }
        }

        /// <summary> Throws an <see cref="InvalidOperationException" /> if a validation fails on a value. </summary>
        /// <param name="result"> The validation result to evaluate. </param>
        /// <param name="failureMessage"> The message to use in case of a validation failure. </param>
        /// <exception cref="InvalidOperationException"> Thrown when the provided result is a failure. </exception>
        public static void ThatValue(
                ValidationResult result,
                string failureMessage = "Value is invalid."
        ) {
            switch (result) {
                case SuccessResult:
                    break;
                case FailureResult failure:
                    throw new InvalidOperationException(failureMessage, failure.Cause);
                default:
#pragma warning disable RCS1140
                    // Add exception to documentation comment: NotSupportedException should never be thrown.
                    throw new NotSupportedException(result.GetType().AssemblyQualifiedName);
#pragma warning restore RCS1140
            }
        }

        /// <summary> Throws an <see cref="InvalidOperationException" /> if a validation fails on a value. </summary>
        /// <param name="result"> The validation result to evaluate. </param>
        /// <param name="failureMessage"> The lazily evaluated message to use in case of a validation failure. </param>
        /// <exception cref="InvalidOperationException"> Thrown when the provided result is a failure. </exception>
        public static void ThatValue(
                ValidationResult result,
                Func<string> failureMessage) {
            switch (result) {
                case SuccessResult:
                    break;
                case FailureResult failure:
                    throw new InvalidOperationException(failureMessage(), failure.Cause);
                default:
#pragma warning disable RCS1140
                    // Add exception to documentation comment: NotSupportedException should never be thrown.
                    throw new NotSupportedException(result.GetType().AssemblyQualifiedName);
#pragma warning restore RCS1140
            }
        }
    }
}
