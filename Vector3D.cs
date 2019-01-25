using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotVision
{
    public struct Vector3D
    {
        public double x, y, z;

        public Vector3D(double x0, double y0, double z0)
        { x = x0; y = y0; z = z0; }

        public void SetCoord(double x0, double y0, double z0)
        { x = x0; y = y0; z = z0; }

        public static Vector3D Parse(string str)
        {
            Vector3D vec = new Vector3D();
            try
            {
                string[] split = str.Split(';');
                vec.x = double.Parse(split[0]);
                vec.y = double.Parse(split[1]);
                vec.z = double.Parse(split[2]);
            }
            catch (Exception exp) { }
            return vec;
        }

        public double length()
        {
            if (x == -1 || y == -1 || z == -1) return double.MaxValue;
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)+Math.Pow(z,2));
        }

        public double length(Vector3D vec)
        {
            if (x == -1 || y == -1 || z == -1) return double.MaxValue;
            return Math.Sqrt(Math.Pow(x-vec.x, 2) + Math.Pow(y-vec.y, 2) + Math.Pow(z-vec.z, 2));
        }

        public Vector3D Normalize(Vector3D co)
        {
            Vector3D vec = new Vector3D();
            vec.x =x- co.x;
            vec.y =y- co.y;
            vec.z =z- co.z;
            double len = length();
            vec.x /= len;
            vec.y /= len;
            vec.z /= len;
            return vec;
        }

        public override string ToString()
        {
            return x.ToString()+";"+y.ToString()+";"+z.ToString();
        }
    };
}
