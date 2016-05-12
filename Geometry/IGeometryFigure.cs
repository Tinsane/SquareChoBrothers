﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public interface IGeometryFigure : IHavingIntersectionLine<Circle>, IHavingIntersectionLine<Square>
    {
        void Transfer(Vector transferVector);
    }
}
