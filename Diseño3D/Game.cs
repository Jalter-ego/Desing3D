using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;

namespace Diseño3D
{
    public class Game : GameWindow
    {
        public float posX = 0.0f;
        public float posY = 0.0f;
        public float posZ = 0.0f;

        // Posición y orientación de la cámara
        private Vector3 cameraPosition = new Vector3(1.5f, 2f, 3f);
        private Vector3 cameraFront = new Vector3(-0.5f, -0.5f, -1f);
        private Vector3 cameraUp = Vector3.UnitY;
        private float cameraSpeed = 0.05f;

        public Escenario escenarioU;

        public Game() : base(800, 600)
        {
            this.escenarioU = new Escenario(this.getObjetosU(), new Vector(0, 0, 0));
            //U2 = new Escenario(this.getObjetosU(), new Vector(0, 0, 0));
            //escenarioU = Serializador.DeserializarObjeto<Escenario>( "U.json");
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(new Color4(51f / 255f, 51f / 255f, 51f / 255f, 1.0f));
            Objeto newU = new Objeto(this.GetPartes(),new Vector(0,0,0));
            newU.SetCentro(new Vector(1, 0, 0));
            escenarioU.AddObjeto("ObjU2", newU);
            //this.escenarioU.SetCentro(this.escenarioU.CalcularCentroMasa());
            // Normalizar el vector de dirección de la cámara
            cameraFront.Normalize();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(45.0f),
                Width / (float)Height,
                0.1f,
                100.0f);
            GL.LoadMatrix(ref projection);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            Matrix4 modelview = Matrix4.LookAt(
                cameraPosition,
                cameraPosition + cameraFront,
                cameraUp);
            GL.LoadMatrix(ref modelview);
            escenarioU.Draw();


            DrawAxes();
            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Key.W))  // Mover hacia adelante
            {
                cameraPosition += cameraFront * cameraSpeed;
            }
            if (keyboardState.IsKeyDown(Key.S))  // Mover hacia atrás
            {
                cameraPosition -= cameraFront * cameraSpeed;
            }
            if (keyboardState.IsKeyDown(Key.A))  // Mover a la izquierda
            {
                cameraPosition -= Vector3.Normalize(Vector3.Cross(cameraFront, cameraUp)) * cameraSpeed;
            }
            if (keyboardState.IsKeyDown(Key.D))  // Mover a la derecha
            {
                cameraPosition += Vector3.Normalize(Vector3.Cross(cameraFront, cameraUp)) * cameraSpeed;
            }
            if (keyboardState.IsKeyDown(Key.C))
            {
                //this.U.GetObjeto("ObjU").GetParte("base").Trasladar(new Vector3(.005f, 0, 0));
                
                this.escenarioU.GetObjeto("ObjU").SetCentro(this.escenarioU.GetObjeto("ObjU").CalcularCentroMasa());
                this.escenarioU.GetObjeto("ObjU").Rotar(5,new  Vector3(1f,0,0));

            }
            if (keyboardState.IsKeyDown(Key.S))
            {
                Console.WriteLine(escenarioU.listaDeObjetos.Values.ToString());
                Serializador.SerializarObjeto(escenarioU, "U.json");
            }

        }



        private Dictionary<String,Objeto> getObjetosU()
        {
            Dictionary<String,Objeto> objetos = new Dictionary<string, Objeto>();
            Objeto objetoU = new Objeto(this.GetPartes(),new Vector(0,0,0));
            objetos.Add("ObjU",objetoU);
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
            baseUPa1.Add("b1", baseUPo1);
            baseUPa1.Add("b2", baseUPo2);
            baseUPa1.Add("b3", baseUPo3);
            baseUPa1.Add("b4", baseUPo4);
            baseUPa1.Add("b5", baseUPo5);
            baseUPa1.Add("b6", baseUPo6);

            // ----- COLUMNA DERECHA -----
            Parte columnaDerecha = new Parte();

            // Parte delantera
            Poligono colDer1 = new Poligono(colorBase);
            colDer1.Add(new Vector(0.3f, 0.8f, 0.25f));
            colDer1.Add(new Vector(0.4f, 0.8f, 0.25f));
            colDer1.Add(new Vector(0.4f, -0.4f, 0.25f));
            colDer1.Add(new Vector(0.3f, -0.4f, 0.25f));
            columnaDerecha.Add("cd1", colDer1);

            // Parte trasera
            Poligono colDer2 = new Poligono(colorBase);
            colDer2.Add(new Vector(0.3f, 0.8f, 0.15f));
            colDer2.Add(new Vector(0.4f, 0.8f, 0.15f));
            colDer2.Add(new Vector(0.4f, -0.4f, 0.15f));
            colDer2.Add(new Vector(0.3f, -0.4f, 0.15f));
            columnaDerecha.Add("cd2", colDer2);

            // Parte arriba
            Poligono colDer3 = new Poligono(colorBase);
            colDer3.Add(new Vector(0.4f, -0.4f, 0.25f));
            colDer3.Add(new Vector(0.3f, -0.4f, 0.25f));
            colDer3.Add(new Vector(0.3f, -0.4f, 0.15f));
            colDer3.Add(new Vector(0.4f, -0.4f, 0.15f));
            columnaDerecha.Add("cd3", colDer3);

            // Parte abajo
            Poligono colDer4 = new Poligono(colorBase);
            colDer4.Add(new Vector(0.4f, 0.8f, 0.25f));
            colDer4.Add(new Vector(0.3f, 0.8f, 0.25f));
            colDer4.Add(new Vector(0.3f, 0.8f, 0.15f));
            colDer4.Add(new Vector(0.4f, 0.8f, 0.15f));
            columnaDerecha.Add("cd4", colDer4);

            // Lado derecho
            Poligono colDer5 = new Poligono(colorBase);
            colDer5.Add(new Vector(0.3f, 0.8f, 0.25f));
            colDer5.Add(new Vector(0.3f, 0.8f, 0.15f));
            colDer5.Add(new Vector(0.3f, -0.4f, 0.15f));
            colDer5.Add(new Vector(0.3f, -0.4f, 0.25f));
            columnaDerecha.Add("dc5", colDer5);

            // Lado izquierdo
            Poligono colDer6 = new Poligono(colorBase);
            colDer6.Add(new Vector(0.4f, 0.8f, 0.25f));
            colDer6.Add(new Vector(0.4f, 0.8f, 0.15f));
            colDer6.Add(new Vector(0.4f, -0.4f, 0.15f));
            colDer6.Add(new Vector(0.4f, -0.4f, 0.25f));
            columnaDerecha.Add("cd6", colDer6);


            // ----- COLUMNA IZQUIERDA -----
            Parte columnaIzquierda = new Parte();

            // Parte delantera
            Poligono colIzq1 = new Poligono(colorBase);
            colIzq1.Add(new Vector(-0.4f, 0.8f, 0.25f));
            colIzq1.Add(new Vector(-0.3f, 0.8f, 0.25f));
            colIzq1.Add(new Vector(-0.3f, -0.4f, 0.25f));
            colIzq1.Add(new Vector(-0.4f, -0.4f, 0.25f));
            columnaIzquierda.Add("ci1", colIzq1);

            // Parte trasera
            Poligono colIzq2 = new Poligono(colorBase);
            colIzq2.Add(new Vector(-0.4f, 0.8f, 0.15f));
            colIzq2.Add(new Vector(-0.3f, 0.8f, 0.15f));
            colIzq2.Add(new Vector(-0.3f, -0.4f, 0.15f));
            colIzq2.Add(new Vector(-0.4f, -0.4f, 0.15f));
            columnaIzquierda.Add("ci2", colIzq2);

            // Parte arriba
            Poligono colIzq3 = new Poligono(colorBase);
            colIzq3.Add(new Vector(-0.4f, -0.4f, 0.25f));
            colIzq3.Add(new Vector(-0.3f, -0.4f, 0.25f));
            colIzq3.Add(new Vector(-0.3f, -0.4f, 0.15f));
            colIzq3.Add(new Vector(-0.4f, -0.4f, 0.15f));
            columnaIzquierda.Add("ci3", colIzq3);

            // Parte abajo
            Poligono colIzq4 = new Poligono(colorBase);
            colIzq4.Add(new Vector(-0.4f, 0.8f, 0.25f));
            colIzq4.Add(new Vector(-0.3f, 0.8f, 0.25f));
            colIzq4.Add(new Vector(-0.3f, 0.8f, 0.15f));
            colIzq4.Add(new Vector(-0.4f, 0.8f, 0.15f));
            columnaIzquierda.Add("ci4", colIzq4);

            // Lado izquierdo
            Poligono colIzq5 = new Poligono(colorBase);
            colIzq5.Add(new Vector(-0.4f, 0.8f, 0.25f));
            colIzq5.Add(new Vector(-0.4f, 0.8f, 0.15f));
            colIzq5.Add(new Vector(-0.4f, -0.4f, 0.15f));
            colIzq5.Add(new Vector(-0.4f, -0.4f, 0.25f));
            columnaIzquierda.Add("ci5", colIzq5);

            // Lado derecho
            Poligono colIzq6 = new Poligono(colorBase);
            colIzq6.Add(new Vector(-0.3f, 0.8f, 0.25f));
            colIzq6.Add(new Vector(-0.3f, 0.8f, 0.15f));
            colIzq6.Add(new Vector(-0.3f, -0.4f, 0.15f));
            colIzq6.Add(new Vector(-0.3f, -0.4f, 0.25f));
            columnaIzquierda.Add("ci6", colIzq6);

            Dictionary<String, Parte> partes = new Dictionary<string, Parte>();
            partes.Add("base", baseUPa1);
            partes.Add("ColIzq", columnaIzquierda);
            partes.Add("ColDer", columnaDerecha);

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





    }
}
