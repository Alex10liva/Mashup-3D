using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obj
{
    public class ObjFileReader
    {
        public List<Vertex> vertices = new List<Vertex>();
        public List<Vertex> normals = new List<Vertex>();
        public List<Vertex> texCoords = new List<Vertex>();
        public List<int> indices = new List<int>();
        public string name;

        public void LoadObj(string filePath)
        {
            name = Path.GetFileNameWithoutExtension(filePath);
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("v "))
                    {
                        // Vertex data
                        string[] values = line.Split(' ');
                        float x = float.Parse(values[1]);
                        float y = float.Parse(values[2]);
                        float z = float.Parse(values[3]);
                        vertices.Add(new Vertex(x, y, z));
                    }
                    else if (line.StartsWith("vn "))
                    {
                        // Normal data
                        string[] values = line.Split(' ');
                        float x = float.Parse(values[1]);
                        float y = float.Parse(values[2]);
                        float z = float.Parse(values[3]);
                        normals.Add(new Vertex(x, y, z));
                    }
                    else if (line.StartsWith("vt "))
                    {
                        // Texture coordinate data
                        string[] values = line.Split(' ');
                        float x = float.Parse(values[1]);
                        float y = float.Parse(values[2]);
                        texCoords.Add(new Vertex(x, y, 0));
                    }
                    else if (line.StartsWith("f "))
                    {
                        // Face data
                        string[] values = line.Split(' ');

                        for (int i = 1; i < values.Length; i++)
                        {
                            string[] indicesStr = values[i].Split('/');


                            // Vertex index
                            int vIndex = int.Parse(indicesStr[0]) - 1;

                            // Texture coordinate index
                            //int tIndex = int.Parse(indicesStr[1]) - 1;

                            // Normal index
                            //int nIndex = int.Parse(indicesStr[2]) - 1;

                            // Add the vertex, texture coordinate and normal indices to the index list
                            indices.Add(vIndex);
                            //indices.Add(tIndex);
                            //indices.Add(nIndex);
                        }
                    }
                }
            }

            // Create the mesh using the loaded data
            Mesh mesh = new Mesh(vertices.ToArray(), indices.ToArray());
            mesh.Normals = normals.ToArray();
            mesh.TexCoords = texCoords.ToArray();
        }
    }
}
