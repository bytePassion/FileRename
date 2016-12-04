using System.Collections.Generic;
using bytePassion.FileRename.RenameLogic;
using bytePassion.FileRename.RenameLogic.Enums;
using Xunit;

namespace bytePassion.FileRename.Test
{
	
    public class RenameProcessorTest
    {
	    [Fact]
	    public void DeleteWhiteSpaceTest()
	    {
		    var sut = RenameProcessorBuilder.Build(SearchType.WhiteSpace, "", false, ReplaceType.Delete, "");

		    const string filename = "my file.txt";

		    bool isMatch = sut.IsMatch(filename, ItemType.File);
		    var refactoredFilename = sut.RefactoredName(filename, ItemType.File);

			Assert.True(isMatch);
			Assert.Equal("myfile.txt", refactoredFilename);
	    }

	    [Theory]
		[MemberData("TestDataForDeleteWhiteSpaceTest")]
	    public void MultipleDeleteWhiteSpaceTest(string filename, string correctRefactoredFilename)
	    {
			var sut = RenameProcessorBuilder.Build(SearchType.WhiteSpace, "", false, ReplaceType.Delete, "");
			

			bool isMatch = sut.IsMatch(filename, ItemType.File);
			var refactoredFilename = sut.RefactoredName(filename, ItemType.File);

			Assert.True(isMatch);
			Assert.Equal(correctRefactoredFilename, refactoredFilename);
	    }

	    public static readonly IEnumerable<object[]> TestDataForDeleteWhiteSpaceTest = 
			new []
			{
				new object[]{ "my file.txt",     "myfile.txt" },
				new object[]{ "my fi le.txt",    "myfile.txt" },
				new object[]{ "my  fi le.txt",   "myfile.txt" },
				new object[]{ " my file.txt",    "myfile.txt" },
				new object[]{ "   my file.txt",  "myfile.txt" },
				new object[]{ " my file   .txt", "myfile.txt" },
				new object[]{ " my file .txt",   "myfile.txt" }
			};



//		[Theory]
//		[InlineData("my file.txt",     "myfile.txt")]
//		[InlineData("my fi le.txt",    "myfile.txt")]
//		[InlineData("my  fi le.txt",   "myfile.txt")]
//		[InlineData(" my file.txt",    "myfile.txt")]
//		[InlineData("   my file.txt",  "myfile.txt")]
//		[InlineData(" my file   .txt", "myfile.txt")]
//		[InlineData(" my file .txt",   "myfile.txt")]
//		public void MultipleDeleteWhiteSpaceTest2 (string filename, string correctRefactoredFilename)
//		{
//			var sut = RenameProcessorBuilder.Build(SearchType.WhiteSpace, "", false, ReplaceType.Delete, "");
//
//			bool isMatch = sut.IsMatch(filename);
//			var refactoredFilename = sut.RefactoredName(filename, ItemType.File);
//
//			Assert.True(isMatch);
//			Assert.Equal(correctRefactoredFilename, refactoredFilename);
//		}
    }
}
