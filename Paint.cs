using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotVision
{
    public class Paint
    {
        public Vector3D mPos;   // Вектор позиции камеры
        public Vector3D mRot;   // Направление, куда смотрит камера
        public Vector3D size;
        public int color;

        public Paint(Vector3D p, Vector3D r, Vector3D s, int c)
        {
            mPos = p;
            mRot = r;
            size = s;
            color = c;
        }

        public Paint(string str)
        {
            string[] split = str.Split('.');
            mPos = Vector3D.Parse(split[0]);
            mRot = Vector3D.Parse(split[1]);
            size = Vector3D.Parse(split[2]);
            color = int.Parse(split[3]);
        }

        public override string ToString()
        {
            return mPos.ToString() + "." + mRot.ToString() + "." + size.ToString() + "." + color.ToString();
        }
    }
}
