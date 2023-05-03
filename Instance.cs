using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Obj
{
    public class Instance
    {
        public Model model;
        public Vertex position;
        public Mtx orientation;
        public float scale;
        public Mtx transform;

        public Instance(Model model, Vertex position, Mtx orientation = null, float scale = 1.0f)
        {
            this.model = model;
            this.position = position;
            this.orientation = orientation ?? Mtx.Identity;
            this.scale = scale;
            UpdateInstance();
        }

        public void UpdateInstance()
        {
            this.transform = Mtx.MakeTranslationMatrix(this.position) * this.orientation * Mtx.MakeScalingMatrix(this.scale);
        }

        public override string ToString()
        {
            return this.model + " " + this.position + " " + this.orientation + " " + this.scale;
        }

        public Model getModel()
        {
            return this.model;
        }

        public Vertex getPosition()
        {
            return this.position;
        }

        public Mtx getOrientation()
        {
            return this.orientation;
        }

        public float getScale()
        {
            return this.scale;
        }
    }
}
