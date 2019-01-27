using System;
using System.Collections.Generic;
using bytePassion.FileRename.RenameLogic.Helper;
using Xunit;


namespace bytePassion.FileRename.Test
{
	public class StringReplacerTest
	{
		[Fact]
		public void StringReplacerHasNothingToReplace()
		{
			const string name = "myFile";
			string refactoredName = StringReplacer.GetReplacedString(name, "foo", new List<StringIntervalIndecies>());

			Assert.Equal(name, refactoredName);
		}

		[Theory]
		[MemberData("TestDataForStringReplacerHasNothingToReplaceTest")]
		public void StringReplacerReplacesOnce (string name, string replacement, string correctRefactoredName,
                                                StringIntervalIndecies replacementIndex)
		{
			string refactoredName = StringReplacer.GetReplacedString(name, replacement, new List<StringIntervalIndecies> {replacementIndex});

			Assert.Equal(correctRefactoredName, refactoredName);
		}

		public static readonly IEnumerable<object[]> TestDataForStringReplacerHasNothingToReplaceTest = 
			new[]
			{
				new object[]{ "mybarfile", "foo",   "myfoofile",   new StringIntervalIndecies(2, 5) },
				new object[]{ "mybarfile", "foooo", "myfoooofile", new StringIntervalIndecies(2, 5) },
				new object[]{ "mybarfile", "fo",    "myfofile",    new StringIntervalIndecies(2, 5) },
				new object[]{ "mybarfile", "fo",    "myfofile",    new StringIntervalIndecies(2, 5) },
				new object[]{ "mybarfile", "",      "myfile",      new StringIntervalIndecies(2, 5) },

				new object[]{ "myfile",   "foo",    "foo",         new StringIntervalIndecies(0, 6) },

				new object[]{ "barmyfile", "foo",   "foomyfile",   new StringIntervalIndecies(0, 3) },
			};
	}
}
