using System;
using System.Collections;
using System.Collections.Generic;


namespace bytePassion.Lib.Types.Clonable
{
	/// <summary>
	/// A <c>ClonableList</c> is a partial wrapped <c>System.Collections.Generic.List</c>, extended by a deep- and shallow-copy function.	
	/// </summary>
	/// <typeparam name="T">IGenericClonable</typeparam>
	
	public class ClonableList<T> : IClonableList<T> where T : IGenericClonable<T>
	{
		private readonly List<T> list;

		
		public ClonableList()                 { list = new List<T>();         }
		public ClonableList(int capacity)     { list = new List<T>(capacity); }
		public ClonableList(List<T> initList) { list = initList;              } 		
		

		private IClonableList<T> CloneList(Func<T, T> copyFunc)
		{
			var resultList = new ClonableList<T>();

			foreach (var genericClonable in list)
			{
				resultList.Add(copyFunc(genericClonable));
			}

			return resultList;
		} 

		public IClonableList<T> DeepCopy()    => CloneList(genericClonable => genericClonable.Clone());
		public IClonableList<T> ShallowCopy() => CloneList(genericClonable => genericClonable);
	

		public   IEnumerator<T> GetEnumerator() => list.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public void Add      (T item)    { list.Add(item); }
		public void Clear    ()          { list.Clear();}		
		public void RemoveAt (int index) { list.RemoveAt(index); }
		public bool Contains (T item)    => list.Contains(item);
		public bool Remove   (T item)    => list.Remove(item);
		public int  IndexOf  (T item)    => list.IndexOf(item);

		public int  Count      => list.Count;
		public bool IsReadOnly => (list as IList<T>).IsReadOnly;

		public void Insert(int index, T item)         { list.Insert(index, item);}
		public void CopyTo(T[] array, int arrayIndex) { list.CopyTo(array, arrayIndex);}		

		public T this[int index]
		{
			get { return list[index]; }
			set { list[index] = value; }
		}		
		
		public void AppendList(IReadOnlyList<T> items) { list.AddRange(items); }
		public void AppendList(IClonableList<T> items) { AppendList((IReadOnlyList<T>)items); }

		public void Sort()                         { list.Sort();           }		
		public void Sort(IComparer<T> comparer)    { list.Sort(comparer);   }		
		public void Sort(Comparison<T> comparison) { list.Sort(comparison); }
				
	}
}
