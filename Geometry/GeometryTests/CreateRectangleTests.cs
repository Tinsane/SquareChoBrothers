using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Geometry;

namespace GeometryTests
{
    [TestClass]
    public class CreateRectangleTests
    {   
        public void TestRectangle(Rectangle rect)
        {
            Assert.AreEqual(rect == new Rectangle(rect.A, rect.B, rect.C), true);
            Assert.AreEqual(rect == new Rectangle(rect.A, rect.B, rect.D), true);
            Assert.AreEqual(rect == new Rectangle(rect.A, rect.C, rect.D), true);
            Assert.AreEqual(rect == new Rectangle(rect.B, rect.C, rect.D), true);
        }

        [TestMethod]
        public void SidesAreParallelToAxes()
        {
            TestRectangle(new Rectangle(new Point(0, 0), new Point(0, 1), new Point(1, 1), new Point(1, 0)));
        }

        [TestMethod]
        public void SidesAreNotParallelToAxes()
        {
            TestRectangle(new Rectangle(new Point(0, 0), new Point(1, 1), new Point(2, 0), new Point(1, -1)));
        }

        [TestMethod]
        public void SidesHaveIrrationalAngleWithAxes()
        {
            TestRectangle(new Rectangle(new Point(0, 0), new Point(3, 4), new Point(-1, 7), new Point(-4, 3)));
        }

        [TestMethod]
        public void RectangleIsPoint()
        {
            TestRectangle(new Rectangle(new Point(1.5, 1.5), new Point(1.5, 1.5), 
                new Point(1.5, 1.5), new Point(1.5, 1.5)));
        }
    }
}
