using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Test {
	/// <summary>
	/// Represents a conversion from a collection of objects to a Windows command line argument string.
	/// </summary>
	internal sealed class WindowsCommandLineArgumentEscaper : ICommandLineArgumentEscaper {
		private const string Quote = @"""";

		/// <summary>
		/// Escapes a collection of objects to a usable Windows command line argument string.
		/// </summary>
		/// <param name="rawArguments">A collection of objects to be used to compose a Windows command line argument string.</param>
		/// <returns>A usable Windows command line argument string.</returns>
		public string Escape(IEnumerable<object> rawArguments) {
			return string.Join(" ",
				rawArguments.Select(a => EscapeArgument(a.ToString())));
		}

		private string EscapeArgument(string rawArgument) {
			string output;
			output = Regex.Replace(rawArgument, @"(\\*)" + "\"", @"$1$1\" + "\"");
			output = Regex.Replace(output, @"(\\+)$", @"$1$1");
			return Quote + output + Quote;
		}
	}
}
