using System.Collections.Generic;


namespace bytePassion.Lib.GeometryLib.Intersections {	

	public class IntersectionResult <T> {

		public T					  Value      { get; }
		public List<T>				  Values	 { get; }
		public IntersectionResultType ResultType { get; }

		public IntersectionResult (IntersectionResultType resultType) {

			ResultType = resultType;
		}

		public IntersectionResult (T value) {

			ResultType = IntersectionResultType.Value;
			Value = value;
		}

		public IntersectionResult (List<T> values) {

			ResultType = IntersectionResultType.Multiple;
			Values = values;
		}
	}
}