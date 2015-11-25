using System;
using System.Collections.Generic;

namespace Test {
	/// <summary>
	/// Represents a conversion from a collection of objects to a fully-fledged command line argument string.
	/// </summary>
	internal interface ICommandLineArgumentEscaper {
		/// <summary>
		/// Escapes a collection of objects to a usable command line argument string.
		/// </summary>
		/// <param name="rawArguments">A collection of objects to be used to compose a command line argument string.</param>
		/// <returns>A usable command line argument string.</returns>
		string Escape(IEnumerable<object> rawArguments);
	}
}
