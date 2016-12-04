using bytePassion.Lib.GeometryLib.Base;
using bytePassion.Lib.GeometryLib.MeshGeometry;
using bytePassion.Lib.GeometryLib.MeshGeometry.ObjectBase;
using bytePassion.Lib.GeometryLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;


namespace bytePassion.Lib.GeometryLib.Intersections
{

    public class Intersection {


		static double[] ComputeBarycentricCoords3D(CartesianCoordinate a, 
                                                   CartesianCoordinate b, 
                                                   CartesianCoordinate c,
                                                   CartesianCoordinate p){

			// First, compute two clockwise edge vectors
			var d1 = b - a;
			var d2 = c - b;

			// Compute surface normal using cross product. In many cases
			// this step could be skipped, since we would have the surface
			// normal precomputed. We do not need to normalize it, although
			// if a precomputed normal was normalized, it would be OK.
			var n = d1%d2;
		
			// Locate dominant axis of normal, and select plane of projection
			double u1, u2, u3, u4;
			double v1, v2, v3, v4;

			if ((Abs(n.X) >= Abs(n.Y)) && 
				(Abs(n.X) >= Abs(n.Z))) {

				// Discard x, project onto yz plane
				u1 = a.Y - c.Y;     v1 = a.Z - c.Z;
				u2 = b.Y - c.Y;     v2 = b.Z - c.Z;
				u3 = p.Y - a.Y;     v3 = p.Z - a.Z;
				u4 = p.Y - c.Y;     v4 = p.Z - c.Z;
		
			} else if (Abs(n.Y) >= Abs(n.Z)) {

				// Discard y, project onto xz plane
				u1 = a.Z - c.Z;     v1 = a.X - c.X;
				u2 = b.Z - c.Z;     v2 = b.X - c.X;
				u3 = p.Z - a.Z;     v3 = p.X - a.X;
				u4 = p.Z - c.Z;     v4 = p.X - c.X;						

			} else {

				u1 = a.X - c.X;     v1 = a.Y - c.Y;
				u2 = b.X - c.X;     v2 = b.Y - c.Y;
				u3 = p.X - a.X;     v3 = p.Y - a.Y;
				u4 = p.X - c.X;     v4 = p.Y - c.Y;									
			}

			// Compute denominator, check for invalid
			var denom = v1 * u2 - v2 * u1;
			
			if (GeometryLibUtils.DoubleEquals(denom, 0.0)) {
				// Bogus triangle - probably triangle has zero area
				return null;
			}

			// Compute barycentric coordinates
			var barycentricCoords = new double[3];

			var oneOverDenom = 1.0 / denom;
			barycentricCoords[0] = (v4*u2 - v2*u4) * oneOverDenom;
			barycentricCoords[1] = (v1*u3 - v3*u1) * oneOverDenom;			
			barycentricCoords[2] = 1.0 - barycentricCoords[0] - barycentricCoords[1];
		
			// OK
			return barycentricCoords;
		}		

		#region Ray-GeometricObject intersection

		public static IntersectionResult<double> RayGeometricObjectIntersection (Ray r, GeometricObject g, bool getAllIntersections) {

			var minT = Double.NaN;
			var allIntersecT = new List<double>();
			var mesh = g.Mesh;

			for (var triangleIndex = 0; triangleIndex < mesh.Indecies.Length; triangleIndex += 3) {

				var a = (CartesianCoordinate)mesh.PositionsWorldSpace[mesh.Indecies[triangleIndex + 0]];
				var b = (CartesianCoordinate)mesh.PositionsWorldSpace[mesh.Indecies[triangleIndex + 1]];
				var c = (CartesianCoordinate)mesh.PositionsWorldSpace[mesh.Indecies[triangleIndex + 2]];

				var trianglePlane = new Plane(a, b, c);

				var planeIntersection = RayPlaneIntersection(r, trianglePlane);

				if (planeIntersection.ResultType == IntersectionResultType.Value) {

					var baricentricCoordinates = ComputeBarycentricCoords3D(a, b, c, r.GetPointOnRay(planeIntersection.Value));

					var b1 = baricentricCoordinates[0] >= 0 && baricentricCoordinates[0] <= 1;
					var b2 = baricentricCoordinates[1] >= 0 && baricentricCoordinates[1] <= 1;
					var b3 = baricentricCoordinates[2] >= 0 && baricentricCoordinates[2] <= 1;

					if (b1 && b2 && b3) {

						if (getAllIntersections) {

							allIntersecT.Add(planeIntersection.Value);
						} else {

							if (Double.IsNaN(minT))
								minT = planeIntersection.Value;
							else if (planeIntersection.Value < minT)
								minT = planeIntersection.Value;
						}
					}
				}
			}

			if (Double.IsNaN(minT) && allIntersecT.Count == 0)
				return new IntersectionResult<double>(IntersectionResultType.NoIntersection);
			else {
				if (getAllIntersections)
					return new IntersectionResult<double>(allIntersecT);
				else
					return new IntersectionResult<double>(minT);
			}
				
		}

		#endregion

		#region Ray-BoundingBox intersection
		private static IntersectionResult<double> RayRectangeXYIntersection (Ray r, Plane p, double xMin, double xMax, double yMin, double yMax) {

			var res = RayPlaneIntersection(r, p);

			if (res.ResultType == IntersectionResultType.Value) {

				var intersectionPoint = r.GetPointOnRay(res.Value);

				if (intersectionPoint.X <= xMax && intersectionPoint.X >= xMin &&
					intersectionPoint.Y <= yMax && intersectionPoint.Y >= yMin) {

					return new IntersectionResult<double>(res.Value);
				}
			}

			return new IntersectionResult<double>(IntersectionResultType.NoIntersection);
		}

		private static IntersectionResult<double> RayRectangeXZIntersection (Ray r, Plane p, double xMin, double xMax, double zMin, double zMax) {

			var res = RayPlaneIntersection(r, p);

			if (res.ResultType == IntersectionResultType.Value) {

				var intersectionPoint = r.GetPointOnRay(res.Value);

				if (intersectionPoint.X <= xMax && intersectionPoint.X >= xMin &&
					intersectionPoint.Z <= zMax && intersectionPoint.Z >= zMin) {

					return new IntersectionResult<double>(res.Value);
				}
			}

			return new IntersectionResult<double>(IntersectionResultType.NoIntersection);
		}

		private static IntersectionResult<double> RayRectangeYZIntersection (Ray r, Plane p, double yMin, double yMax, double zMin, double zMax) {

			var res = RayPlaneIntersection(r, p);

			if (res.ResultType == IntersectionResultType.Value) {

				var intersectionPoint = r.GetPointOnRay(res.Value);

				if (intersectionPoint.Y <= yMax && intersectionPoint.Y >= yMin &&
					intersectionPoint.Z <= zMax && intersectionPoint.Z >= zMin) {

					return new IntersectionResult<double>(res.Value);
				}
			}

			return new IntersectionResult<double>(IntersectionResultType.NoIntersection);
		}

		public static IntersectionResult<double> RayBoundingBoxIntersection (Ray r, BoundingBox b) {

			var xy1 = new Plane(new Vec3(0.0, 0.0, 1.0), b.Min.Z);
			var xy2 = new Plane(new Vec3(0.0, 0.0, 1.0), b.Max.Z);

			var xz1 = new Plane(new Vec3(0.0, 1.0, 0.0), b.Min.Y);
			var xz2 = new Plane(new Vec3(0.0, 1.0, 0.0), b.Max.Y);

			var yz1 = new Plane(new Vec3(1.0, 0.0, 0.0), b.Min.X);
			var yz2 = new Plane(new Vec3(1.0, 0.0, 0.0), b.Max.X);

			var resXy1 = RayRectangeXYIntersection(r, xy1, b.Min.X, b.Max.X, b.Min.Y, b.Max.Y);
			var resXy2 = RayRectangeXYIntersection(r, xy2, b.Min.X, b.Max.X, b.Min.Y, b.Max.Y);

			var resXz1 = RayRectangeXZIntersection(r, xz1, b.Min.X, b.Max.X, b.Min.Z, b.Max.Z);
			var resXz2 = RayRectangeXZIntersection(r, xz2, b.Min.X, b.Max.X, b.Min.Z, b.Max.Z);

			var resYz1 = RayRectangeYZIntersection(r, yz1, b.Min.Y, b.Max.Y, b.Min.Z, b.Max.Z);
			var resYz2 = RayRectangeYZIntersection(r, yz2, b.Min.Y, b.Max.Y, b.Min.Z, b.Max.Z);

			var results = new List<double>();

			if (resXy1.ResultType == IntersectionResultType.Value) results.Add(resXy1.Value);
			if (resXy2.ResultType == IntersectionResultType.Value) results.Add(resXy2.Value);

			if (resXz1.ResultType == IntersectionResultType.Value) results.Add(resXz1.Value);
			if (resXz2.ResultType == IntersectionResultType.Value) results.Add(resXz2.Value);

			if (resYz1.ResultType == IntersectionResultType.Value) results.Add(resYz1.Value);
			if (resYz2.ResultType == IntersectionResultType.Value) results.Add(resYz2.Value);

			if (results.Count == 0)
				return new IntersectionResult<double>(IntersectionResultType.NoIntersection);
			
		    var tmin = results.Min();

		    return new IntersectionResult<double>(tmin);

		} 

		#endregion

		#region Ray-Plane intersection
		private static IntersectionResult<double> RayPlaneIntersection (Ray r, Plane p) {

			var quotient = r.Direction * p.Normal;
			if (GeometryLibUtils.DoubleEquals(quotient, 0)) return new IntersectionResult<double>(IntersectionResultType.NoIntersection);

			var t = (p.Distance-((Vec3)r.Origin*p.Normal))/quotient;

			if (t < 0)
				return new IntersectionResult<double>(IntersectionResultType.NoIntersection);
			else
				return new IntersectionResult<double>(t);
		}
		#endregion
	}
}

