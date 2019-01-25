using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;

namespace NotVision
{
    public class Camera
    {
        private Vector3D mPos;  // Вектор позиции камеры
        private Vector3D mView;  // Направление, куда смотрит камера
        private Vector3D angle;

        public Vector3D Pos { get { return mPos; } }
        public Vector3D Rot { get { return mView; } }

        double speed = 10;
        double height = 5;
        double dist = 0.5;

        Map map;
        double sizeBlock;

        public Camera(Map m, double s)
        {
            mPos.y = height; mPos.x = mPos.z = 0f;
            mView.x = mView.y = 0; mView.z = 1f;
            angle.x = angle.y = angle.z = 0f;
            Rotate_View(0, 0);
            map = m;
            sizeBlock = s;
        }

        public void Position_Camera(double pos_x, double pos_z)
        {
            mPos.x = pos_x;
            mPos.z = pos_z;
        }

        public void Position_Camera(float pos_x, float pos_y, float pos_z,
        float view_x, float view_y, float view_z)
        {
            mPos.x = pos_x; // Позиция камеры
            mPos.y = pos_y; //
            mPos.z = pos_z; //
            mView.x = view_x; // Куда смотрит, т.е. взгляд
            mView.y = view_y; //
            mView.z = view_z; //
            angle.x = angle.y = 0;
        }

        public void Rotate_View(double ax, double ay)
        {
            angle.x += ax;
            angle.y += ay;

            if (angle.x > 360) angle.x -= 360;
            if (angle.x < 0) angle.x += 360;

            if (angle.y < -89.0f) angle.y = -89.0f;
            if (angle.y > 89.0f) angle.y = 89.0f;

            mView.x = mPos.x - Math.Sin(angle.x / 180 * Math.PI);
            mView.y = mPos.y + (Math.Tan(angle.y / 180 * Math.PI));
            mView.z = mPos.z - Math.Cos(angle.x / 180 * Math.PI);
        }

        public void Move_View(int mw, int ms)
        {
            double dx = 0, dz = 0;
            if (mw > 0) //вперед
            {
                dx -= Math.Sin(angle.x / 180 * Math.PI) * speed * GameForm.dtime;
                dz -= Math.Cos(angle.x / 180 * Math.PI) * speed * GameForm.dtime;
            }
            if (mw < 0) //назад
            {
                dx += Math.Sin(angle.x / 180 * Math.PI) * speed * GameForm.dtime;
                dz += Math.Cos(angle.x / 180 * Math.PI) * speed * GameForm.dtime;
            }
            if (ms < 0)//влево
            {
                dx += Math.Sin((angle.x - 90) / 180 * Math.PI) * speed * GameForm.dtime;
                dz += Math.Cos((angle.x - 90) / 180 * Math.PI) * speed * GameForm.dtime;
            }
            if (ms > 0)//вправо
            {
                dx += Math.Sin((angle.x + 90) / 180 * Math.PI) * speed * GameForm.dtime;
                dz += Math.Cos((angle.x + 90) / 180 * Math.PI) * speed * GameForm.dtime;
            }

            if (isEmpty(mPos.x + dx + dist, mPos.z) && isEmpty(mPos.x + dx - dist, mPos.z)) mPos.x += dx;
            if (isEmpty(mPos.x, mPos.z + dz + dist) && isEmpty(mPos.x, mPos.z + dz - dist)) mPos.z += dz;
        }

        public bool isWin()
        {
            double x0 = mPos.x, y0 = mPos.z;
            x0 += sizeBlock / 2; y0 += sizeBlock / 2;
            int x = (int)(x0 / sizeBlock); if (x0 - (x + 1) * sizeBlock > 0) x++;
            int y = (int)(y0 / sizeBlock); if (y0 - (y + 1) * sizeBlock > 0) y++;
            if (x == map.xe && y == map.ye) return true;
            else return false;
        }

        public bool isEmpty(double x0, double y0)
        {
            x0 += sizeBlock / 2; y0 += sizeBlock / 2;
            int x = (int)(x0 / sizeBlock); if (x0 - (x + 1) * sizeBlock > 0) x++;
            int y = (int)(y0 / sizeBlock); if (y0 - (y + 1) * sizeBlock > 0) y++;
            if (map.GetBlock(x, y) == 0 || map.GetBlock(x, y) == 3) return true;
            else return false;
        }

        public Vector3D RayCast(Vector3D m0, Vector3D q, double px, double py, double pz, double pd)
        {
            q = q.Normalize(m0);
            Vector3D vec = new Vector3D();
            double c;
            if (Math.Abs(px) == 1)
            {
                c = (pd - m0.x) / q.x;
                if (c * px > 0)
                {
                    vec.x = pd;
                    vec.y = c * q.y + m0.y;
                    vec.z = c * q.z + m0.z;
                }
                else vec.SetCoord(-1, -1, -1);
                return vec;
            }
            if (Math.Abs(py) == 1)
            {
                c = (pd - m0.y) / q.y;
                if (c * py > 0)
                {
                    vec.y = pd;
                    vec.x = c * q.x + m0.x;
                    vec.z = c * q.z + m0.z;
                }
                else vec.SetCoord(-1, -1, -1);
                return vec;
            }
            if (Math.Abs(pz) == 1)
            {
                c = (pd - m0.z) / q.z;
                if (c * pz > 0)
                {
                    vec.z = pd;
                    vec.y = c * q.y + m0.y;
                    vec.x = c * q.x + m0.x;
                }
                else vec.SetCoord(-1, -1, -1);
                return vec;
            }
            return vec;

        }

        public void Look()
        {
            Glu.gluLookAt(mPos.x, mPos.y, mPos.z, // Ранее упомянутая команда 
                          mView.x, mView.y, mView.z,
                          0, 1, 0);
        }

    }
}
