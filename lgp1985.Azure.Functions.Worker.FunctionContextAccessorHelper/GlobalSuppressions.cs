// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

#if NET6_0 || NET7_0 || NETSTANDARD2_0_OR_GREATER
[assembly: SuppressMessage("Maintainability", "CA1510:Use ArgumentNullException throw helper", Justification = "Not supported prior to .NET8")]
#endif
