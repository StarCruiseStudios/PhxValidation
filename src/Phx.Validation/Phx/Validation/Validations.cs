// -----------------------------------------------------------------------------
//  <copyright file="Validation.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License 2.0 License.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Phx.Validation
{
    /// <summary>
    ///     Contains extension methods used to validate constraints of values.
    /// </summary>
    public static class Validations
    {
        /// <summary>
        ///     Validates that the given condition is <c>true</c>.
        /// </summary>
        /// <param name="condition"> The condition to validate. </param>
        /// <returns> The <see cref="ValidationResult"/>. </returns>
        public static ValidationResult IsTrue(this bool condition)
        {
            return condition
                ? ValidationResult.Success()
                : ValidationResult.Failure("The expression was expected to be true.");
        }

        /// <summary>
        ///     Validates that the given condition is <c>false</c>.
        /// </summary>
        /// <param name="condition"> The condition to validate. </param>
        /// <returns> The <see cref="ValidationResult"/>. </returns>
        public static ValidationResult IsFalse(this bool condition)
        {
            return condition
                ? ValidationResult.Failure("The expression was expected to be false.")
                : ValidationResult.Success();
        }

        /// <summary>
        ///     Validates that the given value is equal to the given expected value.
        /// </summary>
        /// <typeparam name="T"> The type of the value to validate. </typeparam>
        /// <param name="value"> The value to validate. </param>
        /// <param name="expected"> The expected value to validate against. </param>
        /// <returns> The <see cref="ValidationResult"/>. </returns>
        public static ValidationResult IsEqualTo<T>(this T value, T expected)
        {
            return EqualityComparer<T>.Default.Equals(value, expected)
                ? new SuccessResult()
                : ValidationResult.Failure($"The value <{value}> did not match expected value <{expected}>.");
        }

        /// <summary>
        ///     Validates that the given value is not equal to the given unexpected value.
        /// </summary>
        /// <typeparam name="T"> The type of the value to validate. </typeparam>
        /// <param name="value"> The value to validate. </param>
        /// <param name="unexpected"> The unexpected value to validate against. </param>
        /// <returns> The <see cref="ValidationResult"/>. </returns>
        public static ValidationResult IsNotEqualTo<T>(this T value, T unexpected)
        {
            return EqualityComparer<T>.Default.Equals(value, unexpected)
                ? ValidationResult.Failure($"The value <{value}> matched unexpected value <{unexpected}>.")
                : new SuccessResult();
        }

        /// <summary>
        ///     Validates that the given value is <c>null</c>.
        /// </summary>
        /// <param name="value"> The value to validate. </param>
        /// <returns> The <see cref="ValidationResult"/>. </returns>
        public static ValidationResult IsNull(this object? value)
        {
            return value == null
                ? ValidationResult.Success()
                : ValidationResult.Failure($"The value <{value}> is not null.");
        }

        /// <summary>
        ///     Validates that the given value is not <c>null</c>.
        /// </summary>
        /// <param name="value"> The value to validate. </param>
        /// <returns> The <see cref="ValidationResult"/>. </returns>
        public static ValidationResult IsNotNull(this object? value)
        {
            return value == null
                ? ValidationResult.Failure("The value is null.")
                : ValidationResult.Success();
        }

        /// <summary>
        ///     Validates that the given string value is <c>null</c> or empty.
        /// </summary>
        /// <param name="value"> The value to validate. </param>
        /// <returns> The <see cref="ValidationResult"/>. </returns>
        public static ValidationResult IsNullOrEmpty(this string? value)
        {
            return string.IsNullOrEmpty(value)
                ? ValidationResult.Success()
                : ValidationResult.Failure($"The value <{value}> is not null or empty.");
        }

        /// <summary>
        ///     Validates that the given string value is not <c>null</c> or empty.
        /// </summary>
        /// <param name="value"> The value to validate. </param>
        /// <returns> The <see cref="ValidationResult"/>. </returns>
        public static ValidationResult IsNotNullOrEmpty(this string? value)
        {
            return string.IsNullOrEmpty(value)
                ? ValidationResult.Failure($"The value <{value}> is null or empty.")
                : ValidationResult.Success();
        }

        /// <summary>
        ///     Validates that the given string value is <c>null</c>, empty or entirely whitespace.
        /// </summary>
        /// <param name="value"> The value to validate. </param>
        /// <returns> The <see cref="ValidationResult"/>. </returns>
        public static ValidationResult IsBlank(this string? value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? ValidationResult.Success()
                : ValidationResult.Failure($"The value <{value}> is not blank.");
        }

        /// <summary>
        ///     Validates that the given string value is not <c>null</c>, empty or entirely whitespace.
        /// </summary>
        /// <param name="value"> The value to validate. </param>
        /// <returns> The <see cref="ValidationResult"/>. </returns>
        public static ValidationResult IsNotBlank(this string? value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? ValidationResult.Failure($"The value <{value}> is blank.")
                : ValidationResult.Success();
        }

        /// <summary>
        ///     Validates that the given collection is empty.
        /// </summary>
        /// <param name="collection"> The collection to validate. </param>
        /// <returns> The <see cref="ValidationResult"/>. </returns>
        public static ValidationResult IsEmpty(this ICollection collection)
        {
            var count = collection.Count;
            return count == 0
                ? ValidationResult.Success()
                : ValidationResult.Failure($"The collection (size:{count}) is not empty.");
        }

        /// <summary>
        ///     Validates that the given collection is not empty.
        /// </summary>
        /// <param name="collection"> The collection to validate. </param>
        /// <returns> The <see cref="ValidationResult"/>. </returns>
        public static ValidationResult IsNotEmpty(this ICollection collection)
        {
            return collection.Count == 0
                ? ValidationResult.Failure("The collection is empty.")
                : ValidationResult.Success();
        }

        /// <summary>
        ///     Validates that the given collection contains the given elements.
        /// </summary>
        /// <typeparam name="T"> The type of elements contained in the collection. </typeparam>
        /// <param name="collection"> The collection to validate. </param>
        /// <param name="elements"> The elements to search for. </param>
        /// <returns> The <see cref="ValidationResult"/>. </returns>
        public static ValidationResult ContainsAll<T>(this ICollection<T> collection, params T[] elements)
        {
            return ContainsAll(collection, elements.AsEnumerable());
        }

        /// <summary>
        ///     Validates that the given collection contains the given elements.
        /// </summary>
        /// <typeparam name="T"> The type of elements contained in the collection. </typeparam>
        /// <param name="collection"> The collection to validate. </param>
        /// <param name="elements"> The elements to search for. </param>
        /// <returns> The <see cref="ValidationResult"/>. </returns>
        public static ValidationResult ContainsAll<T>(this ICollection<T> collection, IEnumerable<T> elements)
        {
            var searchElements = new HashSet<T>(elements);
            foreach (var value in collection)
            {
                _ = searchElements.Remove(value);

                if (searchElements.Count == 0)
                {
                    return ValidationResult.Success();
                }
            }

            return ValidationResult.Failure(
                $"The collection did not contain elements: [{string.Join(" ", searchElements)}]");
        }

        /// <summary>
        ///     Validates that the given collection contains at least one of the given elements.
        /// </summary>
        /// <typeparam name="T"> The type of elements contained in the collection. </typeparam>
        /// <param name="collection"> The collection to validate. </param>
        /// <param name="elements"> The elements to search for. </param>
        /// <returns> The <see cref="ValidationResult"/>. </returns>
        public static ValidationResult ContainsAny<T>(this ICollection<T> collection, params T[] elements)
        {
            return ContainsAny(collection, elements.AsEnumerable());
        }

        /// <summary>
        ///     Validates that the given collection contains at least one of the given elements.
        /// </summary>
        /// <typeparam name="T"> The type of elements contained in the collection. </typeparam>
        /// <param name="collection"> The collection to validate. </param>
        /// <param name="elements"> The elements to search for. </param>
        /// <returns> The <see cref="ValidationResult"/>. </returns>
        public static ValidationResult ContainsAny<T>(this ICollection<T> collection, IEnumerable<T> elements)
        {
            foreach (var searchValue in elements)
            {
                if (collection.Contains(searchValue))
                {
                    return ValidationResult.Success();
                }
            }

            return ValidationResult.Failure("The collection did not contain any of the provided elements.");
        }

        /// <summary>
        ///     Validates that the given value is not 0.
        /// </summary>
        /// <param name="value"> The value to validate. </param>
        /// <returns> The <see cref="ValidationResult"/>. </returns>
        public static ValidationResult IsNotZero(this int value)
        {
            return value == 0
                ? ValidationResult.Failure("The value is 0.")
                : ValidationResult.Success();
        }

        /// <summary>
        ///     Validates that the given value is within the given range.
        /// </summary>
        /// <typeparam name="T"> The type of the value to validate. </typeparam>
        /// <param name="value"> The value to validate. </param>
        /// <param name="min"> The minimum value of the range. <paramref name="min"/> is inclusive by default, use the
        ///                    <see cref="minInclusive"/> parameter to change this. </param>
        /// <param name="max"> The maximum value of the range. <paramref name="max"/> is inclusive by default, use the
        ///                    <see cref="maxInclusive"/> parameter to change this. </param>
        /// <param name="minInclusive"> A value indicating whether <paramref name="min"/> is inclusive. </param>
        /// <param name="maxInclusive"> A value indicating whether <paramref name="max"/> is inclusive. </param>
        /// <returns> The <see cref="ValidationResult"/>. </returns>
        public static ValidationResult IsInRange<T>(
            this T value,
            T min,
            T max,
            bool minInclusive = true,
            bool maxInclusive = true
        ) where T : IComparable<T>
        {
            var minComparison = value.CompareTo(min);
            if (minComparison < 0 || (!minInclusive && minComparison == 0))
            {
                return ValidationResult.Failure(
                    $"The value is {value} is outside of the range {(minInclusive ? "[" : "(")}{min}, {max}{(maxInclusive ? "]" : ")")}");
            }

            var maxComparison = value.CompareTo(max);
            if (maxComparison > 0 || (!maxInclusive && maxComparison == 0))
            {
                return ValidationResult.Failure(
                    $"The value is {value} is outside of the range {(minInclusive ? "[" : "(")}{min}, {max}{(maxInclusive ? "]" : ")")}");
            }

            return ValidationResult.Success();
        }

        /// <summary>
        ///     Validates that the given value is assignable to the given type.
        /// </summary>
        /// <typeparam name="T"> The type to validate against. </typeparam>
        /// <param name="value"> The value to validate. </param>
        /// <returns> The <see cref="ValidationResult"/>. </returns>
        public static ValidationResult IsType<T>(this object? value)
        {
            return value is T
                ? ValidationResult.Success()
                : ValidationResult.Failure($"The value <{value}> is not assignable to type <{typeof(T)}>.");
        }

        /// <summary>
        ///     Validates that the given value is assignable to the given type or is <c>null</c>.
        /// </summary>
        /// <typeparam name="T"> The type to validate against. </typeparam>
        /// <param name="value"> The value to validate. </param>
        /// <returns> The <see cref="ValidationResult"/>. </returns>
        public static ValidationResult IsTypeOrNull<T>(this object? value)
        {
            return value is null or T
                ? ValidationResult.Success()
                : ValidationResult.Failure(
                    $"The value <{value}> is not null and not assignable to type <{typeof(T)}>.");
        }
    }
}