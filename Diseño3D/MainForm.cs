using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Diseño3D
{
    public partial class MainForm : Form
    {
        private GLControl glControl;
        private IterableObject selectObject;
        private IterableObject selectObjectAnterior;
        private Dictionary<string, IterableObject> lObjetos;
        private Escenario escenarioU;
        private float angulo;
        private float escalar;
        private int indice;
        private Vector3 cameraPosition = new Vector3(1.5f, 2f, 3f);
        private Vector3 cameraFront = new Vector3(-0.5f, -0.5f, -1f);
        private Vector3 cameraUp = Vector3.UnitY;
        public MainForm()
        {
            InitializeComponent();
            this.Text = "GLControl";

            glControl = new GLControl(new GraphicsMode(32, 24, 0, 8));
            glControl.Dock = DockStyle.Fill;
            glControl.Paint += GlControl_Paint;
            glControl.Load += GlControl_Load;
            this.Controls.Add(glControl);

        }

        private void GlControl_Load(object sender, EventArgs e)
        {
            GL.ClearColor(new Color4(51 / 255f, 51 / 255f, 51 / 255f, 1f));
            cameraFront.Normalize();

            this.angulo = 5f;
            this.escalar = 0.1f;
            this.indice = 0;
            lObjetos = new Dictionary<string, IterableObject>();
            InitializeEscenario();

            lObjetos.Clear();
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Escenario");
            AddObjetosEscenario(this.escenarioU);
        }

        private void InitializeEscenario()
        {
            
            escenarioU = new Escenario(new Dictionary<string, Objeto>(), new Vector(0, 0, 0));
            indice += 1;
            Objeto newobj2 = new Objeto(this.GetPartes(), new Vector(0, 0, 0));
            newobj2.SetCentro(new Vector(-1, 0, 0));
            escenarioU.AddObjeto("ObjU" + indice, newobj2);
            indice += 1;
            Objeto newobj = new Objeto(this.GetPartes(), new Vector(0, 0, 0));
            newobj.SetCentro(new Vector(.5f, 0, 0));
            escenarioU.AddObjeto("ObjU" + indice, newobj);
            Serializador.SerializarObjeto(escenarioU, "U.json");
            /*escenarioU = Serializador.DeserializarObjeto<Escenario>("U.json");/*/
            indice = escenarioU.listaDeObjetos.Count;
            Console.WriteLine(indice);
        }

        private void GlControl_Paint(object sender, PaintEventArgs e)
        {
            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(45.0f),
                glControl.Width / (float)glControl.Height,
                0.1f,
                100.0f);

            Matrix4 modelview = Matrix4.LookAt(
                cameraPosition,
                cameraPosition + cameraFront,
                cameraUp);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);

            escenarioU.Draw();
            DrawAxes();

            glControl.SwapBuffers();
        }

        protected void AddObjetosEscenario(Escenario e)
        {
            foreach (var key in e.listaDeObjetos.Keys)
            {
                IterableObject obj = e.GetObjeto(key);
                lObjetos.Add(key, obj);
                comboBox1.Items.Add(key);
                AddPartesObjeto(e.GetObjeto(key));
            }
        }

        protected void AddPartesObjeto(Objeto o)
        {
            foreach (var keyParte in o.listaDePartes.Keys)
            {
                IterableObject objParte = o.GetParte(keyParte);
                Console.WriteLine(keyParte);
                lObjetos.Add(keyParte, objParte);
                comboBox1.Items.Add(keyParte);
                AddPoligonosParte(o.GetParte(keyParte));
            }
        }

        protected void AddPoligonosParte(Parte p)
        {
            foreach (var keyPoligono in p.listaDePoligonos.Keys)
            {
                IterableObject parPol = p.GetPoligono(keyPoligono);
                lObjetos.Add(keyPoligono, parPol);
                comboBox1.Items.Add(keyPoligono);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string items = comboBox1.SelectedItem.ToString();
            if (items == "Escenario")
            {
                selectObject = this.escenarioU;
                Run.Enabled = true;
                Console.WriteLine(selectObject);
            }
            else
            {
                selectObject = lObjetos[items.ToString()];
                Run.Enabled = true;
                Console.WriteLine(selectObject);
            }
            if (selectObjectAnterior != null)
                selectObjectAnterior.SetColor(new Color4(1.0f, 0.3f, 0.3f, 1f));
            selectObjectAnterior = selectObject;
            selectObject.SetColor(Color4.Beige);
        }

        private void Run_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
                Run.Text = "STOP";
            else
                Run.Text = "RUN";
            timer1.Enabled = !timer1.Enabled;
        }

        private void AplicarTransformacionConCentroMasa2(Vector3 v)
        {
            Vector centroMasa = this.selectObject.CalcularCentroMasa();
            this.selectObject.Rotar(angulo, v, centroMasa);
        }



        private void RBMas_CheckedChanged(object sender, EventArgs e)
        {
            angulo = 5f;
            escalar = 1f;
        }

        private void RBMenos_CheckedChanged(object sender, EventArgs e)
        {
            angulo = -5f;
            escalar = -1f;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkRotacion.Checked && selectObject != null)
            {
                if (this.selectObject is Escenario)
                {
                    if (checkX.Checked)
                        this.selectObject.Rotar(angulo, new Vector3(1, 0, 0));
                    if (checkY.Checked)
                        this.selectObject.Rotar(angulo, new Vector3(0, 1, 0));
                    if (checkZ.Checked)
                        this.selectObject.Rotar(angulo,new Vector3(0, 0, 1));
                }
                else
                {
                    if (checkX.Checked)
                        AplicarTransformacionConCentroMasa2(new Vector3(1, 0, 0));
                    if (checkY.Checked)
                        AplicarTransformacionConCentroMasa2(new Vector3(0, 1, 0));
                    if (checkZ.Checked)
                        AplicarTransformacionConCentroMasa2(new Vector3(0, 0, 1));
                }
            }
            if (checkTraslacion.Checked)
            {
                if (checkX.Checked)
                    this.selectObject.Trasladar(new Vector3(0.01f * escalar, 0, 0));
                if (checkY.Checked)
                    this.selectObject.Trasladar(new Vector3(0, 0.01f * escalar, 0));
                if (checkZ.Checked)
                    this.selectObject.Trasladar(new Vector3(0, 0, 0.01f * escalar));
            }
            if (checkEscalacion.Checked)
            {
                if (checkX.Checked)
                    this.selectObject.Escalar(0.01f * escalar, new Vector3(1, 0, 0));
                if (checkY.Checked)
                    this.selectObject.Escalar(0.01f * escalar, new Vector3(0, 1, 0));
                if (checkZ.Checked)
                    this.selectObject.Escalar(0.01f * escalar, new Vector3(0, 0, 1));
            }

            glControl.Invalidate(); // Redibuja
        }


        private Dictionary<String, Objeto> getObjetosU()
        {
            Dictionary<String, Objeto> objetos = new Dictionary<string, Objeto>();
            Objeto objetoU = new Objeto(this.GetPartes(), new Vector(0, 0, 0));
            objetos.Add("ObjU"+indice, objetoU);
            return objetos;
        }

        public Dictionary<String, Parte> GetPartes()
        {
            Color4 colorBase = new Color4(1.0f, 0.3f, 0.3f, 1f);

            // Base 
            // Parte delantera
            Poligono baseUPo1 = new Poligono(colorBase);
            baseUPo1.Add(new Vector(-0.4f, -0.5f, 0.25f));
            baseUPo1.Add(new Vector(0.4f, -0.5f, 0.25f));
            baseUPo1.Add(new Vector(0.4f, -0.4f, 0.25f));
            baseUPo1.Add(new Vector(-0.4f, -0.4f, 0.25f));

            // Parte trasera
            Poligono baseUPo2 = new Poligono(colorBase);
            baseUPo2.Add(new Vector(-0.4f, -0.5f, 0.15f));
            baseUPo2.Add(new Vector(0.4f, -0.5f, 0.15f));
            baseUPo2.Add(new Vector(0.4f, -0.4f, 0.15f));
            baseUPo2.Add(new Vector(-0.4f, -0.4f, 0.15f));

            // Parte arriba
            Poligono baseUPo3 = new Poligono(colorBase);
            baseUPo3.Add(new Vector(-0.4f, -0.4f, 0.25f));
            baseUPo3.Add(new Vector(0.4f, -0.4f, 0.25f));
            baseUPo3.Add(new Vector(0.4f, -0.4f, 0.15f));
            baseUPo3.Add(new Vector(-0.4f, -0.4f, 0.15f));

            // Parte abajo
            Poligono baseUPo4 = new Poligono(colorBase);
            baseUPo4.Add(new Vector(-0.4f, -0.5f, 0.25f));
            baseUPo4.Add(new Vector(0.4f, -0.5f, 0.25f));
            baseUPo4.Add(new Vector(0.4f, -0.5f, 0.15f));
            baseUPo4.Add(new Vector(-0.4f, -0.5f, 0.15f));

            // Lado derecho
            Poligono baseUPo5 = new Poligono(colorBase);
            baseUPo5.Add(new Vector(0.4f, -0.5f, 0.25f));
            baseUPo5.Add(new Vector(0.4f, -0.5f, 0.15f));
            baseUPo5.Add(new Vector(0.4f, -0.4f, 0.15f));
            baseUPo5.Add(new Vector(0.4f, -0.4f, 0.25f));

            // Lado izquierdo
            Poligono baseUPo6 = new Poligono(colorBase);
            baseUPo6.Add(new Vector(-0.4f, -0.5f, 0.25f));
            baseUPo6.Add(new Vector(-0.4f, -0.5f, 0.15f));
            baseUPo6.Add(new Vector(-0.4f, -0.4f, 0.15f));
            baseUPo6.Add(new Vector(-0.4f, -0.4f, 0.25f));

            Parte baseUPa1 = new Parte();
            baseUPa1.Add("b1" + '-' + indice, baseUPo1);
            baseUPa1.Add("b2" + '-' + indice, baseUPo2);
            baseUPa1.Add("b3" + '-' + indice, baseUPo3);
            baseUPa1.Add("b4" + '-' + indice, baseUPo4);
            baseUPa1.Add("b5" + '-' + indice, baseUPo5);
            baseUPa1.Add("b6" + '-' + indice, baseUPo6);

            // ----- COLUMNA DERECHA -----
            Parte columnaDerecha = new Parte();

            // Parte delantera
            Poligono colDer1 = new Poligono(colorBase);
            colDer1.Add(new Vector(0.3f, 0.8f, 0.25f));
            colDer1.Add(new Vector(0.4f, 0.8f, 0.25f));
            colDer1.Add(new Vector(0.4f, -0.4f, 0.25f));
            colDer1.Add(new Vector(0.3f, -0.4f, 0.25f));
            columnaDerecha.Add("cd1" + '-' + indice, colDer1);

            // Parte trasera
            Poligono colDer2 = new Poligono(colorBase);
            colDer2.Add(new Vector(0.3f, 0.8f, 0.15f));
            colDer2.Add(new Vector(0.4f, 0.8f, 0.15f));
            colDer2.Add(new Vector(0.4f, -0.4f, 0.15f));
            colDer2.Add(new Vector(0.3f, -0.4f, 0.15f));
            columnaDerecha.Add("cd2" + '-' + indice, colDer2);

            // Parte arriba
            Poligono colDer3 = new Poligono(colorBase);
            colDer3.Add(new Vector(0.4f, -0.4f, 0.25f));
            colDer3.Add(new Vector(0.3f, -0.4f, 0.25f));
            colDer3.Add(new Vector(0.3f, -0.4f, 0.15f));
            colDer3.Add(new Vector(0.4f, -0.4f, 0.15f));
            columnaDerecha.Add("cd3" + '-' + indice, colDer3);

            // Parte abajo
            Poligono colDer4 = new Poligono(colorBase);
            colDer4.Add(new Vector(0.4f, 0.8f, 0.25f));
            colDer4.Add(new Vector(0.3f, 0.8f, 0.25f));
            colDer4.Add(new Vector(0.3f, 0.8f, 0.15f));
            colDer4.Add(new Vector(0.4f, 0.8f, 0.15f));
            columnaDerecha.Add("cd4" + '-' + indice, colDer4);

            // Lado derecho
            Poligono colDer5 = new Poligono(colorBase);
            colDer5.Add(new Vector(0.3f, 0.8f, 0.25f));
            colDer5.Add(new Vector(0.3f, 0.8f, 0.15f));
            colDer5.Add(new Vector(0.3f, -0.4f, 0.15f));
            colDer5.Add(new Vector(0.3f, -0.4f, 0.25f));
            columnaDerecha.Add("cd5" + '-' + indice, colDer5);

            // Lado izquierdo
            Poligono colDer6 = new Poligono(colorBase);
            colDer6.Add(new Vector(0.4f, 0.8f, 0.25f));
            colDer6.Add(new Vector(0.4f, 0.8f, 0.15f));
            colDer6.Add(new Vector(0.4f, -0.4f, 0.15f));
            colDer6.Add(new Vector(0.4f, -0.4f, 0.25f));
            columnaDerecha.Add("cd6" + '-' + indice, colDer6);


            // ----- COLUMNA IZQUIERDA -----
            Parte columnaIzquierda = new Parte();

            // Parte delantera
            Poligono colIzq1 = new Poligono(colorBase);
            colIzq1.Add(new Vector(-0.4f, 0.8f, 0.25f));
            colIzq1.Add(new Vector(-0.3f, 0.8f, 0.25f));
            colIzq1.Add(new Vector(-0.3f, -0.4f, 0.25f));
            colIzq1.Add(new Vector(-0.4f, -0.4f, 0.25f));
            columnaIzquierda.Add("ci1" + '-' + indice, colIzq1);

            // Parte trasera
            Poligono colIzq2 = new Poligono(colorBase);
            colIzq2.Add(new Vector(-0.4f, 0.8f, 0.15f));
            colIzq2.Add(new Vector(-0.3f, 0.8f, 0.15f));
            colIzq2.Add(new Vector(-0.3f, -0.4f, 0.15f));
            colIzq2.Add(new Vector(-0.4f, -0.4f, 0.15f));
            columnaIzquierda.Add("ci2" + '-' + indice, colIzq2);

            // Parte arriba
            Poligono colIzq3 = new Poligono(colorBase);
            colIzq3.Add(new Vector(-0.4f, -0.4f, 0.25f));
            colIzq3.Add(new Vector(-0.3f, -0.4f, 0.25f));
            colIzq3.Add(new Vector(-0.3f, -0.4f, 0.15f));
            colIzq3.Add(new Vector(-0.4f, -0.4f, 0.15f));
            columnaIzquierda.Add("ci3" + '-' + indice, colIzq3);

            // Parte abajo
            Poligono colIzq4 = new Poligono(colorBase);
            colIzq4.Add(new Vector(-0.4f, 0.8f, 0.25f));
            colIzq4.Add(new Vector(-0.3f, 0.8f, 0.25f));
            colIzq4.Add(new Vector(-0.3f, 0.8f, 0.15f));
            colIzq4.Add(new Vector(-0.4f, 0.8f, 0.15f));
            columnaIzquierda.Add("ci4" + '-' + indice, colIzq4);

            // Lado izquierdo
            Poligono colIzq5 = new Poligono(colorBase);
            colIzq5.Add(new Vector(-0.4f, 0.8f, 0.25f));
            colIzq5.Add(new Vector(-0.4f, 0.8f, 0.15f));
            colIzq5.Add(new Vector(-0.4f, -0.4f, 0.15f));
            colIzq5.Add(new Vector(-0.4f, -0.4f, 0.25f));
            columnaIzquierda.Add("ci5" + '-' + indice, colIzq5);

            // Lado derecho
            Poligono colIzq6 = new Poligono(colorBase);
            colIzq6.Add(new Vector(-0.3f, 0.8f, 0.25f));
            colIzq6.Add(new Vector(-0.3f, 0.8f, 0.15f));
            colIzq6.Add(new Vector(-0.3f, -0.4f, 0.15f));
            colIzq6.Add(new Vector(-0.3f, -0.4f, 0.25f));
            columnaIzquierda.Add("ci6" + '-' + indice, colIzq6);

            Dictionary<String, Parte> partes = new Dictionary<string, Parte>();
            partes.Add("base" + indice, baseUPa1);
            partes.Add("ColIzq" + indice, columnaIzquierda);
            partes.Add("ColDer" + indice, columnaDerecha);

            return partes;
        }

        private void DrawAxes()
        {
            GL.Begin(PrimitiveType.Lines);

            // Eje X (Rojo)
            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Vertex3(-3.0f, 0.0f, 0.0f);
            GL.Vertex3(3.0f, 0.0f, 0.0f);

            // Eje Y (Verde)
            GL.Color3(0.0f, 1.0f, 0.0f);
            GL.Vertex3(0.0f, -3.0f, 0.0f);
            GL.Vertex3(0.0f, 3.0f, 0.0f);

            // Eje Z (Azul)
            GL.Color3(0.0f, 0.0f, 1.0f);
            GL.Vertex3(0.0f, 0.0f, -3.0f);
            GL.Vertex3(0.0f, 0.0f, 3.0f);


            GL.End();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
