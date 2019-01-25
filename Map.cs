using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotVision
{
    [Serializable]
    public class Map
    {
        int[,] mas;
        public int Wid;
        public int Hei;

        public int xe = -1, ye = -1;

        public int kspawn=0;
        public int[,] spawn;

        public Map(int w, int h)
        {
            Wid = w + 2; Hei = h + 2;
            xe = -1; ye = -1;
            mas = new int[Wid, Hei];
            for (int i = 0; i < Wid; i++)
                for (int j = 0; j < Hei; j++)
                    if (i == 0 || i == Wid - 1 || j == 0 || j == Hei - 1) mas[i, j] = 1;
                    else mas[i, j] = 0;
        }

        public void SetBlock(int x, int y, int k)
        {
            if (x < 0 || x > Wid - 1 || y < 0 || y > Hei - 1) return;
            if (k == 0)
            {
                if(x == xe && y == ye) xe = ye = -1;
                if (mas[x, y] == 2) kspawn--;
            }
            if (k == 2 && mas[x,y]!=2) kspawn++;
            if (k == 3)
            {
                if (xe > -1 && ye > -1) mas[xe, ye] = 0;
                xe = x; ye = y;
                mas[x, y] = 3;
            }
            else mas[x, y] = k;
        }

        public int GetBlock(int x, int y)
        {
            if (x < 0 || x > Wid - 1 || y < 0 || y > Hei - 1) return -1;
            return mas[x, y];
        }

        public void StartMap()
        {
            spawn = new int[kspawn, 2];
            int k = 0;
            for (int i = 1; i < Wid - 1; i++)
                for (int j = 1; j < Hei - 1; j++)
                {
                    switch (mas[i, j])
                    {
                        case 2: spawn[k,0]=i;
                                spawn[k, 1] = j;
                            mas[i, j] = 0;
                            k++;
                            break;
                        case 3: xe = i;
                            ye = j;
                            //mas[i, j] = 0;
                            break;
                    }
                }
        }
    }
}
