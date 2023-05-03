using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obj
{
    public class Transform
    {
        public float scale;
        public Vertex traslation;
        public Vertex rotation;
        public Transform(float scale, Vertex rotation, Vertex traslation)
        {
            this.scale = scale;
            this.rotation = rotation;
            this.traslation = traslation;
        }
    }
}
