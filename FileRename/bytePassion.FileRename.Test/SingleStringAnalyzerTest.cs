using System.Collections.Generic;
using bytePassion.FileRename.RenameLogic.NameAnalyzer;
using Xunit;


namespace bytePassion.FileRename.Test
{
	public class SingleStringAnalyzerTest
	{
		[Theory]
		[MemberData("TestDataForCaseSensitiveSingleStringRecognitionTest")]
		public void CaseSensitiveSingleStringRecognitionTest (string name, string search, bool containsSpecifiedString)
		{
			var sut = new SingleStringAnalyzer(search, true);
			
			var spcifiedStringRecognized = sut.IsMatch(name);

			Assert.Equal(containsSpecifiedString, spcifiedStringRecognized);
		}

		public static readonly IEnumerable<object[]> TestDataForCaseSensitiveSingleStringRecognitionTest = 
			new[]
			{ 
				new object[]{ "myfile", "file" , true  },
				new object[]{ "myfile", "f" ,    true  },
				new object[]{ "myfile", "my" ,   true  },
				new object[]{ "myfile", "le" ,   true  },
				new object[]{ "myfile", "myt" ,  false },
				new object[]{ "myFile", "myf" ,  false }
			};

		[Theory]
		[MemberData("TestDataForNonCaseSensitiveSingleStringRecognitionTest")]
		public void NonCaseSensitiveSingleStringRecognitionTest (string name, string search, bool containsSpecifiedString)
		{
			var sut = new SingleStringAnalyzer(search, false);

			var spcifiedStringRecognized = sut.IsMatch(name);

			Assert.Equal(containsSpecifiedString, spcifiedStringRecognized);
		}

		public static readonly IEnumerable<object[]> TestDataForNonCaseSensitiveSingleStringRecognitionTest = 
			new[]
			{ 
				new object[]{ "myfile", "file" , true  },
				new object[]{ "myfile", "f" ,    true  },
				new object[]{ "myfile", "my" ,   true  },
				new object[]{ "myfile", "le" ,   true  },
				new object[]{ "myfile", "myt" ,  false },
				new object[]{ "myFile", "myf" ,  true  }
			};

	}
}
