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

        public Objeto objet_U;
        public Objeto objet_U2;

        public Game() : base(800, 600)
        {
            this.objet_U = new Objeto(this.getVerticesU(), new Vector3(0, 0, 0));
            this.objet_U2 = new Objeto(this.getVerticesU(), new Vector3(0.5f, 0, -1));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(new Color4(51f / 255f, 51f / 255f, 51f / 255f, 1.0f));
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

            // Usar las variables de la cámara para la vista
            Matrix4 modelview = Matrix4.LookAt(
                cameraPosition,
                cameraPosition + cameraFront,
                cameraUp);
            GL.LoadMatrix(ref modelview);

            this.objet_U.Draw();
            this.objet_U2.Draw();
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

        }



        private List<Vector3> getVerticesU()
        {
            List<Vector3> array = new List<Vector3>();
            // Base 
            // Parte delantera
            array.Add(new Vector3(-0.4f + posX, -0.5f + posY, 0.25f + posZ));
            array.Add(new Vector3(0.4f + posX, -0.5f + posY, 0.25f + posZ));
            array.Add(new Vector3(0.4f + posX, -0.4f + posY, 0.25f + posZ));
            array.Add(new Vector3(-0.4f + posX, -0.4f + posY, 0.25f + posZ));

            // Parte trasera
            array.Add(new Vector3(-0.4f + posX, -0.5f + posY, 0.15f + posZ));
            array.Add(new Vector3(0.4f + posX, -0.5f + posY, 0.15f + posZ));
            array.Add(new Vector3(0.4f + posX, -0.4f + posY, 0.15f + posZ));
            array.Add(new Vector3(-0.4f + posX, -0.4f + posY, 0.15f + posZ));

            // Parte arriba
            array.Add(new Vector3(-0.4f + posX, -0.4f + posY, 0.25f + posZ));
            array.Add(new Vector3(0.4f + posX, -0.4f + posY, 0.25f + posZ));
            array.Add(new Vector3(0.4f + posX, -0.4f + posY, 0.15f + posZ));
            array.Add(new Vector3(-0.4f + posX, -0.4f + posY, 0.15f + posZ));

            // Parte abajo
            array.Add(new Vector3(-0.4f + posX, -0.5f + posY, 0.25f + posZ));
            array.Add(new Vector3(0.4f + posX, -0.5f + posY, 0.25f + posZ));
            array.Add(new Vector3(0.4f + posX, -0.5f + posY, 0.15f + posZ));
            array.Add(new Vector3(-0.4f + posX, -0.5f + posY, 0.15f + posZ));

            // Lado derecho
            array.Add(new Vector3(0.4f + posX, -0.5f + posY, 0.25f + posZ));
            array.Add(new Vector3(0.4f + posX, -0.5f + posY, 0.15f + posZ));
            array.Add(new Vector3(0.4f + posX, -0.4f + posY, 0.15f + posZ));
            array.Add(new Vector3(0.4f + posX, -0.4f + posY, 0.25f + posZ));

            // Lado izquierdo
            array.Add(new Vector3(-0.4f + posX, -0.5f + posY, 0.25f + posZ));
            array.Add(new Vector3(-0.4f + posX, -0.5f + posY, 0.15f + posZ));
            array.Add(new Vector3(-0.4f + posX, -0.4f + posY, 0.15f + posZ));
            array.Add(new Vector3(-0.4f + posX, -0.4f + posY, 0.25f + posZ));

            // Columna derecha
            // Parte delantera
            array.Add(new Vector3(0.3f + posX, 0.8f + posY, 0.25f + posZ));
            array.Add(new Vector3(0.4f + posX, 0.8f + posY, 0.25f + posZ));
            array.Add(new Vector3(0.4f + posX, -0.4f + posY, 0.25f + posZ));
            array.Add(new Vector3(0.3f + posX, -0.4f + posY, 0.25f + posZ));

            // Parte trasera
            array.Add(new Vector3(0.3f + posX, 0.8f + posY, 0.15f + posZ));
            array.Add(new Vector3(0.4f + posX, 0.8f + posY, 0.15f + posZ));
            array.Add(new Vector3(0.4f + posX, -0.4f + posY, 0.15f + posZ));
            array.Add(new Vector3(0.3f + posX, -0.4f + posY, 0.15f + posZ));

            // Parte arriba
            array.Add(new Vector3(0.4f + posX, -0.4f + posY, 0.25f + posZ));
            array.Add(new Vector3(0.3f + posX, -0.4f + posY, 0.25f + posZ));
            array.Add(new Vector3(0.3f + posX, -0.4f + posY, 0.15f + posZ));
            array.Add(new Vector3(0.4f + posX, -0.4f + posY, 0.15f + posZ));

            // Parte abajo
            array.Add(new Vector3(0.4f + posX, 0.8f + posY, 0.25f + posZ));
            array.Add(new Vector3(0.3f + posX, 0.8f + posY, 0.25f + posZ));
            array.Add(new Vector3(0.3f + posX, 0.8f + posY, 0.15f + posZ));
            array.Add(new Vector3(0.4f + posX, 0.8f + posY, 0.15f + posZ));

            // Lado derecho
            array.Add(new Vector3(0.3f + posX, 0.8f + posY, 0.25f + posZ));
            array.Add(new Vector3(0.3f + posX, 0.8f + posY, 0.15f + posZ));
            array.Add(new Vector3(0.3f + posX, -0.4f + posY, 0.15f + posZ));
            array.Add(new Vector3(0.3f + posX, -0.4f + posY, 0.25f + posZ));

            // Lado izquierdo
            array.Add(new Vector3(0.4f + posX, 0.8f + posY, 0.25f + posZ));
            array.Add(new Vector3(0.4f + posX, 0.8f + posY, 0.15f + posZ));
            array.Add(new Vector3(0.4f + posX, -0.4f + posY, 0.15f + posZ));
            array.Add(new Vector3(0.4f + posX, -0.4f + posY, 0.25f + posZ));

            // Columna izquierda
            // Parte delantera
            array.Add(new Vector3(-0.4f + posX, 0.8f + posY, 0.25f + posZ));
            array.Add(new Vector3(-0.3f + posX, 0.8f + posY, 0.25f + posZ));
            array.Add(new Vector3(-0.3f + posX, -0.4f + posY, 0.25f + posZ));
            array.Add(new Vector3(-0.4f + posX, -0.4f + posY, 0.25f + posZ));

            // Parte trasera
            array.Add(new Vector3(-0.4f + posX, 0.8f + posY, 0.15f + posZ));
            array.Add(new Vector3(-0.3f + posX, 0.8f + posY, 0.15f + posZ));
            array.Add(new Vector3(-0.3f + posX, -0.4f + posY, 0.15f + posZ));
            array.Add(new Vector3(-0.4f + posX, -0.4f + posY, 0.15f + posZ));

            // Parte arriba
            array.Add(new Vector3(-0.4f + posX, -0.4f + posY, 0.25f + posZ));
            array.Add(new Vector3(-0.3f + posX, -0.4f + posY, 0.25f + posZ));
            array.Add(new Vector3(-0.3f + posX, -0.4f + posY, 0.15f + posZ));
            array.Add(new Vector3(-0.4f + posX, -0.4f + posY, 0.15f + posZ));

            // Parte abajo
            array.Add(new Vector3(-0.4f + posX, 0.8f + posY, 0.25f + posZ));
            array.Add(new Vector3(-0.3f + posX, 0.8f + posY, 0.25f + posZ));
            array.Add(new Vector3(-0.3f + posX, 0.8f + posY, 0.15f + posZ));
            array.Add(new Vector3(-0.4f + posX, 0.8f + posY, 0.15f + posZ));

            // Lado izquierdo
            array.Add(new Vector3(-0.4f + posX, 0.8f + posY, 0.25f + posZ));
            array.Add(new Vector3(-0.4f + posX, 0.8f + posY, 0.15f + posZ));
            array.Add(new Vector3(-0.4f + posX, -0.4f + posY, 0.15f + posZ));
            array.Add(new Vector3(-0.4f + posX, -0.4f + posY, 0.25f + posZ));

            // Lado derecho
            array.Add(new Vector3(-0.3f + posX, 0.8f + posY, 0.25f + posZ));
            array.Add(new Vector3(-0.3f + posX, 0.8f + posY, 0.15f + posZ));
            array.Add(new Vector3(-0.3f + posX, -0.4f + posY, 0.15f + posZ));
            array.Add(new Vector3(-0.3f + posX, -0.4f + posY, 0.25f + posZ));


            return array;
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
