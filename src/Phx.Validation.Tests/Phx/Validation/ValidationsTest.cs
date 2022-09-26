// -----------------------------------------------------------------------------
//  <copyright file="ValidationsTest.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License 2.0 License.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NUnit.Framework;
using static Phx.Validation.TestObject;

namespace Phx.Validation
{
    [TestFixture]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(ParallelScope.All)]
    public class ValidationsTest
    {
        private static void Verify(ValidationResult result, bool expectSuccess)
        {
            if (expectSuccess && result is FailureResult f)
            {
                Console.WriteLine(f.Cause);
            }

            Assert.That(result.IsSuccess, Is.EqualTo(expectSuccess));
        }

        [TestCase(true, true)]
        [TestCase(false, false)]
        public void IsTrue(bool value, bool expectSuccess)
        {
            Verify(value.IsTrue(), expectSuccess);
        }

        [TestCase(false, true)]
        [TestCase(true, false)]
        public void IsFalse(bool value, bool expectSuccess)
        {
            Verify(value.IsFalse(), expectSuccess);
        }

        [TestCase(ObjectA, ObjectB, false)]
        [TestCase(ObjectA, Null, false)]
        [TestCase(Null, ObjectB, false)]
        [TestCase(ObjectA, ObjectA, true)]
        [TestCase(Null, Null, true)]
        public void IsEqualTo(TestObject a, TestObject b, bool expectSuccess)
        {
            Verify(a.GetObject().IsEqualTo(b.GetObject()), expectSuccess);
        }

        [TestCase(ObjectA, ObjectB, true)]
        [TestCase(ObjectA, Null, true)]
        [TestCase(Null, ObjectB, true)]
        [TestCase(ObjectA, ObjectA, false)]
        [TestCase(Null, Null, false)]
        public void IsNotEqualTo(TestObject a, TestObject b, bool expectSuccess)
        {
            Verify(a.GetObject().IsNotEqualTo(b.GetObject()), expectSuccess);
        }

        [TestCase(ObjectA, true)]
        [TestCase(Null, false)]
        public void IsNotNull(TestObject value, bool expectSuccess)
        {
            Verify(value.GetObject().IsNotNull(), expectSuccess);
        }

        [TestCase(ObjectA, false)]
        [TestCase(Null, true)]
        public void IsNull(TestObject value, bool expectSuccess)
        {
            Verify(value.GetObject().IsNull(), expectSuccess);
        }

        [TestCase("string", false)]
        [TestCase("", true)]
        [TestCase(null, true)]
        public void IsNullOrEmpty(string value, bool expectSuccess)
        {
            Verify(value.IsNullOrEmpty(), expectSuccess);
        }

        [TestCase("string", true)]
        [TestCase("", false)]
        [TestCase(null, false)]
        public void IsNotNullOrEmpty(string value, bool expectSuccess)
        {
            Verify(value.IsNotNullOrEmpty(), expectSuccess);
        }

        [TestCase("string", false)]
        [TestCase("", true)]
        [TestCase(" ", true)]
        [TestCase("\t", true)]
        [TestCase(null, true)]
        public void IsBlank(string value, bool expectSuccess)
        {
            Verify(value.IsBlank(), expectSuccess);
        }

        [TestCase("string", true)]
        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("\t", false)]
        [TestCase(null, false)]
        public void IsNotBlank(string value, bool expectSuccess)
        {
            Verify(value.IsNotBlank(), expectSuccess);
        }

        [TestCase(0, false)]
        [TestCase(1, true)]
        public void IsNotEmpty(int count, bool expectSuccess)
        {
            var value = new List<object>();
            for (int i = 0; i < count; i++)
            {
                value.Add(new object());
            }
            Verify(value.IsNotEmpty(), expectSuccess);
        }

        [TestCase(0, true)]
        [TestCase(1, false)]
        public void IsEmpty(int count, bool expectSuccess)
        {
            var value = new List<object>();
            for (int i = 0; i < count; i++)
            {
                value.Add(new object());
            }
            Verify(value.IsEmpty(), expectSuccess);
        }

        [TestCase(1, 2, 3, 1, null, true)]
        [TestCase(1, 2, 3, 1, 2, true)]
        [TestCase(1, 2, 3, 4, null, false)]
        [TestCase(1, 2, 3, 1, 4, false)]
        public void ContainsAll(int a, int b, int c, int search1, int? search2, bool expectSuccess)
        {
            var value = new List<int> { a, b, c };
            if (search2 == null)
            {
                Verify(value.ContainsAll(search1), expectSuccess);
            }
            else
            {
                Verify(value.ContainsAll(search1, (int)search2), expectSuccess);
            }
        }

        [TestCase(1, 2, 3, 1, null, true)]
        [TestCase(1, 2, 3, 1, 2, true)]
        [TestCase(1, 2, 3, 4, null, false)]
        [TestCase(1, 2, 3, 1, 4, true)]
        [TestCase(1, 2, 3, 0, 3, true)]
        public void ContainsAny(int a, int b, int c, int search1, int? search2, bool expectSuccess)
        {
            var value = new List<int> { a, b, c };
            if (search2 == null)
            {
                Verify(value.ContainsAny(search1), expectSuccess);
            }
            else
            {
                Verify(value.ContainsAny(search1, (int)search2), expectSuccess);
            }
        }

        [TestCase(0, false)]
        [TestCase(1, true)]
        [TestCase(-1, true)]
        public void IsNotZero(int value, bool expectSuccess)
        {
            Verify(value.IsNotZero(), expectSuccess);
        }

        [TestCase(0, 1, 10, true, true, false, "Value under[]")]
        [TestCase(11, 1, 10, true, true, false, "Value over[]")]
        [TestCase(5, 1, 10, true, true, true, "Inside range[]")]
        [TestCase(1, 1, 10, true, true, true, "At lower bounds[]")]
        [TestCase(10, 1, 10, true, true, true, "At upper bounds[]")]
        [TestCase(0, 1, 10, false, true, false, "Value under(]")]
        [TestCase(5, 1, 10, false, true, true, "Inside range(]")]
        [TestCase(1, 1, 10, false, true, false, "At lower bounds(]")]
        [TestCase(11, 1, 10, true, false, false, "Value over[)")]
        [TestCase(5, 1, 10, true, false, true, "Inside range[)")]
        [TestCase(10, 1, 10, true, false, false, "At upper bounds[)")]
        public void IsInRange_Int(
            int value,
            int min,
            int max,
            bool minInclusive,
            bool maxInclusive,
            bool expectSuccess,
            string description
        )
        {
            Console.WriteLine(description);
            Verify(value.IsInRange(min, max, minInclusive, maxInclusive), expectSuccess);
        }

        [TestCase(ComparableLess, ComparableMin, ComparableMax, true, true, false, "Value under[]")]
        [TestCase(ComparableMore, ComparableMin, ComparableMax, true, true, false, "Value over[]")]
        [TestCase(ComparableMiddle, ComparableMin, ComparableMax, true, true, true, "Inside range[]")]
        [TestCase(ComparableMin, ComparableMin, ComparableMax, true, true, true, "At lower bounds[]")]
        [TestCase(ComparableMax, ComparableMin, ComparableMax, true, true, true, "At upper bounds[]")]
        [TestCase(ComparableLess, ComparableMin, ComparableMax, false, true, false, "Value under(]")]
        [TestCase(ComparableMiddle, ComparableMin, ComparableMax, false, true, true, "Inside range(]")]
        [TestCase(ComparableMin, ComparableMin, ComparableMax, false, true, false, "At lower bounds(]")]
        [TestCase(ComparableMore, ComparableMin, ComparableMax, true, false, false, "Value over[)")]
        [TestCase(ComparableMiddle, ComparableMin, ComparableMax, true, false, true, "Inside range[)")]
        [TestCase(ComparableMax, ComparableMin, ComparableMax, true, false, false, "At upper bounds[)")]
        public void IsInRange_Object(
            TestObject value,
            TestObject min,
            TestObject max,
            bool minInclusive,
            bool maxInclusive,
            bool expectSuccess,
            string description
        )
        {
            Console.WriteLine(description);
            var valueObject = (string)value.GetObject()!;
            var minObject = (string)min.GetObject()!;
            var maxObject = (string)max.GetObject()!;

            Verify(valueObject.IsInRange(minObject, maxObject, minInclusive, maxInclusive), expectSuccess);
        }

        [TestCase(ComparableMiddle, true)]
        [TestCase(ObjectA, false)]
        [TestCase(Null, false)]
        public void IsType(TestObject value, bool expectSuccess)
        {
            Verify(value.GetObject().IsType<string>(), expectSuccess);
        }

        [Test]
        public void IsType_Inherited()
        {
            Verify(ComparableMiddle.GetObject().IsType<object>(), true);
        }

        [TestCase(ComparableMiddle, true)]
        [TestCase(ObjectA, false)]
        [TestCase(Null, true)]
        public void IsTypeOrNull(TestObject value, bool expectSuccess)
        {
            Verify(value.GetObject().IsTypeOrNull<string>(), expectSuccess);
        }

        [Test]
        public void IsTypeOrNull_Inherited()
        {
            Verify(ComparableMiddle.GetObject().IsTypeOrNull<object>(), true);
        }
    }
}