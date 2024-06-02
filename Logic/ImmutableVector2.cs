using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class ImmutableVector2
    {
        public float X { get; }
        public float Y { get; }

        internal ImmutableVector2(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}
