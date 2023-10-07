using System;
using System.Collections.Generic;
using DiseñoRepisa;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

public class Game : GameWindow
{
    private float angle = 0.0f;
    private List<Objeto> objetos = new List<Objeto>();
    //escenario1
    private Escenario escenario1;
    //escenario2
    private Escenario escenario2;

    public Game(int width, int height) : base(width, height, GraphicsMode.Default, "Diseño 3D")
    {
        this.escenario1 = new Escenario(new Vector3(1, 1, 1));
        this.escenario2 = new Escenario(new Vector3(-1, -1, -1));
    }
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        GL.Enable(EnableCap.DepthTest);
        GL.ClearColor(0.1f, 0.12f, 0.13f, 0.1f);
        this.CargarElementos();
    }

    private void CargarElementos()
    {
        //----------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------
        //Pared1
        Objeto objetoPared1 = new Objeto();
        Partes partePared1 = new Partes();
        Poligono poligonoPared1 = new Poligono(new Vector3(1, 1, 1), new Vector3(1, 1, .15f), new Color4(.6f, .3f, .0f, 1.0f), 0, 0, 0);

        partePared1.add(poligonoPared1);
        objetoPared1.addParte(partePared1);
        escenario1.addObjeto("Pared1", objetoPared1);

        //Pared2
        Objeto objetoPared2 = new Objeto();
        Partes partePared2 = new Partes();
        Poligono poligonoPared2 = new Poligono(new Vector3(-1, -1, -1), new Vector3(1, 1, .15f), new Color4(.6f, .3f, .0f, 1.0f), 0, 0, 0);

        partePared2.add(poligonoPared2);
        objetoPared2.addParte(partePared2);
        escenario2.addObjeto("Pared1", objetoPared2);

        //Auto1
        Objeto auto1 = new Objeto();

        Partes parteAutoArriba1 = new Partes();
        Poligono poligonoAutoArriba1 = new Poligono(new Vector3(1, 1, 1), new Vector3(.16f, .04f, .16f), new Color4(0.0f, 0.5f, 1.0f, 1.0f), .24f, .34f, .32f);

        Partes parteAutoAbajo1 = new Partes();
        Poligono poligonoAutoAbajo1 = new Poligono(new Vector3(1, 1, 1), new Vector3(.28f, .04f, .16f), new Color4(.0f, .5f, 1f, 1.0f), .23f, .3f, .32f);

        Partes parteEspejoFrontend1 = new Partes();
        Poligono poligonoEspejoFrontend1 = new Poligono(new Vector3(1, 1, 1), new Vector3(.0f, .035f, .12f), new Color4(0.8f, 0.8f, 1.0f, 1.0f), .1585f, .34f, .32f);

        Partes parteEspejoBakend1 = new Partes();
        Poligono poligonoEspejoBakend1 = new Poligono(new Vector3(1, 1, 1), new Vector3(.0f, .035f, .12f), new Color4(0.8f, 0.8f, 1.0f, 1.0f), .3209f, .34f, .32f);

        Partes parteEspejoLadoIzq1 = new Partes();
        Poligono poligonoEspejoLadoIzq1 = new Poligono(new Vector3(1, 1, 1), new Vector3(.065f, .039f, .0f), new Color4(0.8f, 0.8f, 1.0f, 1.0f), .2f, .34f, .4009f);

        Partes parteEspejoLadoDer1 = new Partes();
        Poligono poligonoEspejoLadoDer1 = new Poligono(new Vector3(1, 1, 1), new Vector3(.065f, .039f, .0f), new Color4(0.8f, 0.8f, 1.0f, 1.0f), .275f, .34f, .4009f);



        parteAutoArriba1.add(poligonoAutoArriba1);
        parteAutoAbajo1.add(poligonoAutoAbajo1);
        parteEspejoFrontend1.add(poligonoEspejoFrontend1);
        parteEspejoBakend1.add(poligonoEspejoBakend1);
        parteEspejoLadoIzq1.add(poligonoEspejoLadoIzq1);
        parteEspejoLadoDer1.add(poligonoEspejoLadoDer1);

        auto1.addParte(parteAutoArriba1);
        auto1.addParte(parteAutoAbajo1);
        auto1.addParte(parteEspejoFrontend1);
        auto1.addParte(parteEspejoBakend1);
        auto1.addParte(parteEspejoLadoIzq1);
        auto1.addParte(parteEspejoLadoDer1);

        escenario1.addObjeto("Auto1",auto1);



//----------------------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------------------

        //repisa1
        Objeto objetoRespisa1 = new Objeto();
        Partes parteRepisa1 = new Partes();
        Poligono poligonoRepisa1 = new Poligono(new Vector3(1, 1, 1), new Vector3(1, .04f, .4f), new Color4(0.87f, 0.72f, 0.53f, 1.0f), 0, .22f, .275f);
        parteRepisa1.add(poligonoRepisa1);
        objetoRespisa1.addParte(parteRepisa1);
        escenario1.addObjeto("Repisa1",objetoRespisa1);
        //repisa2
        Objeto objetoRespisa2 = new Objeto();
        Partes parteRepisa2 = new Partes();
        Poligono poligonoRepisa2 = new Poligono(new Vector3(-1, -1, -1), new Vector3(1, .04f, .4f), new Color4(0.87f, 0.72f, 0.53f, 1.0f), 0, .22f, .275f);
        parteRepisa2.add(poligonoRepisa2);
        objetoRespisa2.addParte(parteRepisa2);
        escenario2.addObjeto("Repisa1", objetoRespisa2);
        //paredes

        /*Objeto pared1 = new Objeto();
        pared1.addParte(new Vector3(1, 1, 1), new Vector3(1, 1, .15f), new Color4(.6f, .3f, .0f, 1.0f), 0, 0, 0);
        this.objetos.Add(pared1);

        Objeto pared2 = new Objeto();
        pared2.addParte(new Vector3(-1, -1, -1), new Vector3(1, 1, .15f), new Color4(.6f, .3f, .0f, 1.0f), 0, 0, 0);
        this.objetos.Add(pared2);
        //repisas
        Objeto repisa1 = new Objeto();
        repisa1.addParte(new Vector3(1, 1, 1), new Vector3(1, .04f, .4f), new Color4(0.87f, 0.72f, 0.53f, 1.0f), 0, .22f, .275f);
        this.objetos.Add(repisa1);

        Objeto repisa2 = new Objeto();
        repisa2.addParte(new Vector3(-1, -1, -1), new Vector3(1, .04f, .4f), new Color4(0.87f, 0.72f, 0.53f, 1.0f), 0, .22f, .275f);
        this.objetos.Add(repisa2);
        //cuerpo auto
        Objeto cuerpo1 = new Objeto();
        cuerpo1.addParte(new Vector3(1, 1, 1), new Vector3(.16f, .04f, .16f), new Color4(0.0f, 0.5f, 1.0f, 1.0f), .24f, .34f, .32f);
        this.objetos.Add(cuerpo1);
        Objeto cuerpo2 = new Objeto();
        cuerpo2.addParte(new Vector3(-1, -1, -1), new Vector3(.16f, .04f, .16f), new Color4(0.0f, 0.5f, 1.0f, 1.0f), .24f, .34f, .32f);
        objetos.Add(cuerpo2);
        //parte baja auto
        Objeto cuerpoBajo1 = new Objeto();
        cuerpoBajo1.addParte(new Vector3(1, 1, 1), new Vector3(.28f, .04f, .16f), new Color4(.0f, .5f, 1f, 1.0f), .23f, .3f, .32f);
        this.objetos.Add(cuerpoBajo1);

        Objeto cuerpoBajo2 = new Objeto();
        cuerpoBajo2.addParte(new Vector3(-1, -1, -1), new Vector3(.28f, .04f, .16f), new Color4(.0f, .5f, 1f, 1.0f), .23f, .3f, .32f);
        this.objetos.Add(cuerpoBajo2);
        //espejos frontales
        Objeto frontend1 = new Objeto();
        frontend1.addParte(new Vector3(1, 1, 1), new Vector3(.0f, .035f, .12f), new Color4(0.8f, 0.8f, 1.0f, 1.0f), .1585f, .34f, .32f);
        this.objetos.Add(frontend1);

        Objeto frontend2 = new Objeto();
        frontend2.addParte(new Vector3(-1, -1, -1), new Vector3(.0f, .035f, .12f), new Color4(0.8f, 0.8f, 1.0f, 1.0f), .1585f, .34f, .32f);
        objetos.Add(frontend2);
        //espejos detras
        Objeto backend1 = new Objeto();
        backend1.addParte(new Vector3(1, 1, 1), new Vector3(.0f, .035f, .12f), new Color4(0.8f, 0.8f, 1.0f, 1.0f), .3209f, .34f, .32f);
        this.objetos.Add(backend1);

        Objeto backend2 = new Objeto();
        backend2.addParte(new Vector3(-1, -1, -1), new Vector3(.0f, .035f, .12f), new Color4(0.8f, 0.8f, 1.0f, 1.0f), .3209f, .34f, .32f);
        this.objetos.Add(backend2);
        //laterales Izquierda
        Objeto lateralIzquierda1 = new Objeto();
        lateralIzquierda1.addParte(new Vector3(1, 1, 1), new Vector3(.065f, .039f, .0f), new Color4(0.8f, 0.8f, 1.0f, 1.0f), .2f, .34f, .4009f);
        this.objetos.Add(lateralIzquierda1);

        Objeto lateralIzquierda2 = new Objeto();
        lateralIzquierda2.addParte(new Vector3(-1, -1, -1), new Vector3(.065f, .039f, .0f), new Color4(0.8f, 0.8f, 1.0f, 1.0f), .2f, .34f, .4009f);
        this.objetos.Add(lateralIzquierda2);
        //lateral Derecha
        Objeto lateralDerecha1 = new Objeto();
        lateralDerecha1.addParte(new Vector3(1, 1, 1), new Vector3(.065f, .039f, .0f), new Color4(0.8f, 0.8f, 1.0f, 1.0f), .275f, .34f, .4009f);
        this.objetos.Add(lateralDerecha1);

        Objeto lateralDerecha2 = new Objeto();
        lateralDerecha2.addParte(new Vector3(-1, -1, -1), new Vector3(.065f, .039f, .0f), new Color4(0.8f, 0.8f, 1.0f, 1.0f), .275f, .34f, .4009f);
        this.objetos.Add(lateralDerecha2);

        //Ruedas auto 1
        Objeto auto1Rueda1 = new Objeto();
        auto1Rueda1.addParte(new Vector3(1, 1, 1), new Vector3(.04f, .04f, .03f), new Color4(0, 0, 0, 1.0f), 0.16f, .26f, .37f);
        this.objetos.Add(auto1Rueda1);
        Objeto auto1Rueda2 = new Objeto();
        auto1Rueda2.addParte(new Vector3(1, 1, 1), new Vector3(.04f, .04f, .03f), new Color4(0, 0, 0, 1.0f), 0.32f, .26f, .37f);
        this.objetos.Add(auto1Rueda2);
        Objeto auto1Rueda3 = new Objeto();
        auto1Rueda3.addParte(new Vector3(1, 1, 1), new Vector3(.04f, .04f, .03f), new Color4(0, 0, 0, 1.0f), 0.16f, .26f, .275f);
        this.objetos.Add(auto1Rueda3);
        Objeto auto1Rueda4 = new Objeto();
        auto1Rueda4.addParte(new Vector3(1, 1, 1), new Vector3(.04f, .04f, .03f), new Color4(0, 0, 0, 1.0f), 0.32f, .26f, .275f);
        this.objetos.Add(auto1Rueda4);

        //Ruedas auto 2
        Objeto auto2Rueda1 = new Objeto();
        auto2Rueda1.addParte(new Vector3(-1, -1, -1), new Vector3(.04f, .04f, .03f), new Color4(0, 0, 0, 1.0f), 0.16f, .26f, .37f);
        this.objetos.Add(auto2Rueda1);
        Objeto auto2Rueda2 = new Objeto();
        auto2Rueda2.addParte(new Vector3(-1, -1, -1), new Vector3(.04f, .04f, .03f), new Color4(0, 0, 0, 1.0f), 0.32f, .26f, .37f);
        this.objetos.Add(auto2Rueda2);
        Objeto auto2Rueda3 = new Objeto();
        auto2Rueda3.addParte(new Vector3(-1, -1, -1), new Vector3(.04f, .04f, .03f), new Color4(0, 0, 0, 1.0f), 0.16f, .26f, .275f);
        this.objetos.Add(auto2Rueda3);
        Objeto auto2Rueda4 = new Objeto();
        auto2Rueda4.addParte(new Vector3(-1, -1, -1), new Vector3(.04f, .04f, .03f), new Color4(0, 0, 0, 1.0f), 0.32f, .26f, .275f);
        this.objetos.Add(auto2Rueda4);*/
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
        angle += (float)e.Time * 40.0f;
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        // Configura la cámara
        Matrix4 modelview = Matrix4.LookAt(
            new Vector3(0.0f, .8f, 6.0f), // Posición de la cámara
            new Vector3(0.0f, 0.0f, 0.0f), // Punto de mira
            Vector3.UnitY); // Vector arriba
        GL.LoadMatrix(ref modelview);

        // Aplica una rotación al dibujo
        GL.Rotate(angle, Vector3.UnitY);

        escenario1.dibujar();
        escenario2.dibujar();

        // Dibuja la pared vertical (un cuadrado grande)
        /*GL.Begin(PrimitiveType.Quads);
        //pared
        GL.Color3(.6f, .3f, .0f); // Rojo

        GL.Vertex3(-1.0f, -1.0f, 0.0f);
        GL.Vertex3(1.0f, -1.0f, 0.0f);
        GL.Vertex3(1.0f, 1.0f, 0.0f);
        GL.Vertex3(-1.0f, 1.0f, 0.0f);

        GL.Vertex3(-1.0f, -1.0f, 0.15f);
        GL.Vertex3(1.0f, -1.0f, 0.15f);
        GL.Vertex3(1.0f, 1.0f, 0.15f);
        GL.Vertex3(-1.0f, 1.0f, 0.15f);

        // Cara superior 
        GL.Vertex3(-1.0f, 1.0f, .15f);
        GL.Vertex3(1.0f, 1.0f, .15f);
        GL.Vertex3(1.0f, 1.0f, .0f);
        GL.Vertex3(-1.0f, 1.0f, .0f);

        // Cara inferior
        GL.Vertex3(-1.0f, -1.0f, .0f);
        GL.Vertex3(1.0f, -1.0f, .0f);
        GL.Vertex3(1.0f, -1.0f, .15f);
        GL.Vertex3(-1.0f, -1.0f, .15f);

        // Cara izquierda
        GL.Vertex3(-1.0f, -1.0f, .0f);
        GL.Vertex3(-1.0f, 1.0f, .0f);
        GL.Vertex3(-1.0f, 1.0f, .15f);
        GL.Vertex3(-1.0f, -1.0f, .15f);

        // Cara derecha 
        GL.Vertex3(1.0f, -1.0f, .15f);
        GL.Vertex3(1.0f, 1.0f, .15f);
        GL.Vertex3(1.0f, 1.0f, .0f);
        GL.Vertex3(1.0f, -1.0f, .0f);
        //---------------------------------------------------------------------
        //repisa
        GL.Color3(0.87f, 0.72f, 0.53f);

        GL.Vertex3(-1.0f, .4f, 0.45f);
        GL.Vertex3(1.0f, .4f, 0.45f);
        GL.Vertex3(1.0f, .56f, 0.45f);
        GL.Vertex3(-1.0f, .56f, 0.45f);

        GL.Vertex3(-1.0f, .4f, 0.15f);
        GL.Vertex3(1.0f, .4f, 0.15f);
        GL.Vertex3(1.0f, .56f, 0.15f);
        GL.Vertex3(-1.0f, .56f, 0.15f);

        // Cara superior 
        GL.Vertex3(-1.0f, .56f, .15f);
        GL.Vertex3(1.0f, .56f, .15f);
        GL.Vertex3(1.0f, .56f, .45f);
        GL.Vertex3(-1.0f, .56f, .45f);

        // Cara inferior
        GL.Vertex3(-1.0f, .4f, .45f);
        GL.Vertex3(1.0f, .4f, .45f);
        GL.Vertex3(1.0f, .4f, .15f);
        GL.Vertex3(-1.0f, .4f, .15f);

        // Cara izquierda
        GL.Vertex3(-1.0f, .4f, .45f);
        GL.Vertex3(-1.0f, .56f, .45f);
        GL.Vertex3(-1.0f, .56f, .15f);
        GL.Vertex3(-1.0f, .4f, .15f);

        // Cara derecha 
        GL.Vertex3(1.0f, .4f, .15f);
        GL.Vertex3(1.0f, .56f, .15f);
        GL.Vertex3(1.0f, .56f, .45f);
        GL.Vertex3(1.0f, .4f, .45f);

        // espejos
        GL.Color3(0.8f, 0.8f, 1.0f);

        // frontal
        GL.Vertex3(.79f, .64f, .39f);
        GL.Vertex3(.79f, .67f, .39f);
        GL.Vertex3(.79f, .67f, .31f);
        GL.Vertex3(.79f, .64f, .31f);

        //  atras
        GL.Vertex3(.89f, .64f, .31f);
        GL.Vertex3(.89f, .67f, .31f);
        GL.Vertex3(.89f, .67f, .39f);
        GL.Vertex3(.89f, .64f, .39f);

        //adelante laterales
        GL.Vertex3(.795f, .67f, 0.4f);
        GL.Vertex3(.84f, .67f, 0.4f);
        GL.Vertex3(.84f, .64f, 0.4f);
        GL.Vertex3(.795f, .64f, 0.4f);

        GL.Vertex3(.795f, .64f, 0.3f);
        GL.Vertex3(.84f, .64f, 0.3f);
        GL.Vertex3(.84f, .67f, 0.3f);
        GL.Vertex3(.795f, .67f, 0.3f);

        //atras laterales
        GL.Vertex3(.845f, .67f, 0.4f);
        GL.Vertex3(.885f, .67f, 0.4f);
        GL.Vertex3(.885f, .64f, 0.4f);
        GL.Vertex3(.845f, .64f, 0.4f);

        GL.Vertex3(.845f, .64f, 0.3f);
        GL.Vertex3(.885f, .64f, 0.3f);
        GL.Vertex3(.885f, .67f, 0.3f);
        GL.Vertex3(.845f, .67f, 0.3f);
        //auto-----------------------------------------------------------
        // Dibuja el cuerpo del auto
        GL.Color3(0.0f, 0.5f, 1.0f); // Color azul claro

        GL.Vertex3(.79f, .68f, 0.4f);
        GL.Vertex3(.89f, .68f, 0.4f);
        GL.Vertex3(.89f, .64f, 0.4f);
        GL.Vertex3(.79f, .64f, 0.4f);

        GL.Vertex3(.79f, .64f, 0.3f);
        GL.Vertex3(.89f, .64f, 0.3f);
        GL.Vertex3(.89f, .68f, 0.3f);
        GL.Vertex3(.79f, .68f, 0.3f);

        // Cara superior 
        GL.Vertex3(.79f, .68f, .3f);
        GL.Vertex3(.89f, .68f, .3f);
        GL.Vertex3(.89f, .68f, .4f);
        GL.Vertex3(.79f, .68f, .4f);

        // Cara inferior
        GL.Vertex3(.79f, .64f, .4f);
        GL.Vertex3(.89f, .64f, .4f);
        GL.Vertex3(.89f, .64f, .3f);
        GL.Vertex3(.79f, .64f, .3f);

        // Cara izquierda
        GL.Vertex3(.79f, .64f, .4f);
        GL.Vertex3(.79f, .68f, .4f);
        GL.Vertex3(.79f, .68f, .3f);
        GL.Vertex3(.79f, .64f, .3f);

        // Cara derecha 
        GL.Vertex3(.89f, .64f, .3f);
        GL.Vertex3(.89f, .68f, .3f);
        GL.Vertex3(.89f, .68f, .4f);
        GL.Vertex3(.89f, .64f, .4f);
        //----------------------------------------------------------------------
        //parte baja del auto
        GL.Vertex3(.74f, .6f, 0.4f);
        GL.Vertex3(.94f, .6f, 0.4f);
        GL.Vertex3(.94f, .64f, 0.4f);
        GL.Vertex3(.74f, .64f, 0.4f);

        GL.Vertex3(.74f, .64f, 0.3f);
        GL.Vertex3(.94f, .64f, 0.3f);
        GL.Vertex3(.94f, .6f, 0.3f);
        GL.Vertex3(.74f, .6f, 0.3f);

        // Cara superior 
        GL.Vertex3(.74f, .6f, .3f);
        GL.Vertex3(.94f, .6f, .3f);
        GL.Vertex3(.94f, .6f, .4f);
        GL.Vertex3(.74f, .6f, .4f);

        // Cara inferior
        GL.Vertex3(.74f, .64f, .4f);
        GL.Vertex3(.94f, .64f, .4f);
        GL.Vertex3(.94f, .64f, .3f);
        GL.Vertex3(.74f, .64f, .3f);

        // Cara izquierda
        GL.Vertex3(.74f, .6f, .4f);
        GL.Vertex3(.74f, .64f, .4f);
        GL.Vertex3(.74f, .64f, .3f);
        GL.Vertex3(.74f, .6f, .3f);

        // Cara derecha 
        GL.Vertex3(.94f, .6f, .3f);
        GL.Vertex3(.94f, .64f, .3f);
        GL.Vertex3(.94f, .64f, .4f);
        GL.Vertex3(.94f, .6f, .4f);
        //---------------------------------------------------------------------

        // Dibuja las ruedas del auto
        GL.Color3(0.0f, 0.0f, 0.0f); // Color negro
        
        // rueda1
        //Eje Z
        GL.Vertex3(.89f, .6f, 0.4f);
        GL.Vertex3(.93f, .6f, 0.4f);
        GL.Vertex3(.93f, .56f, 0.4f);
        GL.Vertex3(.89f, .56f, 0.4f);

        GL.Vertex3(.89f, .56f, 0.38f);
        GL.Vertex3(.93f, .56f, 0.38f);
        GL.Vertex3(.93f, .6f, 0.38f);
        GL.Vertex3(.89f, .6f, 0.38f);

        //Eje Y
        GL.Vertex3(.89f, .6f, .38f);
        GL.Vertex3(.93f, .6f, .38f);
        GL.Vertex3(.93f, .6f, .4f);
        GL.Vertex3(.89f, .6f, .4f);

        GL.Vertex3(.89f, .56f, .4f);
        GL.Vertex3(.93f, .56f, .4f);
        GL.Vertex3(.93f, .56f, .38f);
        GL.Vertex3(.89f, .56f, .38f);

        // Eje X
        GL.Vertex3(.89f, .6f, .4f);
        GL.Vertex3(.89f, .56f, .4f);
        GL.Vertex3(.89f, .56f, .38f);
        GL.Vertex3(.89f, .6f, .38f);

        GL.Vertex3(.93f, .6f, .38f);
        GL.Vertex3(.93f, .56f, .38f);
        GL.Vertex3(.93f, .56f, .4f);
        GL.Vertex3(.93f, .6f, .4f);

        //rueda2
        //Eje Z
        GL.Vertex3(.89f, .6f, 0.3f);
        GL.Vertex3(.93f, .6f, 0.3f);
        GL.Vertex3(.93f, .56f, 0.3f);
        GL.Vertex3(.89f, .56f, 0.3f);

        GL.Vertex3(.89f, .56f, 0.32f);
        GL.Vertex3(.93f, .56f, 0.32f);
        GL.Vertex3(.93f, .6f, 0.32f);
        GL.Vertex3(.89f, .6f, 0.32f);

        //Eje Y
        GL.Vertex3(.89f, .6f, .32f);
        GL.Vertex3(.93f, .6f, .32f);
        GL.Vertex3(.93f, .6f, .3f);
        GL.Vertex3(.89f, .6f, .3f);

        GL.Vertex3(.89f, .56f, .3f);
        GL.Vertex3(.93f, .56f, .3f);
        GL.Vertex3(.93f, .56f, .32f);
        GL.Vertex3(.89f, .56f, .32f);

        // Eje X
        GL.Vertex3(.89f, .6f, .3f);
        GL.Vertex3(.89f, .56f, .3f);
        GL.Vertex3(.89f, .56f, .32f);
        GL.Vertex3(.89f, .6f, .32f);

        GL.Vertex3(.93f, .6f, .32f);
        GL.Vertex3(.93f, .56f, .32f);
        GL.Vertex3(.93f, .56f, .3f);
        GL.Vertex3(.93f, .6f, .3f);

        //rueda3
        //Eje Z
        GL.Vertex3(.79f, .6f, 0.4f);
        GL.Vertex3(.75f, .6f, 0.4f);
        GL.Vertex3(.75f, .56f, 0.4f);
        GL.Vertex3(.79f, .56f, 0.4f);

        GL.Vertex3(.79f, .56f, 0.38f);
        GL.Vertex3(.75f, .56f, 0.38f);
        GL.Vertex3(.75f, .6f, 0.38f);
        GL.Vertex3(.79f, .6f, 0.38f);

        //Eje Y
        GL.Vertex3(.79f, .6f, .38f);
        GL.Vertex3(.75f, .6f, .38f);
        GL.Vertex3(.75f, .6f, .4f);
        GL.Vertex3(.79f, .6f, .4f);

        GL.Vertex3(.79f, .56f, .4f);
        GL.Vertex3(.75f, .56f, .4f);
        GL.Vertex3(.75f, .56f, .38f);
        GL.Vertex3(.79f, .56f, .38f);

        // Eje X
        GL.Vertex3(.79f, .6f, .4f);
        GL.Vertex3(.79f, .56f, .4f);
        GL.Vertex3(.79f, .56f, .38f);
        GL.Vertex3(.79f, .6f, .38f);

        GL.Vertex3(.75f, .6f, .38f);
        GL.Vertex3(.75f, .56f, .38f);
        GL.Vertex3(.75f, .56f, .4f);
        GL.Vertex3(.75f, .6f, .4f);
        //rueda4
        //Eje Z
        GL.Vertex3(.79f, .6f, 0.3f);
        GL.Vertex3(.75f, .6f, 0.3f);
        GL.Vertex3(.75f, .56f, 0.3f);
        GL.Vertex3(.79f, .56f, 0.3f);

        GL.Vertex3(.79f, .56f, 0.32f);
        GL.Vertex3(.75f, .56f, 0.32f);
        GL.Vertex3(.75f, .6f, 0.32f);
        GL.Vertex3(.79f, .6f, 0.32f);

        //Eje Y
        GL.Vertex3(.79f, .6f, .32f);
        GL.Vertex3(.75f, .6f, .32f);
        GL.Vertex3(.75f, .6f, .3f);
        GL.Vertex3(.79f, .6f, .3f);

        GL.Vertex3(.79f, .56f, .3f);
        GL.Vertex3(.75f, .56f, .3f);
        GL.Vertex3(.75f, .56f, .32f);
        GL.Vertex3(.79f, .56f, .32f);

        // Eje X
        GL.Vertex3(.79f, .6f, .3f);
        GL.Vertex3(.79f, .56f, .3f);
        GL.Vertex3(.79f, .56f, .32f);
        GL.Vertex3(.79f, .6f, .32f);

        GL.Vertex3(.75f, .6f, .32f);
        GL.Vertex3(.75f, .56f, .32f);
        GL.Vertex3(.75f, .56f, .3f);
        GL.Vertex3(.75f, .6f, .3f);*/

        GL.End();

        SwapBuffers();
    }
}


