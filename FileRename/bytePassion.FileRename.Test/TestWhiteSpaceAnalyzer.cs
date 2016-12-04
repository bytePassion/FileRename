using System.Collections.Generic;
using bytePassion.FileRename.RenameLogic.NameAnalyzer;
using Xunit;


namespace bytePassion.FileRename.Test
{
	public class TestWhiteSpaceAnalyzer
	{

		[Theory]
		[MemberData("TestDataForWhiteSpaceRecognitionTest")]
		public void WhiteSpaceRecognitionTest(string name, bool containsWhiteSpace)
		{
			var sut = new WhiteSpaceAnalyzer();

			var whiteSpaceRecognized = sut.IsMatch(name);

			Assert.Equal(containsWhiteSpace, whiteSpaceRecognized);
		}

		public static readonly IEnumerable<object[]> TestDataForWhiteSpaceRecognitionTest = 
			new[]
			{
				new object[]{ "my file", true  },
				new object[]{ " file",   true  },
				new object[]{ "file ",   true  },
				new object[]{ "myfile",  false }				
			};

	}
}
