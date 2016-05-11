﻿using System.Drawing;
using Geometry;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model
{
    public abstract class DynamicPhysicalObject<T> : PhysicalObject<T>
        where T : IIntersectable<Circle>, IIntersectable<Rectangle>, IHavingIntersectionLine<T>
    {
        public abstract Vector Velocity { get; set; }

        protected DynamicPhysicalObject(Rectangle graphicalPosition, Brush brush) : base(graphicalPosition, brush)
        {
        }
    }
}