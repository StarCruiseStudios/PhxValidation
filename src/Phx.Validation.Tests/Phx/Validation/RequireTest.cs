// -----------------------------------------------------------------------------
//  <copyright file="RequireTest.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License, Version 2.0.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

namespace Phx.Validation {
    using System;
    using NUnit.Framework;
    using Phx.Test;

    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.All)]
    public class RequireTest {
        [Test]
        public void RequireThatArgumentOnSuccess() {
            var result = ValidationResult.Success();

            Require.ThatArgument(nameof(result), result);
        }

        [Test]
        public void RequireThatArgumentOnFailure() {
            var result = ValidationResult.Failure("Test failed");

            _ = TestUtils.TestForError<ArgumentException>(
                    () => Require.ThatArgument(nameof(result), result));
        }

        [Test]
        public void RequireThatValueOnSuccess() {
            var result = ValidationResult.Success();

            Require.ThatValue(result);
        }

        [Test]
        public void RequireThatValueOnFailure() {
            var result = ValidationResult.Failure("Test failed");

            _ = TestUtils.TestForError<InvalidOperationException>(
                    () => Require.ThatValue(result));
        }
    }
}
