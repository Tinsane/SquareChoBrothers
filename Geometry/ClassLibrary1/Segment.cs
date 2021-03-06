﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public class Segment : Geom
    {
        public Point A, B;

        public Segment(Point A, Point B)
        {
            this.A = A;
            this.B = B;
        }

        public Segment GetReversedSegment()
        {
            return new Segment(B, A);
        }

        /// <summary>
        /// Возвращает точку на отрезку, делящую его в заданном отношении считая от вершины A.
        /// </summary>
        public Point GetDividingPointByRatio(double ratio)
        {
            if (ratio < 0)
                ratio = 0;
            if (ratio > 1)
                ratio = 1;
            var fromAToB = new Vector(A, B);
            fromAToB.Normalize(ratio * GetLength());
            return A + fromAToB;
        }

        /// <summary>
        /// Возвращает точку на отрезке, находящуюся на заданном расстоянии от вершины A.
        /// </summary>
        public Point GetPointOnSegmentOnDistanceFromA(double distance)
        {
            double ratio = distance / GetLength();
            return GetDividingPointByRatio(ratio);
        }

        /// <summary>
        /// Возвращает подотрезок, образованный точками, делящими заданный отрезок в заданном отношении.
        /// </summary>
        public Segment GetSubsegmentByDividingPoints(double ratio1, double ratio2)
        {
            return new Segment(GetDividingPointByRatio(ratio1), GetDividingPointByRatio(ratio2));
        }

        /// <summary>
        /// Возвращает подотрезок, образованный точками, лежащими на заданных расстояниях от точки A.
        /// </summary>
        public Segment GetSubsegmentOnDistancesFromA(double distance1, double distance2)
        {
            return new Segment(GetPointOnSegmentOnDistanceFromA(distance1), 
                GetPointOnSegmentOnDistanceFromA(distance2));
        }

        public double GetLength()
        {
            return A.GetDistance(B);
        }

        private bool IsOnFirstSide(Point point)
        {
            var firstVector = new Vector(A, B);
            var secondVector = new Vector(A, point);
            return (firstVector.GetScalarProduct(secondVector) < 0);
        }

        private bool IsOnSecondSize(Point point)
        {
            return GetReversedSegment().IsOnFirstSide(point);
        }

        public Line GetLineBySegment()
        {
            return new Line(A, B);
        }

        public double GetDistance(Point point)
        {
            if (IsOnFirstSide(point))
                return point.GetDistance(A);
            if (IsOnSecondSize(point))
                return point.GetDistance(B);
            var line = GetLineBySegment();
            return line.GetDistance(point);
        }

        public bool ContainsPoint(Point point)
        {
            if (!GetLineBySegment().ContainsPoint(point))
                return false;
            if ((point == A) || (point == B))
                return true;
            return new Vector(point, A).DifferentDirected(new Vector(point, B));
        }
    }
}
