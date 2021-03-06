﻿using System;
using System.Collections.Generic;
using System.Linq;
using bytePassion.FileRename.RenameLogic.Helper;
using bytePassion.FileRename.RenameLogic.NameAnalyzer;
using Xunit;


namespace bytePassion.FileRename.Test
{
	public class MultipleStringAnalyzerTest
	{
		[Theory]
		[MemberData("TestDataForCaseSensitiveMultipleStringRecognitionTest")]
		public void CaseSensitiveMultipleStringRecognitionTest (string name, string search, bool containsSpecifiedString)
		{
			var sut = new MultiStringAnalyzer(search, true);
			
			var spcifiedStringRecognized = sut.IsMatch(name);

			Assert.Equal(containsSpecifiedString, spcifiedStringRecognized);
		}

		public static readonly IEnumerable<object[]> TestDataForCaseSensitiveMultipleStringRecognitionTest = 
			new[]
			{ 
				new object[]{ "myfile", "\"file\", \"foo\"" , true  },				
				new object[]{ "myfile", "\"f\", \"e\""      , true  },
				new object[]{ "myfile", "\"bar\", \"foo\""  , false },
				new object[]{ "myFiLe", "\"fIle\", \"foo\"" , false  },
			};

		[Theory]
		[MemberData("TestDataForNonCaseSensitiveMultipleStringRecognitionTest")]
		public void NonCaseSensitiveMultipleStringRecognitionTest (string name, string search, bool containsSpecifiedString)
		{
			var sut = new MultiStringAnalyzer(search, false);

			var spcifiedStringRecognized = sut.IsMatch(name);

			Assert.Equal(containsSpecifiedString, spcifiedStringRecognized);
		}

		public static readonly IEnumerable<object[]> TestDataForNonCaseSensitiveMultipleStringRecognitionTest = 
			new[]
			{ 
				new object[]{ "myfile", "\"file\", \"foo\"" , true  },				
				new object[]{ "myfile", "\"f\", \"e\""      , true  },
				new object[]{ "myfile", "\"bar\", \"foo\""  , false },
				new object[]{ "myFiLe", "\"fIle\", \"foo\"" , true  },
			};

		[Theory]
		[MemberData("TestDataForMultipleStringReplacementIndeciesTest")]
		public void MultipleStringReplacementIndeciesTest (string name, string search, IReadOnlyList<StringIntervalIndecies> expectedIndecies)
		{
			var sut = new MultiStringAnalyzer(search, false);

			var indexList = sut.ReplacementIndecies(name).ToList();			

			Assert.Equal(indexList.Count, expectedIndecies.Count);

			foreach (var replacementIndex in expectedIndecies)
			{
				Assert.True(indexList.Contains(replacementIndex));
			}
		}

		public static readonly IEnumerable<object[]> TestDataForMultipleStringReplacementIndeciesTest = 
			new[]
			{				
				new object[]{ "myfile", "\"file\", \"foo\"" , new List<StringIntervalIndecies> { new StringIntervalIndecies(2, 6) }},				
				new object[]{ "myfile", "\"f\", \"e\""      , new List<StringIntervalIndecies> { new StringIntervalIndecies(2, 3), new StringIntervalIndecies(5, 6) }},
				new object[]{ "myfile", "\"my\", \"yfi\"" , new List<StringIntervalIndecies> { new StringIntervalIndecies(0, 2) }},	
				new object[]{ "foooobar", "\"oo\", \"foo\"" , new List<StringIntervalIndecies> { new StringIntervalIndecies(0, 3), new StringIntervalIndecies(3,5) }}		
			};

	}
}
