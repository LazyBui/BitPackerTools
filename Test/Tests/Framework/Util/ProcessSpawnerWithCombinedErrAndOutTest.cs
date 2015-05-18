using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
	[TestClass]
	public class ProcessSpawnerWithCombinedErrAndOutTest {
		[TestMethod]
		[TestCategory("Framework.Util")]
		public void Constructor() {
			// File only
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(null as string)) { } });
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(null as FileInfo)) { } });
			Assert.ThrowsExact<ArgumentException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(string.Empty)) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.InvalidBinary)) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.InvalidBinaryInfo)) { } });
			Assert.DoesNotThrow(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.SimpleInterspersed)) { } });
			Assert.DoesNotThrow(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.SimpleInterspersedInfo)) { } });

			// File/args only
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(null as string, null as object[])) { } });
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(null as FileInfo, null as object[])) { } });
			Assert.ThrowsExact<ArgumentException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(string.Empty, null as object[])) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.InvalidBinary, null as object[])) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.InvalidBinaryInfo, null as object[])) { } });
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(null as string, "abc")) { } });
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(null as FileInfo, "abc")) { } });
			Assert.ThrowsExact<ArgumentException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(string.Empty, "abc")) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.InvalidBinary, "abc")) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.InvalidBinaryInfo, "abc")) { } });
			Assert.DoesNotThrow(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.SimpleInterspersed, null as object[])) { } });
			Assert.DoesNotThrow(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.SimpleInterspersedInfo, null as object[])) { } });
			Assert.DoesNotThrow(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.SimpleInterspersed, "abc")) { } });
			Assert.DoesNotThrow(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.SimpleInterspersedInfo, "abc")) { } });

			// File/escaper/args
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(null as string, new WindowsCommandLineArgumentEscaper(), null as object[])) { } });
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(null as FileInfo, new WindowsCommandLineArgumentEscaper(), null as object[])) { } });
			Assert.ThrowsExact<ArgumentException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(string.Empty, new WindowsCommandLineArgumentEscaper(), null as object[])) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.InvalidBinary, new WindowsCommandLineArgumentEscaper(), null as object[])) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.InvalidBinaryInfo, new WindowsCommandLineArgumentEscaper(), null as object[])) { } });
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(null as string, null, null as object[])) { } });
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(null as FileInfo, null, null as object[])) { } });
			Assert.ThrowsExact<ArgumentException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(string.Empty, null, null as object[])) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.InvalidBinary, null, null as object[])) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.InvalidBinaryInfo, null, null as object[])) { } });
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(null as string, new WindowsCommandLineArgumentEscaper(), "abc")) { } });
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(null as FileInfo, new WindowsCommandLineArgumentEscaper(), "abc")) { } });
			Assert.ThrowsExact<ArgumentException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(string.Empty, new WindowsCommandLineArgumentEscaper(), "abc")) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.InvalidBinary, new WindowsCommandLineArgumentEscaper(), "abc")) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.InvalidBinaryInfo, new WindowsCommandLineArgumentEscaper(), "abc")) { } });
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(null as string, null, "abc")) { } });
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(null as FileInfo, null, "abc")) { } });
			Assert.ThrowsExact<ArgumentException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(string.Empty, null, "abc")) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.InvalidBinary, null, "abc")) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.InvalidBinaryInfo, null, "abc")) { } });
			Assert.DoesNotThrow(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.SimpleInterspersed, null, null as object[])) { } });
			Assert.DoesNotThrow(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.SimpleInterspersedInfo, null, null as object[])) { } });
			Assert.DoesNotThrow(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.SimpleInterspersed, new WindowsCommandLineArgumentEscaper(), null as object[])) { } });
			Assert.DoesNotThrow(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.SimpleInterspersedInfo, new WindowsCommandLineArgumentEscaper(), null as object[])) { } });
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.SimpleInterspersed, null, "abc")) { } });
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.SimpleInterspersedInfo, null, "abc")) { } });
			Assert.DoesNotThrow(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.SimpleInterspersed, new WindowsCommandLineArgumentEscaper(), "abc")) { } });
			Assert.DoesNotThrow(() => { using (new ProcessSpawnerWithCombinedErrAndOut(TestApplications.SimpleInterspersedInfo, new WindowsCommandLineArgumentEscaper(), "abc")) { } });
		}

		[TestMethod]
		[TestCategory("Framework.Util")]
		public void SimpleInterspersed() {
			ProcessResult result = null;
			Assert.DoesNotThrow(() => {
				using (var process = new ProcessSpawnerWithCombinedErrAndOut(TestApplications.SimpleInterspersedInfo)) {
					result = process.Run();
				}
			});
			Assert.NotNull(result);
			Assert.Equal(result.ExitCode, 3);
			Assert.GreaterThan(result.PeakPagedMemorySize, 0);
			Assert.GreaterThan(result.PeakVirtualMemorySize, 0);
			Assert.GreaterThan(result.PeakWorkingSet, 0);
			Assert.NotEqual(result.StartTime, DateTime.MinValue);
			Assert.NotEqual(result.ExitTime, DateTime.MinValue);
			Assert.GreaterThan(result.ExitTime - result.StartTime, TimeSpan.Zero);
			Assert.NotNull(result.FullBuffer);
			Assert.Null(result.FullOutput);
			Assert.Null(result.FullError);
			Assert.Equal(result.FullBuffer, @"abc
def
abc
def
abc
def
def
abc
aabbccddeeff");
		}

		[TestMethod]
		[TestCategory("Framework.Util")]
		public void BlankLinesInterspersed() {
			ProcessResult result = null;
			Assert.DoesNotThrow(() => {
				using (var process = new ProcessSpawnerWithCombinedErrAndOut(TestApplications.BlankLinesInterspersedInfo)) {
					result = process.Run();
				}
			});
			Assert.NotNull(result);
			Assert.Equal(result.ExitCode, 0);
			Assert.GreaterThan(result.PeakPagedMemorySize, 0);
			Assert.GreaterThan(result.PeakVirtualMemorySize, 0);
			Assert.GreaterThan(result.PeakWorkingSet, 0);
			Assert.NotEqual(result.StartTime, DateTime.MinValue);
			Assert.NotEqual(result.ExitTime, DateTime.MinValue);
			Assert.GreaterThan(result.ExitTime - result.StartTime, TimeSpan.Zero);
			Assert.NotNull(result.FullBuffer);
			Assert.Null(result.FullOutput);
			Assert.Null(result.FullError);
			Assert.Equal(result.FullBuffer, @"abc

def

abc
def

abc
def
def

abc");
		}

		[TestMethod]
		[TestCategory("Framework.Util")]
		public void NoOutput() {
			ProcessResult result = null;
			Assert.DoesNotThrow(() => {
				using (var process = new ProcessSpawnerWithCombinedErrAndOut(TestApplications.NoOutputInfo)) {
					result = process.Run();
				}
			});
			Assert.NotNull(result);
			Assert.Equal(result.ExitCode, 0);
			Assert.GreaterThan(result.PeakPagedMemorySize, 0);
			Assert.GreaterThan(result.PeakVirtualMemorySize, 0);
			Assert.GreaterThan(result.PeakWorkingSet, 0);
			Assert.NotEqual(result.StartTime, DateTime.MinValue);
			Assert.NotEqual(result.ExitTime, DateTime.MinValue);
			Assert.GreaterThan(result.ExitTime - result.StartTime, TimeSpan.Zero);
			Assert.NotNull(result.FullBuffer);
			Assert.Null(result.FullOutput);
			Assert.Null(result.FullError);
			Assert.Equal(result.FullBuffer, @"");
		}

		[TestMethod]
		[TestCategory("Framework.Util")]
		public void NakedInput() {
			ProcessResult result = null;
			Assert.DoesNotThrow(() => {
				using (var process = new ProcessSpawnerWithCombinedErrAndOut(TestApplications.NakedInput)) {
					process.OnInputRequested += (buf, writer) => {
						if (!string.IsNullOrEmpty(buf) && buf != "acb876") {
							Assert.Fail();
						}
						writer.WriteLine("acb876");
						return ProcessInputHandleResult.Handled;
					};

					result = process.Run();
				}
			});
			Assert.NotNull(result);
			Assert.NotNull(result.FullBuffer);
			Assert.Null(result.FullOutput);
			Assert.Null(result.FullError);
			Assert.Equal(result.FullBuffer, @"acb876");
		}

		[TestMethod]
		[TestCategory("Framework.Util")]
		public void PlusTwoNumber() {
			ProcessResult result = null;
			Assert.DoesNotThrow(() => {
				using (var process = new ProcessSpawnerWithCombinedErrAndOut(TestApplications.PlusTwoNumberInfo)) {
					process.OnInputRequested += (buf, writer) => {
						if (string.IsNullOrEmpty(buf)) {
							return ProcessInputHandleResult.Ignored;
						}
						if (buf == "878") {
							return ProcessInputHandleResult.Ignored;
						}
						if (buf != "Please enter a number...") {
							Assert.Fail();
							return ProcessInputHandleResult.Ignored;
						}
						writer.WriteLine("876");
						return ProcessInputHandleResult.Handled;
					};

					result = process.Run();
				}
			});
			Assert.NotNull(result);
			Assert.NotNull(result.FullBuffer);
			Assert.Equal(result.FullBuffer, @"Please enter a number...
878");

			Assert.DoesNotThrow(() => {
				using (var process = new ProcessSpawnerWithCombinedErrAndOut(TestApplications.PlusTwoNumberInfo)) {
					process.OnInputRequested += (buf, writer) => {
						if (string.IsNullOrEmpty(buf)) {
							return ProcessInputHandleResult.Ignored;
						}
						if (buf == "Bad number") {
							return ProcessInputHandleResult.Ignored;
						}
						if (buf != "Please enter a number...") {
							Assert.Fail();
							return ProcessInputHandleResult.Ignored;
						}
						writer.WriteLine("abc");
						return ProcessInputHandleResult.Handled;
					};

					result = process.Run();
				}
			});
			Assert.NotNull(result);
			Assert.NotNull(result.FullBuffer);
			Assert.Equal(result.FullBuffer, @"Please enter a number...
Bad number");
		}
	}
}
