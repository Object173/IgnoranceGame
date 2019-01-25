using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Tao.FreeGlut;
using Tao.OpenGl;
using Tao.DevIl;
using Tao.Platform.Windows;
using System.IO;
using System.Threading;

namespace NotVision
{
    public partial class GameForm : Form
    {
        private class Key
        {
            public bool pressed=false;
            public Keys code;

            public Key(Keys k) { code = k; }
        }

        //клавиши
        Key kUp, kDown, kLeft, kRight;
        
        List<Paint> lPaint = new List<Paint>();
        double sizePaint = 10;

        // текстурный объект
        public List<uint> Textures = new List<uint>();
        public uint mGlTextureObject = 0;

        public Map map;
        double sizeBlock=15;
        Camera cam;

        public int PlayerColor = 0;

        public int Wid, Hei;
        bool fullscreen;

        //время
        DateTime time = DateTime.Now;
        static public float dtime;

        public Client player;
        Thread netThread;

        public GameForm()
        {
            CallBackMy.paintAddHandler = new CallBackMy.paintAddEvent(this.PaintAdd);
            InitializeComponent();
        }

        public GameForm(Client cl)
        {
            CallBackMy.paintAddHandler = new CallBackMy.paintAddEvent(this.PaintAdd);
            InitializeComponent();
            player = cl;
            PlayerColor = player.PlayerColor;

            map = player.map;
            map.StartMap();

            netThread = new Thread(new ThreadStart(player.Game));
            netThread.IsBackground = true;
            netThread.Start();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            //инициализация
            try
            {
                AnT.InitializeContexts();
                InitOptions();
                InitGL();
                InitKey();
                InitTexture();
            } catch (Exception exp) { }

            Cursor.Position = new Point(Left+Width/2, Top+Height/2);
            
            //спаун игрока
            Random r = new Random();
            int n = r.Next(0,map.kspawn);

            cam = new Camera(map,sizeBlock);
            cam.Position_Camera(map.spawn[n, 0]*sizeBlock, map.spawn[n, 1]*sizeBlock);
            
            //запуск таймера
            timer1.Start();
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        public void Refresh()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            
            Gl.glLoadIdentity();

            cam.Look();

            DrawMap();

            Gl.glFlush();

            AnT.Invalidate();
        }

        private void DrawMap()
        {
            Gl.glColor3f(1f, 1f, 1f);
            for (int i = 0; i < map.Wid; i++)
                for (int j = 0; j < map.Hei; j++)
                    if (map.GetBlock(i, j) == 1) DrawBlock(i, j);
            Gl.glColor3f(0f, 1.0f, 0f);
            DrawBlock(map.xe,map.ye);

            Gl.glColor3f(1f, 1f, 1f);

            Gl.glPushMatrix();
            for (int i = 0; i < 2; i++)
            {
                Gl.glTranslated(0,i*sizeBlock,0);
                Gl.glBegin(Gl.GL_QUADS);
                Gl.glVertex3d(map.Wid * sizeBlock, 0, map.Hei * sizeBlock);
                Gl.glTexCoord2d(0, 0);
                Gl.glVertex3d(map.Wid * sizeBlock, 0, 0);
                Gl.glTexCoord2d(1, 0);
                Gl.glVertex3d(0, 0, 0);
                Gl.glTexCoord2d(1, 1);
                Gl.glVertex3d(0, 0, map.Hei * sizeBlock);
                Gl.glTexCoord2d(0, 1);
                Gl.glEnd();
            }
            Gl.glPopMatrix();

            foreach (Paint p in lPaint) DrawPaint(p);
        }

        private void DrawBlock(int x, int z)
        {
            Gl.glPushMatrix();

            Gl.glTranslated(x*sizeBlock, sizeBlock/2, z*sizeBlock);
            Glut.glutSolidCube(sizeBlock);

            Gl.glPopMatrix();
        }

        private void DrawPaint(Paint paint)
        {
            Gl.glPolygonOffset(-1, -1);

            Gl.glEnable(Gl.GL_POLYGON_OFFSET_FILL);

            // включаем режим текстурирования 
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            // включаем режим текстурирования, указывая идентификатор mGlTextureObject 
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, Textures[paint.color]);

            // сохраняем состояние матрицы 
            Gl.glPushMatrix();

            // выполняем перемещение для более наглядного представления сцены 
            Gl.glTranslated(paint.mPos.x, paint.mPos.y, paint.mPos.z);
            // реализуем поворот объекта 
            if (Math.Abs(paint.mRot.x) == 1) Gl.glRotated(90, 0, paint.mRot.x, 0);
            if (paint.mRot.y == 1) Gl.glRotated(90, 1, 0, 0);
            if (paint.mRot.z == -1) Gl.glRotated(180, 0, 0, 1);

            // отрисовываем полигон 
            Gl.glBegin(Gl.GL_POLYGON);

            // указываем поочередно вершины и текстурные координаты 
            Gl.glVertex3d(paint.size.x, sizePaint / 2, 0);
            Gl.glTexCoord2d(0.5-paint.size.x/sizePaint, 0);
            Gl.glVertex3d(paint.size.x, -sizePaint / 2, 0);
            Gl.glTexCoord2d(paint.size.y / sizePaint + 0.5, 0);
            Gl.glVertex3d(-paint.size.y, -sizePaint / 2, 0);
            Gl.glTexCoord2d(paint.size.y / sizePaint + 0.5, 1);
            Gl.glVertex3d(-paint.size.y, sizePaint / 2, 0);
            Gl.glTexCoord2d(0.5-paint.size.x / sizePaint, 1);

            // завершаем отрисовку 
            Gl.glEnd();

            // возвращаем матрицу 
            Gl.glPopMatrix();
            // отключаем режим текстурирования 
            Gl.glDisable(Gl.GL_TEXTURE_2D);

            Gl.glDisable(Gl.GL_POLYGON_OFFSET_FILL);
        }

        // Инициализация OpenGL
        public void InitGL()
        {
            // инициализация Glut 
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

            // инициализация библиотеки openIL 
            Il.ilInit();
            Il.ilEnable(Il.IL_ORIGIN_SET);

            // отчитка окна 
            Gl.glClearColor(255, 255, 255, 1);

            // установка порта вывода в соответствии с размерами элемента anT 
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);


            // настройка проекции 
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(45, (float)AnT.Width / (float)AnT.Height, 0.1, 500);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            // настройка параметров OpenGL для визуализации 
            Gl.glEnable(Gl.GL_DEPTH_TEST);

            /*Gl.glEnable(Gl.GL_ALPHA_TEST);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);*/
        }

        //инициализация настроек
        private void InitOptions()
        {
            StreamReader reader = new StreamReader(Environment.CurrentDirectory + @"\Source\options.pop");
            string str;
            str = reader.ReadLine();
            str = reader.ReadLine();
            string[] size = str.Split('x');
            Wid = int.Parse(size[0]);
            Hei = int.Parse(size[1]);
            str = reader.ReadLine();
            if (str.IndexOf("1") > -1) fullscreen = true;
            else fullscreen = false;
            reader.Close();

            Width = Wid;
            Height = Hei;

            if (fullscreen)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
        }

        //инициализация кнопок
        private void InitKey()
        {
            kUp = new Key(Keys.W);
            kDown = new Key(Keys.S);
            kLeft = new Key(Keys.A);
            kRight = new Key(Keys.D);
        }

        private void InitTexture()
        {
            // Включаем прозрачность.
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);

            LoadTexture(Environment.CurrentDirectory + @"\Source\texture\black.png", 0);
            LoadTexture(Environment.CurrentDirectory + @"\Source\texture\red.png", 1);
            LoadTexture(Environment.CurrentDirectory + @"\Source\texture\orange.png", 2);
            LoadTexture(Environment.CurrentDirectory + @"\Source\texture\yellow.png", 3);
            LoadTexture(Environment.CurrentDirectory + @"\Source\texture\green.png", 4);
            LoadTexture(Environment.CurrentDirectory + @"\Source\texture\lblue.png", 5);
            LoadTexture(Environment.CurrentDirectory + @"\Source\texture\blue.png", 6);
            LoadTexture(Environment.CurrentDirectory + @"\Source\texture\violet.png", 7);
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Owner.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dtime = (float)((TimeSpan)(DateTime.Now - time)).TotalSeconds;
            time = DateTime.Now;
            KeyPressed();
            if (cam.isWin()) Win();
            MouseMove();
            Refresh();
        }

        public void MouseMove()
        {
                float ax, ay;

                ax = (Left + Width / 2 - Cursor.Position.X) / 5; //2 — чувствительность 
                ay = (Top + Height / 2 - Cursor.Position.Y) / 5;
                Cursor.Position = new Point(Left + Width / 2, Top + Height / 2);

                cam.Rotate_View(ax, ay);
        }

        public void KeyPressed()
        {
            if (kUp.pressed) cam.Move_View(1, 0);
            if (kDown.pressed) cam.Move_View(-1, 0);
            if (kLeft.pressed) cam.Move_View(0, -1);
            if (kRight.pressed) cam.Move_View(0, 1);
        }

        private void AnT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == kUp.code) kUp.pressed = true;
            if (e.KeyCode == kDown.code) kDown.pressed = true;
            if (e.KeyCode == kLeft.code) kLeft.pressed = true;
            if (e.KeyCode == kRight.code) kRight.pressed = true;
            if (e.KeyCode == Keys.Escape) Pause();

            if (e.KeyCode == Keys.NumPad9)
                PlayerColor++;
            if (e.KeyCode == Keys.NumPad6)
                PlayerColor--;
        }

        private void AnT_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == kUp.code) kUp.pressed = false;
            if (e.KeyCode == kDown.code) kDown.pressed = false;
            if (e.KeyCode == kLeft.code) kLeft.pressed = false;
            if (e.KeyCode == kRight.code) kRight.pressed = false;
        }

        private void Shot()
        {
            Vector3D Rot = new Vector3D(0, 1, 0);
            Vector3D Size = new Vector3D(sizePaint / 2, sizePaint / 2, 0);

            Vector3D vec = cam.RayCast(cam.Pos, cam.Rot, 0, 1, 0, 0);
            Vector3D nvec = cam.RayCast(cam.Pos, cam.Rot, 0, 1, 0, sizeBlock);

            if (vec.length(cam.Pos) > nvec.length(cam.Pos)) vec = nvec;
            if (!cam.isEmpty(vec.x, vec.z)) vec.x = -1;

            int dx, dz;
            if (cam.Rot.Normalize(cam.Pos).x >= 0) dx = 1;
            else dx = -1;
            if (cam.Rot.Normalize(cam.Pos).z >= 0) dz = 1;
            else dz = -1;

            double kd; int k;

            Vector3D xvec; Vector3D xsize = new Vector3D();
            double x0 = cam.Pos.x + sizeBlock / 2;
            int x = (int)(x0 / sizeBlock); if (x0 - (x + 1) * sizeBlock > 0) x++;
            for (int i = x + dx; true; i += dx)
            {
                xvec = cam.RayCast(cam.Pos, cam.Rot, 1, 0, 0, i * sizeBlock - dx * sizeBlock / 2);
                if (xvec.y > sizeBlock || xvec.y < 0) { xvec.x = -1; break; }
                if (!cam.isEmpty(xvec.x + dx, xvec.z))
                {
                    if (!cam.isEmpty(xvec.x - dx, xvec.z)) xvec.x = -1;
                    kd = xvec.z + sizeBlock / 2;
                    k = (int)(kd / sizeBlock); if (kd - (k + 1) * sizeBlock > 0) k++;
                    xsize.SetCoord(xvec.z - (k * sizeBlock - sizeBlock / 2), (k * sizeBlock + sizeBlock / 2) - xvec.z, 0);
                    if (xsize.x > sizePaint / 2 || !cam.isEmpty(xvec.x + dx, xvec.z - sizeBlock / 2)) xsize.x = sizePaint / 2;
                    if (xsize.y > sizePaint / 2 || !cam.isEmpty(xvec.x + dx, xvec.z + sizeBlock / 2)) xsize.y = sizePaint / 2;
                    if (dx == -1)
                    {
                        kd = xsize.x;
                        xsize.x = xsize.y;
                        xsize.y = kd;
                    }
                    break;
                }
            }

            Vector3D zvec; Vector3D zsize = new Vector3D();
            double z0 = cam.Pos.z + sizeBlock / 2;
            int z = (int)(z0 / sizeBlock); if (z0 - (z + 1) * sizeBlock > 0) z++;
            for (int i = z + dz; true; i += dz)
            {
                zvec = cam.RayCast(cam.Pos, cam.Rot, 0, 0, 1, i * sizeBlock - dz * sizeBlock / 2);

                if (zvec.y > sizeBlock || zvec.y < 0) { zvec.x = -1; break; }
                if (!cam.isEmpty(zvec.x, zvec.z + dz))
                {
                    if (!cam.isEmpty(zvec.x, zvec.z - dz)) zvec.x = -1;
                    kd = zvec.x + sizeBlock / 2;
                    k = (int)(kd / sizeBlock); if (kd - (k + 1) * sizeBlock > 0) k++;
                    zsize.SetCoord(zvec.x - (k * sizeBlock - sizeBlock / 2), (k * sizeBlock + sizeBlock / 2) - zvec.x, 0);
                    if (zsize.x > sizePaint / 2 || !cam.isEmpty(zvec.x - sizeBlock / 2, zvec.z + dz)) zsize.x = sizePaint / 2;
                    if (zsize.y > sizePaint / 2 || !cam.isEmpty(zvec.x + sizeBlock / 2, zvec.z + dz)) zsize.y = sizePaint / 2;
                    if (dz == 1)
                    {
                        kd = zsize.x;
                        zsize.x = zsize.y;
                        zsize.y = kd;
                    }
                    break;
                }
            }

            if (xvec.length(cam.Pos) < zvec.length(cam.Pos)) { nvec = xvec; Rot.SetCoord(dx, 0, 0); Size = xsize; }
            else { nvec = zvec; Rot.SetCoord(0, 0, dz); Size = zsize; }
            if (vec.length(cam.Pos) > nvec.length(cam.Pos)) vec = nvec;
            else { Rot.SetCoord(0, 1, 0); Size.SetCoord(sizePaint / 2, sizePaint / 2, 0); }

            Paint nPaint = new Paint(vec, Rot, Size, PlayerColor);

            player.kraska++;
            player.showMessage("PAINT+");
            player.showMessage(nPaint.ToString());
        }

        public void PaintAdd(string str)
        {
            lPaint.Add(new Paint(str));
        }

        private void AnT_MouseClick(object sender, MouseEventArgs e)
        {
            Shot();
        }

        private void LoadTexture(string fname, int id)
        {
            // создаем изображение с идентификатором imageId 
            Il.ilGenImages(1, out id);
            // делаем изображение текущим 
            Il.ilBindImage(id);

            // пробуем загрузить изображение 
            if (Il.ilLoadImage(fname))
            {

                // если загрузка прошла успешно 
                // сохраняем размеры изображения 
                int width = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
                int height = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);

                // определяем число бит на пиксель 
                int bitspp = Il.ilGetInteger(Il.IL_IMAGE_BITS_PER_PIXEL);
                
                switch (bitspp) // в зависимости от полученного результата 
                {
                    // создаем текстуру, используя режим GL_RGB или GL_RGBA 
                    case 24:
                        mGlTextureObject = MakeGlTexture(Gl.GL_RGB, Il.ilGetData(), width, height);
                        break;
                    case 32:
                        mGlTextureObject = MakeGlTexture(Gl.GL_RGBA, Il.ilGetData(), width, height);
                        break;
                }
                // очищаем память 
                Il.ilDeleteImages(1, ref id);

                Textures.Add(mGlTextureObject);
            }

      }

        // создание текстуры в памяти openGL 
        private static uint MakeGlTexture( int Format, IntPtr pixels, int w, int h)
        {
            // идентификатор текстурного объекта 
            uint texObject;

            // генерируем текстурный объект 
            Gl.glGenTextures(1, out texObject);

            // устанавливаем режим упаковки пикселей 
            Gl.glPixelStorei( Gl.GL_UNPACK_ALIGNMENT, 1);

            // создаем привязку к только что созданной текстуре 
            Gl.glBindTexture( Gl.GL_TEXTURE_2D, texObject);

            // устанавливаем режим фильтрации и повторения текстуры 
            Gl.glTexParameteri( Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_CLAMP);
            Gl.glTexParameteri( Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_CLAMP);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            Gl.glTexEnvf( Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_DECAL);

            // создаем RGB или RGBA текстуру 
            switch (Format)
            { 
                case Gl.GL_RGB:
                    Gl.glTexImage2D( Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB, w, h, 0, Gl.GL_RGB, Gl.GL_UNSIGNED_BYTE, pixels);
                break;

                case Gl.GL_RGBA:
                    Gl.glTexImage2D( Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, w, h, 0, Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, pixels);
                break;
            }

            // возвращаем идентификатор текстурного объекта 
            return texObject;
        }

        public void Start()
        {
            timer1.Enabled = true;
        }

        private void Pause()
        {
            PauseMenu form = new PauseMenu();
            form.Owner = this;
            timer1.Enabled = false;
            form.ShowDialog();
        }

        public void Exit()
        {
            GameMenu menu=(GameMenu)Owner;
            menu.netThread.Resume();
            Owner.Show();
            Close();
        }

        private void Win()
        {
            timer1.Enabled = false;

            WinForm winform = new WinForm(Owner);

            netThread.Suspend();

            winform.Owner = this;
            winform.ShowDialog();

            Close();
        }

    }



}
