using System;
using System.IO;

namespace Test {
	internal static class TestApplications {
		private const string BinaryDirectory = "../../Tests/Framework/Util/";
		public const string InvalidBinary = "WillNeverExist.exe";
		public static readonly FileInfo InvalidBinaryInfo = new FileInfo(InvalidBinary);

		public const string SimpleInterspersed = BinaryDirectory + "_SimpleInterspersed.exe";
		public static readonly FileInfo SimpleInterspersedInfo = new FileInfo(SimpleInterspersed);
		public static readonly string SimpleInterspersedSource = @"
using System;

namespace CsTestConsole {
	class Program {
		static int Main(string[] args) {
			Console.Error.WriteLine(""abc"");
			Console.Out.WriteLine(""def"");
			Console.Error.WriteLine(""abc"");
			Console.Out.WriteLine(""def"");
			Console.Error.WriteLine(""abc"");
			Console.Out.WriteLine(""def"");
			Console.Out.WriteLine(""def"");
			Console.Error.WriteLine(""abc"");

			foreach (var c in ""abcdef"") {
				Console.Out.Write(c);
				Console.Error.Write(c);
			}

			return 3;
		}
	}
}
";

		public const string BlankLinesInterspersed = BinaryDirectory + "_BlankLinesInterspersed.exe";
		public static readonly FileInfo BlankLinesInterspersedInfo = new FileInfo(BlankLinesInterspersed);
		public static readonly string BlankLinesInterspersedSource = @"
using System;

namespace CsTestConsole {
	class Program {
		static void Main(string[] args) {
			Console.Error.WriteLine(""abc"");
			Console.Error.WriteLine("""");
			Console.Out.WriteLine(""def"");
			Console.Out.WriteLine("""");
			Console.Error.WriteLine(""abc"");
			Console.Out.WriteLine(""def"");
			Console.Out.WriteLine("""");
			Console.Error.WriteLine(""abc"");
			Console.Out.WriteLine(""def"");
			Console.Out.WriteLine(""def"");
			Console.Error.WriteLine("""");
			Console.Error.WriteLine(""abc"");
		}
	}
}
";

		public const string NoOutput = BinaryDirectory + "_NoOutput.exe";
		public static readonly FileInfo NoOutputInfo = new FileInfo(NoOutput);
		public static readonly string NoOutputSource = @"
using System;

namespace CsTestConsole {
	class Program {
		static void Main(string[] args) {

		}
	}
}
";

		public const string NakedInput = BinaryDirectory + "_NakedInput.exe";
		public static readonly FileInfo NakedInputInfo = new FileInfo(NakedInput);
		public static readonly string NakedInputSource = @"
using System;

namespace CsTestConsole {
	class Program {
		static void Main(string[] args) {
			Console.WriteLine(Console.ReadLine());
		}
	}
}
";

		public const string PlusTwoNumber = BinaryDirectory + "_PlusTwoNumber.exe";
		public static readonly FileInfo PlusTwoNumberInfo = new FileInfo(PlusTwoNumber);
		public static readonly string PlusTwoNumberSource = @"
using System;

namespace CsTestConsole {
	class Program {
		static void Main(string[] args) {
			int value;
			Console.WriteLine(""Please enter a number..."");
			if (!int.TryParse(Console.ReadLine(), out value)) {
				Console.Error.WriteLine(""Bad number"");
			}
			else {
				Console.WriteLine(""{0}"", value + 2);
			}
		}
	}
}
";
	}
}
