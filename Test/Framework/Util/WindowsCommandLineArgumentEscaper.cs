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
		/// <param name="pRawArguments">A collection of objects to be used to compose a Windows command line argument string.</param>
		/// <returns>A usable Windows command line argument string.</returns>
		public string Escape(IEnumerable<object> pRawArguments) {
			return string.Join(" ",
				pRawArguments.Select(a => EscapeArgument(a.ToString())));
		}

		private string EscapeArgument(string pRawArgument) {
			string output;
			output = Regex.Replace(pRawArgument, @"(\\*)" + "\"", @"$1$1\" + "\"");
			output = Regex.Replace(output, @"(\\+)$", @"$1$1");
			return Quote + output + Quote;
		}
	}
}
