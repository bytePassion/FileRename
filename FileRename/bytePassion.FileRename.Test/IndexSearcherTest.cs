using System;
using System.Collections.Generic;
using System.Linq;
using bytePassion.FileRename.RenameLogic.Helper;
using Xunit;


namespace bytePassion.FileRename.Test
{
	public class IndexSearcherTest
	{
		[Theory]
		[MemberData("TestDataForIndexSearcherFindsNoIndexTuplesTest")]
		public void IndexSearcherFindsNoIndexTuples(string name, string search)
		{
			var resultList = IndexSearcher.GetReplacementIndexTuples(name, search);
			Assert.Equal(resultList.Count(), 0);
		}
		
		public static readonly IEnumerable<object[]> TestDataForIndexSearcherFindsNoIndexTuplesTest = 
			new[]
			{
				new object[]{ "myfile", "myt"},
				new object[]{ "myt",    "myfile"}
			};


		[Theory]
		[MemberData("TestDataForIndexSearcherFindsOneIndexTupleTest")]
		public void IndexSearcherFindsOneIndexTuple (string name, string search, Tuple<int, int> indexTuple)
		{
			IReadOnlyList<Tuple<int, int>> resultList = IndexSearcher.GetReplacementIndexTuples(name, search).ToList();	
		
			Assert.Equal(resultList.Count, 1);
			Assert.Equal(resultList[0], indexTuple);
		}
		
		public static readonly IEnumerable<object[]> TestDataForIndexSearcherFindsOneIndexTupleTest = 
			new[]
			{
				new object[]{ "myfile", "myf", new Tuple<int, int>(0, 3)},
				new object[]{ "myfile", "yfi", new Tuple<int, int>(1, 4)},
				new object[]{ "myfile", "ile", new Tuple<int, int>(3, 6)},
				new object[]{ "myfile", "m"  , new Tuple<int, int>(0, 1)},
				new object[]{ "myfile", "y"  , new Tuple<int, int>(1, 2)},
				new object[]{ "myfile", "e"  , new Tuple<int, int>(5, 6)},
			};

		[Theory]
		[MemberData("TestDataForIndexSearcherFindsTwoIndexTupleTest")]
		public void IndexSearcherFindsTwoIndexTuples (string name, string search, 
													  Tuple<int, int> indexTuple1, Tuple<int, int> indexTuple2)
		{
			IReadOnlyList<Tuple<int, int>> resultList = IndexSearcher.GetReplacementIndexTuples(name, search).ToList();

			Assert.Equal(resultList.Count, 2);
			Assert.Equal(resultList[0], indexTuple1);
			Assert.Equal(resultList[1], indexTuple2);
		}

		public static readonly IEnumerable<object[]> TestDataForIndexSearcherFindsTwoIndexTupleTest = 
			new[]
			{
				new object[]{ "myfilmye", "my", new Tuple<int, int>(0, 2), new Tuple<int, int>(5, 7)},
				new object[]{ "myfilemy", "my", new Tuple<int, int>(0, 2), new Tuple<int, int>(6, 8)},
				new object[]{ "mymyfile", "my", new Tuple<int, int>(0, 2), new Tuple<int, int>(2, 4)},
				new object[]{ "mmmmfile", "mm", new Tuple<int, int>(0, 2), new Tuple<int, int>(2, 4)},
				new object[]{ "mmmmmile", "mm", new Tuple<int, int>(0, 2), new Tuple<int, int>(2, 4)},
				new object[]{ "mmmmmilm", "mm", new Tuple<int, int>(0, 2), new Tuple<int, int>(2, 4)},
				new object[]{ "mmmmmime", "mm", new Tuple<int, int>(0, 2), new Tuple<int, int>(2, 4)},
			};
	}
}
