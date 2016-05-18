﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = Geometry.Point;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model.Factories
{
    public class HeroFactory : MapObjectFactory<Hero, Rectangle>
    {
        public HeroFactory(string imageName) : base(imageName)
        {
        }

        public override Hero GetNext(Rectangle rectangle) => new Hero(rectangle.GetCopy(), imageName);
    }
}
