using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IDataBall
    {
        Vector2 Position { get; }
        Vector2 Velocity { get; set; }
    }
}
