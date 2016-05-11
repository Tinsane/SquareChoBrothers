﻿using System.Drawing;
using Geometry;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model
{
    public class Monster : DynamicPhysicalObject<Circle>
    {
        public Monster(Rectangle graphicalPosition, Brush brush) : base(graphicalPosition, brush)
        {

        }
        public override Circle HitBox { get; set; }
        public override Vector Velocity { get; set; }
    }
}