using System;
using System.Collections.Generic;
using System.Linq;

namespace Test {
	internal partial class Assert {
		/// <summary>
		/// Asserts that sequence has no elements.
		/// </summary>
		public static void Empty<TValue>(IEnumerable<TValue> pSequence, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pSequence.Any()) throw pException ?? new AssertionException("Expected no elements");
		}

		/// <summary>
		/// Asserts that sequence has one or more elements.
		/// </summary>
		public static void NotEmpty<TValue>(IEnumerable<TValue> pSequence, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (!pSequence.Any()) throw pException ?? new AssertionException("Expected elements");
		}

		/// <summary>
		/// Asserts that an entire sequence with one or more elements matches the predicate.
		/// </summary>
		public static void All<TValue>(IEnumerable<TValue> pSequence, Func<TValue, bool> pPredicate, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pPredicate == null) throw new ArgumentNullException("pPredicate");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (!pSequence.All(pPredicate)) throw pException ?? new AssertionException("Expected all elements to match predicate");
		}

		/// <summary>
		/// Asserts that one or more elements of a sequence with one or more elements matches the predicate.
		/// </summary>
		public static void Any<TValue>(IEnumerable<TValue> pSequence, Func<TValue, bool> pPredicate, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pPredicate == null) throw new ArgumentNullException("pPredicate");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (!pSequence.Any(pPredicate)) throw pException ?? new AssertionException("Expected at least 1 element to match predicate");
		}

		/// <summary>
		/// Asserts that exactly one element of a sequence with one or more elements matches the predicate.
		/// </summary>
		public static void Only<TValue>(IEnumerable<TValue> pSequence, Func<TValue, bool> pPredicate, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pPredicate == null) throw new ArgumentNullException("pPredicate");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (pSequence.Count(pPredicate) != 1) throw pException ?? new AssertionException("Expected only 1 element to match predicate");
		}

		/// <summary>
		/// Asserts that exactly a specified number of elements of a sequence with one or more elements matches the predicate.
		/// </summary>
		public static void Exactly<TValue>(IEnumerable<TValue> pSequence, int pCount, Func<TValue, bool> pPredicate, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pPredicate == null) throw new ArgumentNullException("pPredicate");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (pCount < 0) throw new ArgumentException("Negative pCount values are not valid");
			if (pSequence.Count(pPredicate) != pCount) throw pException ?? new AssertionException("Expected {0} elements to match predicate", pCount);
		}

		/// <summary>
		/// Asserts that no elements of a sequence with one or more elements matches the predicate.
		/// </summary>
		public static void None<TValue>(IEnumerable<TValue> pSequence, Func<TValue, bool> pPredicate, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pPredicate == null) throw new ArgumentNullException("pPredicate");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (pSequence.Any(pPredicate)) throw pException ?? new AssertionException("Expected no elements to match predicate");
		}

		/// <summary>
		/// Asserts that a sequence has a specified number of elements.
		/// </summary>
		public static void Count<TValue>(IEnumerable<TValue> pSequence, int pCount, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (pCount < 0) throw new ArgumentException("Negative pCount values are not valid");
			if (pSequence.Count() != pCount) throw pException ?? new AssertionException("Expected {0} elements in sequence", pCount);
		}

		/// <summary>
		/// Asserts that a sequence has a specified number of a specific element.
		/// </summary>
		public static void Count<TValue>(IEnumerable<TValue> pSequence, int pCount, TValue pValue, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (pCount < 0) throw new ArgumentException("Negative pCount values are not valid");
			if (pSequence.Count(e => IsEqual(e, pValue)) != pCount) throw pException ?? new AssertionException("Expected {0} elements in sequence", pCount);
		}

		/// <summary>
		/// Asserts that a sequence has a specified number of a specific element with a specific comparer.
		/// </summary>
		public static void Count<TValue>(IEnumerable<TValue> pSequence, int pCount, TValue pValue, IEqualityComparer<TValue> pComparer, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pComparer == null) throw new ArgumentNullException("pComparer");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (pCount < 0) throw new ArgumentException("Negative pCount values are not valid");
			if (pSequence.Count(e => IsEqual(e, pValue, pComparer)) != pCount) throw pException ?? new AssertionException("Expected {0} elements in sequence", pCount);
		}

		/// <summary>
		/// Asserts that a sequence contains a specific value.
		/// </summary>
		public static void Contains<TValue>(IEnumerable<TValue> pSequence, TValue pValue, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (!pSequence.Contains(pValue)) throw pException ?? new AssertionException("Expected value is missing");
		}

		/// <summary>
		/// Asserts that a sequence contains a specific value with a specific comparer.
		/// </summary>
		public static void Contains<TValue>(IEnumerable<TValue> pSequence, TValue pValue, IEqualityComparer<TValue> pComparer, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pComparer == null) throw new ArgumentNullException("pComparer");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (!pSequence.Contains(pValue, pComparer)) throw pException ?? new AssertionException("Expected value is missing");
		}

		/// <summary>
		/// Asserts that a sequence does not contain a specific value.
		/// </summary>
		public static void DoesNotContain<TValue>(IEnumerable<TValue> pSequence, TValue pValue, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (pSequence.Contains(pValue)) throw pException ?? new AssertionException("Expected value is present");
		}

		/// <summary>
		/// Asserts that a sequence does not contain a specific value with a specific comparer.
		/// </summary>
		public static void DoesNotContain<TValue>(IEnumerable<TValue> pSequence, TValue pValue, IEqualityComparer<TValue> pComparer, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pComparer == null) throw new ArgumentNullException("pComparer");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (pSequence.Contains(pValue, pComparer)) throw pException ?? new AssertionException("Expected value is present");
		}

		/// <summary>
		/// Asserts that an entire sequence with two or more elements has no duplicates that match the predicate.
		/// </summary>
		public static void Unique<TValue>(IEnumerable<TValue> pSequence, Func<TValue, TValue, bool> pPredicate, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pPredicate == null) throw new ArgumentNullException("pPredicate");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (pSequence.Count() == 1) throw new ArgumentException("Sequence cannot be assessed for duplicates if there is only a single element");
			if (pSequence.Distinct(new EqualityComparerStateful<TValue>(pPredicate)).Count() != pSequence.Count()) throw pException ?? new AssertionException("Expected all elements to be unique based on the predicate");
		}

		/// <summary>
		/// Asserts that an entire sequence with two or more elements has no duplicates that match the predicate.
		/// </summary>
		public static void Unique<TValue, TCompare>(IEnumerable<TValue> pSequence, Func<TValue, TCompare> pPredicate, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pPredicate == null) throw new ArgumentNullException("pPredicate");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (pSequence.Count() == 1) throw new ArgumentException("Sequence cannot be assessed for duplicates if there is only a single element");
			if (pSequence.Distinct(new EqualityComparerStateless<TValue, TCompare>(pPredicate)).Count() != pSequence.Count()) throw pException ?? new AssertionException("Expected all elements to be unique based on the predicate");
		}

		/// <summary>
		/// Asserts that an entire sequence with two or more elements has no duplicates.
		/// </summary>
		public static void Unique<TValue>(IEnumerable<TValue> pSequence, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (pSequence.Count() == 1) throw new ArgumentException("Sequence cannot be assessed for duplicates if there is only a single element");
			if (pSequence.Distinct().Count() != pSequence.Count()) throw pException ?? new AssertionException("Expected all elements to be unique");
		}

		/// <summary>
		/// Asserts that an entire sequence with two or more elements has no duplicates based on a specific comparer.
		/// </summary>
		public static void Unique<TValue>(IEnumerable<TValue> pSequence, IEqualityComparer<TValue> pComparer, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pComparer == null) throw new ArgumentNullException("pComparer");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (pSequence.Count() == 1) throw new ArgumentException("Sequence cannot be assessed for duplicates if there is only a single element");
			if (pSequence.Distinct(pComparer).Count() != pSequence.Count()) throw pException ?? new AssertionException("Expected all elements to be unique");
		}

		/// <summary>
		/// Asserts that an entire sequence with two or more elements has at least one duplicate pair that matches the predicate.
		/// </summary>
		public static void NotUnique<TValue>(IEnumerable<TValue> pSequence, Func<TValue, TValue, bool> pPredicate, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pPredicate == null) throw new ArgumentNullException("pPredicate");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (pSequence.Count() == 1) throw new ArgumentException("Sequence cannot be assessed for duplicates if there is only a single element");
			if (pSequence.Distinct(new EqualityComparerStateful<TValue>(pPredicate)).Count() == pSequence.Count()) throw pException ?? new AssertionException("Expected at least one duplicate pair of elements based on the predicate");
		}

		/// <summary>
		/// Asserts that an entire sequence with two or more elements has at least one duplicate pair that matches the predicate.
		/// </summary>
		public static void NotUnique<TValue, TCompare>(IEnumerable<TValue> pSequence, Func<TValue, TCompare> pPredicate, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pPredicate == null) throw new ArgumentNullException("pPredicate");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (pSequence.Count() == 1) throw new ArgumentException("Sequence cannot be assessed for duplicates if there is only a single element");
			if (pSequence.Distinct(new EqualityComparerStateless<TValue, TCompare>(pPredicate)).Count() == pSequence.Count()) throw pException ?? new AssertionException("Expected at least one duplicate pair of elements based on the predicate");
		}

		/// <summary>
		/// Asserts that an entire sequence with two or more elements has at least one duplicate pair.
		/// </summary>
		public static void NotUnique<TValue>(IEnumerable<TValue> pSequence, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (pSequence.Count() == 1) throw new ArgumentException("Sequence cannot be assessed for duplicates if there is only a single element");
			if (pSequence.Distinct().Count() == pSequence.Count()) throw pException ?? new AssertionException("Expected at least one duplicate pair of elements");
		}

		/// <summary>
		/// Asserts that an entire sequence with two or more elements has at least one duplicate pair based on a specific comparer.
		/// </summary>
		public static void NotUnique<TValue>(IEnumerable<TValue> pSequence, IEqualityComparer<TValue> pComparer, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pComparer == null) throw new ArgumentNullException("pComparer");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (pSequence.Count() == 1) throw new ArgumentException("Sequence cannot be assessed for duplicates if there is only a single element");
			if (pSequence.Distinct(pComparer).Count() == pSequence.Count()) throw pException ?? new AssertionException("Expected at least one duplicate pair of elements");
		}

		/// <summary>
		/// Ensures that a sequence starts with a particular set of values.
		/// </summary>
		public static void StartsWith<TValue>(IEnumerable<TValue> pSequence, IEnumerable<TValue> pStart, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pStart == null) throw new ArgumentNullException("pStart");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (!pStart.Any()) throw new ArgumentException("Expected elements", "pStart");
			if (pStart.Count() > pSequence.Count()) throw new ArgumentException("pStart is larger than pSequence", "pStart");

			var seqIter = pSequence.GetEnumerator();
			var startIter = pStart.GetEnumerator();
			while (seqIter.MoveNext() && startIter.MoveNext()) {
				if (!IsEqual(seqIter.Current, startIter.Current)) {
					throw pException ?? new AssertionException("Expected pSequence to begin with the elements of pStart");
				}
			}
		}

		/// <summary>
		/// Ensures that a sequence starts with a particular set of values based on a specific comparer.
		/// </summary>
		public static void StartsWith<TValue>(IEnumerable<TValue> pSequence, IEnumerable<TValue> pStart, IEqualityComparer<TValue> pComparer, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pStart == null) throw new ArgumentNullException("pStart");
			if (pComparer == null) throw new ArgumentNullException("pComparer");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (!pStart.Any()) throw new ArgumentException("Expected elements", "pStart");
			if (pStart.Count() > pSequence.Count()) throw new ArgumentException("pStart is larger than pSequence", "pStart");

			var seqIter = pSequence.GetEnumerator();
			var startIter = pStart.GetEnumerator();
			while (seqIter.MoveNext() && startIter.MoveNext()) {
				if (!IsEqual(seqIter.Current, startIter.Current, pComparer)) {
					throw pException ?? new AssertionException("Expected pSequence to begin with the elements of pStart");
				}
			}
		}

		/// <summary>
		/// Ensures that a sequence ends with a particular set of values.
		/// </summary>
		public static void EndsWith<TValue>(IEnumerable<TValue> pSequence, IEnumerable<TValue> pEnd, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pEnd == null) throw new ArgumentNullException("pEnd");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (!pEnd.Any()) throw new ArgumentException("Expected elements", "pEnd");
			if (pEnd.Count() > pSequence.Count()) throw new ArgumentException("pEnd is larger than pSequence", "pEnd");

			pSequence = pSequence.Reverse();
			pEnd = pEnd.Reverse();

			var seqIter = pSequence.GetEnumerator();
			var endIter = pEnd.GetEnumerator();
			while (seqIter.MoveNext() && endIter.MoveNext()) {
				if (!IsEqual(seqIter.Current, endIter.Current)) {
					throw pException ?? new AssertionException("Expected pSequence to end with the elements of pEnd");
				}
			}
		}

		/// <summary>
		/// Ensures that a sequence ends with a particular set of values based on a specific comparer.
		/// </summary>
		public static void EndsWith<TValue>(IEnumerable<TValue> pSequence, IEnumerable<TValue> pEnd, IEqualityComparer<TValue> pComparer, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pEnd == null) throw new ArgumentNullException("pEnd");
			if (pComparer == null) throw new ArgumentNullException("pComparer");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (!pEnd.Any()) throw new ArgumentException("Expected elements", "pEnd");
			if (pEnd.Count() > pSequence.Count()) throw new ArgumentException("pEnd is larger than pSequence", "pEnd");

			pSequence = pSequence.Reverse();
			pEnd = pEnd.Reverse();

			var seqIter = pSequence.GetEnumerator();
			var endIter = pEnd.GetEnumerator();
			while (seqIter.MoveNext() && endIter.MoveNext()) {
				if (!IsEqual(seqIter.Current, endIter.Current, pComparer)) {
					throw pException ?? new AssertionException("Expected pSequence to end with the elements of pEnd");
				}
			}
		}

		/// <summary>
		/// Ensures that a sequence contains a particular set of values.
		/// </summary>
		public static void IsSubsetOf<TValue>(IEnumerable<TValue> pSequence, IEnumerable<TValue> pValues, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pValues == null) throw new ArgumentNullException("pValues");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (!pValues.Any()) throw new ArgumentException("Expected elements", "pValues");
			if (pValues.Count() > pSequence.Count()) throw new ArgumentException("pValues is larger than pSequence and therefore cannot be a subset", "pValues");

			if (pValues.Intersect(pSequence).Count() != pValues.Count()) {
				throw pException ?? new AssertionException("Expected all of pValues to be found within pSequence");
			}
		}

		/// <summary>
		/// Ensures that a sequence contains a particular set of values with a specific comparer.
		/// </summary>
		public static void IsSubsetOf<TValue>(IEnumerable<TValue> pSequence, IEnumerable<TValue> pValues, IEqualityComparer<TValue> pComparer, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pComparer == null) throw new ArgumentNullException("pComparer");
			if (pValues == null) throw new ArgumentNullException("pValues");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (!pValues.Any()) throw new ArgumentException("Expected elements", "pValues");
			if (pValues.Count() > pSequence.Count()) throw new ArgumentException("pValues is larger than pSequence and therefore cannot be a subset", "pValues");

			if (pValues.Intersect(pSequence, pComparer).Count() != pValues.Count()) {
				throw pException ?? new AssertionException("Expected all of pValues to be found within pSequence");
			}
		}

		/// <summary>
		/// Ensures that a sequence does not contain a specific full set of values.
		/// </summary>
		public static void IsNotSubsetOf<TValue>(IEnumerable<TValue> pSequence, IEnumerable<TValue> pValues, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pValues == null) throw new ArgumentNullException("pValues");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (!pValues.Any()) throw new ArgumentException("Expected elements", "pValues");
			if (pValues.Count() > pSequence.Count()) throw new ArgumentException("pValues is larger than pSequence and therefore cannot be a subset", "pValues");

			if (pValues.Intersect(pSequence).Count() == pValues.Count()) {
				throw pException ?? new AssertionException("pValues was a subset of pSequence");
			}
		}

		/// <summary>
		/// Ensures that a sequence does not contain a specific full set of values based on a specific comparer.
		/// </summary>
		public static void IsNotSubsetOf<TValue>(IEnumerable<TValue> pSequence, IEnumerable<TValue> pValues, IEqualityComparer<TValue> pComparer, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pValues == null) throw new ArgumentNullException("pValues");
			if (pComparer == null) throw new ArgumentNullException("pComparer");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (!pValues.Any()) throw new ArgumentException("Expected elements", "pValues");
			if (pValues.Count() > pSequence.Count()) throw new ArgumentException("pValues is larger than pSequence and therefore cannot be a subset", "pValues");

			if (pValues.Intersect(pSequence, pComparer).Count() == pValues.Count()) {
				throw pException ?? new AssertionException("pValues was a subset of pSequence");
			}
		}

		/// <summary>
		/// Ensures that a sequence strictly contains a particular set of values.
		/// </summary>
		public static void IsStrictSubsetOf<TValue>(IEnumerable<TValue> pSequence, IEnumerable<TValue> pValues, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pValues == null) throw new ArgumentNullException("pValues");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (!pValues.Any()) throw new ArgumentException("Expected elements", "pValues");
			if (pValues.Count() >= pSequence.Count()) throw new ArgumentException("pValues is larger than or equal to pSequence and therefore cannot be a strict subset", "pValues");

			if (pValues.Intersect(pSequence).Count() != pValues.Count()) {
				throw pException ?? new AssertionException("Expected all of pValues to be found within pSequence");
			}
		}

		/// <summary>
		/// Ensures that a sequence strictly contains a particular set of values based on a specific comparer.
		/// </summary>
		public static void IsStrictSubsetOf<TValue>(IEnumerable<TValue> pSequence, IEnumerable<TValue> pValues, IEqualityComparer<TValue> pComparer, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pComparer == null) throw new ArgumentNullException("pComparer");
			if (pValues == null) throw new ArgumentNullException("pValues");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (!pValues.Any()) throw new ArgumentException("Expected elements", "pValues");
			if (pValues.Count() >= pSequence.Count()) throw new ArgumentException("pValues is larger than or equal to pSequence and therefore cannot be a strict subset", "pValues");

			if (pValues.Intersect(pSequence, pComparer).Count() != pValues.Count()) {
				throw pException ?? new AssertionException("Expected all of pValues to be found within pSequence");
			}
		}

		/// <summary>
		/// Ensures that a sequence does not strictly contain a specific full set of values.
		/// </summary>
		public static void IsNotStrictSubsetOf<TValue>(IEnumerable<TValue> pSequence, IEnumerable<TValue> pValues, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pValues == null) throw new ArgumentNullException("pValues");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (!pValues.Any()) throw new ArgumentException("Expected elements", "pValues");
			if (pValues.Count() >= pSequence.Count()) throw new ArgumentException("pValues is larger than or equal to pSequence and therefore cannot be a strict subset", "pValues");

			if (pValues.Intersect(pSequence).Count() == pValues.Count()) {
				throw pException ?? new AssertionException("pValues was a strict subset of pSequence");
			}
		}

		/// <summary>
		/// Ensures that a sequence does not strictly contain a specific full set of values based on a specific comparer.
		/// </summary>
		public static void IsNotStrictSubsetOf<TValue>(IEnumerable<TValue> pSequence, IEnumerable<TValue> pValues, IEqualityComparer<TValue> pComparer, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pComparer == null) throw new ArgumentNullException("pComparer");
			if (pValues == null) throw new ArgumentNullException("pValues");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (!pValues.Any()) throw new ArgumentException("Expected elements", "pValues");
			if (pValues.Count() >= pSequence.Count()) throw new ArgumentException("pValues is larger than or equal to pSequence and therefore cannot be a strict subset", "pValues");

			if (pValues.Intersect(pSequence, pComparer).Count() == pValues.Count()) {
				throw pException ?? new AssertionException("pValues was a strict subset of pSequence");
			}
		}
	}
}
