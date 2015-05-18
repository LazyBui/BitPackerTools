using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
	public partial class AssertTest {
		private const string ExistingFile = "Test.dll";
		private const string NonExistingFile = "abc.txt";
		private const string ExistingDirectory = "../../Framework/";
		private const string NonExistingDirectory = "NotAValidDirectory";
		private static readonly FileInfo ExistingFileInfo = new FileInfo(ExistingFile);
		private static readonly FileInfo NonExistingFileInfo = new FileInfo(NonExistingFile);
		private static readonly DirectoryInfo ExistingDirectoryInfo = new DirectoryInfo(ExistingDirectory);
		private static readonly DirectoryInfo NonExistingDirectoryInfo = new DirectoryInfo(NonExistingDirectory);

		private const string TestDirectory = "../../Tests/Framework/";
		private const string EqualCompareFile1 = TestDirectory + "_eq1.bin";
		private const string EqualCompareFile2 = TestDirectory + "_eq2.bin";
		private const string NotEqualCompareFileLength = TestDirectory + "_difflength.bin";
		private const string NotEqualCompareFileData = TestDirectory + "_diffcontent.bin";
		private static readonly FileInfo EqualCompareFileInfo1 = new FileInfo(EqualCompareFile1);
		private static readonly FileInfo EqualCompareFileInfo2 = new FileInfo(EqualCompareFile2);
		private static readonly FileInfo NotEqualCompareFileLengthInfo = new FileInfo(NotEqualCompareFileLength);
		private static readonly FileInfo NotEqualCompareFileDataInfo = new FileInfo(NotEqualCompareFileData);

		[TestMethod]
		[TestCategory("Framework")]
		public void FileExists() {
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.FileExists(null as string));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.FileExists(null as FileInfo));
			Assert.ThrowsExact<AssertionException>(() => Assert.FileExists(NonExistingFile));
			Assert.ThrowsExact<AssertionException>(() => Assert.FileExists(NonExistingFileInfo));
			Assert.ThrowsExact<AssertionException>(() => Assert.FileExists(new FileInfo(NonExistingFile)));
			Assert.DoesNotThrow(() => Assert.FileExists(ExistingFile));
			Assert.DoesNotThrow(() => Assert.FileExists(ExistingFileInfo));
		}

		[TestMethod]
		[TestCategory("Framework")]
		public void FileNotExists() {
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.FileNotExists(null as string));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.FileNotExists(null as FileInfo));
			Assert.DoesNotThrow(() => Assert.FileNotExists(NonExistingFile));
			Assert.DoesNotThrow(() => Assert.FileNotExists(NonExistingFileInfo));
			Assert.DoesNotThrow(() => Assert.FileNotExists(new FileInfo(NonExistingFile)));
			Assert.ThrowsExact<AssertionException>(() => Assert.FileNotExists(ExistingFile));
			Assert.ThrowsExact<AssertionException>(() => Assert.FileNotExists(ExistingFileInfo));
		}

		[TestMethod]
		[TestCategory("Framework")]
		public void DirectoryExists() {
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.DirectoryExists(null as string));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.DirectoryExists(null as DirectoryInfo));
			Assert.ThrowsExact<AssertionException>(() => Assert.DirectoryExists(NonExistingDirectory));
			Assert.ThrowsExact<AssertionException>(() => Assert.DirectoryExists(NonExistingDirectoryInfo));
			Assert.ThrowsExact<AssertionException>(() => Assert.DirectoryExists(new DirectoryInfo(NonExistingDirectory)));
			Assert.DoesNotThrow(() => Assert.DirectoryExists(ExistingDirectory));
			Assert.DoesNotThrow(() => Assert.DirectoryExists(ExistingDirectoryInfo));
		}

		[TestMethod]
		[TestCategory("Framework")]
		public void DirectoryNotExists() {
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.DirectoryNotExists(null as string));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.DirectoryNotExists(null as DirectoryInfo));
			Assert.DoesNotThrow(() => Assert.DirectoryNotExists(NonExistingDirectory));
			Assert.DoesNotThrow(() => Assert.DirectoryNotExists(NonExistingDirectoryInfo));
			Assert.DoesNotThrow(() => Assert.DirectoryNotExists(new DirectoryInfo(NonExistingDirectory)));
			Assert.ThrowsExact<AssertionException>(() => Assert.DirectoryNotExists(ExistingDirectory));
			Assert.ThrowsExact<AssertionException>(() => Assert.DirectoryNotExists(ExistingDirectoryInfo));
		}

		[TestMethod]
		[TestCategory("Framework")]
		public void FilesEqual() {
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.FilesEqual(null as string, EqualCompareFile2));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.FilesEqual(null as string, EqualCompareFileInfo2));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.FilesEqual(null as FileInfo, EqualCompareFile2));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.FilesEqual(null as FileInfo, EqualCompareFileInfo2));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.FilesEqual(EqualCompareFile1, null as string));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.FilesEqual(EqualCompareFile1, null as FileInfo));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.FilesEqual(EqualCompareFileInfo1, null as string));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.FilesEqual(EqualCompareFileInfo1, null as FileInfo));

			Assert.ThrowsExact<ArgumentException>(() => Assert.FilesEqual(string.Empty, EqualCompareFile2));
			Assert.ThrowsExact<ArgumentException>(() => Assert.FilesEqual(EqualCompareFile1, string.Empty));

			Assert.ThrowsExact<ArgumentException>(() => Assert.FilesEqual(NonExistingFile, ExistingFile));
			Assert.ThrowsExact<ArgumentException>(() => Assert.FilesEqual(NonExistingFileInfo, ExistingFile));
			Assert.ThrowsExact<ArgumentException>(() => Assert.FilesEqual(NonExistingFile, ExistingFileInfo));
			Assert.ThrowsExact<ArgumentException>(() => Assert.FilesEqual(NonExistingFileInfo, ExistingFileInfo));
			Assert.ThrowsExact<ArgumentException>(() => Assert.FilesEqual(ExistingFile, NonExistingFile));
			Assert.ThrowsExact<ArgumentException>(() => Assert.FilesEqual(ExistingFile, NonExistingFileInfo));
			Assert.ThrowsExact<ArgumentException>(() => Assert.FilesEqual(ExistingFileInfo, NonExistingFile));
			Assert.ThrowsExact<ArgumentException>(() => Assert.FilesEqual(ExistingFileInfo, NonExistingFileInfo));

			Assert.DoesNotThrow(() => Assert.FilesEqual(EqualCompareFile1, EqualCompareFile1));
			Assert.DoesNotThrow(() => Assert.FilesEqual(EqualCompareFile1, EqualCompareFileInfo1));
			Assert.DoesNotThrow(() => Assert.FilesEqual(EqualCompareFileInfo1, EqualCompareFile1));
			Assert.DoesNotThrow(() => Assert.FilesEqual(EqualCompareFileInfo1, EqualCompareFileInfo1));

			Assert.DoesNotThrow(() => Assert.FilesEqual(EqualCompareFile1, EqualCompareFile2));
			Assert.DoesNotThrow(() => Assert.FilesEqual(EqualCompareFileInfo1, EqualCompareFileInfo2));
			Assert.DoesNotThrow(() => Assert.FilesEqual(EqualCompareFile1, EqualCompareFileInfo2));
			Assert.DoesNotThrow(() => Assert.FilesEqual(EqualCompareFileInfo1, EqualCompareFile2));

			Assert.ThrowsExact<AssertionException>(() => Assert.FilesEqual(EqualCompareFile1, NotEqualCompareFileLength));
			Assert.ThrowsExact<AssertionException>(() => Assert.FilesEqual(EqualCompareFile1, NotEqualCompareFileLengthInfo));
			Assert.ThrowsExact<AssertionException>(() => Assert.FilesEqual(EqualCompareFileInfo1, NotEqualCompareFileLength));
			Assert.ThrowsExact<AssertionException>(() => Assert.FilesEqual(EqualCompareFileInfo1, NotEqualCompareFileLengthInfo));

			Assert.ThrowsExact<AssertionException>(() => Assert.FilesEqual(EqualCompareFile1, NotEqualCompareFileData));
			Assert.ThrowsExact<AssertionException>(() => Assert.FilesEqual(EqualCompareFile1, NotEqualCompareFileDataInfo));
			Assert.ThrowsExact<AssertionException>(() => Assert.FilesEqual(EqualCompareFileInfo1, NotEqualCompareFileData));
			Assert.ThrowsExact<AssertionException>(() => Assert.FilesEqual(EqualCompareFileInfo1, NotEqualCompareFileDataInfo));
		}

		[TestMethod]
		[TestCategory("Framework")]
		public void FilesNotEqual() {
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.FilesNotEqual(null as string, EqualCompareFile2));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.FilesNotEqual(null as string, EqualCompareFileInfo2));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.FilesNotEqual(null as FileInfo, EqualCompareFile2));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.FilesNotEqual(null as FileInfo, EqualCompareFileInfo2));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.FilesNotEqual(EqualCompareFile1, null as string));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.FilesNotEqual(EqualCompareFile1, null as FileInfo));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.FilesNotEqual(EqualCompareFileInfo1, null as string));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.FilesNotEqual(EqualCompareFileInfo1, null as FileInfo));

			Assert.ThrowsExact<ArgumentException>(() => Assert.FilesNotEqual(string.Empty, EqualCompareFile2));
			Assert.ThrowsExact<ArgumentException>(() => Assert.FilesNotEqual(EqualCompareFile1, string.Empty));

			Assert.ThrowsExact<ArgumentException>(() => Assert.FilesNotEqual(NonExistingFile, ExistingFile));
			Assert.ThrowsExact<ArgumentException>(() => Assert.FilesNotEqual(NonExistingFileInfo, ExistingFile));
			Assert.ThrowsExact<ArgumentException>(() => Assert.FilesNotEqual(NonExistingFile, ExistingFileInfo));
			Assert.ThrowsExact<ArgumentException>(() => Assert.FilesNotEqual(NonExistingFileInfo, ExistingFileInfo));
			Assert.ThrowsExact<ArgumentException>(() => Assert.FilesNotEqual(ExistingFile, NonExistingFile));
			Assert.ThrowsExact<ArgumentException>(() => Assert.FilesNotEqual(ExistingFile, NonExistingFileInfo));
			Assert.ThrowsExact<ArgumentException>(() => Assert.FilesNotEqual(ExistingFileInfo, NonExistingFile));
			Assert.ThrowsExact<ArgumentException>(() => Assert.FilesNotEqual(ExistingFileInfo, NonExistingFileInfo));

			Assert.ThrowsExact<AssertionException>(() => Assert.FilesNotEqual(EqualCompareFile1, EqualCompareFile1));
			Assert.ThrowsExact<AssertionException>(() => Assert.FilesNotEqual(EqualCompareFile1, EqualCompareFileInfo1));
			Assert.ThrowsExact<AssertionException>(() => Assert.FilesNotEqual(EqualCompareFileInfo1, EqualCompareFile1));
			Assert.ThrowsExact<AssertionException>(() => Assert.FilesNotEqual(EqualCompareFileInfo1, EqualCompareFileInfo1));

			Assert.ThrowsExact<AssertionException>(() => Assert.FilesNotEqual(EqualCompareFile1, EqualCompareFile2));
			Assert.ThrowsExact<AssertionException>(() => Assert.FilesNotEqual(EqualCompareFileInfo1, EqualCompareFileInfo2));
			Assert.ThrowsExact<AssertionException>(() => Assert.FilesNotEqual(EqualCompareFile1, EqualCompareFileInfo2));
			Assert.ThrowsExact<AssertionException>(() => Assert.FilesNotEqual(EqualCompareFileInfo1, EqualCompareFile2));

			Assert.DoesNotThrow(() => Assert.FilesNotEqual(EqualCompareFile1, NotEqualCompareFileLength));
			Assert.DoesNotThrow(() => Assert.FilesNotEqual(EqualCompareFile1, NotEqualCompareFileLengthInfo));
			Assert.DoesNotThrow(() => Assert.FilesNotEqual(EqualCompareFileInfo1, NotEqualCompareFileLength));
			Assert.DoesNotThrow(() => Assert.FilesNotEqual(EqualCompareFileInfo1, NotEqualCompareFileLengthInfo));

			Assert.DoesNotThrow(() => Assert.FilesNotEqual(EqualCompareFile1, NotEqualCompareFileData));
			Assert.DoesNotThrow(() => Assert.FilesNotEqual(EqualCompareFile1, NotEqualCompareFileDataInfo));
			Assert.DoesNotThrow(() => Assert.FilesNotEqual(EqualCompareFileInfo1, NotEqualCompareFileData));
			Assert.DoesNotThrow(() => Assert.FilesNotEqual(EqualCompareFileInfo1, NotEqualCompareFileDataInfo));
		}
	}
}
