using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;

namespace Diseño3D
{
    public class Game : GameWindow
    {
        private float rotationX = 0.0f;
        private float rotationY = 0.0f;

        public float posX = 0.5f;
        public float posY = 0.0f;
        public float posZ = 0.0f;

        public Game() : base(800, 600) { }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(new Color4(51f / 255f, 51f / 255f, 51f / 255f, 1.0f));  // Fondo gris oscuro
            
        }

        // Método para renderizar la escena
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);  // Limpiar la pantalla

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            // Utilizamos Matrix4.Perspective para crear la proyección en perspectiva
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(45.0f), // Campo de visión de 45 grados
                Width / (float)Height, // Relación de aspecto
                0.1f, // Distancia del plano cercano
                100.0f); // Distancia del plano lejano
            GL.LoadMatrix(ref projection); // Cargar la matriz de proyección

            // Utilizamos Matrix4.Perspective para crear la proyección en perspectiva
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(45.0f), // Campo de visión de 45 grados
                Width / (float)Height, // Relación de aspecto
                0.1f, // Distancia del plano cercano
                100.0f); // Distancia del plano lejano
            GL.LoadMatrix(ref projection); // Cargar la matriz de proyección

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            // Configura la cámara usando LookAt
            Matrix4 modelview = Matrix4.LookAt(
                new Vector3(1.5f, 2f, 3.5f), // Posición de la cámara
                new Vector3(0.0f, 0.1f, 0.0f), // Punto de mira
                Vector3.UnitY); // Vector "arriba"
            GL.LoadMatrix(ref modelview);

            // Configura la cámara usando LookAt
            Matrix4 modelview = Matrix4.LookAt(
                new Vector3(1.5f, 2f, 3.5f), // Posición de la cámara
                new Vector3(0.0f, 0.1f, 0.0f), // Punto de mira
                Vector3.UnitY); // Vector "arriba"
            GL.LoadMatrix(ref modelview);

            DrawAxes();
            DrawU();

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Key.W))  // Rotar hacia arriba
            {
                rotationX -= 1.0f;
            }
            if (keyboardState.IsKeyDown(Key.S))  // Rotar hacia abajo
            {
                rotationX += 1.0f;
            }
            if (keyboardState.IsKeyDown(Key.A))  // Rotar hacia la izquierda
            {
                rotationY -= 1.0f;
            }
            if (keyboardState.IsKeyDown(Key.D))  // Rotar hacia la derecha
            {
                rotationY += 1.0f;
            }
        }

        public void setCentro(float x, float y, float z)
        {
            this.posX = x;
            this.posY = y;
            this.posZ = z;
        }




        private void DrawU()
        {
            GL.PushMatrix(); 
            GL.Rotate(rotationX, 1.0f, 0.0f, 0.0f);  
            GL.Rotate(rotationY, 0.0f, 1.0f, 0.0f);
            GL.Translate(posX, posY, posZ);

            GL.Color3(1.0f, 0.3f, 0.3f);  
            GL.Begin(PrimitiveType.Quads);
            
            // Base 
            //parte delantera
            GL.Vertex3(-0.4f, -0.5f, 0.25f);    
            GL.Vertex3(0.4f, -0.5f, 0.25f);     
            GL.Vertex3(0.4f, -0.4f, 0.25f);     
            GL.Vertex3(-0.4f, -0.4f, 0.25f);    
            //parte trasera
            GL.Vertex3(-0.4f, -0.5f, 0.15f);    
            GL.Vertex3(0.4f, -0.5f, 0.15f);     
            GL.Vertex3(0.4f, -0.4f, 0.15f);     
            GL.Vertex3(-0.4f, -0.4f, 0.15f);    
            //parte arriba
            GL.Vertex3(-0.4f, -0.4f, 0.25f);    
            GL.Vertex3(0.4f, -0.4f, 0.25f);     
            GL.Vertex3(0.4f, -0.4f, 0.15f);     
            GL.Vertex3(-0.4f, -0.4f, 0.15f);    
            //parte abajo
            GL.Vertex3(-0.4f, -0.5f, 0.25f);    
            GL.Vertex3(0.4f, -0.5f, 0.25f);     
            GL.Vertex3(0.4f, -0.5f, 0.15f);     
            GL.Vertex3(-0.4f, -0.5f, 0.15f);    
            // Lado derecho
            GL.Vertex3(0.4f, -0.5f, 0.25f);     
            GL.Vertex3(0.4f, -0.5f, 0.15f);     
            GL.Vertex3(0.4f, -0.4f, 0.15f);     
            GL.Vertex3(0.4f, -0.4f, 0.25f);    

            // Lado izquierdo
            GL.Vertex3(-0.4f, -0.5f, 0.25f);  
            GL.Vertex3(-0.4f, -0.5f, 0.15f);  
            GL.Vertex3(-0.4f, -0.4f, 0.15f);  
            GL.Vertex3(-0.4f, -0.4f, 0.25f);   

            //columna derecha
            //parte delantera
            GL.Vertex3(0.3f, .8f, 0.25f);    
            GL.Vertex3(0.4f, .8f, 0.25f);     
            GL.Vertex3(0.4f, -0.4f, 0.25f);     
            GL.Vertex3(0.3f, -0.4f, 0.25f);    
            //parte trasera
            GL.Vertex3(.3f, .8f, 0.15f);    
            GL.Vertex3(0.4f, .8f, 0.15f);     
            GL.Vertex3(0.4f, -0.4f, 0.15f);     
            GL.Vertex3(.3f, -0.4f, 0.15f);    
            //parte arriba
            GL.Vertex3(.4f, -0.4f, 0.25f);    
            GL.Vertex3(0.3f, -0.4f, 0.25f);     
            GL.Vertex3(0.3f, -0.4f, 0.15f);     
            GL.Vertex3(.4f, -0.4f, 0.15f);    
            //parte abajo
            GL.Vertex3(.4f, .8f, 0.25f);    
            GL.Vertex3(0.3f, .8f, 0.25f);     
            GL.Vertex3(0.3f, .8f, 0.15f);     
            GL.Vertex3(.4f, .8f, 0.15f);    
            // Lado derecho
            GL.Vertex3(0.3f, .8f, 0.25f);     
            GL.Vertex3(0.3f, .8f, 0.15f);     
            GL.Vertex3(0.3f, -0.4f, 0.15f);     
            GL.Vertex3(0.3f, -0.4f, 0.25f);   

            // Lado izquierdo
            GL.Vertex3(.4f, .8f, 0.25f);  
            GL.Vertex3(.4f, .8f, 0.15f);
            GL.Vertex3(.4f, -0.4f, 0.15f);   
            GL.Vertex3(.4f, -0.4f, 0.25f);    

            //columna izquierda
            //parte delantera
            GL.Vertex3(-0.4f, .8f, 0.25f);    
            GL.Vertex3(-0.3f, .8f, 0.25f);     
            GL.Vertex3(-0.3f, -0.4f, 0.25f);     
            GL.Vertex3(-0.4f, -0.4f, 0.25f);    
            //parte trasera
            GL.Vertex3(-.4f, .8f, 0.15f);    
            GL.Vertex3(-0.3f, .8f, 0.15f);     
            GL.Vertex3(-0.3f, -0.4f, 0.15f);     
            GL.Vertex3(-.4f, -0.4f, 0.15f);    
            //parte arriba
            GL.Vertex3(-.4f, -0.4f, 0.25f);    
            GL.Vertex3(-0.3f, -0.4f, 0.25f);     
            GL.Vertex3(-0.3f, -0.4f, 0.15f);     
            GL.Vertex3(-.4f, -0.4f, 0.15f);    
            //parte abajo
            GL.Vertex3(-.4f, .8f, 0.25f);    
            GL.Vertex3(-0.3f, .8f, 0.25f);    
            GL.Vertex3(-0.3f, .8f, 0.15f);    
            GL.Vertex3(-.4f, .8f, 0.15f);
            // Lado izquierdo
            GL.Vertex3(-.4f, .8f, 0.25f);  
            GL.Vertex3(-.4f, .8f, 0.15f);  
            GL.Vertex3(-.4f, -0.4f, 0.15f);  
            GL.Vertex3(-.4f, -0.4f, 0.25f);  
            // Lado derecho

            GL.Color3(.1f, 0.2f, 0.2f);
            GL.Vertex3(-0.3f, .8f, 0.25f);   
            GL.Vertex3(-0.3f, .8f, 0.15f);   
            GL.Vertex3(-0.3f, -0.4f, 0.15f);   
            GL.Vertex3(-0.3f, -0.4f, 0.25f);




            GL.End();
            GL.PopMatrix();
        }

        private void DrawAxes()
        {
            GL.Begin(PrimitiveType.Lines);

            // Eje X (Rojo)
            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Vertex3(-2.0f, 0.0f, 0.0f);
            GL.Vertex3(2.0f, 0.0f, 0.0f);

            // Eje Y (Verde)
            GL.Color3(0.0f, 1.0f, 0.0f);
            GL.Vertex3(0.0f, -2.0f, 0.0f);
            GL.Vertex3(0.0f, 2.0f, 0.0f);

            // Eje Z (Azul)
            GL.Color3(0.0f, 0.0f, 1.0f);
            GL.Vertex3(0.0f, 0.0f, -2.0f);
            GL.Vertex3(0.0f, 0.0f, 2.0f);
            // Eje X (Rojo)
            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Vertex3(-2.0f, 0.0f, 0.0f);
            GL.Vertex3(2.0f, 0.0f, 0.0f);

            // Eje Y (Verde)
            GL.Color3(0.0f, 1.0f, 0.0f);
            GL.Vertex3(0.0f, -2.0f, 0.0f);
            GL.Vertex3(0.0f, 2.0f, 0.0f);

            // Eje Z (Azul)
            GL.Color3(0.0f, 0.0f, 1.0f);
            GL.Vertex3(0.0f, 0.0f, -2.0f);
            GL.Vertex3(0.0f, 0.0f, 2.0f);

            GL.End();
        }



        

    }
}
