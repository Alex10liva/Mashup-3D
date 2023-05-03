using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obj
{
    public class Util
    {
        private static Util ins;

        public static Util Ins
        {
            get
            {
                if (ins == null)
                    ins = new Util();
                return ins;
            }
            set { ins = value; }
        }

        public PointF NextStep(PointF position, PointF direction, float alpha)
        {
            PointF res;

            res = new PointF();
            res.X = ((1 - alpha) * position.X) + ((alpha) * direction.X);
            res.Y = ((1 - alpha) * position.Y) + ((alpha) * direction.Y);

            return res;
        }
    }
}
