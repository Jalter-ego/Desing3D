using DiseñoRepisa;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;

public class Game : GameWindow
{
    private float angle = 0.0f;
    private Escenario escenario1;
    private KeyboardState keyBoardState;

    public Game(int width, int height) : base(width, height, GraphicsMode.Default, "Diseño 3D")
    {
        this.escenario1 = new Escenario(new Vector3(0, 0, 0));
    }
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        GL.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
        this.CargarElementos();
    }

    private void CargarElementos()
    {
        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        //Pared

        Poligono poligonoPiso = new Poligono(new Color4(.5f, .5f, .5f, 1));
        poligonoPiso.addVertice(-5,-1,-5);
        poligonoPiso.addVertice(5, -1, -5);
        poligonoPiso.addVertice(5, -1, 5);
        poligonoPiso.addVertice(-5, -1, 5);

        Partes partePiso = new Partes();
        partePiso.add("Piso", poligonoPiso);

        Objeto objetoPiso = new Objeto();
        objetoPiso.addParte("Piso", partePiso);

        escenario1.addObjeto("Piso", objetoPiso);
        
        Poligono poligonoPared = new Poligono(new Color4(.6f, .3f, .0f, 1));
        poligonoPared.addVertice(-1.0f, -1.0f, 0.0f);
        poligonoPared.addVertice(1.0f, -1.0f, 0.0f);
        poligonoPared.addVertice(1.0f, 1.5f, 0.0f);
        poligonoPared.addVertice(-1.0f, 1.5f, 0.0f);

        poligonoPared.addVertice(-1.0f, -1.0f, 0.15f);
        poligonoPared.addVertice(1.0f, -1.0f, 0.15f);
        poligonoPared.addVertice(1.0f, 1.5f, 0.15f);
        poligonoPared.addVertice(-1.0f, 1.5f, 0.15f);

        poligonoPared.addVertice(-1.0f, 1.5f, 0.15f);
        poligonoPared.addVertice(1.0f, 1.5f, 0.15f);
        poligonoPared.addVertice(1.0f, 1.5f, 0f);
        poligonoPared.addVertice(-1.0f, 1.5f, 0f);

        poligonoPared.addVertice(-1.0f, -1.0f, 0f);
        poligonoPared.addVertice(1.0f, -1.0f, 0f);
        poligonoPared.addVertice(1.0f, -1.0f, 0.15f);
        poligonoPared.addVertice(-1.0f, -1.0f, 0.15f);

        poligonoPared.addVertice(-1.0f, -1.0f, 0f);
        poligonoPared.addVertice(-1.0f, 1.5f, 0f);
        poligonoPared.addVertice(-1.0f, 1.5f, 0.15f);
        poligonoPared.addVertice(-1.0f, -1.0f, 0.15f);

        poligonoPared.addVertice(1.0f, -1.0f, 0.15f);
        poligonoPared.addVertice(1.0f, 1.5f, 0.15f);
        poligonoPared.addVertice(1.0f, 1.5f, 0f);
        poligonoPared.addVertice(1.0f, -1.0f, 0f);

        Partes partePared = new Partes();
        partePared.add("Pared", poligonoPared);

        Objeto objetoPared = new Objeto();
        objetoPared.addParte("Pared", partePared);

        escenario1.addObjeto("Pared", objetoPared);

        //repisa
        Poligono poligonoRepisa = new Poligono(new Color4(0.87f, 0.72f, 0.53f, 1.0f));
        poligonoRepisa.addVertice(-1.0f, 1f, 0.15f);
        poligonoRepisa.addVertice(1.0f, 1f, 0.15f);
        poligonoRepisa.addVertice(1.0f, 1.1f, 0.15f);
        poligonoRepisa.addVertice(-1.0f, 1.1f, 0.15f);

        poligonoRepisa.addVertice(-1.0f, 1f, 0.6f);
        poligonoRepisa.addVertice(1.0f, 1f, 0.6f);
        poligonoRepisa.addVertice(1.0f, 1.1f, 0.6f);
        poligonoRepisa.addVertice(-1.0f, 1.1f, 0.6f);

        poligonoRepisa.addVertice(-1.0f, 1.1f, 0.6f);
        poligonoRepisa.addVertice(1.0f, 1.1f, 0.6f);
        poligonoRepisa.addVertice(1.0f, 1.1f, 0.15f);
        poligonoRepisa.addVertice(-1.0f, 1.1f, 0.15f);

        poligonoRepisa.addVertice(-1.0f, 1f, 0.15f);
        poligonoRepisa.addVertice(1.0f, 1f, 0.15f);
        poligonoRepisa.addVertice(1.0f, 1f, 0.6f);
        poligonoRepisa.addVertice(-1.0f, 1f, 0.6f);

        poligonoRepisa.addVertice(-1.0f, 1f, 0.15f);
        poligonoRepisa.addVertice(-1.0f, 1.1f, 0.15f);
        poligonoRepisa.addVertice(-1.0f, 1.1f, 0.6f);
        poligonoRepisa.addVertice(-1.0f, 1f, 0.6f);

        poligonoRepisa.addVertice(1.0f, 1f, 0.6f);
        poligonoRepisa.addVertice(1.0f, 1.1f, 0.6f);
        poligonoRepisa.addVertice(1.0f, 1.1f, 0.15f);
        poligonoRepisa.addVertice(1.0f, 1f, 0.15f);

        Partes parteRepisa = new Partes();
        parteRepisa.add("Repisa", poligonoRepisa);
        Objeto objetoRepisa = new Objeto();
        objetoRepisa.addParte("Repisa", parteRepisa);
        escenario1.addObjeto("Repisa", objetoRepisa);

        //rueda1
        Poligono poligonoRueda1 = new Poligono(new Color4(0,0,0,1));
        poligonoRueda1.addVertice(-.5f, 1.1f, 0.5f);
        poligonoRueda1.addVertice(-.55f, 1.1f, 0.5f);
        poligonoRueda1.addVertice(-.55f, 1.15f, 0.5f);
        poligonoRueda1.addVertice(-.5f, 1.15f, 0.5f);

        poligonoRueda1.addVertice(-.5f, 1.1f, 0.52f);
        poligonoRueda1.addVertice(-.55f, 1.1f, 0.52f);
        poligonoRueda1.addVertice(-.55f, 1.15f, 0.52f);
        poligonoRueda1.addVertice(-.5f, 1.15f, 0.52f);

        poligonoRueda1.addVertice(-.5f, 1.15f, 0.52f);
        poligonoRueda1.addVertice(-.55f, 1.15f, 0.52f);
        poligonoRueda1.addVertice(-.55f, 1.15f, 0.5f);
        poligonoRueda1.addVertice(-.5f, 1.15f, 0.5f);

        poligonoRueda1.addVertice(-.5f, 1.1f, 0.5f);
        poligonoRueda1.addVertice(-.55f, 1.1f, 0.5f);
        poligonoRueda1.addVertice(-.55f, 1.1f, 0.52f);
        poligonoRueda1.addVertice(-.5f, 1.1f, 0.52f);

        poligonoRueda1.addVertice(-.5f, 1.1f, 0.5f);
        poligonoRueda1.addVertice(-.5f, 1.15f, 0.5f);
        poligonoRueda1.addVertice(-.5f, 1.15f, 0.52f);
        poligonoRueda1.addVertice(-.5f, 1.1f, 0.52f);

        poligonoRueda1.addVertice(-.55f, 1.1f, 0.52f);
        poligonoRueda1.addVertice(-.55f, 1.15f, 0.52f);
        poligonoRueda1.addVertice(-.55f, 1.15f, 0.5f);
        poligonoRueda1.addVertice(-.55f, 1.1f, 0.5f);

        //rueda2

        //rueda3

        //rueda4
  
        Partes parteRueda1 = new Partes();
        parteRueda1.add("rueda1",poligonoRueda1);
        Objeto auto = new Objeto();
        auto.addParte("rueda1",parteRueda1);
        escenario1.addObjeto("auto",auto);


        Serializador.SerializarObjeto(objetoPiso,"piso.json");
        Serializador.SerializarObjeto(objetoPared,"Pared.json");
        Serializador.SerializarObjeto(objetoRepisa,"Repisa.json");

    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        GL.Viewport(0, 0, Width, Height);
        float aspectRatio = (float)Width / Height;
        Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(
            MathHelper.DegreesToRadians(45.0f), aspectRatio, 0.1f, 100.0f);
        GL.MatrixMode(MatrixMode.Projection);
        GL.LoadMatrix(ref projection);
        GL.MatrixMode(MatrixMode.Modelview);
        GL.LoadIdentity();
    }



    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);
        // Rotación de la pared
        angle += (float)e.Time * 10f;
        keyBoardState = Keyboard.GetState();
    }


    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        GL.Enable(EnableCap.DepthTest);
        // Configura la cámara
        Matrix4 modelview = Matrix4.LookAt(
            new Vector3(0.0f, 1f, 3.0f), // Posición de la cámara
            new Vector3(0.0f, 0.0f, 0.0f), // Punto de mira
            Vector3.UnitY); // Vector arriba
        GL.LoadMatrix(ref modelview);

        // Aplica una rotación al dibujo
        //GL.Rotate(angle, Vector3.UnitY);
        // Dibuja la pared vertical (un cuadrado grande)



        GL.Begin(PrimitiveType.Quads);






        escenario1.dibujar(new Vector3(0, 0, 0));
        if (keyBoardState.IsKeyDown(Key.A))
        {
           // escenario1.getObjeto("auto").getParte("rueda1").getPoligono("rueda1").setCentro(new Punto(-.525f,1.13f,.51f));
            escenario1.getObjeto("auto").getParte("rueda1").Rotar((float)e.Time, "z");
        }
        if (keyBoardState.IsKeyDown(Key.B))
        {
            escenario1.getObjeto("Pared").Trasladar(new Vector3(-0.01f, -0.01f, 0));
        }
        if (keyBoardState.IsKeyDown(Key.C))
        {
            escenario1.escalar(1.01f);
        }
        if (keyBoardState.IsKeyDown(Key.D))
        {
            escenario1.escalar(.99f);
        }
        GL.End();
        SwapBuffers();
    }
}


