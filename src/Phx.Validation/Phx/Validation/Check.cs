// -----------------------------------------------------------------------------
//  <copyright file="Check.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License 2.0 License.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Validation {
    /// <summary> Contains validation methods that return a boolean indicating whether a validation is evaluated successfully. </summary>
    public static class Check {
        /// <summary> Returns a valud indicating whether a validation is evaluated successfully. </summary>
        /// <param name="result"> The result of the validation. </param>
        /// <return> <c> true </c> if the validation is successful, otherwise <c> false </c>. </return>
        public static bool That(ValidationResult result) {
            return result.IsSuccess;
        }
    }
}
