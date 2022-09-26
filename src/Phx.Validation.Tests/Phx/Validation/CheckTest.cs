// -----------------------------------------------------------------------------
//  <copyright file="CheckTest.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License 2.0 License.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

using NUnit.Framework;
using Phx.Test;

namespace Phx.Validation
{
    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.All)]
    public class CheckTest : LoggingTestClass
    {
        [Test]
        public void CheckThatOnSuccess()
        {
            var result = Given("A successful validation result", () => ValidationResult.Success());
            var actual = When("Checking the result", () => Check.That(result));
            Then("The validation succeeds", () => Verify.That(actual.IsTrue()));
        }

        [Test]
        public void CheckThatOnFailure()
        {
            var result = Given("An unsuccessful validation result", () => ValidationResult.Failure("Check failed"));
            var actual = When("Checking the result", () => Check.That(result));
            Then("The validation succeeds", () => Verify.That(actual.IsFalse()));
        }
    }
}