using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Geometry;

namespace GeometryTests
{
    [TestClass]
    public class ContainsPointTests
    {
        public void AssertContains(Segment segment, Point point, bool expectedResult)
        {
            bool result = segment.ContainsPoint(point);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void SegmentIsParallelToOX()
        {
            var segment = new Segment(new Point(0, 0), new Point(1, 0));
            AssertContains(segment, new Point(0.0002023, 0), true);
            AssertContains(segment, new Point(0, 0), true);
            AssertContains(segment, new Point(2, 0), false);
            AssertContains(segment, new Point(-2, 0), false);
            AssertContains(segment, new Point(1, 1), false);
        }

        [TestMethod]
        public void SegmentIsParallelToOY()
        {
            var segment = new Segment(new Point(0, 0), new Point(0, 1));
            AssertContains(segment, new Point(0, 1), true);
            AssertContains(segment, new Point(0, 0.8), true);
            AssertContains(segment, new Point(0, 2), false);
            AssertContains(segment, new Point(0, -2), false);
            AssertContains(segment, new Point(-1, -3.4342), false);
        }

        [TestMethod]
        public void SegmentIsNotParallelToAxes()
        {
            var segment = new Segment(new Point(1, 1), new Point(-3, -1));
            AssertContains(segment, new Point(1, 1), true);
            AssertContains(segment, new Point(-1, 0), true);
            AssertContains(segment, new Point(10, 10), false);
            AssertContains(segment, new Point(3, 2), false);
            AssertContains(segment, new Point(-5, -2), false);
        }
    }
}
