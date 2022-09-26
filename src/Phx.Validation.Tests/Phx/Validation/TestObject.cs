// -----------------------------------------------------------------------------
//  <copyright file="TestObject.cs" company="Star Cruise Studios LLC">
//      Copyright (c) 2022 Star Cruise Studios LLC. All rights reserved.
//      Licensed under the Apache License 2.0 License.
//      See http://www.apache.org/licenses/LICENSE-2.0 for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

using System;

namespace Phx.Validation
{
    public enum TestObject
    {
        ObjectA,
        ObjectB,
        ComparableLess,
        ComparableMin,
        ComparableMiddle,
        ComparableMax,
        ComparableMore,
        Null
    }

    public static class TestObjectExtensions
    {
        private static readonly object ObjectA = new object();
        private static readonly object ObjectB = new object();

        public static object? GetObject(this TestObject odr)
        {
            return odr switch
            {
                TestObject.ObjectA => TestObjectExtensions.ObjectA,
                TestObject.ObjectB => TestObjectExtensions.ObjectB,
                TestObject.ComparableLess => "A",
                TestObject.ComparableMin => "B",
                TestObject.ComparableMiddle => "M",
                TestObject.ComparableMax => "Y",
                TestObject.ComparableMore => "Z",
                TestObject.Null => null,
                _ => throw new NotSupportedException($"Unknown Test Object ${odr}."),
            };
        }
    }
}