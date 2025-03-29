using System;

namespace Ardalis.HttpClientTestExtensions;

/// <summary>
/// Indicates that a method is an assertion method.
/// </summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public class AssertionMethodAttribute : Attribute
{
  
}
