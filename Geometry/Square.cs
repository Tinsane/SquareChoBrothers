﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public class Square : Rectangle
    {
        public Square(Point center, double size) : base(center, size, size) { }

        public double Size => Width;

        public new Square GetCopy() => new Square(Center, Size);

        public new Square GetTransfered(Vector transferVector) => new Square(Center + transferVector, Size);
    }
}
