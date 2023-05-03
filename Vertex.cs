using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obj
{
    public class Vertex
    {
        public float x;
        public float y;
        public float z;
        public float w;
        public float h;
        public float a, r, g, b;

        private Color c;

        public float A
        {
            get { return c.A; }
        }
        public float R
        {
            get { return c.R; }
        }
        public float G
        {
            get { return c.G; }
        }
        public float B
        {
            get { return c.B; }
        }

        public Color C
        {
            get { return c; }
            set
            {
                c = value;
                // CAMBIOS PARA SCENA
                a = c.A;
                r = c.R;
                g = c.G;
                b = c.B;

            }
        }

        public float X
        {
            get { return x; }
            set { x = value; }
        }

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public float Z
        {
            get { return z; }
            set { z = value; }
        }

        public float W
        {
            get { return w; }
            set { w = value; }
        }

        public float H
        {
            get { return h; }
            set { h = value; }
        }

        //CAMBIOS PARA SCENA
        public Vertex(float x, float y, float z, Color c)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = 1;
            this.c = c;
        }

        public Vertex(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = 1;
        }

        public Vertex(float x, float y, float z, float h)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = 1;
            this.h = h;

        }

        public static Vertex Cross(Vertex a, Vertex b)
        {
            return new Vertex(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x, a.h * b.h);
        }

        // Computes dot product.
        public static float operator *(Vertex v1, Vertex v2)
        {
            return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
        }

        public static Vertex operator +(Vertex v1, Vertex v2)
        {
            return new Vertex(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.H + v2.H);
        }

        public static Vertex operator -(Vertex v1, Vertex v2)
        {
            return new Vertex(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.H - v2.H);
        }

        public static Vertex operator -(Vertex v)
        {
            return new Vertex(-v.X, -v.Y, -v.Z, v.H);
        }
        public static Vertex operator /(Vertex v1, float a)
        {
            return new Vertex(v1.X / a, v1.Y / a, v1.Z / a, v1.H);
        }

        public static Vertex operator *(Vertex v1, float b)
        {
            return new Vertex(v1.X * b, v1.Y * b, v1.Z * b, v1.H);
        }

        public float Mag()
        {
            return (float)Math.Sqrt(x * x + y * y + z * z);
        }

        public Vertex Normalize()
        {
            float mag = this.Mag();
            return new Vertex(x / mag, y / mag, z / mag, this.H);
        }

        public override string ToString()
        {
            if (w != 0)
                return x + ", " + y + ", " + z + ", " + w;

            return x + ", " + y + ", " + z;
        }
    }
}
