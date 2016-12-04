using System.Collections.Generic;


namespace bytePassion.Lib.Types.Clonable
{
	// ReSharper disable once PossibleInterfaceMemberAmbiguity
	public interface IClonableList<T> : IList<T>, IReadOnlyList<T> where T : IGenericClonable<T>
	{
		IClonableList<T> ShallowCopy();
		IClonableList<T> DeepCopy();
	}
}
