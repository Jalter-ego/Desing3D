using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;

namespace Diseño3D
{
    class Game : GameWindow
    {
        private float rotationX = 0.0f;
        private float rotationY = 0.0f;

        private float posX = 0.0f;
        private float posY = 0.0f;
        private float posZ = 0.0f;

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
            GL.Ortho(-1.0f, 1.0f, -1.0f, 1.0f, -1.0f, 1.0f); // Proyección ortogonal

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            // Aquí movemos la cámara. Se mueve 0.5 en el eje X (derecha) y 0.5 en el eje Y (arriba)
            GL.Translate(posX - 0.5f, posY - 0.5f, posZ -.5f); // Desplazamos la cámara

            // Dibujamos los ejes y los objetos
            DrawAxes();
            DrawU();

            SwapBuffers();
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

            // Dibuja el eje X (color rojo)
            GL.Color3(1.0f, 0.0f, 0.0f); // Rojo
            GL.Vertex3(-1.0f, 0.0f, 0.0f); // Punto inicial (X negativo)
            GL.Vertex3(1.0f, 0.0f, 0.0f);  // Punto final (X positivo)

            GL.Color3(.0f, 1.0f, 0.0f); // Rojo
            GL.Vertex3(.0f, -1.0f, 0.0f); // Punto inicial (X negativo)
            GL.Vertex3(.0f, 1.0f, 0.0f);  // Punto final (X positivo)

            // Dibuja el eje Z (color azul)
            GL.Color3(0.0f, 0.0f, 1.0f); // Azul
            GL.Vertex3(0.0f, 0.0f, -1.0f); // Punto inicial (Z negativo)
            GL.Vertex3(0.0f, 0.0f, 1.0f);  // Punto final (Z positivo)

            GL.End();
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

    }
}
