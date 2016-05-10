using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Geometry;

namespace GeometryTests
{
    [TestClass]
    public class RotateRectangleTests
    {
        public void Test(double angle, Rectangle rect, Point point, Rectangle expectedRect)
        {
            rect.Rotate(point, angle);
            Assert.AreEqual(rect == expectedRect, true);
        }

        [TestMethod]
        public void RotateOnPi()
        {
            Test(Math.PI, new Rectangle(new Point(1, 1), new Point(1, 2), new Point(2, 2)),
            new Point(0, 0), new Rectangle(new Point(-1, -1), new Point(-1, -2), new Point(-2, -2)));
        }

        [TestMethod]
        public void RotateRelativelyPointOnRectangle()
        {
            Test(Math.PI / 2, new Rectangle(new Point(0, 0), new Point(1, 1), new Point(0, 2)),
            new Point(0, 0), new Rectangle(new Point(0, 0), new Point(-1, 1), new Point(-2, 0)));
        }

        [TestMethod]
        public void RotateOnAngleBiggerThan2PI()
        {
            Rectangle rect = new Rectangle(new Point(0, 0), new Point(1, 1), new Point(2, 0));
            Test(Math.PI * 3.5, rect, new Point(1, 0), rect);
        }
    }
}
