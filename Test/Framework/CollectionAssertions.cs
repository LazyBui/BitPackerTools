using System;
using System.Collections.Generic;
using System.Linq;

namespace Test {
	internal partial class Assert {
		/// <summary>
		/// Asserts that sequence has no elements.
		/// </summary>
		public static void Empty<TValue>(IEnumerable<TValue> sequence, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (sequence.Any()) throw exception ?? new AssertionException("Expected no elements");
		}

		/// <summary>
		/// Asserts that sequence has one or more elements.
		/// </summary>
		public static void NotEmpty<TValue>(IEnumerable<TValue> sequence, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (!sequence.Any()) throw exception ?? new AssertionException("Expected elements");
		}

		/// <summary>
		/// Asserts that an entire sequence with one or more elements matches the predicate.
		/// </summary>
		public static void All<TValue>(IEnumerable<TValue> sequence, Func<TValue, bool> predicate, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (!sequence.All(predicate)) throw exception ?? new AssertionException("Expected all elements to match predicate");
		}

		/// <summary>
		/// Asserts that one or more elements of a sequence with one or more elements matches the predicate.
		/// </summary>
		public static void Any<TValue>(IEnumerable<TValue> sequence, Func<TValue, bool> predicate, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (!sequence.Any(predicate)) throw exception ?? new AssertionException("Expected at least 1 element to match predicate");
		}

		/// <summary>
		/// Asserts that exactly one element of a sequence with one or more elements matches the predicate.
		/// </summary>
		public static void Only<TValue>(IEnumerable<TValue> sequence, Func<TValue, bool> predicate, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (sequence.Count(predicate) != 1) throw exception ?? new AssertionException("Expected only 1 element to match predicate");
		}

		/// <summary>
		/// Asserts that exactly a specified number of elements of a sequence with one or more elements matches the predicate.
		/// </summary>
		public static void Exactly<TValue>(IEnumerable<TValue> sequence, int count, Func<TValue, bool> predicate, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (count < 0) throw new ArgumentException("Must be non-negative", nameof(count));
			if (sequence.Count(predicate) != count) throw exception ?? new AssertionException("Expected {0} elements to match predicate", count);
		}

		/// <summary>
		/// Asserts that no elements of a sequence with one or more elements matches the predicate.
		/// </summary>
		public static void None<TValue>(IEnumerable<TValue> sequence, Func<TValue, bool> predicate, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (sequence.Any(predicate)) throw exception ?? new AssertionException("Expected no elements to match predicate");
		}

		/// <summary>
		/// Asserts that a sequence has a specified number of elements.
		/// </summary>
		public static void Count<TValue>(IEnumerable<TValue> sequence, int count, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (count < 0) throw new ArgumentException("Must be non-negative", nameof(count));
			if (sequence.Count() != count) throw exception ?? new AssertionException("Expected {0} elements in sequence", count);
		}

		/// <summary>
		/// Asserts that a sequence has a specified number of a specific element.
		/// </summary>
		public static void Count<TValue>(IEnumerable<TValue> sequence, int count, TValue value, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (count < 0) throw new ArgumentException("Must be non-negative", nameof(count));
			if (sequence.Count(e => IsEqual(e, value)) != count) throw exception ?? new AssertionException("Expected {0} elements in sequence", count);
		}

		/// <summary>
		/// Asserts that a sequence has a specified number of a specific element with a specific comparer.
		/// </summary>
		public static void Count<TValue>(IEnumerable<TValue> sequence, int count, TValue value, IEqualityComparer<TValue> comparer, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (comparer == null) throw new ArgumentNullException(nameof(comparer));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (count < 0) throw new ArgumentException("Must be non-negative", nameof(count));
			if (sequence.Count(e => IsEqual(e, value, comparer)) != count) throw exception ?? new AssertionException("Expected {0} elements in sequence", count);
		}

		/// <summary>
		/// Asserts that a sequence contains a specific value.
		/// </summary>
		public static void Contains<TValue>(IEnumerable<TValue> sequence, TValue value, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (!sequence.Contains(value)) throw exception ?? new AssertionException("Expected value is missing");
		}

		/// <summary>
		/// Asserts that a sequence contains a specific value with a specific comparer.
		/// </summary>
		public static void Contains<TValue>(IEnumerable<TValue> sequence, TValue value, IEqualityComparer<TValue> comparer, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (comparer == null) throw new ArgumentNullException(nameof(comparer));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (!sequence.Contains(value, comparer)) throw exception ?? new AssertionException("Expected value is missing");
		}

		/// <summary>
		/// Asserts that a sequence does not contain a specific value.
		/// </summary>
		public static void DoesNotContain<TValue>(IEnumerable<TValue> sequence, TValue value, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (sequence.Contains(value)) throw exception ?? new AssertionException("Expected value is present");
		}

		/// <summary>
		/// Asserts that a sequence does not contain a specific value with a specific comparer.
		/// </summary>
		public static void DoesNotContain<TValue>(IEnumerable<TValue> sequence, TValue value, IEqualityComparer<TValue> comparer, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (comparer == null) throw new ArgumentNullException(nameof(comparer));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (sequence.Contains(value, comparer)) throw exception ?? new AssertionException("Expected value is present");
		}

		/// <summary>
		/// Asserts that an entire sequence with two or more elements has no duplicates that match the predicate.
		/// </summary>
		public static void Unique<TValue>(IEnumerable<TValue> sequence, Func<TValue, TValue, bool> predicate, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (sequence.Count() == 1) throw new ArgumentException("Sequence cannot be assessed for duplicates if there is only a single element");
			if (sequence.Distinct(new EqualityComparerStateful<TValue>(predicate)).Count() != sequence.Count()) throw exception ?? new AssertionException("Expected all elements to be unique based on the predicate");
		}

		/// <summary>
		/// Asserts that an entire sequence with two or more elements has no duplicates that match the predicate.
		/// </summary>
		public static void Unique<TValue, TCompare>(IEnumerable<TValue> sequence, Func<TValue, TCompare> predicate, AssertionException exception = null) where TCompare : IEquatable<TCompare> {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (sequence.Count() == 1) throw new ArgumentException("Sequence cannot be assessed for duplicates if there is only a single element");
			if (sequence.Distinct(new EqualityComparerStateless<TValue, TCompare>(predicate)).Count() != sequence.Count()) throw exception ?? new AssertionException("Expected all elements to be unique based on the predicate");
		}

		/// <summary>
		/// Asserts that an entire sequence with two or more elements has no duplicates.
		/// </summary>
		public static void Unique<TValue>(IEnumerable<TValue> sequence, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (sequence.Count() == 1) throw new ArgumentException("Sequence cannot be assessed for duplicates if there is only a single element");
			if (sequence.Distinct().Count() != sequence.Count()) throw exception ?? new AssertionException("Expected all elements to be unique");
		}

		/// <summary>
		/// Asserts that an entire sequence with two or more elements has no duplicates based on a specific comparer.
		/// </summary>
		public static void Unique<TValue>(IEnumerable<TValue> sequence, IEqualityComparer<TValue> comparer, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (comparer == null) throw new ArgumentNullException(nameof(comparer));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (sequence.Count() == 1) throw new ArgumentException("Sequence cannot be assessed for duplicates if there is only a single element");
			if (sequence.Distinct(comparer).Count() != sequence.Count()) throw exception ?? new AssertionException("Expected all elements to be unique");
		}

		/// <summary>
		/// Asserts that an entire sequence with two or more elements has at least one duplicate pair that matches the predicate.
		/// </summary>
		public static void NotUnique<TValue>(IEnumerable<TValue> sequence, Func<TValue, TValue, bool> predicate, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (sequence.Count() == 1) throw new ArgumentException("Sequence cannot be assessed for duplicates if there is only a single element");
			if (sequence.Distinct(new EqualityComparerStateful<TValue>(predicate)).Count() == sequence.Count()) throw exception ?? new AssertionException("Expected at least one duplicate pair of elements based on the predicate");
		}

		/// <summary>
		/// Asserts that an entire sequence with two or more elements has at least one duplicate pair that matches the predicate.
		/// </summary>
		public static void NotUnique<TValue, TCompare>(IEnumerable<TValue> sequence, Func<TValue, TCompare> predicate, AssertionException exception = null) where TCompare : IEquatable<TCompare> {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (sequence.Count() == 1) throw new ArgumentException("Sequence cannot be assessed for duplicates if there is only a single element");
			if (sequence.Distinct(new EqualityComparerStateless<TValue, TCompare>(predicate)).Count() == sequence.Count()) throw exception ?? new AssertionException("Expected at least one duplicate pair of elements based on the predicate");
		}

		/// <summary>
		/// Asserts that an entire sequence with two or more elements has at least one duplicate pair.
		/// </summary>
		public static void NotUnique<TValue>(IEnumerable<TValue> sequence, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (sequence.Count() == 1) throw new ArgumentException("Sequence cannot be assessed for duplicates if there is only a single element");
			if (sequence.Distinct().Count() == sequence.Count()) throw exception ?? new AssertionException("Expected at least one duplicate pair of elements");
		}

		/// <summary>
		/// Asserts that an entire sequence with two or more elements has at least one duplicate pair based on a specific comparer.
		/// </summary>
		public static void NotUnique<TValue>(IEnumerable<TValue> sequence, IEqualityComparer<TValue> comparer, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (comparer == null) throw new ArgumentNullException(nameof(comparer));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (sequence.Count() == 1) throw new ArgumentException("Sequence cannot be assessed for duplicates if there is only a single element");
			if (sequence.Distinct(comparer).Count() == sequence.Count()) throw exception ?? new AssertionException("Expected at least one duplicate pair of elements");
		}

		/// <summary>
		/// Ensures that a sequence starts with a particular set of values.
		/// </summary>
		public static void StartsWith<TValue>(IEnumerable<TValue> sequence, IEnumerable<TValue> start, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (start == null) throw new ArgumentNullException(nameof(start));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (!start.Any()) throw new ArgumentException("Expected elements", nameof(start));
			if (start.Count() > sequence.Count()) throw new ArgumentException("Specified start sequence is larger than test sequence", nameof(start));

			var seqIter = sequence.GetEnumerator();
			var startIter = start.GetEnumerator();
			while (seqIter.MoveNext() && startIter.MoveNext()) {
				if (!IsEqual(seqIter.Current, startIter.Current)) {
					throw exception ?? new AssertionException("Expected test sequence to begin with the elements of start sequence");
				}
			}
		}

		/// <summary>
		/// Ensures that a sequence starts with a particular set of values based on a specific comparer.
		/// </summary>
		public static void StartsWith<TValue>(IEnumerable<TValue> sequence, IEnumerable<TValue> start, IEqualityComparer<TValue> comparer, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (start == null) throw new ArgumentNullException(nameof(start));
			if (comparer == null) throw new ArgumentNullException(nameof(comparer));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (!start.Any()) throw new ArgumentException("Expected elements", nameof(start));
			if (start.Count() > sequence.Count()) throw new ArgumentException("Specified start sequence is larger than test sequence", nameof(start));

			var seqIter = sequence.GetEnumerator();
			var startIter = start.GetEnumerator();
			while (seqIter.MoveNext() && startIter.MoveNext()) {
				if (!IsEqual(seqIter.Current, startIter.Current, comparer)) {
					throw exception ?? new AssertionException("Expected test sequence to begin with the elements of start sequence");
				}
			}
		}

		/// <summary>
		/// Ensures that a sequence ends with a particular set of values.
		/// </summary>
		public static void EndsWith<TValue>(IEnumerable<TValue> sequence, IEnumerable<TValue> end, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (end == null) throw new ArgumentNullException(nameof(end));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (!end.Any()) throw new ArgumentException("Expected elements", nameof(end));
			if (end.Count() > sequence.Count()) throw new ArgumentException("Specified end sequence is larger than test sequence", nameof(end));

			sequence = sequence.Reverse();
			end = end.Reverse();

			var seqIter = sequence.GetEnumerator();
			var endIter = end.GetEnumerator();
			while (seqIter.MoveNext() && endIter.MoveNext()) {
				if (!IsEqual(seqIter.Current, endIter.Current)) {
					throw exception ?? new AssertionException("Expected test sequence to end with the elements of end sequence");
				}
			}
		}

		/// <summary>
		/// Ensures that a sequence ends with a particular set of values based on a specific comparer.
		/// </summary>
		public static void EndsWith<TValue>(IEnumerable<TValue> sequence, IEnumerable<TValue> end, IEqualityComparer<TValue> comparer, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (end == null) throw new ArgumentNullException(nameof(end));
			if (comparer == null) throw new ArgumentNullException(nameof(comparer));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (!end.Any()) throw new ArgumentException("Expected elements", nameof(end));
			if (end.Count() > sequence.Count()) throw new ArgumentException("Specified end sequence is larger than test sequence", nameof(end));

			sequence = sequence.Reverse();
			end = end.Reverse();

			var seqIter = sequence.GetEnumerator();
			var endIter = end.GetEnumerator();
			while (seqIter.MoveNext() && endIter.MoveNext()) {
				if (!IsEqual(seqIter.Current, endIter.Current, comparer)) {
					throw exception ?? new AssertionException("Expected test sequence to end with the elements of end sequence");
				}
			}
		}

		/// <summary>
		/// Ensures that a sequence contains a particular set of values.
		/// </summary>
		public static void IsSubsetOf<TValue>(IEnumerable<TValue> sequence, IEnumerable<TValue> values, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (values == null) throw new ArgumentNullException(nameof(values));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (!values.Any()) throw new ArgumentException("Expected elements", nameof(values));
			if (values.Count() > sequence.Count()) throw new ArgumentException("Specified set of values is larger than sequence and therefore cannot be a subset", nameof(values));

			if (values.Intersect(sequence).Count() != values.Count()) {
				throw exception ?? new AssertionException("Expected all of specified values to be found within sequence");
			}
		}

		/// <summary>
		/// Ensures that a sequence contains a particular set of values with a specific comparer.
		/// </summary>
		public static void IsSubsetOf<TValue>(IEnumerable<TValue> sequence, IEnumerable<TValue> values, IEqualityComparer<TValue> comparer, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (comparer == null) throw new ArgumentNullException(nameof(comparer));
			if (values == null) throw new ArgumentNullException(nameof(values));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (!values.Any()) throw new ArgumentException("Expected elements", nameof(values));
			if (values.Count() > sequence.Count()) throw new ArgumentException("Specified set of values is larger than sequence and therefore cannot be a subset", nameof(values));

			if (values.Intersect(sequence, comparer).Count() != values.Count()) {
				throw exception ?? new AssertionException("Expected all of specified values to be found within sequence");
			}
		}

		/// <summary>
		/// Ensures that a sequence does not contain a specific full set of values.
		/// </summary>
		public static void IsNotSubsetOf<TValue>(IEnumerable<TValue> sequence, IEnumerable<TValue> values, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (values == null) throw new ArgumentNullException(nameof(values));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (!values.Any()) throw new ArgumentException("Expected elements", nameof(values));
			if (values.Count() > sequence.Count()) throw new ArgumentException("Specified set of values is larger than sequence and therefore cannot be a subset", nameof(values));

			if (values.Intersect(sequence).Count() == values.Count()) {
				throw exception ?? new AssertionException("Values were a subset of sequence");
			}
		}

		/// <summary>
		/// Ensures that a sequence does not contain a specific full set of values based on a specific comparer.
		/// </summary>
		public static void IsNotSubsetOf<TValue>(IEnumerable<TValue> sequence, IEnumerable<TValue> values, IEqualityComparer<TValue> comparer, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (values == null) throw new ArgumentNullException(nameof(values));
			if (comparer == null) throw new ArgumentNullException(nameof(comparer));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (!values.Any()) throw new ArgumentException("Expected elements", nameof(values));
			if (values.Count() > sequence.Count()) throw new ArgumentException("Specified set of values is larger than sequence and therefore cannot be a subset", nameof(values));

			if (values.Intersect(sequence, comparer).Count() == values.Count()) {
				throw exception ?? new AssertionException("Values were a subset of sequence");
			}
		}

		/// <summary>
		/// Ensures that a sequence strictly contains a particular set of values.
		/// </summary>
		public static void IsStrictSubsetOf<TValue>(IEnumerable<TValue> sequence, IEnumerable<TValue> values, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (values == null) throw new ArgumentNullException(nameof(values));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (!values.Any()) throw new ArgumentException("Expected elements", nameof(values));
			if (values.Count() >= sequence.Count()) throw new ArgumentException("Specified set of values is larger than or equal to the specified sequence and therefore cannot be a strict subset", nameof(values));

			if (values.Intersect(sequence).Count() != values.Count()) {
				throw exception ?? new AssertionException("Expected all of specified values to be found within sequence");
			}
		}

		/// <summary>
		/// Ensures that a sequence strictly contains a particular set of values based on a specific comparer.
		/// </summary>
		public static void IsStrictSubsetOf<TValue>(IEnumerable<TValue> sequence, IEnumerable<TValue> values, IEqualityComparer<TValue> comparer, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (comparer == null) throw new ArgumentNullException(nameof(comparer));
			if (values == null) throw new ArgumentNullException(nameof(values));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (!values.Any()) throw new ArgumentException("Expected elements", nameof(values));
			if (values.Count() >= sequence.Count()) throw new ArgumentException("Specified set of values is larger than or equal to the specified sequence and therefore cannot be a strict subset", nameof(values));

			if (values.Intersect(sequence, comparer).Count() != values.Count()) {
				throw exception ?? new AssertionException("Expected all of specified values to be found within sequence");
			}
		}

		/// <summary>
		/// Ensures that a sequence does not strictly contain a specific full set of values.
		/// </summary>
		public static void IsNotStrictSubsetOf<TValue>(IEnumerable<TValue> sequence, IEnumerable<TValue> values, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (values == null) throw new ArgumentNullException(nameof(values));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (!values.Any()) throw new ArgumentException("Expected elements", nameof(values));
			if (values.Count() >= sequence.Count()) throw new ArgumentException("Specified set of values is larger than or equal to the specified sequence and therefore cannot be a strict subset", nameof(values));

			if (values.Intersect(sequence).Count() == values.Count()) {
				throw exception ?? new AssertionException("Specified values were a strict subset of sequence");
			}
		}

		/// <summary>
		/// Ensures that a sequence does not strictly contain a specific full set of values based on a specific comparer.
		/// </summary>
		public static void IsNotStrictSubsetOf<TValue>(IEnumerable<TValue> sequence, IEnumerable<TValue> values, IEqualityComparer<TValue> comparer, AssertionException exception = null) {
			if (sequence == null) throw new ArgumentNullException(nameof(sequence));
			if (comparer == null) throw new ArgumentNullException(nameof(comparer));
			if (values == null) throw new ArgumentNullException(nameof(values));
			if (!sequence.Any()) throw new ArgumentException("Expected elements", nameof(sequence));
			if (!values.Any()) throw new ArgumentException("Expected elements", nameof(values));
			if (values.Count() >= sequence.Count()) throw new ArgumentException("Specified set of values is larger than or equal to the specified sequence and therefore cannot be a strict subset", nameof(values));

			if (values.Intersect(sequence, comparer).Count() == values.Count()) {
				throw exception ?? new AssertionException("Specified values were a strict subset of sequence");
			}
		}
	}
}
