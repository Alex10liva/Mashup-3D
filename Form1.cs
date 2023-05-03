using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Obj.Instance;

namespace Obj
{
    public partial class Form1 : Form
    {
        Canvas canvas;
        Graphics g;
        Point l1, l2, l3, l4;
        Point origen;
        int width, height, wMid, hMid;
        Pen marcadorL = new Pen(Color.Gray, 1);
        bool loadedOnce;
        ObjFileReader obj;

        bool IsMouseDownX = false;
        float deltaX = 0;
        float deltaPositionX = 0;

        bool IsMouseDownY = false;
        float deltaY = 0;
        float deltaPositionY = 0;

        bool IsMouseDownZ = false;
        float deltaZ = 0;
        float deltaPositionZ = 0;

        bool IsMouseDownScale = false;
        float deltaScale = 2;


        float topZ = 10;
        Point ptX, ptY, ptZ, ptScale;
        Bitmap bmpX, bmpY, bmpZ, bmpScale;
        Graphics gX, gY, gZ, gScale;

        int selectedFigure;

        private Mtx currentRotation = Mtx.Identity; // matriz de rotación actual
        Mtx cameraMatrix;

        List<Instance> savedInstances0, savedInstances1, savedInstances2, savedInstances3, savedInstances4, savedInstances5;

        bool recorded0, recorded1, recorded2, recorded3, recorded4, recorded5;

        bool scrollValue0, scrollValue1, scrollValue2, scrollValue3, scrollValue4, scrollValue5;
        bool timerPlayBtn = false;
        int recordedRestFigures = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                PCT_Canvas.Width = panel4.Width - (panel5.Width + panel2.Width);
                PCT_Canvas.Height = panel4.Height - (panel3.Height);

                Init();
                this.MinimumSize = this.Size;
            }
        }
        public void Init()
        {
            canvas = new Canvas(PCT_Canvas.Size);
            PCT_Canvas.Image = canvas.bmp;
            g = Graphics.FromImage(canvas.bmp);

            cameraMatrix = (canvas.camera.orientation.Transposed()) * Mtx.MakeTranslationMatrix(-canvas.camera.position) * Mtx.FOV();
            recorded0 = recorded1 = recorded2 = recorded3 = recorded4 = recorded5 = false;
            scrollValue0 = scrollValue1 = scrollValue2 = scrollValue3 = scrollValue4 = scrollValue5 = false;

            width = PCT_Canvas.Width;
            height = PCT_Canvas.Height;
            wMid = width / 2;
            hMid = height / 2;
            origen = new Point(wMid, hMid);

            Rotate_RadioBTN.Checked = true;
            Move_RadioBTN.Checked = false;

            InitPB();
            PCT_Canvas.Invalidate();
            loadedOnce = true;

            PCT_SLIDEER_X.Width = panel4.Width - (panel5.Width + panel2.Width);
            PCT_SLIDEER_Y.Width = panel4.Width - (panel5.Width + panel2.Width);
            PCT_SLIDEER_Z.Width = panel4.Width - (panel5.Width + panel2.Width);
            PCT_SLIDER_SCALE.Height = panel5.Height - (panel1.Height + panel3.Height);

            bmpX = new Bitmap(PCT_SLIDEER_X.Width, PCT_SLIDEER_X.Height);
            gX = Graphics.FromImage(bmpX);
            PCT_SLIDEER_X.Image = bmpX;
            gX.DrawLine(Pens.DimGray, 0, bmpX.Height / 2, bmpX.Width, bmpX.Height / 2);
            gX.FillEllipse(Brushes.Aquamarine, bmpX.Width / 2, bmpX.Height / 4, bmpX.Height / 2, bmpX.Height / 2);

            bmpY = new Bitmap(PCT_SLIDEER_Y.Width, PCT_SLIDEER_Y.Height);
            gY = Graphics.FromImage(bmpY);
            PCT_SLIDEER_Y.Image = bmpY;
            gY.DrawLine(Pens.DimGray, 0, bmpY.Height / 2, bmpY.Width, bmpY.Height / 2);
            gY.FillEllipse(Brushes.Aquamarine, bmpY.Width / 2, bmpY.Height / 4, bmpY.Height / 2, bmpY.Height / 2);

            bmpZ = new Bitmap(PCT_SLIDEER_Z.Width, PCT_SLIDEER_Z.Height);
            gZ = Graphics.FromImage(bmpZ);
            PCT_SLIDEER_Z.Image = bmpZ;
            gZ.DrawLine(Pens.DimGray, 0, bmpZ.Height / 2, bmpZ.Width, bmpZ.Height / 2);
            gZ.FillEllipse(Brushes.Aquamarine, bmpZ.Width / 2, bmpZ.Height / 4, bmpZ.Height / 2, bmpZ.Height / 2);

            bmpScale = new Bitmap(PCT_SLIDER_SCALE.Width, PCT_SLIDER_SCALE.Height);
            gScale = Graphics.FromImage(bmpScale);
            PCT_SLIDER_SCALE.Image = bmpScale;
            gScale.DrawLine(Pens.DimGray, bmpScale.Width / 2, 0, bmpScale.Width / 2, bmpScale.Height);
            gScale.FillEllipse(Brushes.Aquamarine, bmpScale.Width / 4, bmpScale.Height / 2, bmpX.Height / 2, bmpX.Height / 2);

        }

        private void FileBTN_Click(object sender, EventArgs e)
        {
            if (!canvas.fileLoaded)
            {
                obj = canvas.ButtonClicked();
            }
            else
            {
                canvas.fileLoaded = false;
                obj = canvas.ButtonClicked();
            }

            //nums.Last();
            canvas.instances.Last().scale = deltaScale;
            canvas.instances.Last().UpdateInstance();

            int stockCounter = 1;
            TreeNode node;

            foreach (TreeNode element in treeView.Nodes)
            {
                string nodeName = element.Text;
                string nameToCompare = "Fig " + obj.name;

                if (stockCounter > 1)
                {
                    int difference = nodeName.Length - nameToCompare.Length;
                    if (difference > 0 && nodeName.Length > difference)
                    {
                        string newNodeName = nodeName.Remove(nodeName.Length - difference);
                        if (newNodeName == nameToCompare)
                            stockCounter++;
                    }
                }

                if (stockCounter == 1 && nodeName == nameToCompare)
                    stockCounter++;
            }

            if (stockCounter == 1)
                node = new TreeNode("Fig " + obj.name);
            else
                node = new TreeNode("Fig " + obj.name + " " + stockCounter);
            node.Tag = obj;
            treeView.Nodes.Add(node);
        }

        private void recordButton_Click(object sender, EventArgs e)
        {
            if (!timerPlayBtn)
            {
                // Obtener el valor del trackbar para saber en que "segundo" se esta
                switch (trackBarTime.Value)
                {
                    case 0:
                        savedInstances0 = new List<Instance>();
                        // En el segundo 0 creamos una nueva instancia con los valores de las instancias que estan creadas
                        // y la almacenamos en la lista savedInstances0
                        for (int i = 0; i < canvas.nFigures; i++)
                        {
                            Instance newInstance = new Instance(canvas.instances[i].getModel(), canvas.instances[i].getPosition(), canvas.instances[i].getOrientation(), canvas.instances[i].getScale());
                            savedInstances0.Add(newInstance);
                        }
                        recorded0 = true;
                        break;
                    case 1:
                        savedInstances1 = new List<Instance>();
                        // En el segundo 0 creamos una nueva instancia con los valores de las instancias que estan creadas
                        // y la almacenamos en la lista savedInstances1
                        for (int i = 0; i < canvas.nFigures; i++)
                        {
                            Instance newInstance = new Instance(canvas.instances[i].getModel(), canvas.instances[i].getPosition(), canvas.instances[i].getOrientation(), canvas.instances[i].getScale());
                            savedInstances1.Add(newInstance);
                        }
                        recorded1 = true;
                        break;
                    case 2:
                        savedInstances2 = new List<Instance>();
                        // En el segundo 0 creamos una nueva instancia con los valores de las instancias que estan creadas
                        // y la almacenamos en la lista savedInstances2
                        for (int i = 0; i < canvas.nFigures; i++)
                        {
                            Instance newInstance = new Instance(canvas.instances[i].getModel(), canvas.instances[i].getPosition(), canvas.instances[i].getOrientation(), canvas.instances[i].getScale());
                            savedInstances2.Add(newInstance);
                        }
                        recorded2 = true;
                        break;
                    case 3:
                        savedInstances3 = new List<Instance>();
                        // En el segundo 0 creamos una nueva instancia con los valores de las instancias que estan creadas
                        // y la almacenamos en la lista savedInstances3
                        for (int i = 0; i < canvas.nFigures; i++)
                        {
                            Instance newInstance = new Instance(canvas.instances[i].getModel(), canvas.instances[i].getPosition(), canvas.instances[i].getOrientation(), canvas.instances[i].getScale());
                            savedInstances3.Add(newInstance);
                        }
                        recorded3 = true;
                        break;
                    case 4:
                        savedInstances4 = new List<Instance>();
                        // En el segundo 0 creamos una nueva instancia con los valores de las instancias que estan creadas
                        // y la almacenamos en la lista savedInstances4
                        for (int i = 0; i < canvas.nFigures; i++)
                        {
                            Instance newInstance = new Instance(canvas.instances[i].getModel(), canvas.instances[i].getPosition(), canvas.instances[i].getOrientation(), canvas.instances[i].getScale());
                            savedInstances4.Add(newInstance);
                        }
                        recorded4 = true;
                        break;
                    case 5:
                        savedInstances5 = new List<Instance>();
                        // En el segundo 0 creamos una nueva instancia con los valores de las instancias que estan creadas
                        // y la almacenamos en la lista savedInstances5
                        for (int i = 0; i < canvas.nFigures; i++)
                        {
                            Instance newInstance = new Instance(canvas.instances[i].getModel(), canvas.instances[i].getPosition(), canvas.instances[i].getOrientation(), canvas.instances[i].getScale());
                            savedInstances5.Add(newInstance);
                        }
                        recorded5 = true;
                        break;
                }
            }
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            if (!timerPlayBtn)
            {
                trackBarTime.Value = 0;
                TIMER_PlayButton.Start();
                timerPlayBtn = true;
                playButton.Text = "Stop";
                canvas.Render();
            }
        }

        private void TIMER_PlayButton_Tick(object sender, EventArgs e)
        {
            if (trackBarTime.Value > 4)
            {
                trackBarTime.Value = 5;
                TIMER_PlayButton.Stop();
                timerPlayBtn = false;
                playButton.Text = "Play";
                canvas.Render();
            }
        }

        public void Follow(List<Instance> savedInstanceFrom, List<Instance> savedInstanceTo, float timeToReachTargetInSeconds)
        {
            for (int i = 0; i < savedInstanceFrom.Count; i++)
            {
                Instance instanceFromCopy = new Instance(savedInstanceFrom[i].getModel(), savedInstanceFrom[i].getPosition(), savedInstanceFrom[i].getOrientation(), savedInstanceFrom[i].getScale());
                Instance instanceToCopy = new Instance(savedInstanceTo[i].getModel(), savedInstanceTo[i].getPosition(), savedInstanceTo[i].getOrientation(), savedInstanceTo[i].getScale());

                float directionX = instanceToCopy.position.X - instanceFromCopy.position.X;
                float directionY = instanceToCopy.position.Y - instanceFromCopy.position.Y;
                float directionZ = instanceToCopy.position.Z - instanceFromCopy.position.Z;

                float distance = (float)Math.Sqrt(directionX * directionX + directionY * directionY + directionZ * directionZ);

                directionX /= distance;
                directionY /= distance;
                directionZ /= distance;

                float speed;

                if (trackBarTime.Value > 4)
                    speed = distance;
                else
                    speed =  timeToReachTargetInSeconds;

                if (instanceFromCopy.getPosition() != instanceToCopy.getPosition())
                {
                    canvas.instances[i].position.X += directionX * speed;
                    canvas.instances[i].position.Y += directionY * speed;
                    canvas.instances[i].position.Z += directionZ * speed;

                    float remainingDistance = (float)Math.Sqrt(
                        (instanceToCopy.position.X - instanceFromCopy.position.X) * (instanceToCopy.position.X - instanceFromCopy.position.X) +
                        (instanceToCopy.position.Y - instanceFromCopy.position.Y) * (instanceToCopy.position.Y - instanceFromCopy.position.Y) +
                        (instanceToCopy.position.Z - instanceFromCopy.position.Z) * (instanceToCopy.position.Z - instanceFromCopy.position.Z));

                    if (remainingDistance < speed)
                    {
                        canvas.instances[i].position = instanceToCopy.getPosition();
                        if (trackBarTime.Value != 5)
                            trackBarTime.Value++;
                    }
                }
                canvas.instances[i].UpdateInstance();
            }
        }


        private void trackBarTime_Scroll(object sender, EventArgs e)
        {
            if (!timerPlayBtn)
            {
                // Cada que se hace scroll se obtiene el valor del track bar
                switch (trackBarTime.Value)
                {
                    case 0:
                        scrollValue1 = scrollValue2 = scrollValue3 = scrollValue4 = scrollValue5 = false;
                        // Si el valor es 0 (segundo 0) y ya se guardo el estado en el segundo 0
                        if (recorded0)
                        {
                            scrollValue0 = true;

                            if (canvas.nFigures != savedInstances0.Count)
                                recordedRestFigures = canvas.nFigures - savedInstances0.Count;
                            else
                                recordedRestFigures = canvas.nFigures;
                            // Le damos a las instancias del canvas los valores que guardamos en savedInstances0
                            for (int i = 0; i < recordedRestFigures; i++)
                            {
                                Instance newInstance = new Instance(savedInstances0[i].getModel(), savedInstances0[i].getPosition(), savedInstances0[i].getOrientation(), savedInstances0[i].getScale());
                                canvas.instances[i] = newInstance;
                            }
                        }
                        break;
                    case 1:
                        scrollValue0 = scrollValue2 = scrollValue3 = scrollValue4 = scrollValue5 = false;
                        // Si el valor es 1 (segundo 1) y ya se guardo el estado en el segundo 1
                        if (recorded1)
                        {
                            scrollValue1 = true;

                            if (canvas.nFigures != savedInstances1.Count)
                                recordedRestFigures = canvas.nFigures - savedInstances1.Count;
                            else
                                recordedRestFigures = canvas.nFigures;

                            // Le damos a las instancias del canvas los valores que guardamos en savedInstances1
                            for (int i = 0; i < recordedRestFigures; i++)
                            {
                                Instance newInstance = new Instance(savedInstances1[i].getModel(), savedInstances1[i].getPosition(), savedInstances1[i].getOrientation(), savedInstances1[i].getScale());
                                canvas.instances[i] = newInstance;
                            }
                        }
                        break;
                    case 2:
                        scrollValue0 = scrollValue1 = scrollValue3 = scrollValue4 = scrollValue5 = false;
                        // Si el valor es 2 (segundo 2) y ya se guardo el estado en el segundo 2
                        if (recorded2)
                        {
                            scrollValue2 = true;

                            if (canvas.nFigures != savedInstances2.Count)
                                recordedRestFigures = canvas.nFigures - savedInstances2.Count;
                            else
                                recordedRestFigures = canvas.nFigures;

                            // Le damos a las instancias del canvas los valores que guardamos en savedInstances2
                            for (int i = 0; i < recordedRestFigures; i++)
                            {
                                Instance newInstance = new Instance(savedInstances2[i].getModel(), savedInstances2[i].getPosition(), savedInstances2[i].getOrientation(), savedInstances2[i].getScale());
                                canvas.instances[i] = newInstance;
                            }
                        }
                        break;
                    case 3:
                        scrollValue0 = scrollValue1 = scrollValue2 = scrollValue4 = scrollValue5 = false;
                        // Si el valor es 3 (segundo 3) y ya se guardo el estado en el segundo 3
                        if (recorded3)
                        {
                            scrollValue3 = true;

                            if (canvas.nFigures != savedInstances3.Count)
                                recordedRestFigures = canvas.nFigures - savedInstances3.Count;
                            else
                                recordedRestFigures = canvas.nFigures;

                            // Le damos a las instancias del canvas los valores que guardamos en savedInstances3
                            for (int i = 0; i < recordedRestFigures; i++)
                            {
                                Instance newInstance = new Instance(savedInstances3[i].getModel(), savedInstances3[i].getPosition(), savedInstances3[i].getOrientation(), savedInstances3[i].getScale());
                                canvas.instances[i] = newInstance;
                            }
                        }
                        break;
                    case 4:
                        scrollValue0 = scrollValue1 = scrollValue2 = scrollValue3 = scrollValue5 = false;
                        // Si el valor es 4 (segundo 4) y ya se guardo el estado en el segundo 4
                        if (recorded4)
                        {
                            scrollValue4 = true;

                            if (canvas.nFigures != savedInstances4.Count)
                                recordedRestFigures = canvas.nFigures - savedInstances4.Count;
                            else
                                recordedRestFigures = canvas.nFigures;

                            // Le damos a las instancias del canvas los valores que guardamos en savedInstances4
                            for (int i = 0; i < recordedRestFigures; i++)
                            {
                                Instance newInstance = new Instance(savedInstances4[i].getModel(), savedInstances4[i].getPosition(), savedInstances4[i].getOrientation(), savedInstances4[i].getScale());
                                canvas.instances[i] = newInstance;
                            }
                        }
                        break;
                    case 5:
                        scrollValue0 = scrollValue1 = scrollValue2 = scrollValue3 = scrollValue4 = false;
                        // Si el valor es 5 (segundo 5) y ya se guardo el estado en el segundo 5
                        if (recorded5)
                        {
                            scrollValue5 = true;

                            if (canvas.nFigures != savedInstances5.Count)
                                recordedRestFigures = canvas.nFigures - savedInstances5.Count;
                            else
                                recordedRestFigures = canvas.nFigures;

                            // Le damos a las instancias del canvas los valores que guardamos en savedInstances5
                            for (int i = 0; i < recordedRestFigures; i++)
                            {
                                Instance newInstance = new Instance(savedInstances5[i].getModel(), savedInstances5[i].getPosition(), savedInstances5[i].getOrientation(), savedInstances5[i].getScale());
                                canvas.instances[i] = newInstance;
                            }

                            scrollValue0 = true;

                        }
                        break;
                }
            }
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Sacar el index para saber que figura sera modificada
            selectedFigure = treeView.SelectedNode.Index;

            deltaScale = canvas.instances[selectedFigure].scale;
            currentRotation = canvas.instances[selectedFigure].orientation;
            deltaPositionX = canvas.instances[selectedFigure].position.X * 10f;
            deltaPositionY = canvas.instances[selectedFigure].position.Y * 10f;
            deltaPositionZ = canvas.instances[selectedFigure].position.Z - 10f;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timerPlayBtn)
            {
                switch (trackBarTime.Value)
                {
                    case 0:

                        // Si el valor es 0 (segundo 0) y ya se guardo el estado en el segundo 0
                        if (recorded0)
                        {
                            Follow(savedInstances0, savedInstances1, 0.05f);

                            if (canvas.nFigures != savedInstances0.Count)
                                recordedRestFigures = canvas.nFigures - savedInstances0.Count;
                            else
                                recordedRestFigures = canvas.nFigures;

                            //for (int i = 0; i < recordedRestFigures; i++){
                            //    canvas.instances[i] = savedInstances0[i];
                            //    canvas.instances[i].UpdateInstance();
                            //}
                        }
                        break;
                    case 1:

                        // Si el valor es 0 (segundo 0) y ya se guardo el estado en el segundo 0
                        if (recorded1)
                        {
                            Follow(savedInstances1, savedInstances2, 0.05f);

                            if (canvas.nFigures != savedInstances1.Count)
                                recordedRestFigures = canvas.nFigures - savedInstances1.Count;
                            else
                                recordedRestFigures = canvas.nFigures;

                            //for (int i = 0; i < recordedRestFigures; i++)
                            //{
                            //    canvas.instances[i] = savedInstances1[i];
                            //    canvas.instances[i].UpdateInstance();
                            //}
                        }
                        break;
                    case 2:

                        // Si el valor es 0 (segundo 0) y ya se guardo el estado en el segundo 0
                        if (recorded2)
                        {
                            Follow(savedInstances2, savedInstances3, 0.05f);

                            if (canvas.nFigures != savedInstances2.Count)
                                recordedRestFigures = canvas.nFigures - savedInstances2.Count;
                            else
                                recordedRestFigures = canvas.nFigures;

                            //for (int i = 0; i < recordedRestFigures; i++)
                            //{
                            //    canvas.instances[i] = savedInstances2[i];
                            //    canvas.instances[i].UpdateInstance();
                            //}

                        }
                        break;
                    case 3:

                        // Si el valor es 0 (segundo 0) y ya se guardo el estado en el segundo 0
                        if (recorded3)
                        {
                            Follow(savedInstances3, savedInstances4, 0.05f);

                            if (canvas.nFigures != savedInstances3.Count)
                                recordedRestFigures = canvas.nFigures - savedInstances3.Count;
                            else
                                recordedRestFigures = canvas.nFigures;

                            //for (int i = 0; i < recordedRestFigures; i++)
                            //{
                            //    canvas.instances[i] = savedInstances3[i];
                            //    canvas.instances[i].UpdateInstance();
                            //}
                        }
                        break;
                    case 4:

                        // Si el valor es 0 (segundo 0) y ya se guardo el estado en el segundo 0
                        if (recorded4)
                        {
                            Follow(savedInstances4, savedInstances5, 0.05f);

                            if (canvas.nFigures != savedInstances4.Count)
                                recordedRestFigures = canvas.nFigures - savedInstances4.Count;
                            else
                                recordedRestFigures = canvas.nFigures;

                            //for (int i = 0; i < recordedRestFigures; i++)
                            //{
                            //    canvas.instances[i] = savedInstances4[i];
                            //    canvas.instances[i].UpdateInstance();
                            //}
                        }
                        break;
                    case 5:

                        // Si el valor es 0 (segundo 0) y ya se guardo el estado en el segundo 0
                        if (recorded5)
                        {
                            //Follow(savedInstances5, savedInstances0, 1.0f);

                            if (canvas.nFigures != savedInstances5.Count)
                                recordedRestFigures = canvas.nFigures - savedInstances5.Count;
                            else
                                recordedRestFigures = canvas.nFigures;

                            //for (int i = 0; i < recordedRestFigures; i++)
                            //{
                            //    canvas.instances[i] = savedInstances5[i];
                            //    canvas.instances[i].UpdateInstance();
                            //}
                        }
                        break;
                }
            }

            if (fillCheckBox.Checked)
            {
                canvas.filled = true;
                canvas.wireframe = false;
            }

            else
            {
                canvas.filled = false;
                canvas.wireframe = true;
            }

            if (canvas.fileLoaded)
            {
                canvas.FastClear();
            }

            InitPB();

            if (canvas.fileLoaded)
            {
                for (int i = 0; i < canvas.instances.Count; i++)
                    canvas.instances[i].UpdateInstance();
                canvas.Render();
            }
            PCT_Canvas.Invalidate();
        }

        private void InitPB()
        {
            if (loadedOnce)
            {
                // points to draw intersecting lines (center of the picture box)
                l1 = new Point(origen.X, 0);
                l2 = new Point(origen.X, height);
                l3 = new Point(0, origen.Y);
                l4 = new Point(width, origen.Y);

                // Draw intersecting lines
                g.DrawLine(marcadorL, l1, l2);
                g.DrawLine(marcadorL, l3, l4);
            }
        }

        private void PCT_SLIDEER_X_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDownX)
            {
                gX.Clear(Color.Transparent);
                gX.DrawLine(Pens.DimGray, 0, bmpX.Height / 2, bmpX.Width, bmpX.Height / 2);
                gX.FillEllipse(Brushes.Aquamarine, e.X, bmpX.Height / 4, bmpX.Height / 2, bmpX.Height / 2);

                PCT_SLIDEER_X.Invalidate();
                if (Rotate_RadioBTN.Checked)
                    deltaX += (float)(e.Location.X - ptX.X) / 300;
                else
                    deltaPositionX += (float)(e.Location.X - ptX.X) / 20;
                ptX.X = e.Location.X;

                if (Rotate_RadioBTN.Checked)
                {
                    // actualiza la matriz de rotación actual
                    Mtx rotX = Mtx.RotX(deltaX);
                    currentRotation = rotX * currentRotation;

                    // aplica la rotación actual a todas las instancias
                    for (int i = 0; i < canvas.instances.Count; i++)
                    {
                        canvas.instances[selectedFigure].orientation = currentRotation;
                    }
                }
                else
                {
                    if ((deltaPositionZ + 10) >= topZ)
                    {
                        canvas.instances[selectedFigure].position = new Vertex(deltaPositionX / 10f, deltaPositionY / 10f, deltaPositionZ + 10);
                    }
                    else
                        deltaPositionZ = 0;
                }
            }
        }


        private void PCT_SLIDEER_X_MouseDown(object sender, MouseEventArgs e)
        {
            ptX = e.Location;
            IsMouseDownX = true;
        }

        private void PCT_SLIDEER_X_MouseUp(object sender, MouseEventArgs e)
        {
            deltaX = 0;
            IsMouseDownX = false;
            gX.Clear(Color.Transparent);
            gX.DrawLine(Pens.DimGray, 0, bmpX.Height / 2, bmpX.Width, bmpX.Height / 2);
            gX.FillEllipse(Brushes.Aquamarine, bmpX.Width / 2, bmpX.Height / 4, bmpX.Height / 2, bmpX.Height / 2);

            PCT_SLIDEER_X.Invalidate();
        }

        private void PCT_SLIDEER_Y_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDownY)
            {
                gY.Clear(Color.Transparent);
                gY.DrawLine(Pens.DimGray, 0, bmpY.Height / 2, bmpY.Width, bmpY.Height / 2);
                gY.FillEllipse(Brushes.Aquamarine, e.X, bmpY.Height / 4, bmpY.Height / 2, bmpY.Height / 2);

                PCT_SLIDEER_Y.Invalidate();
                if (Rotate_RadioBTN.Checked)
                    deltaY += (float)(e.Location.X - ptY.X) / 300;
                else
                    deltaPositionY += (float)(e.Location.X - ptY.X) / 20;

                ptY.X = e.Location.X;

                if (Rotate_RadioBTN.Checked)
                {
                    // actualiza la matriz de rotación actual
                    Mtx rotY = Mtx.RotY(deltaY);
                    currentRotation = rotY * currentRotation;

                    // aplica la rotación actual a todas las instancias
                    for (int i = 0; i < canvas.instances.Count; i++)
                    {
                        canvas.instances[selectedFigure].orientation = currentRotation;
                    }
                }
                else
                {
                    if ((deltaPositionZ + 10) >= topZ)
                    {
                        canvas.instances[selectedFigure].position = new Vertex(deltaPositionX / 10f, deltaPositionY / 10f, deltaPositionZ + 10);
                    }
                    else
                        deltaPositionZ = 0;
                }


            }
        }

        private void PCT_SLIDEER_Y_MouseDown(object sender, MouseEventArgs e)
        {
            ptY = e.Location;
            IsMouseDownY = true;
        }

        private void PCT_SLIDEER_Y_MouseUp(object sender, MouseEventArgs e)
        {
            deltaY = 0;
            IsMouseDownY = false;
            gY.Clear(Color.Transparent);
            gY.DrawLine(Pens.DimGray, 0, bmpY.Height / 2, bmpY.Width, bmpY.Height / 2);
            gY.FillEllipse(Brushes.Aquamarine, bmpY.Width / 2, bmpY.Height / 4, bmpY.Height / 2, bmpY.Height / 2);

            PCT_SLIDEER_Y.Invalidate();
        }

        private void PCT_SLIDEER_Z_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDownZ)
            {
                gZ.Clear(Color.Transparent);
                gZ.DrawLine(Pens.DimGray, 0, bmpZ.Height / 2, bmpZ.Width, bmpZ.Height / 2);
                gZ.FillEllipse(Brushes.Aquamarine, e.X, bmpZ.Height / 4, bmpZ.Height / 2, bmpZ.Height / 2);

                PCT_SLIDEER_Z.Invalidate();
                if (Rotate_RadioBTN.Checked)
                    deltaZ += (float)(e.Location.X - ptZ.X) / 300;
                else
                    deltaPositionZ += (float)(e.Location.X - ptZ.X) / 20;

                ptZ.X = e.Location.X;

                if (Rotate_RadioBTN.Checked)
                {
                    // actualiza la matriz de rotación actual
                    Mtx rotZ = Mtx.RotZ(deltaZ);
                    currentRotation = rotZ * currentRotation;

                    // aplica la rotación actual a todas las instancias
                    for (int i = 0; i < canvas.instances.Count; i++)
                    {
                        canvas.instances[selectedFigure].orientation = currentRotation;
                    }

                }

                else
                {
                    if ((deltaPositionZ + 10) >= topZ)
                    {
                        canvas.instances[selectedFigure].position = new Vertex(deltaPositionX / 10f, deltaPositionY / 10f, deltaPositionZ + 10);
                    }
                    else
                        deltaPositionZ = 0;
                }
            }
        }


        private void PCT_SLIDEER_Z_MouseDown(object sender, MouseEventArgs e)
        {
            ptZ = e.Location;
            IsMouseDownZ = true;
        }

        private void PCT_SLIDEER_Z_MouseUp(object sender, MouseEventArgs e)
        {
            deltaZ = 0;
            IsMouseDownZ = false;
            gZ.Clear(Color.Transparent);
            gZ.DrawLine(Pens.DimGray, 0, bmpZ.Height / 2, bmpZ.Width, bmpZ.Height / 2);
            gZ.FillEllipse(Brushes.Aquamarine, bmpZ.Width / 2, bmpZ.Height / 4, bmpZ.Height / 2, bmpZ.Height / 2);

            PCT_SLIDEER_Z.Invalidate();
        }

        private void PCT_SLIDER_SCALE_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDownScale)
            {
                gScale.Clear(Color.Transparent);
                gScale.DrawLine(Pens.DimGray, bmpScale.Width / 2, 0, bmpScale.Width / 2, bmpScale.Height);
                gScale.FillEllipse(Brushes.Aquamarine, bmpScale.Width / 4, e.Y, bmpX.Height / 2, bmpX.Height / 2);

                PCT_SLIDER_SCALE.Invalidate();
                deltaScale += (float)(ptScale.Y - e.Location.Y) / 100;//------------------

                canvas.instances[selectedFigure].scale = deltaScale;
                ptScale.Y = e.Location.Y;
            }
        }

        private void PCT_SLIDER_SCALE_MouseDown(object sender, MouseEventArgs e)
        {
            ptScale = e.Location;
            IsMouseDownScale = true;
        }

        private void PCT_SLIDER_SCALE_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDownScale = false;
            gScale.Clear(Color.Transparent);
            gScale.DrawLine(Pens.DimGray, bmpScale.Width / 2, 0, bmpScale.Width / 2, bmpScale.Height);
            gScale.FillEllipse(Brushes.Aquamarine, bmpScale.Width / 4, bmpScale.Height / 2, bmpX.Height / 2, bmpX.Height / 2);

            PCT_SLIDER_SCALE.Invalidate();
        }
    }
}
