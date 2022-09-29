# PHX.Validation
PHX.Validation provides argument and value validation utilities.

It does this by providing a framework for defining concise, self documenting
expressions that check common conditions and perform an action based on that
condition.

## Set up
PHX.Validation can be installed as a Nuget package using the .NET CLI.
```shell
dotnet add package Phx.Validation
```

## Getting Started

The framework is broken up into two parts, both of which are easily extensible.
* **Validations** are simple methods that check common conditions and return a
  `ValidationResult` indicating if the condition check was successful. They
  can check conditions such as whether a numeric value is within a certain
  range, or whether a collection is empty.
* **Validators** are methods that perform an action based on a
  `ValidationResult`. They can perform actions such as throwing an exception,
  or logging a warning based on the given result.

## Provided Validators
The framework provides several validators.
* `Require.ThatValue(ValidationResult)` will check a validation and throw an
  `InvalidOperationException` with a message indicating the failure.
  ```csharp
  Require.ThatValue(size.IsNotZero());
  ```

* `Require.ThatArgument(ArgumentName, ValidationResult)` will check a validation
  and throw an `ArgumentException` indicating the invalid argument.
  ```csharp
  Require.ThatArgument(nameof(userName), userName.IsNotBlank());
  ```

* `Check.That(ValidationResult)` will check a validation and return a boolean
  value that can be used to branch logic.
  ```csharp
  if (Check.That(currentValues.ContainsAll(newValues))) {
      return currentValues;
  }
  ```

## Provided Validations
The following validations are provided as extension methods on the validated
types.

* `IsTrue`/`IsFalse`: Validates a boolean value is either true or false.
  ```csharp
  Check.That(x.IsInitialized.IsTrue());
  Require.ThatValue(y.IsDisposed.IsFalse());
  ```

* `IsEqualTo`/`IsNotEqualTo`: Validates a value is or is not equal to another
  type using the default `EqualityComparer`.
  ```csharp
  Check.That(x.IsEqualTo(y));
  Require.ThatValue(x.IsNotEqualTo(y));
  ```

* `IsReferenceEqualTo`/`IsReferenceNotEqualTo`: Validates two objects do or do
  not reference the same instance.
  ```csharp
  Check.That(x.IsReferenceEqualTo(x));
  Require.ThatValue(x.IsReferenceNotEqualTo(y));
  ```

* `IsNull`/`IsNotNull`: Validates a value is or is not `null`.
  ```csharp
  Check.That(x.IsNull());
  Require.ThatValue(x.IsNotNull());
  ```

* `IsNullOrEmpty`/`IsNotNullOrEmpty`: Validates a string is or is not `null` or
  an empty string.
  ```csharp
  Check.That("".IsNullOrEmpty());
  Require.ThatValue(password.IsNotNullOrEmpty());
  ```

* `IsBlank`/`IsNotBlank`: Validates a string is or is not `null`, an empty
  string, or a string of entirely whitespace.
  ```csharp
  Check.That("\t".IsBlank());
  Require.ThatValue(userName.IsNotBlank());
  ```

* `IsEmpty`/`IsNotEmpty`: Validates an `ICollection` is or is not empty. This
  uses the collection's `Count` property and will not enumerate the collection.
  ```csharp
  Check.That(items.IsEm
  pty());
  Require.ThatValue(addresses.IsNotEmpty());
  ```

* `ContainsAll`/`ContainsAny`: Validates an `IEnumerable` collection contains
  all of or at least one of the given values.
  ```csharp
  Check.That(validItems.ContainsAll(foundItems));
  Require.ThatValue(workDays.ContainsAny(Monday, Tuesday, Wednesday));
  ```

* `IsNotZero`: Validates an integer value is non-zero.
  ```csharp
  Check.That(size.IsNotZero());
  ```

* `IsInRange`: Validates an `IComparable` value is inside a given range.
  ```csharp
  Check.That(numPets.IsInRange(1, 3));
  Require.ThatValue(cupsOfCoffeeConsumed.IsInRange(1, 9, maxInclusive: false));
  ```

* `IsType`/`IsTypeOrNull`: Validates a value is assignable to the given type.
  ```csharp
  Check.That(x.IsType<IDisposable>());
  Require.That((foo as IVehicle).IsTypeOrNull<ICar>());
  ```

## Defining Custom Validations
To define validations with custom logic, define a method (typically an extension
method) that returns a `ValidationResult`. Then use the custom logic to return
either `ValidationResult.Success()` or `ValidationResult.Failure(reason)`.
```csharp
public static class CustomValidations {
    public static ValidationResult IsEqualTo(this float a, float b, float precision) {
        return Math.Abs(a - b) <= precision
            ? ValidationResult.Success()
            : ValidationResult.Failure(
                    $"The floating point value {a} is not equal to value {b} within precision {precision}.");
    }
}
```

The custom validation can be used with any validator.
```csharp
Require.ThatArgument(operandA.IsEqualTo(1.0f, precision: 0.001f));
```

## Defining Custom Validators
To define validators that perform custom logic based on the result of a
validation, the validator should define a method that accepts a
`ValidationResult` as an argument, then performs the custom logic based on that
result.

```csharp
public static class Assert {
    public static void That(ValidationResult result, string failureMessage) {
        switch (result) {
            case SuccessResult:
                break;
            case FailureResult failure:
                throw new AssertionFailedException(failureMessage, failure.Cause);
        }
    }
}
```

The custom validator can then be used with any validation.
```csharp
Assert.That(result.IsNotNull(), "No result was returned from the operation.")
```

---

<div align="center">
Copyright (c) 2022 Star Cruise Studios LLC.<br/>
All rights reserved. Licensed under the Apache License 2.0 License.<br/>
See http://www.apache.org/licenses/LICENSE-2.0 for full license information.<br/>
</div>