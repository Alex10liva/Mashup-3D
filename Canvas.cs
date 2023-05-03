using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Obj
{
    public class Canvas
    {
        int counter = 1;
        public Bitmap bmp;
        byte[] bits;
        int pixelFormatSize, stride;
        Graphics g;
        public int Width, Height;
        float viewport_size = 1;
        float projection_plane_z = 1;
        Model objModel;
        public Camera camera = new Camera(new Vertex(0, 0, 0), Mtx.RotY(0));
        //public Instance[] instances;
        public List<Instance> instances = new List<Instance>();
        public Size size;
        ObjFileReader obj;
        Vertex[] vertices;
        Triangle[] triangles;

        Vertex line1, line2;
        Vertex triangleNormal;

        public bool fileLoaded = false;
        public int FOV = 90;
        float[,] buffer;

        List<ObjFileReader> objsAll = new List<ObjFileReader>();
        List<Vertex[]> verticesAll = new List<Vertex[]>();
        List<Triangle[]> trianglesAll = new List<Triangle[]>();

        public bool wireframe, filled;
        public int nFigures = 0;

        Random random = new Random(); // Instancia de la clase Random

        public ObjFileReader ButtonClicked()
        {
            obj = new ObjFileReader();
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "obj files (*.obj)|*.obj";

            if (fileLoaded == false)
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileLoaded = true;
                    obj.LoadObj(openFileDialog.FileName);
                    createObj(obj);
                    objsAll.Add(obj);

                    SetModelInstances();
                    nFigures++;
                }
            }

            return obj;
        }

        public Canvas(Size size)
        {
            bmp = new Bitmap(size.Width, size.Height);
            this.size = size;
            Init(size.Width, size.Height);
        }

        private void createObj(ObjFileReader obj)
        {
            vertices = new Vertex[obj.vertices.Count];

            for (int i = 0; i < obj.vertices.Count; i++)
            {
                vertices[i] = obj.vertices[i];
            }
            verticesAll.Add(vertices);
            triangles = new Triangle[obj.indices.Count / 3];

            Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)); // Generación de un color aleatorio
            int j = 0;
            for (int i = 0; i < obj.indices.Count; i++)
            {
                if (i % 3 == 0)
                {
                    triangles[j] = new Triangle(obj.indices[i], obj.indices[i + 1], obj.indices[i + 2], randomColor);
                    j++;
                }
            }

            trianglesAll.Add(triangles);
        }

        public void Init(int width, int height)
        {
            PixelFormat format;
            GCHandle handle;
            IntPtr bitPtr;
            int padding;
            buffer = new float[width, height];

            format = PixelFormat.Format32bppArgb;
            Width = width;
            Height = height;
            pixelFormatSize = Image.GetPixelFormatSize(format) / 8; // 8 bits = 1 byte
            stride = width * pixelFormatSize;                       // total pixels (width) times ARGB (4)
            padding = (stride % 4);                                 // PADD = move every pixel in bytes
            stride += padding == 0 ? 0 : 4 - padding;               // 4 byte multiple Alpha, Red, Green, BLue
            bits = new byte[stride * height];                       // total pixels (width) times ARGB (4) times Height
            handle = GCHandle.Alloc(bits, GCHandleType.Pinned);     // To lock the memory
            bitPtr = Marshal.UnsafeAddrOfPinnedArrayElement(bits, 0);
            bmp = new Bitmap(width, height, stride, format, bitPtr);

            g = Graphics.FromImage(bmp);                         // Para hacer pruebas regulares



            //llenar el buffer
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    buffer[i, j] = float.MaxValue;
                }
            }
            ClippingPlanes(70);



        }


        public void SetModelInstances()
        {
            objModel = new Model(verticesAll[nFigures], trianglesAll[nFigures]);
            instances.Add(new Instance(objModel, new Vertex(0, 0, 10f), Mtx.Identity, 0));

            ClippingPlanes(FOV);
        }

        public void Render()
        {

            for (int it = 0; it < buffer.GetLength(0); it++)
            {
                for (int j = 0; j < buffer.GetLength(1); j++)
                {
                    buffer[it, j] = float.MaxValue;
                }
            }
            RenderScene(camera, instances);
        }

        private void ClippingPlanes(float fov)
        {
            Vertex left, right, bottom, top;
            float vista = 1f;
            float cercania = 0.1f;

            float tanFov = (float)Math.Tan(fov * 0.5f * Math.PI / 180f);
            float height = 2f * tanFov * cercania;
            float width = height * vista;

            // izquierda 
            float sx = 1f * (width / 2f);
            float sy = 0f;
            float sz = cercania;

            left = new Vertex(sx, sy, sz);
            left = left.Normalize();

            // derecha 
            sx = -width / 2f;
            sy = 0f;
            sz = cercania;
            right = new Vertex(sx, sy, sz);
            right = right.Normalize();

            // abajo 
            sx = 0f;
            sy = -1f * (height / 2f);
            sz = cercania;
            bottom = new Vertex(sx, sy, sz);
            bottom = bottom.Normalize();

            // arriba
            sx = 0f;
            sy = height / 2f;
            sz = cercania;
            top = new Vertex(sx, sy, sz);
            top = top.Normalize();

            camera.clipping_planes.Add(new Plane(new Vertex(0, 0, 1), 0));   // cercania
            camera.clipping_planes.Add(new Plane(left, 0));  // izquierda
            camera.clipping_planes.Add(new Plane(right, 0));  // derecha
            camera.clipping_planes.Add(new Plane(top, 0));  // arriba
            camera.clipping_planes.Add(new Plane(bottom, 0));  // abajo
        }

        public void PutPixelParaZ(int x, int y, float z, Color c)
        {
            x = (int)(Width / 2) + x;
            y = (int)(Height / 2) - y - 1;

            if (x < 0 || x >= Width || y < 0 || y >= Height) return;

            if (z < buffer[(int)x, y])
            {
                int res = (int)((x * pixelFormatSize) + (y * stride)); //x an y point of your image. Stride is the complete size of a row and its multiply by x that is the number of rows

                bits[res + 0] = c.B;
                bits[res + 1] = c.G;
                bits[res + 2] = c.R;
                bits[res + 3] = c.A; //Transparency

                buffer[(int)x, y] = z;
            }

        }

        public void PutPixelOriginal(int x, int y, Color c)
        {
            x = (int)(Width / 2) + x;
            y = (int)(Height / 2) - y - 1;

            if (x < 0 || x >= Width || y < 0 || y >= Height) return;

            int res = (int)((x * pixelFormatSize) + (y * stride)); //x an y point of your image. Stride is the complete size of a row and its multiply by x that is the number of rows

            bits[res + 0] = c.B;
            bits[res + 1] = c.G;
            bits[res + 2] = c.R;
            bits[res + 3] = c.A; //Transparency

        }



        public void FastClear()
        {
            unsafe
            {
                BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                    ImageLockMode.ReadWrite, bmp.PixelFormat);
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(bmp.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                try
                {
                    Parallel.For(0, heightInPixels, y => // usando proceso en paralelo
                    {
                        byte* bits = PtrFirstPixel + (y * bitmapData.Stride);
                        for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                        {
                            bits[x + 0] = 0; // (byte) Blue
                            bits[x + 1] = 0; // (byte) Green
                            bits[x + 2] = 0; // (byte) Red
                            bits[x + 3] = 0; // (byte) Alpha
                        }
                    });
                }
                catch (Exception)
                {
                }
                bmp.UnlockBits(bitmapData);
            }
        }

        public static List<float> Interpolate(int i0, float d0, int i1, float d1)
        {
            if (i0 == i1)
            {
                return new List<float> { d0 };
            }

            List<float> values = new List<float>();
            float a = (d1 - d0) / (i1 - i0);
            float d = d0;
            for (int i = i0; i <= i1; i++)
            {
                values.Add(d);
                d += a;
            }

            return values;
        }

        public void DrawLine(Vertex p0, Vertex p1, Color color)
        {
            float dx = p1.x - p0.x, dy = p1.y - p0.y;

            if (Math.Abs(dx) > Math.Abs(dy))
            {
                // The line is horizontal-ish. Make sure it's left to right.
                if (dx < 0) { var swap = p0; p0 = p1; p1 = swap; }

                // Compute the Y values and draw.
                var ys = Interpolate((int)p0.x, p0.y, (int)p1.x, p1.y);
                for (int x = (int)p0.x; x <= (int)p1.x; x++)
                {
                    try
                    {
                        PutPixelOriginal(x, (int)ys[(int)(x - p0.x)], color);

                    }
                    catch (Exception)
                    {
                    }
                }
            }
            else
            {
                // The line is verical-ish. Make sure it's bottom to top.
                if (dy < 0) { var swap = p0; p0 = p1; p1 = swap; }

                // Compute the X values and draw.
                var xs = Interpolate((int)p0.y, p0.x, (int)p1.y, p1.x);
                for (int y = (int)p0.y; y <= (int)p1.y; y++)
                {
                    try
                    {
                        PutPixelOriginal((int)xs[(int)(y - p0.y)], y, color);

                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void Swap(ref Vertex p1, ref Vertex p2)
        {
            Vertex temp = p1;
            p1 = p2;
            p2 = temp;
        }

        public void DrawWireframeTriangle(Vertex p0, Vertex p1, Vertex p2, Color color)
        {
            if (wireframe)
            {
                DrawLine(p0, p1, color);
                DrawLine(p1, p2, color);
                DrawLine(p2, p0, color);
            }

            if (filled)
                DrawFilledTriangle(p0, p1, p2, color);
        }

        public void DrawFilledTriangle(Vertex p0, Vertex p1, Vertex p2, Color color)
        {
            if (p1.y < p0.y)
                Swap(ref p1, ref p0);
            if (p2.y < p0.y)
                Swap(ref p2, ref p0);
            if (p2.y < p1.y)
                Swap(ref p2, ref p1);

            var x01 = Interpolate((int)p0.y, p0.x, (int)p1.y, p1.x);
            var x12 = Interpolate((int)p1.y, p1.x, (int)p2.y, p2.x);
            var x02 = Interpolate((int)p0.y, p0.x, (int)p2.y, p2.x);

            if (x01.Count > 0)
            {
                x01.RemoveAt(x01.Count - 1);
            }

            var x012 = x01.Concat(x12).ToList();

            List<float> x_left, x_right;

            int m = (int)Math.Floor((double)(x02.Count / 2));
            if (x02[m] < x012[m])
            {
                x_left = x02;
                x_right = x012;
            }
            else
            {
                x_left = x012;
                x_right = x02;
            }

            // Draw the horizontal segments 
            for (int y = (int)p0.y; y <= p2.y; y++)
            {
                for (int x = (int)x_left[(int)(y - p0.y)]; x <= (int)x_right[(int)(y - p0.y)]; x++)
                {
                    PutPixelOriginal(x, y, color);
                }
            }
        }





        private void HiddenFaces(Triangle triangle, Vertex[] vertices)
        {
            line1 = new Vertex(vertices[triangle.v1].X - vertices[triangle.v0].X, vertices[triangle.v1].Y - vertices[triangle.v0].Y, vertices[triangle.v1].Z - vertices[triangle.v0].Z);
            line2 = new Vertex(vertices[triangle.v2].X - vertices[triangle.v0].X, vertices[triangle.v2].Y - vertices[triangle.v0].Y, vertices[triangle.v2].Z - vertices[triangle.v0].Z);
            triangleNormal = new Vertex(line1.Y * line2.Z - line1.Z * line2.Y, line1.Z * line2.X - line1.X * line2.Z, line1.X * line2.Y - line1.Y * line2.X);
            triangleNormal = triangleNormal.Normalize();
        }

        public void DrawShadedTriangle(Vertex p0, Vertex p1, Vertex p2, Color color)
        {
            // Sort the points so that y0 <= y1 <= y2
            if (p1.y < p0.y)
                Swap(ref p1, ref p0);
            if (p2.y < p0.y)
                Swap(ref p2, ref p0);
            if (p2.y < p1.y)
                Swap(ref p2, ref p1);

            // Compute the x coordinates and h values of the triangle edges
            var x01 = Interpolate((int)p0.y, p0.x, (int)p1.y, p1.x);
            var h01 = Interpolate((int)p0.y, p0.h, (int)p1.y, p1.h);

            var x12 = Interpolate((int)p1.y, p1.x, (int)p2.y, p2.x);
            var h12 = Interpolate((int)p1.y, p1.h, (int)p2.y, p2.h);

            var x02 = Interpolate((int)p0.y, p0.x, (int)p2.y, p2.x);
            var h02 = Interpolate((int)p0.y, p0.h, (int)p2.y, p2.h);

            // Concatenate the short sides
            if (x01.Count > 0)
            {
                x01.RemoveAt(x01.Count - 1);
            }
            var x012 = x01.Concat(x12).ToList();

            if (h01.Count > 0)
            {
                h01.RemoveAt(h01.Count - 1);
            }
            var h012 = h01.Concat(h12).ToList();

            List<float> x_left, x_right, h_left, h_right;

            // Determine which is left and which is right
            int m = (int)Math.Floor((double)(x02.Count / 2));
            if (x02[m] < x012[m])
            {
                x_left = x02;
                h_left = h02;

                x_right = x012;
                h_right = h012;
            }
            else
            {
                x_left = x012;
                h_left = h012;

                x_right = x02;
                h_right = h02;
            }

            // Draw the horizontal segments
            for (int y = (int)p0.y; y <= (int)p2.y; y++)
            {
                int x_l = (int)x_left[y - (int)p0.y];
                int x_r = (int)x_right[y - (int)p0.y];

                List<float> h_segment = Interpolate(x_l, h_left[y - (int)p0.y], x_r, h_right[y - (int)p0.y]);

                for (int x = x_l; x <= x_r; x++)
                {
                    PutPixelOriginal(x, y, Multiply(h_segment[x - x_l], color));
                }
            }
        }

        public Vertex ViewportToCanvas(Vertex p2d)
        {
            float vW = (float)Width / Height;
            return new Vertex((p2d.x * Width / vW), (p2d.y * Height / viewport_size), 0, p2d.h);
        }

        public Vertex ProjectVertex(Vertex v)
        {
            return ViewportToCanvas(new Vertex(v.x * projection_plane_z / v.z, v.y * projection_plane_z / v.z, 0, v.h));
        }

        public void RenderTriangle(Triangle triangle, List<Vertex> projected)
        {
            DrawWireframeTriangle(projected[triangle.v0],
                                  projected[triangle.v1],
                                  projected[triangle.v2],
                                  triangle.color);
        }

        public void RenderModel(Instance instance, Mtx transform)
        {
            List<Vertex> projected = new List<Vertex>();
            Model model = instance.model;

            if (fileLoaded)
            {
                for (int i = 0; i < model.vertices.Length; i++)
                {
                    projected.Add(ProjectVertex(transform * model.vertices[i]));
                }

                for (int i = 0; i < model.triangles.Length; i++)
                {
                    HiddenFaces(model.triangles[i], model.vertices);
                    RenderTriangle(model.triangles[i], projected);
                }
            }
        }

        public void RenderScene(Camera camera, List<Instance> instances)
        {
            Mtx cameraMatrix;
            Mtx transform;

            cameraMatrix = (camera.orientation.Transposed()) * Mtx.MakeTranslationMatrix(-camera.position) * Mtx.FOV();
            for (int i = 0; i < instances.Count; i++)
            {
                transform = (cameraMatrix * instances[i].transform);
                RenderModel(instances[i], transform);
            }
        }

        private Color Multiply(float factor, Color color)
        {
            return Color.FromArgb((int)(color.R * factor), (int)(color.G * factor), (int)(color.B * factor));
        }
    }
}
